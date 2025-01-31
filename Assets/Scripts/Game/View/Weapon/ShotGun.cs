// ------------------------------------------------------------
// @file       ShotGun.cs
// @brief
// @author     zheliku
// @Modified   2025-01-31 21:01:30
// @Copyright  Copyright (c) 2025, zheliku
// ------------------------------------------------------------

namespace Game
{
    using Framework.Toolkits.AudioKit;
    using Framework.Toolkits.EventKit;
    using UnityEngine;
    using Framework.Toolkits.FluentAPI;

    public class ShotGun : Gun
    {
        protected override float _BulletSpeed   { get; } = 10;
        
        protected override float _ShootInterval { get; } = 0.5f;

        public float IntervalAngle = 10;
        
        public int BulletCount = 5;

        public override void ShootDown(Vector2 direction)
        {
            if (_CanShoot)
            {
                for (int i = 0; i < BulletCount; i++)
                {
                    var angle = (i - (BulletCount - 1) / 2f) * IntervalAngle;
                    Shoot(direction.Rotate(angle));
                }
                
                AudioKit.PlaySound(ShootSounds.RandomChoose(), volume: 0.4f);
            }
        }

        public override void Shooting(Vector2 direction) { }

        public override void ShootUp(Vector2 direction) { }
        
        private void Shoot(Vector2 direction)
        {
            var bullet = Bullet.Instantiate(Bullet.transform.position)
               .Enable()
               .SetLocalEulerAngles(z: Mathf.Atan2(direction.y, direction.x).Rad2Deg());
            
            var rigidbody2D = bullet.GetComponent<Rigidbody2D>();

            rigidbody2D.linearVelocity = direction * _BulletSpeed;

            bullet.OnCollisionEnter2DEvent(collider2D =>
            {
                if (collider2D.gameObject.GetComponent<Enemy>())
                {
                    collider2D.gameObject.Destroy();
                }

                bullet.Destroy();
            });
        }
    }
}