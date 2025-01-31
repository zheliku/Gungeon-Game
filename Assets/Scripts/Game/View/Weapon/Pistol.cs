// ------------------------------------------------------------
// @file       Pistol.cs
// @brief
// @author     zheliku
// @Modified   2025-01-31 16:01:18
// @Copyright  Copyright (c) 2025, zheliku
// ------------------------------------------------------------

namespace Game
{
    using Framework.Core;
    using Framework.Toolkits.AudioKit;
    using Framework.Toolkits.EventKit;
    using Framework.Toolkits.FluentAPI;
    using UnityEngine;

    public class Pistol : Gun
    {
        protected override float _BulletSpeed { get; } = 10;

        protected override float _ShootInterval { get; } = 0.25f;
        
        public override int BulletCount { get; } = 10;

        public override void ShootDown(Vector2 direction)
        {
            if (_CanShoot)
            {
                ShootOnce(direction);
                
                AudioKit.PlaySound(ShootSounds.RandomChoose(), volume: 0.3f);
            }
        }

        public override void Shooting(Vector2 direction)
        { }

        public override void ShootUp(Vector2 direction) { }
    }
}