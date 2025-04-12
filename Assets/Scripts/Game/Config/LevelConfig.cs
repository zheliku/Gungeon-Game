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
        public static readonly Vector2Int ROOM_SIZE;

        public static readonly List<LevelData> LEVELS = new List<LevelData>()
        {
            Level1.DATA,
            Level2.DATA,
            Level3.DATA,
            Level4.DATA,
        };

        static LevelConfig()
        {
            ROOM_SIZE = new Vector2Int(INIT_ROOM.Row, INIT_ROOM.Column);
        }

        /// <summary>
        /// 1：地块
        /// @：主角
        /// e：敌人
        /// d: 门
        /// </summary>
        public static readonly RoomGrid INIT_ROOM = new RoomGrid(RoomType.Init).Set(new List<string>()
        {
            "111111111d111111111",
            "1                 1",
            "1 @               1",
            "1                 1",
            "1                 1",
            "1                 1",
            "1                 1",
            "1                 1",
            "1                 1",
            "d                 d",
            "1                 1",
            "1                 1",
            "1                 1",
            "1                 1",
            "1                 1",
            "1                 1",
            "1                 1",
            "1                 1",
            "111111111d111111111",
        });


        /// <summary>
        /// 1：地块
        /// e：敌人
        /// </summary>
        public static readonly List<RoomGrid> NORMAL_ROOM = new List<RoomGrid>()
        {
            new RoomGrid(RoomType.Normal).Set(new List<string>()
            {
                "111111111d111111111",
                "1                 1",
                "1 e             e 1",
                "1                 1",
                "1                 1",
                "1                 1",
                "1                 1",
                "1                 1",
                "1        e        1",
                "d      e 1 e      d",
                "1        e        1",
                "1                 1",
                "1                 1",
                "1                 1",
                "1                 1",
                "1                 1",
                "1 e             e 1",
                "1                 1",
                "111111111d111111111",
            }),
            new RoomGrid(RoomType.Normal).Set(new List<string>()
            {
                "111111111d111111111",
                "1                 1",
                "1                 1",
                "1    e       e    1",
                "1                 1",
                "1      e 1 e      1",
                "1                 1",
                "1     1  e  1     1",
                "1                 1",
                "d      e   e      d",
                "1                 1",
                "1     1  e  1     1",
                "1                 1",
                "1      e 1 e      1",
                "1                 1",
                "1    e       e    1",
                "1                 1",
                "1                 1",
                "111111111d111111111",
            }),
            new RoomGrid(RoomType.Normal).Set(new List<string>()
            {
                "111111111d111111111",
                "1                 1",
                "1                 1",
                "1  11         11  1",
                "1  1e         e1  1",
                "1                 1",
                "1                 1",
                "1       e e       1",
                "1        e        1",
                "d       e1e       d",
                "1        e        1",
                "1       e e       1",
                "1                 1",
                "1                 1",
                "1  1e         e1  1",
                "1  11         11  1",
                "1                 1",
                "1                 1",
                "111111111d111111111",
            }),
        };

        /// <summary>
        /// 1：地块
        /// c：宝箱
        /// </summary>
        public static readonly RoomGrid CHEST_ROOM = new RoomGrid(RoomType.Chest).Set(new List<string>()
        {
            "111111111d111111111",
            "1                 1",
            "1                 1",
            "1                 1",
            "1                 1",
            "1                 1",
            "1                 1",
            "1                 1",
            "1                 1",
            "d        c        d",
            "1                 1",
            "1                 1",
            "1                 1",
            "1                 1",
            "1                 1",
            "1                 1",
            "1                 1",
            "1                 1",
            "111111111d111111111",
        });

        /// <summary>
        /// 1：地块
        /// s：商店摊位
        /// </summary>
        public static readonly RoomGrid SHOP_ROOM = new RoomGrid(RoomType.Shop).Set(new List<string>()
        {
            "111111111d111111111",
            "1                 1",
            "1                 1",
            "1                 1",
            "1                 1",
            "1                 1",
            "1                 1",
            "1                 1",
            "1                 1",
            "d        s        d",
            "1                 1",
            "1                 1",
            "1                 1",
            "1                 1",
            "1                 1",
            "1                 1",
            "1                 1",
            "1                 1",
            "111111111d111111111",
        });

        /// <summary>
        /// 1：地块
        /// #：终点
        /// </summary>
        public static readonly RoomGrid FINAL_ROOM = new RoomGrid(RoomType.Final).Set(new List<string>()
        {
            "111111111d111111111",
            "1                 1",
            "1                 1",
            "1                 1",
            "1                 1",
            "1                 1",
            "1                 1",
            "1                 1",
            "1                 1",
            "d            #    d",
            "1                 1",
            "1                 1",
            "1                 1",
            "1                 1",
            "1                 1",
            "1                 1",
            "1                 1",
            "1                 1",
            "111111111d111111111",
        });
    }
}