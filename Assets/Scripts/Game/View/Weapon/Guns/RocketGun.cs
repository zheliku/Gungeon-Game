// ------------------------------------------------------------
// @file       RocketGun.cs
// @brief
// @author     zheliku
// @Modified   2025-01-31 22:01:55
// @Copyright  Copyright (c) 2025, zheliku
// ------------------------------------------------------------

namespace Game
{
    using Framework.Core;
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
        
        public override void ShootOnce(Vector2 direction)
        {
            Clip.Use();             // 弹夹使用子弹
            _ShootInterval.Reset(); // 射击间隔重置

            BulletHelper.Shoot(
                ShootPos.position,
                direction,
                BulletFactory.Instance.RocketBullet.gameObject,
                _gunData.DamageRange.RandomSelect(),
                _BulletSpeed,
                _UnstableAngle);
            
            BackForce.Shoot(_gunData.BackForceA, _gunData.BackForceFrames);

            CameraController.Instance.Shake.Trigger(_gunData.ShootShakeA, _gunData.ShootShakeFrames);

            ShowGunShootLight(direction);

            TypeEventSystem.GLOBAL.Send(new GunShootEvent(this));
        }
    }
}