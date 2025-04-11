// ------------------------------------------------------------
// @file       Armor1.cs
// @brief
// @author     zheliku
// @Modified   2025-04-11 11:46:57
// @Copyright  Copyright (c) 2025, zheliku
// ------------------------------------------------------------

namespace Game
{
    using Framework.Core;
    using Framework.Toolkits.AudioKit;
    using Framework.Toolkits.FluentAPI;
    using UnityEngine;

    public class Armor1 : PowerUp
    {
        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag("Player"))
            {
                this.GetModel<LevelModel>().CurrentRoom.PowerUps.Remove(this); // 从房间中移除
                
                AudioKit.PlaySound(AssetConfig.Sound.ARMOR1);
                this.GetModel<PlayerModel>().Property.Armor.Value += 1;
                this.DestroyGameObject();
            }
        }
    }
}