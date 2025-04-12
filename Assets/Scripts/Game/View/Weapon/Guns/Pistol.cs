// ------------------------------------------------------------
// @file       Pistol.cs
// @brief
// @author     zheliku
// @Modified   2025-01-31 16:01:18
// @Copyright  Copyright (c) 2025, zheliku
// ------------------------------------------------------------

namespace Game
{
    using Framework.Toolkits.ActionKit;
    using Framework.Toolkits.AudioKit;
    using Framework.Toolkits.FluentAPI;
    using UnityEngine;

    public class Pistol : Gun
    {
        public override void ShootDown(Vector2 direction)
        {
            if (_CanShoot)
            {
                ShootOnce(direction);
                AudioKit.PlaySound(ShootSounds.RandomTakeOne(), volume: 0.3f);
                IsShooting = true;
                
                BulletFactory.GenBulletShell(direction, BulletFactory.Instance.PistolShell);
            }
            else if (Clip.IsEmpty)
            {
                TryAutoReload();
            }
        }

        public override void Shooting(Vector2 direction)
        { }

        public override void ShootUp(Vector2 direction)
        {
            IsShooting = false;
        }
    }
}