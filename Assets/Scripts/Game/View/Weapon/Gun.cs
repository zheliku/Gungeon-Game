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
    using Framework.Core;
    using Framework.Toolkits.ActionKit;
    using Framework.Toolkits.EventKit;
    using Framework.Toolkits.FluentAPI;
    using Framework.Toolkits.InputKit;
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

        [HierarchyPath("/Player/Weapon/GunShootLight")]
        public SpriteRenderer GunShootLight;

        public bool IsShooting { get; protected set; }

        [ShowInInspector]
        protected float _BulletSpeed { get; set; }

        [ShowInInspector]
        protected GunShootInterval _ShootInterval { get; set; }

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

        public Vector3 ShootDirection
        {
            get => (MousePosition - transform.position).normalized;
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
            InputKit.BindPerformed("Attack", context =>
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
                Bag.Reload(Clip, ReloadSound);
            }).UnBindAllWhenGameObjectDisabled(gameObject);
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
               .SetTransformRight(direction);

            var rigidbody2D = bullet.GetComponent<Rigidbody2D>();

            rigidbody2D.linearVelocity = direction * _BulletSpeed;

            bullet.OnCollisionEnter2DEvent(collider2D =>
            {
                var enemy = collider2D.gameObject.GetComponent<Enemy>();
                if (enemy)
                {
                    var damage = _gunData.DamageRange.RandomSelect();
                    enemy.Hurt(damage);
                }

                bullet.Destroy();
            });

            ShowGunShootLight(direction);

            TypeEventSystem.GLOBAL.Send(new GunShootEvent(this));

            // 没有子弹，则抬枪
            if (Clip.IsEmpty)
            {
                ShootUp(direction);
            }
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

        protected override IArchitecture _Architecture { get => Game.Interface; }
    }
}