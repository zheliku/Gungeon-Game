// ------------------------------------------------------------
// @file       Game.cs
// @brief
// @author     zheliku
// @Modified   2025-01-29 16:01:37
// @Copyright  Copyright (c) 2025, zheliku
// ------------------------------------------------------------

namespace Game
{
    using System.Collections.Generic;
    using Framework.Core;
    using UnityEngine;

    public class Game : Architecture<Game>
    {
        protected override void Init()
        {
            RegisterModel(new PlayerModel());
            RegisterModel(new EnemyModel());
            RegisterModel(new LevelModel());
        }

        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
        private static void AutoInit()
        {
            LevelConfig.PacingQueue = new Queue<int>(Level1.CONFIG.Pacing);
        }
    }
}