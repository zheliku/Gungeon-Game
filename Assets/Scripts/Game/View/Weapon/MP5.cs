// ------------------------------------------------------------
// @file       MP5.cs
// @brief
// @author     zheliku
// @Modified   2025-01-31 18:01:44
// @Copyright  Copyright (c) 2025, zheliku
// ------------------------------------------------------------

namespace Game
{
    using UnityEngine;
    using Framework.Toolkits.AudioKit;
    using Framework.Toolkits.EventKit;
    using Framework.Toolkits.FluentAPI;

    public class MP5 : Gun
    {
        protected override float _BulletSpeed { get; } = 10;

        protected override float _ShootInterval { get; } = 0.1f;

        private AudioPlayer _audioPlayer;

        public override void ShootDown(Vector2 direction)
        {
            if (_CanShoot)
            {
                Shoot(direction);
            }

            _audioPlayer = AudioKit.PlaySound(ShootSounds.RandomChoose(), volume: 0.3f, loop: true);
        }

        public override void Shooting(Vector2 direction)
        {
            if (_CanShoot)
            {
                Shoot(direction);
            }
        }

        public override void ShootUp(Vector2 direction)
        {
            _audioPlayer.Stop();
        }

        private void Shoot(Vector2 direction)
        {
            var bullet = Bullet.Instantiate(Bullet.transform.position)
               .Enable();
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