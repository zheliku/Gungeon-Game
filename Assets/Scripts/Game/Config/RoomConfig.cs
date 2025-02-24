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
    using UnityEngine;

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
            var row    = code.Count;
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

    public class RoomGenerateNode
    {
        public RoomNode RoomNode;

        public Vector2Int Index;

        public HashSet<Direction> Directions = new HashSet<Direction>();
    }

    public enum Direction
    {
        Up    = 0,
        Left  = 1,
        Right = 2,
        Down  = 3,
    }

    public static class DirectionExtensions
    {
        public static Vector2Int ToVector2Int(this Direction direction)
        {
            switch (direction)
            {
                case Direction.Up:
                    return new Vector2Int(0, 1);
                case Direction.Down:
                    return new Vector2Int(0, -1);
                case Direction.Left:
                    return new Vector2Int(-1, 0);
                case Direction.Right:
                    return new Vector2Int(1, 0);
                default:
                    throw new System.ArgumentOutOfRangeException(nameof(direction), direction, null);
            }
        }

        /// <summary>
        /// 相反朝向
        /// </summary>
        public static Direction Opposite(this Direction direction)
        {
            return 3 - direction;
        }

        public static Direction ToDirection(this Vector2Int vector)
        {
            if (vector == new Vector2Int(0, 1))
                return Direction.Up;
            if (vector == new Vector2Int(0, -1))
                return Direction.Down;
            if (vector == new Vector2Int(-1, 0))
                return Direction.Left;
            if (vector == new Vector2Int(1, 0))
                return Direction.Right;

            throw new System.ArgumentException("Vector2Int is not a valid direction vector", nameof(vector));
        }
    }
}