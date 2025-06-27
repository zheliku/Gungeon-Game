// ------------------------------------------------------------
// @file       PowerUpHalfBullet.cs
// @brief
// @author     zheliku
// @Modified   2025-04-11 11:56:53
// @Copyright  Copyright (c) 2025, zheliku
// ------------------------------------------------------------

namespace Game
{
    using Framework.Core;
    using Framework.Toolkits.AudioKit;
    using Framework.Toolkits.FluentAPI;
    using UnityEngine;

    public class AllGunHalfBullet : PowerUp
    {
        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag("Player"))
            {
                Room.PowerUps.Remove(this); // 从房间中移除

                foreach (var gunData in this.GetSystem<GunSystem>().OwnedGuns)
                {
                    var bulletCountToAdd = gunData.Config.BagMaxBulletCount / 2;
                    gunData.AddBagBullet(bulletCountToAdd);
                }
                
                TypeEventSystem.GLOBAL.Send(new GunBulletChangeEvent(Player.Instance.CurrentGun));
                AudioKit.PlaySound(AssetConfig.Sound.POWER_UP_HALF_BULLET);
                this.DestroyGameObject();
            }
        }
    }
}