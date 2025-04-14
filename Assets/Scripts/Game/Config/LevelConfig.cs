// ------------------------------------------------------------
// @file       LevelConfig.cs
// @brief
// @author     zheliku
// @Modified   2025-04-11 15:04:36
// @Copyright  Copyright (c) 2025, zheliku
// ------------------------------------------------------------

namespace Game
{
    using System.Collections.Generic;
    using UnityEngine;

    public partial class LevelConfig
    {
        public static readonly Vector2Int ROOM_INTERVAL = new Vector2Int(2, 2); // 每个房间目前间距为 2

        public static readonly List<LevelData> LEVELS = new List<LevelData>()
        {
            Level1.DATA,
            Level2.DATA,
            Level3.DATA,
            Level4.DATA,
        };
    }
}