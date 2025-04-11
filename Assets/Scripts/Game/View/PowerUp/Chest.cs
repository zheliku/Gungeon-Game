// ------------------------------------------------------------
// @file       Chest.cs
// @brief
// @author     zheliku
// @Modified   2025-02-23 21:02:17
// @Copyright  Copyright (c) 2025, zheliku
// ------------------------------------------------------------

namespace Game
{
    using Framework.Core;
    using Framework.Toolkits.AudioKit;
    using Framework.Toolkits.FluentAPI;
    using UnityEngine;

    public class Chest : PowerUp
    {
        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag("Player"))
            {
                Room.PowerUps.Remove(this); // 从房间中移除

                var hp1 = PowerUpFactory.Instance.SingleGunFullBullet
                   .Instantiate(this.GetPosition())
                   .EnableGameObject();

                hp1.Room = this.GetModel<LevelModel>().CurrentRoom;

                AudioKit.PlaySound(AssetConfig.Sound.CHEST, 0.6f);
                this.DestroyGameObject();
            }
        }
    }
}