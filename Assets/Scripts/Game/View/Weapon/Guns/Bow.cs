// ------------------------------------------------------------
// @file       Bow.cs
// @brief
// @author     zheliku
// @Modified   2025-01-31 22:01:33
// @Copyright  Copyright (c) 2025, zheliku
// ------------------------------------------------------------

using System.Collections.Generic;

namespace Game
{
    using Framework.Core;
    using Framework.Toolkits.AudioKit;
    using Framework.Toolkits.FluentAPI;
    using UnityEngine;

    public class Bow : Gun
    {
        [HierarchyPath("ArrowPrepare")]
        private GameObject _arrowPrepare;

        public List<AudioClip> PreparedSounds = new();
        
        private AudioPlayer _pullSoundPlayer;

        protected override void Awake()
        {
            base.Awake();

            _arrowPrepare.Disable();
        }

        public override void ShootDown(Vector2 direction)
        {
            if (!Clip.IsEmpty)
            {
                _ShootInterval.Reset();
                IsShooting = true;
                _pullSoundPlayer = AudioKit.PlaySound(PreparedSounds.RandomTakeOne(), onPlayFinish: player =>
                {
                    _pullSoundPlayer = null;
                });
            }
            else if (Clip.IsEmpty)
            {
                TryAutoReload();
            }
        }

        public override void Shooting(Vector2 direction)
        {
            // 满足射击条件，则显示箭头
            if (_CanShoot && _arrowPrepare.IsDisabled())
            {
                _arrowPrepare.Enable();
            }
        }

        public override void ShootUp()
        {
            if (_CanShoot && _arrowPrepare.IsEnabled())
            {
                _arrowPrepare.Disable();

                ShootOnce(ShootDirection);

                AudioKit.PlaySound(ShootSounds.RandomTakeOne(), volume: 0.8f);
            }
            else
            {
                _pullSoundPlayer?.Stop();
                _pullSoundPlayer = null;
            }

            IsShooting = false;
        }

        public override void ShootOnce(Vector2 direction)
        {
            Clip.Use(); // 弹夹使用子弹
            _ShootInterval.Reset(); // 射击间隔重置

            BulletHelper.Shoot(
                ShootPos.position,
                direction,
                BulletFactory.Instance.ArrowBullet,
                _bgGunEntity.DamageRange.RandomSelect(),
                _BulletSpeed,
                _UnstableAngle);

            ShowGunShootLight(direction);

            TypeEventSystem.GLOBAL.Send(new GunShootEvent(this));
        }
    }
}