// ------------------------------------------------------------
// @file       Bow.cs
// @brief
// @author     zheliku
// @Modified   2025-01-31 22:01:33
// @Copyright  Copyright (c) 2025, zheliku
// ------------------------------------------------------------

namespace Game
{
    using Framework.Core;
    using Framework.Toolkits.AudioKit;
    using Framework.Toolkits.EventKit;
    using Framework.Toolkits.FluentAPI;
    using UnityEngine;

    public class Bow : Gun
    {
        protected override float _BulletSpeed { get; } = 10;

        protected override float _ShootInterval { get; } = 0.75f;

        [HierarchyPath("ArrowPrepare")]
        private GameObject _arrowPrepare;

        protected override void Awake()
        {
            base.Awake();

            _arrowPrepare.Disable();
        }

        public override void ShootDown(Vector2 direction)
        {
            _shootTime = _ShootInterval;
        }

        public override void Shooting(Vector2 direction)
        {
            if (_shootTime <= 0 && _arrowPrepare.IsDisabled())
            {
                _arrowPrepare.Enable();
            }
        }

        public override void ShootUp(Vector2 direction)
        {
            if (_CanShoot && _arrowPrepare.IsEnabled())
            {
                _arrowPrepare.Disable();
                
                var arrow = Bullet.Instantiate(Bullet.transform.position)
                   .Enable()
                   .SetLocalEulerAngles(z: Mathf.Atan2(direction.y, direction.x).Rad2Deg());

                var rigidbody2D = arrow.GetComponent<Rigidbody2D>();

                rigidbody2D.linearVelocity = direction * _BulletSpeed;
                
                arrow.OnCollisionEnter2DEvent(collider2D =>
                {
                    if (collider2D.gameObject.GetComponent<Enemy>())
                    {
                        collider2D.gameObject.Destroy();
                    }

                    arrow.Destroy();
                });

                AudioKit.PlaySound(ShootSounds.RandomChoose(), volume: 0.8f);
            }
        }
    }
}