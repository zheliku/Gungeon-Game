// ------------------------------------------------------------
// @file       AK.cs
// @brief
// @author     zheliku
// @Modified   2025-01-31 21:01:13
// @Copyright  Copyright (c) 2025, zheliku
// ------------------------------------------------------------

namespace Game
{
    using Framework.Core;
    using Framework.Toolkits.AudioKit;
    using UnityEngine;

    public class AK : Gun
    {
        private AudioPlayer _audioPlayer;

        protected override void Awake()
        {
            base.Awake();

            // 子弹空时，停止播放音效
            TypeEventSystem.GLOBAL.Register<GunBulletEmptyEvent>(e =>
            {
                if (e.Gun == this)
                {
                    _audioPlayer.Stop();
                }
            }).UnRegisterWhenGameObjectDestroyed(this);
            
            // 子弹装填完成时，若仍按下鼠标，则直接开始发射
            TypeEventSystem.GLOBAL.Register<GunBulletLoadedEvent>(e =>
            {
                if (IsMouseLeftButtonDown && e.Gun == this)
                {
                    _audioPlayer = AudioKit.PlaySound(ShootSounds[0], volume: 0.3f, loop: true);
                    IsShooting   = true;
                }
            }).UnRegisterWhenGameObjectDestroyed(this);
        }

        public override void ShootDown(Vector2 direction)
        {
            if (_CanShoot)
            {
                ShootOnce(direction);
                _audioPlayer = AudioKit.PlaySound(ShootSounds[0], volume: 0.3f, loop: true);
                IsShooting   = true;
            }
            else if (Clip.IsEmpty)
            {
                TryAutoReload();
            }
        }

        public override void Shooting(Vector2 direction)
        {
            if (_CanShoot)
            {
                ShootOnce(direction);
            }
            else if (Clip.IsEmpty) // 子弹空时，播放空夹音效
            {
                PlayBulletEmptySound();
            }
        }

        public override void ShootUp(Vector2 direction)
        {
            _audioPlayer?.Stop();
            AudioKit.PlaySound(ShootSounds[1], volume: 0.3f);
            IsShooting = false;
        }
    }
}