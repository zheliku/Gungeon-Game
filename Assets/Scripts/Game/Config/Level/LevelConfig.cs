// ------------------------------------------------------------
// @file       LevelConfig.cs
// @brief
// @author     zheliku
// @Modified   2025-04-11 15:04:36
// @Copyright  Copyright (c) 2025, zheliku
// ------------------------------------------------------------

namespace Game
{
    using Framework.Toolkits.TreeKit;
    using UnityEngine;

    public partial class LevelConfig
    {
        public static readonly Vector2Int ROOM_INTERVAL = new Vector2Int(2, 2); // 每个房间目前间距为 2
        public static readonly Vector2Int ROOM_SIZE;

        static LevelConfig()
        {
            ROOM_SIZE = new Vector2Int(INIT_ROOM.Row, INIT_ROOM.Column);
        }

        public Tree<RoomType> RoomTree = new Tree<RoomType>(RoomType.Init);
    }
}