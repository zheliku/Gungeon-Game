// ------------------------------------------------------------
// @file       Coin.cs
// @brief
// @author     zheliku
// @Modified   2025-03-14 11:52:52
// @Copyright  Copyright (c) 2025, zheliku
// ------------------------------------------------------------

namespace Game
{
    using Framework.Core;
    using Framework.Toolkits.AudioKit;
    using Framework.Toolkits.FluentAPI;
    using UnityEngine;

    public class Coin : AbstractView
    {
        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag("Player"))
            {
                AudioKit.PlaySound(Config.Sound.COIN);
                this.GetModel<PlayerModel>().Coin.Value += 1;
                this.DestroyGameObject();
            }
        }

        protected override IArchitecture _Architecture { get => Game.Interface; }
    }
}