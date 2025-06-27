// ------------------------------------------------------------
// @file       Chest.cs
// @brief
// @author     zheliku
// @Modified   2025-02-23 21:02:17
// @Copyright  Copyright (c) 2025, zheliku
// ------------------------------------------------------------

namespace Game
{
    using System.Linq;
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
                if (this.GetModel<PlayerModel>().Key.Value == 0)
                {
                    Player.DisplayText("没有钥匙", 1);
                    return;
                }
                else
                {
                    this.GetModel<PlayerModel>().Key.Value--;
                }

                Room.PowerUps.Remove(this); // 从房间中移除

                var ownedGuns = this.GetSystem<GunSystem>().OwnedGuns;
                var unlockedGuns = this.GetSystem<GunSystem>().UnlockedGuns;
                var availableGuns = unlockedGuns.Except(ownedGuns).ToList();

                if (availableGuns.Count > 0) // 随机获取一把枪
                {
                    var randomGun = availableGuns.RandomTakeOne();
                    randomGun.Owned = true;
                    Player.Instance.UseGun(-1); // 使用最后一把枪
                }
                else
                {
                    var hp1 = PowerUpFactory.Instance.SingleGunFullBullet
                        .Instantiate(this.GetPosition())
                        .EnableGameObject();

                    hp1.Room = this.GetModel<LevelModel>().CurrentRoom;
                }

                AudioKit.PlaySound(AssetConfig.Sound.CHEST, 0.6f);
                this.DestroyGameObject();
            }
        }
    }
}