// ------------------------------------------------------------
// @file       Final.cs
// @brief
// @author     zheliku
// @Modified   2025-01-30 21:01:25
// @Copyright  Copyright (c) 2025, zheliku
// ------------------------------------------------------------

namespace Game
{
    using Framework.Core;
    using Framework.Toolkits.ResKit;
    using Framework.Toolkits.UIKit;
    using UnityEngine;

    public class Final : AbstractView
    {
        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag("Player"))
            {
                if (this.GetModel<LevelModel>().NextLevel())
                {
                    ResKit.LoadSceneAsync(AssetConfig.Scene.GAME);
                }
                else
                {
                    UIKit.ShowPanel<GamePass>();
                }
            }
        }

        protected override IArchitecture _Architecture { get => Game.Architecture; }
    }
}