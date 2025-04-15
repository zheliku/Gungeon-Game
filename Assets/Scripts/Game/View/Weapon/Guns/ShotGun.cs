// ------------------------------------------------------------
// @file       ShotGun.cs
// @brief
// @author     zheliku
// @Modified   2025-01-31 21:01:30
// @Copyright  Copyright (c) 2025, zheliku
// ------------------------------------------------------------

namespace Game
{
    using Framework.Core;
    using Framework.Toolkits.AudioKit;
    using Framework.Toolkits.EventKit;
    using UnityEngine;
    using Framework.Toolkits.FluentAPI;

    public class ShotGun : Gun
    {
        public float IntervalAngle = 10;

        public int OneShootCount = 5;

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

            BulletHelper.SpreadShoot(
                fireCount: OneShootCount,
                center: ShootPos.position,
                radius: 0f,
                direction: direction,
                intervalAngle: IntervalAngle,
                bulletPrefab: BulletFactory.Instance.GunBullet.gameObject,
                damage: 1f,
                speed: _BulletSpeed,
                _UnstableAngle);

            BackForce.Shoot(_bgGunEntity.BackForceA, _bgGunEntity.BackForceFrames);
            
            BulletFactory.GenBulletShell(direction, BulletFactory.Instance.ShotGunShell);
            
            ShowGunShootLight(direction);

            CameraController.SHAKE.Trigger(_bgGunEntity.ShootShakeA, _bgGunEntity.ShootShakeFrames);

            TypeEventSystem.GLOBAL.Send(new GunShootEvent(this));

            // 没有子弹，则抬枪
            if (Clip.IsEmpty)
            {
                ShootUp(direction);
                IsShooting = false;
            }
        }
    }
}