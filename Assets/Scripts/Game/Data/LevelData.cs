// ------------------------------------------------------------
// @file       LevelData.cs
// @brief
// @author     zheliku
// @Modified   2025-04-12 01:04:48
// @Copyright  Copyright (c) 2025, zheliku
// ------------------------------------------------------------

namespace Game
{
    using System.Collections.Generic;
    using Framework.Toolkits.TreeKit;

    public class LevelData
    {
        public int LevelId;
        
        public Tree<RoomType> RoomTree = new Tree<RoomType>(RoomType.Init);
        
        // 轻松、中等、稍难
        public List<int> Pacing = new List<int>(); // 每关的节奏进度
    }
}