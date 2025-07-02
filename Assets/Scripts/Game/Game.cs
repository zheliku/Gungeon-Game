// ------------------------------------------------------------
// @file       Game.cs
// @brief
// @author     zheliku
// @Modified   2025-01-29 16:01:37
// @Copyright  Copyright (c) 2025, zheliku
// ------------------------------------------------------------

using Unity.VisualScripting;

namespace Game
{
    using Framework.Core;
    using UnityEngine;

    public class Game : AbstractArchitecture<Game>
    {
        protected override void Init()
        {
            RegisterModel(new PlayerModel());
            RegisterModel(new EnemyModel());
            RegisterModel(new LevelModel());
            
            RegisterSystem(new GunSystem());
            RegisterSystem(new ShopSystem());
        }

        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
        private static void AutoInit()
        {
            // Player.Instance.SendCommand<StartGameCommand>();
            // this.GetModel<LevelModel>().PacingQueue = new Queue<int>(Level1.CONFIG.Pacing);
        }
    }
}