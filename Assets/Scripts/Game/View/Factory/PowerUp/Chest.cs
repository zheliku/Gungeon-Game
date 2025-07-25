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

                var unOwnedGuns = this.GetSystem<GunSystem>().UnOwnedGuns;

                if (unOwnedGuns.Count > 0) // 随机获取一把枪
                {
                    var powerUpGun = PowerUpFactory.Instance.PowerUpGun
                        .Instantiate(this.GetPosition())
                        .Self(self =>
                        {
                            self.Data = unOwnedGuns.RandomTakeOne();
                        })
                        .EnableGameObject();
                    
                    powerUpGun.Room = this.GetModel<LevelModel>().CurrentRoom;
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