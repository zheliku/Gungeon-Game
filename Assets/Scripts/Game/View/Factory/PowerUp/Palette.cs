// ------------------------------------------------------------
// @file       Palette.cs
// @brief
// @author     zheliku
// @Modified   2025-06-09 11:57:07
// @Copyright  Copyright (c) 2025, zheliku
// ------------------------------------------------------------

namespace Game
{
    using Framework.Core;
    using Framework.Toolkits.AudioKit;
    using Framework.Toolkits.FluentAPI;
    using UnityEngine;

    public class Palette : PowerUp
    {
        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag("Player"))
            {
                Room.PowerUps.Remove(this); // 从房间中移除
                
                this.GetModel<PlayerModel>().Palette.Value++;
                AudioKit.PlaySound(AssetConfig.Sound.PALETTE, 0.8f);
                this.DestroyGameObject();
            }
        }
    }
}