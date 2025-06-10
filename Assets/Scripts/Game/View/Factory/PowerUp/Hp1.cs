// ------------------------------------------------------------
// @file       HP1.cs
// @brief
// @author     zheliku
// @Modified   2025-02-23 21:02:35
// @Copyright  Copyright (c) 2025, zheliku
// ------------------------------------------------------------

namespace Game
{
    using Framework.Core;
    using Framework.Toolkits.AudioKit;
    using Framework.Toolkits.FluentAPI;
    using UnityEngine;

    public class Hp1 : PowerUp
    {
        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag("Player"))
            {
                Room.PowerUps.Remove(this); // 从房间中移除

                var playerProperty = this.GetModel<PlayerModel>().Property;
                if (playerProperty.IsFullHp)
                {
                    Player.DisplayText("现在还不需要", 1);
                }
                else
                {
                    playerProperty.Hp.Value++;
                    AudioKit.PlaySound(AssetConfig.Sound.HP1, 0.6f);
                    this.DestroyGameObject();
                }
            }
        }
    }
}