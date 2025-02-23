// ------------------------------------------------------------
// @file       RocketGun.cs
// @brief
// @author     zheliku
// @Modified   2025-01-31 22:01:55
// @Copyright  Copyright (c) 2025, zheliku
// ------------------------------------------------------------

namespace Game
{
    using Framework.Toolkits.AudioKit;
    using Framework.Toolkits.FluentAPI;
    using UnityEngine;

    public class RocketGun : Gun
    {
        public override void ShootDown(Vector2 direction)
        {
            if (_CanShoot)
            {
                ShootOnce(direction);
                AudioKit.PlaySound(ShootSounds.RandomTakeOne(), volume: 0.4f);
                IsShooting = true;
            }
            else if (Clip.IsEmpty)
            {
                TryAutoReload();
            }
        }

        public override void Shooting(Vector2 direction) { }

        public override void ShootUp(Vector2 direction)
        {
            IsShooting = false;
        }
    }
}