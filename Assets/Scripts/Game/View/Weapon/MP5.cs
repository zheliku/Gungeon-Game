// ------------------------------------------------------------
// @file       MP5.cs
// @brief
// @author     zheliku
// @Modified   2025-01-31 18:01:44
// @Copyright  Copyright (c) 2025, zheliku
// ------------------------------------------------------------

namespace Game
{
    using Framework.Core;
    using UnityEngine;
    using Framework.Toolkits.AudioKit;
    using Framework.Toolkits.EventKit;
    using Framework.Toolkits.FluentAPI;

    public class MP5 : Gun
    {
        protected override float _BulletSpeed { get; } = 10;

        protected override float _ShootInterval { get; } = 0.1f;

        public override int BulletCount { get; } = 50;

        private AudioPlayer _audioPlayer;

        public override void ShootDown(Vector2 direction)
        {
            if (_CanShoot)
            {
                ShootOnce(direction);
                
                _audioPlayer = AudioKit.PlaySound(ShootSounds.RandomChoose(), volume: 0.3f, loop: true);
            }
        }

        public override void Shooting(Vector2 direction)
        {
            if (_CanShoot)
            {
                ShootOnce(direction);
            }
        }

        public override void ShootUp(Vector2 direction)
        {
            _audioPlayer.Stop();
        }
    }
}