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
    using Framework.Toolkits.FluentAPI;
    using Framework.Toolkits.TimerKit;

    public class MP5 : Gun
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
                    _audioPlayer = AudioKit.PlaySound(ShootSounds.RandomTakeOne(), volume: 0.3f, loop: true);
                    IsShooting   = true;
                }
            }).UnRegisterWhenGameObjectDestroyed(this);
        }

        public override void ShootDown(Vector2 direction)
        {
            if (_CanShoot)
            {
                ShootOnce(direction);
                _audioPlayer = AudioKit.PlaySound(ShootSounds.RandomTakeOne(), volume: 0.3f, loop: true);
                IsShooting   = true;
                
                BulletFactory.GenBulletShell(direction, BulletFactory.Instance.PistolShell);
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
                
                BulletFactory.GenBulletShell(direction, BulletFactory.Instance.PistolShell);
            }
            else if (Clip.IsEmpty) // 子弹空时，播放空夹音效
            {
                PlayBulletEmptySound();
            }
        }

        public override void ShootUp()
        {
            _audioPlayer?.Stop();
            IsShooting = false;
        }
    }
}