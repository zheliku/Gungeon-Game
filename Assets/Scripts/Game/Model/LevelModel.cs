// ------------------------------------------------------------
// @file       LevelModel.cs
// @brief
// @author     zheliku
// @Modified   2025-01-30 20:01:32
// @Copyright  Copyright (c) 2025, zheliku
// ------------------------------------------------------------

namespace Game
{
    using System.Collections.Generic;
    using Framework.Core.Model;

    public class LevelModel : AbstractModel
    {
        /// <summary>
        /// 1：地块
        /// @：主角
        /// e：敌人
        /// d: 门
        /// </summary>
        public RoomConfig InitRoom = new RoomConfig(RoomType.Init).Set(new List<string>()
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
        public List<RoomConfig> NormalRoom = new List<RoomConfig>()
        {
            new RoomConfig(RoomType.Normal).Set(new List<string>()
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
            new RoomConfig(RoomType.Normal).Set(new List<string>()
            {
                "111111111d111111111",
                "1                 1",
                "1                 1",
                "1                 1",
                "1                 1",
                "1                 1",
                "1    e       e    1",
                "1      e 1 e      1",
                "1      1 e 1      1",
                "d      e   e      d",
                "1      1 e 1      1",
                "1      e 1 e      1",
                "1    e       e    1",
                "1                 1",
                "1                 1",
                "1                 1",
                "1                 1",
                "1                 1",
                "111111111d111111111",
            }),
            new RoomConfig(RoomType.Normal).Set(new List<string>()
            {
                "111111111d111111111",
                "1                 1",
                "1  11         11  1",
                "1  1e         e1  1",
                "1                 1",
                "1                 1",
                "1                 1",
                "1       e e       1",
                "1        e        1",
                "d       e1e       d",
                "1        e        1",
                "1       e e       1",
                "1                 1",
                "1                 1",
                "1                 1",
                "1  1e         e1  1",
                "1  11         11  1",
                "1                 1",
                "111111111d111111111",
            }),
        };
        
        /// <summary>
        /// 1：地块
        /// c：宝箱
        /// </summary>
        public RoomConfig ChestRoom = new RoomConfig(RoomType.Chest).Set(new List<string>()
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
        /// #：终点
        /// </summary>
        public RoomConfig FinalRoom = new RoomConfig(RoomType.Final).Set(new List<string>()
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

        protected override void OnInit()
        { }
    }
}