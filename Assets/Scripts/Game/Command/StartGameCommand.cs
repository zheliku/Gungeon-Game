// ------------------------------------------------------------
// @file       StartGameCommand.cs
// @brief
// @author     zheliku
// @Modified   2025-01-31 14:01:05
// @Copyright  Copyright (c) 2025, zheliku
// ------------------------------------------------------------

using Framework.Toolkits.FluentAPI;

namespace Game
{
    using Framework.Core;
    using Framework.Core.Command;
    using Framework.Toolkits.ResKit;
    using UnityEngine;

    public class StartGameCommand : AbstractCommand
    {
        protected override void OnExecute()
        {
            this.GetModel<PlayerModel>().Reset();
            this.GetModel<LevelModel>().Reset();
            this.GetSystem<GunSystem>().Reset();

            // this.GetSystem<ShopSystem>().Reset();

            ResKit.LoadSceneAsync("Game", () =>
            {
                Time.timeScale = 1;
                EnemyFactory.Instance.Bosses.Shuffle(); // Shuffle the bosses for a new game
            });
        }
    }
}