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
    using Framework.Core;
    using Framework.Toolkits.FluentAPI;
    using Framework.Toolkits.InputKit;
    using Sirenix.OdinInspector;
    using UnityEngine;
    using UnityEngine.InputSystem;

    public abstract class Gun : AbstractView
    {
        [HierarchyPath("Bullet")]
        public GameObject Bullet;

        public List<AudioClip> ShootSounds = new List<AudioClip>();
        
        protected bool _isShooting = false;

        protected float _shootTime = 0;

        [ShowInInspector]
        protected abstract float _BulletSpeed { get; }

        [ShowInInspector]
        protected abstract float _ShootInterval { get; }

        protected bool _CanShoot
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

            InputKit.BindPerformed("Attack", context =>
            {
                ShootDown(_ShootDirection);
                _isShooting = true;
            }).BindCanceled(context =>
            {
                ShootUp(_ShootDirection);
                _isShooting = false;
            }).UnBindAllPerformedWhenGameObjectDestroyed(gameObject);
        }

        private void Update()
        {
            if (_isShooting)
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

        protected override IArchitecture _Architecture { get => Game.Interface; }
    }
}