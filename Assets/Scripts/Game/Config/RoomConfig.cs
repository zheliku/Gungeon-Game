// ------------------------------------------------------------
// @file       RoomConfig.cs
// @brief
// @author     zheliku
// @Modified   2025-02-23 00:02:46
// @Copyright  Copyright (c) 2025, zheliku
// ------------------------------------------------------------

namespace Game
{
    using System.Collections.Generic;
    using Framework.Toolkits.GridKit;

    public class RoomConfig
    {
        public RoomType RoomType { get; }

        public RoomConfig(RoomType roomType)
        {
            RoomType = roomType;
        }

        public EasyGrid<char> Grid;

        public RoomConfig Set(List<string> code)
        {
            var row = code.Count;
            var column = code[0].Length;
            
            Grid = new EasyGrid<char>(row, column);
            
            for (int i = 0; i < row; i++)
            {
                for (int j = 0; j < column; j++)
                {
                    Grid[i, j] = code[i][j];
                }
            }
            
            return this;
        }
    }

    public class RoomNode
    {
        public RoomType RoomType { get; } = RoomType.Init;
        
        public List<RoomNode> Children { get; } = new List<RoomNode>();

        public RoomNode(RoomType type)
        {
            RoomType = type;
        }

        public RoomNode Next(RoomType type)
        {
            var child = new RoomNode(type);
            Children.Add(child);
            return child;
        }
    }
}