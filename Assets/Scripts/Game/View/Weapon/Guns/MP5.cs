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
    using Framework.Toolkits.FluentAPI;

    public class MP5 : Gun
    {
        private AudioPlayer _audioPlayer;

        public override void ShootDown(Vector2 direction)
        {
            if (_CanShoot)
            {
                ShootOnce(direction);
                _audioPlayer = AudioKit.PlaySound(ShootSounds.RandomTakeOne(), volume: 0.3f, loop: true);
                IsShooting   = true;
            }
            else if (Clip.IsEmpty) // 自动装填
            {
                Reload(() =>
                {
                    if (IsMouseLeftButtonDown)
                    {
                        _audioPlayer = AudioKit.PlaySound(ShootSounds.RandomTakeOne(), volume: 0.3f, loop: true);
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
            IsShooting = false;
        }
    }
}