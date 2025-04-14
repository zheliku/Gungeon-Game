// ------------------------------------------------------------
// @file       RoomConfig.cs
// @brief
// @author     zheliku
// @Modified   2025-04-14 14:04:30
// @Copyright  Copyright (c) 2025, zheliku
// ------------------------------------------------------------

namespace Game
{
    using System.Collections.Generic;

    public class RoomConfig
    {
        /// <summary>
        /// 1：地块
        /// @：主角
        /// e：敌人
        /// d: 门
        /// </summary>
        public static readonly RoomGrid INIT_ROOM = new RoomGrid().Set(new List<string>()
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
        /// c：宝箱
        /// </summary>
        public static readonly RoomGrid CHEST_ROOM = new RoomGrid().Set(new List<string>()
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
        public static readonly RoomGrid SHOP_ROOM = new RoomGrid().Set(new List<string>()
        {
            "111111111d111111111",
            "1                 1",
            "1                 1",
            "1                 1",
            "1                 1",
            "1                 1",
            "1      s   s      1",
            "1                 1",
            "1                 1",
            "d    s   s   s    d",
            "1                 1",
            "1                 1",
            "1      s   s      1",
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
        public static readonly RoomGrid FINAL_ROOM = new RoomGrid().Set(new List<string>()
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