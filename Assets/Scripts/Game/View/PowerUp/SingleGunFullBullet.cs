// ------------------------------------------------------------
// @file       SingleGunFullBullet.cs
// @brief
// @author     zheliku
// @Modified   2025-04-11 12:31:39
// @Copyright  Copyright (c) 2025, zheliku
// ------------------------------------------------------------

namespace Game
{
    using Framework.Core;
    using Framework.Toolkits.AudioKit;
    using Framework.Toolkits.FluentAPI;
    using UnityEngine;

    public class SingleGunFullBullet : PowerUp
    {
        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag("Player"))
            {
                this.GetModel<LevelModel>().CurrentRoom.PowerUps.Remove(this); // 从房间中移除

                var gun = Player.Instance.CurrentGun;
                var bag = gun.Bag;

                if (bag.IsFull)
                {
                    return;
                }

                var bulletCountToAdd = bag.MaxBulletCount;
                bag.AddBulletCount(bulletCountToAdd);

                TypeEventSystem.GLOBAL.Send(new GunBulletChangeEvent(gun));
                AudioKit.PlaySound(AssetConfig.Sound.POWER_UP_HALF_BULLET);
                this.DestroyGameObject();
            }
        }
    }
}