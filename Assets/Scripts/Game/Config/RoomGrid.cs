// ------------------------------------------------------------
// @file       RoomConfig.cs
// @brief
// @author     zheliku
// @Modified   2025-02-23 00:02:46
// @Copyright  Copyright (c) 2025, zheliku
// ------------------------------------------------------------

namespace Game
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Framework.Core;
    using Framework.Toolkits.FluentAPI;
    using Framework.Toolkits.GridKit;
    using Sirenix.OdinInspector;
    using UnityEngine;

    public class RoomGrid
    {
        public RoomType RoomType { get; }

        [ShowInInspector]
        public EasyGrid<char> Grid;

        public int Row { get => Grid.Row; }

        public int Column { get => Grid.Column; }

        public char this[int row, int column]
        {
            get => Grid[row, column];
        }

        public RoomGrid(RoomType roomType)
        {
            RoomType = roomType;
        }

        public RoomGrid Set(List<string> code)
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
        [ShowInInspector]
        public RoomType RoomType { get; private set; }

        [ShowInInspector]
        public Dictionary<Direction, RoomNode> ConnectNodes { get; } = new Dictionary<Direction, RoomNode>();

        [ShowInInspector]
        public Vector2Int Index = Vector2Int.zero;

        public bool FullConnect
        {
            get => ConnectNodes.Count == 4;
        }

        public RoomNode(RoomType type)
        {
            RoomType = type;
        }

        public RoomNode Connect(RoomType type)
        {
            if (FullConnect)
            {
                throw new FrameworkException("RoomNode is full connect");
            }

            var allDirections    = Enum.GetValues(typeof(Direction)).ToList<Direction>();
            var remainDirections = allDirections.Except(ConnectNodes.Keys).ToList();
            var direction        = remainDirections.RandomTakeOne();

            var child = new RoomNode(type)
            {
                ConnectNodes = { { direction.Opposite(), this } },
                Index        = Index + direction.ToVector2Int(),
            };
            ConnectNodes.Add(direction, child);
            return child;
        }
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
                    throw new ArgumentOutOfRangeException(nameof(direction), direction, null);
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

            throw new ArgumentException("Vector2Int is not a valid direction vector", nameof(vector));
        }
    }
}