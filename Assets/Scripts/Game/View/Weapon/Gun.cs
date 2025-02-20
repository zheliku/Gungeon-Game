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
    using Framework.Toolkits.AudioKit;
    using Framework.Toolkits.EventKit;
    using Framework.Toolkits.FluentAPI;
    using Framework.Toolkits.InputKit;
    using Sirenix.OdinInspector;
    using UnityEngine;
    using UnityEngine.InputSystem;
    using UnityEngine.Serialization;

    public abstract class Gun : AbstractView
    {
        [HierarchyPath("Bullet")]
        public GameObject Bullet;

        public List<AudioClip> ShootSounds = new List<AudioClip>();

        public AudioClip ReloadSound;

        [HierarchyPath("/Player/Weapon/GunShootLight")]
        public SpriteRenderer GunShootLight;

        public bool IsShooting { get; protected set; }

        public bool IsReloading { get; protected set; }

        protected float _shootTime = 0;

        [ShowInInspector]
        protected abstract float _BulletSpeed { get; }

        [ShowInInspector]
        protected abstract float _ShootInterval { get; }

        [ShowInInspector]
        public abstract int BulletCount { get; }

        [ShowInInspector]
        public int CurrentBulletCount;

        /// <summary>
        /// 是否可以射击
        /// </summary>
        protected bool _CanShoot
        {
            get => _HaveBullet && _ReachCooling && !IsReloading;
        }

        /// <summary>
        /// 达到冷却时间
        /// </summary>
        protected bool _ReachCooling
        {
            get
            {
                if (_shootTime <= 0)
                {
                    _shootTime = _ShootInterval;
                    return true;
                }
                return false;
            }
        }

        protected bool _HaveBullet
        {
            get => CurrentBulletCount > 0;
        }

        protected Vector3 _MousePosition
        {
            get => Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue()).Set(z: 0);
        }

        protected Vector3 _ShootDirection
        {
            get => (_MousePosition - transform.position).normalized;
        }

        protected virtual void Awake()
        {
            this.BindHierarchyComponent();

            Bullet.Disable();
            GunShootLight.DisableGameObject();

            CurrentBulletCount = BulletCount;
        }

        private void OnEnable()
        {
            InputKit.BindPerformed("Attack", context =>
            {
                ShootDown(_ShootDirection);
                IsShooting = true;
            }).BindCanceled(context =>
            {
                // 松手时检查是否还在射击，如果还在射击，则抬枪
                if (IsShooting)
                {
                    ShootUp(_ShootDirection);
                    IsShooting = false;
                }
            }).UnBindAllWhenGameObjectDisabled(gameObject);

            InputKit.BindPerformed("LoadBullet", context =>
            {
                Reload();
            }).UnBindAllWhenGameObjectDisabled(gameObject);
        }

        private void Update()
        {
            if (IsShooting)
            {
                Shooting(_ShootDirection);
            }

            var angle = Mathf.Atan2(_ShootDirection.y, _ShootDirection.x).Rad2Deg();
            this.SetLocalEulerAngles(z: angle);              // 使 Weapon 方向跟手
            this.SetLocalScale(y: _ShootDirection.x.Sign()); // 使 Weapon 随鼠标左右翻转

            _shootTime -= Time.deltaTime;
        }

        public abstract void ShootDown(Vector2 direction);

        public abstract void Shooting(Vector2 direction);

        public abstract void ShootUp(Vector2 direction);

        public virtual void ShootOnce(Vector2 direction)
        {
            CurrentBulletCount--;

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
                    enemy.Hurt(1);
                }

                bullet.Destroy();
            });

            ShowGunShootLight(direction);

            TypeEventSystem.GLOBAL.Send(new GunShootEvent(this));

            // 没有子弹，则抬枪
            if (!_HaveBullet)
            {
                ShootUp(direction);
                IsShooting = false;
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

        protected void Reload()
        {
            // 没有子弹，则不重载
            if (CurrentBulletCount == BulletCount)
            {
                return;
            }
            
            // 重装子弹前首先抬枪
            ShootUp(_ShootDirection);
            
            IsShooting = false;
            IsReloading = true;
            AudioKit.PlaySound(ReloadSound, onPlayFinish: (player) =>
            {
                IsReloading        = false;
                CurrentBulletCount = BulletCount;
                TypeEventSystem.GLOBAL.Send(new GunLoadBulletEvent(this));
            });
        }

        protected override IArchitecture _Architecture { get => Game.Interface; }
    }
}