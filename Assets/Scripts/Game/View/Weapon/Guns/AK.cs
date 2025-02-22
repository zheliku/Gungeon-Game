// ------------------------------------------------------------
// @file       AK.cs
// @brief
// @author     zheliku
// @Modified   2025-01-31 21:01:13
// @Copyright  Copyright (c) 2025, zheliku
// ------------------------------------------------------------

namespace Game
{
    using Framework.Toolkits.AudioKit;
    using UnityEngine;

    public class AK : Gun
    {
        private AudioPlayer _audioPlayer;

        public override void ShootDown(Vector2 direction)
        {
            if (_CanShoot)
            {
                ShootOnce(direction);
                _audioPlayer = AudioKit.PlaySound(ShootSounds[0], volume: 0.3f, loop: true);
                IsShooting   = true;
            }
            else if (Clip.IsEmpty) // 自动装填
            {
                Reload(() =>
                {
                    if (IsMouseLeftButtonDown)
                    {
                        _audioPlayer = AudioKit.PlaySound(ShootSounds[0], volume: 0.3f, loop: true);
                        IsShooting   = true;
                    }
                });
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
            AudioKit.PlaySound(ShootSounds[1], volume: 0.3f);
            IsShooting = false;
        }
    }
}