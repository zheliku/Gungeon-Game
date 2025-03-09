// ------------------------------------------------------------
// @file       AbstractWeapon.cs
// @brief
// @author     zheliku
// @Modified   2025-01-31 18:01:29
// @Copyright  Copyright (c) 2025, zheliku
// ------------------------------------------------------------

namespace Game
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Framework.Core;
    using Framework.Toolkits.ActionKit;
    using Framework.Toolkits.AudioKit;
    using Framework.Toolkits.FluentAPI;
    using Framework.Toolkits.InputKit;
    using Framework.Toolkits.TimerKit;
    using Sirenix.OdinInspector;
    using UnityEngine;
    using UnityEngine.InputSystem;

    public abstract class Gun : AbstractView
    {
        protected BG_GunData _gunData;

        [HierarchyPath("Bullet")]
        public GameObject Bullet;

        public List<AudioClip> ShootSounds = new List<AudioClip>();

        public AudioClip ReloadSound;

        // public AudioClip EmptyBulletSound;

        [HierarchyPath("/Player/Weapon/GunShootLight")]
        public SpriteRenderer GunShootLight;

        [HierarchyPath("/Player/Weapon/Aim")]
        public Transform Aim;

        public InputAction AttackAction { get; private set; }

        public bool IsShooting { get; protected set; }

        [ShowInInspector]
        protected float _BulletSpeed { get; set; }

        [ShowInInspector]
        protected GunShootInterval _ShootInterval { get; set; }

        [ShowInInspector]
        protected IEnemy _TargetEnemy { get; set; }

        [ShowInInspector]
        public GunClip Clip { get; set; }

        public BulletBag Bag { get; set; }

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
                       .OrderBy(e => (e.Position - MousePosition).magnitude)
                       .FirstOrDefault(e =>
                        {
                            var vector2 = this.Position2DTo(e.Position);
                            if (Physics2D.Raycast(this.GetPosition(), vector2.normalized, vector2.magnitude, LayerMask.GetMask("Wall")))
                            {
                                return false;
                            }
                            return true;
                        });

                    if (_TargetEnemy != null) // 自动瞄准
                    {
                        Aim.SetPosition(_TargetEnemy.Position);
                        Aim.EnableGameObject();
                        return this.Direction2DTo(_TargetEnemy.Position);
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
            _gunData = BG_GunData.GetEntity(GetType().Name);

            _BulletSpeed   = _gunData.BulletSpeed;
            _ShootInterval = new GunShootInterval(_gunData.ShootInterval);
            Clip           = new GunClip(this, _gunData.ClipBulletCount);
            Bag            = new BulletBag(this, _gunData.BagBulletCount);

            Bullet.Disable();
            GunShootLight.DisableGameObject();
        }

        private void OnEnable()
        {
            AttackAction = InputKit.BindPerformed("Attack", context =>
            {
                ShootDown(ShootDirection);
            }).BindCanceled(context =>
            {
                // 松手时检查是否还在射击，如果还在射击，则抬枪
                if (IsShooting)
                {
                    ShootUp(ShootDirection);
                }
            }).UnBindAllWhenGameObjectDisabled(gameObject);

            InputKit.BindPerformed("LoadBullet", context =>
            {
                // 重装子弹
                Reload();
            }).UnBindAllWhenGameObjectDisabled(gameObject);

            InputKit.BindPerformed("Move", context =>
                                   { });
        }

        private void Update()
        {
            if (IsShooting)
            {
                Shooting(ShootDirection);
            }

            var angle = Mathf.Atan2(ShootDirection.y, ShootDirection.x).Rad2Deg();
            this.SetLocalEulerAngles(z: angle);             // 使 Weapon 方向跟手
            this.SetLocalScale(y: ShootDirection.x.Sign()); // 使 Weapon 随鼠标左右翻转
        }

        public abstract void ShootDown(Vector2 direction);

        public abstract void Shooting(Vector2 direction);

        public abstract void ShootUp(Vector2 direction);

        public virtual void ShootOnce(Vector2 direction)
        {
            Clip.Use();             // 弹夹使用子弹
            _ShootInterval.Reset(); // 射击间隔重置

            var bullet = Bullet.Instantiate(Bullet.GetPosition())
               .Enable()
               .SetTransformRight(direction)
               .GetComponent<PlayerBullet>();

            var rigidbody2D = bullet.GetComponent<Rigidbody2D>();

            rigidbody2D.linearVelocity = direction * _BulletSpeed;

            bullet.Damage = _gunData.DamageRange.RandomSelect();

            ShowGunShootLight(direction);

            TypeEventSystem.GLOBAL.Send(new GunShootEvent(this));
        }

        protected void ShowGunShootLight(Vector2 direction)
        {
            GunShootLight.SetPosition(Bullet.GetPosition())
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
                AudioKit.PlaySound(Config.Sound.EMPTY_BULLET);
            }
        }

        /// <summary>
        /// 尝试自动装填
        /// </summary>
        protected void TryAutoReload()
        {
            if (_ShootInterval.CanShoot) // 射击间隔冷却完成
            {
                Reload(); // 自动装填
            }
            else
            {
                PlayBulletEmptySound(); // 播放空弹夹音效
            }
        }

        protected override IArchitecture _Architecture { get => Game.Interface; }
    }
}