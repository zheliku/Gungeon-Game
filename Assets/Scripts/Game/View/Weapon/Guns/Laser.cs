// ------------------------------------------------------------
// @file       Laser.cs
// @brief
// @author     zheliku
// @Modified   2025-01-31 22:01:16
// @Copyright  Copyright (c) 2025, zheliku
// ------------------------------------------------------------

namespace Game
{
    using Framework.Core;
    using UnityEngine;
    using Framework.Toolkits.AudioKit;
    using Framework.Toolkits.FluentAPI;

    public class Laser : Gun
    {
        public float Distance = 10;

        [HierarchyPath("Bullet")]
        public LineRenderer LineRenderer;

        private AudioPlayer _audioPlayer;

        public override void ShootDown(Vector2 direction)
        {
            _audioPlayer = AudioKit.PlaySound(ShootSounds.RandomChoose(), volume: 0.6f, loop: true);
            LineRenderer.EnableGameObject();
            IsShooting = true;
        }

        public override void Shooting(Vector2 direction)
        {
            var layers = LayerMask.GetMask("Default", "Enemy");
            var hit    = Physics2D.Raycast(Bullet.GetPosition(), direction, Distance, layers);
            var hitPoint = hit ? hit.point : Bullet.GetPosition().ToVector2() + direction * Distance;
            LineRenderer.SetPositions(new Vector3[] { Bullet.GetPosition(), hitPoint });

            if (hit && _CanShoot)
            {
                var enemy = hit.collider.gameObject.GetComponent<Enemy>();
                if (enemy)
                {
                    enemy.Hurt(1);
                    _ShootInterval.Reset();
                }
            }
        }

        public override void ShootUp(Vector2 direction)
        {
            _audioPlayer.Stop();
            LineRenderer.DisableGameObject();
            IsShooting = false;
        }
    }
}