// ------------------------------------------------------------
// @file       AbstractWeapon.cs
// @brief
// @author     zheliku
// @Modified   2025-01-31 18:01:29
// @Copyright  Copyright (c) 2025, zheliku
// ------------------------------------------------------------

namespace Game
{
    using System.Collections.Generic;
    using System.Linq;
    using Framework.Core;
    using Framework.Toolkits.ActionKit;
    using Framework.Toolkits.AudioKit;
    using Framework.Toolkits.FluentAPI;
    using Framework.Toolkits.InputKit;
    using Framework.Toolkits.TimerKit;
    using Framework.Toolkits.UIKit;
    using Sirenix.OdinInspector;
    using UnityEngine;
    using UnityEngine.InputSystem;

    public abstract class Gun : AbstractView
    {
        protected BG_GunTable _bgGunEntity;

        [HierarchyPath("Sprite")]
        public SpriteRenderer GunSprite;

        [HierarchyPath("ShootPos")]
        public Transform ShootPos;

        public List<AudioClip> ShootSounds = new List<AudioClip>();

        public AudioClip ReloadSound;

        [HierarchyPath("/Player/Weapon/GunShootLight")]
        public SpriteRenderer GunShootLight;

        [HierarchyPath("/Player/Weapon/Aim")]
        public Transform Aim;

        public InputAction AttackAction { get; private set; }
        
        [ShowInInspector]
        protected Enemy _TargetEnemy { get; set; }

        public bool IsShooting { get; protected set; }

        [ShowInInspector]
        protected float _BulletSpeed { get; set; }

        [ShowInInspector]
        protected float _UnstableAngle { get; set; } // 弹道不稳定角度

        public float AdditionalCameraSize { get; set; }

        public GunData Data
        {
            get => this.GetSystem<GunSystem>().AllGuns.Find(data => data.Key == GetType().Name);
        }

        [ShowInInspector]
        protected GunShootInterval _ShootInterval { get; set; }

        [ShowInInspector]
        public GunClip Clip { get; set; }

        public BulletBag Bag { get; set; }

        public ShootBackForce BackForce { get; set; }

        /// <summary>
        /// 是否可以射击
        /// </summary>
        protected bool _CanShoot
        {
            get => !Clip.IsEmpty && !Clip.IsReloading && _ShootInterval.CanShoot;
        }

        public Vector3 MousePosition
        {
            get => Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue()).Set(z: 0);
        }

        public bool IsMouseLeftButtonDown
        {
            get => AttackAction.ReadValue<float>().Approximately(1);
        }

        public Vector3 ShootDirection
        {
            get
            {
                var currentRoom = this.GetModel<LevelModel>().CurrentRoom;
                if (currentRoom && currentRoom.EnemiesInRoom.Count > 0)
                {
                    _TargetEnemy = currentRoom.EnemiesInRoom
                       .OrderBy(e => e.Distance2D(MousePosition))
                       .FirstOrDefault(e =>
                        {
                            var vector2 = this.Position2DTo(e);
                            if (Physics2D.Raycast(this.GetPosition(), vector2.normalized, vector2.magnitude, LayerMask.GetMask("Wall")))
                            {
                                return false;
                            }
                            return e.GameObject.name.StartsWith("Enemy"); // 仅对小怪生效
                        });

                    if (_TargetEnemy) // 自动瞄准
                    {
                        Aim.CopyPositionFrom(_TargetEnemy);
                        Aim.EnableGameObject();
                        return this.Direction2DTo(_TargetEnemy);
                    }
                }

                Aim.DisableGameObject();
                return (MousePosition - transform.position).normalized;
            }
        }

        protected virtual void Awake()
        {
            this.BindHierarchyComponent();

            // 依据子类类名获取 BG 中的数据
            _bgGunEntity = BG_GunTable.GetEntity(GetType().Name);

            _BulletSpeed          = _bgGunEntity.BulletSpeed;
            _UnstableAngle        = _bgGunEntity.UnstableAngle;
            AdditionalCameraSize = _bgGunEntity.AdditionalCameraSize;

            _ShootInterval = new GunShootInterval(_bgGunEntity.ShootInterval);
            Clip           = new GunClip(this);
            Bag            = new BulletBag(this);
            BackForce      = new ShootBackForce(GunSprite);

            GunShootLight.DisableGameObject();
        }

        private void OnEnable()
        {
            AttackAction = InputKit.BindPerformed(AssetConfig.Action.ATTACK, context =>
            {
                if (!UIKit.IsPanelShown<UIMap>() && !UIKit.IsPanelShown<UIGunList>()) // 地图界面打开时，不允许射击
                {
                    ShootDown(ShootDirection);
                }
            }).BindCanceled(context =>
            {
                // 松手时检查是否还在射击，如果还在射击，则抬枪
                if (IsShooting)
                {
                    ShootUp();
                }
            }).UnBindAllWhenGameObjectDisabled(gameObject);

            InputKit.BindPerformed(AssetConfig.Action.LOAD_BULLET, context =>
            {
                // 重装子弹
                Reload();
            }).UnBindAllWhenGameObjectDisabled(gameObject);
        }

        private void Update()
        {
            if (IsShooting)
            {
                Shooting(ShootDirection);
            }

            BackForce.Update();

            var angle = Mathf.Atan2(ShootDirection.y, ShootDirection.x).Rad2Deg();
            this.SetLocalEulerAngles(z: angle);             // 使 Weapon 方向跟手
            this.SetLocalScale(y: ShootDirection.x.Sign()); // 使 Weapon 随鼠标左右翻转
        }

        protected virtual void OnDisable()
        {
            ShootUp(); // 销毁时抬枪，否则死后可能会一直播放射击声音
        }

        public abstract void ShootDown(Vector2 direction);

        public abstract void Shooting(Vector2 direction);

        public abstract void ShootUp();

        public virtual void ShootOnce(Vector2 direction)
        {
            Clip.Use();             // 弹夹使用子弹
            _ShootInterval.Reset(); // 射击间隔重置

            BulletHelper.Shoot(
                ShootPos.position,
                direction,
                BulletFactory.Instance.GunBullet,
                _bgGunEntity.DamageRange.RandomSelect(),
                _BulletSpeed,
                _UnstableAngle);

            BackForce.Shoot(_bgGunEntity.BackForceA, _bgGunEntity.BackForceFrames);

            ShowGunShootLight(direction);

            CameraController.SHAKE.Trigger(_bgGunEntity.ShootShakeA, _bgGunEntity.ShootShakeFrames);

            TypeEventSystem.GLOBAL.Send(new GunShootEvent(this));
        }

        protected void ShowGunShootLight(Vector2 direction)
        {
            GunShootLight.SetPosition(ShootPos.position)
               .SetTransformRight(direction)
               .EnableGameObject();

            ActionKit.DelayFrame(3, () =>
            {
                GunShootLight.DisableGameObject();
            }).StartCurrentScene();
        }

        public void Reload()
        {
            Bag.Reload(Clip, ReloadSound); // 重装子弹
        }

        protected void PlayBulletEmptySound()
        {
            if (TimerKit.HasPassedInterval(this, 0.3f)) // 每隔 2 倍冷却间隔播放空弹夹音效
            {
                AudioKit.PlaySound(AssetConfig.Sound.EMPTY_BULLET);
            }
        }

        /// <summary>
        /// 尝试自动装填
        /// </summary>
        protected void TryAutoReload()
        {
            if (_ShootInterval.CanShoot && !Bag.IsEmpty) // 射击间隔冷却完成
            {
                Reload(); // 自动装填
            }
            else
            {
                PlayBulletEmptySound(); // 播放空弹夹音效
            }
        }

        protected override IArchitecture _Architecture { get => Game.Architecture; }
    }
}