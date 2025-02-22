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
}