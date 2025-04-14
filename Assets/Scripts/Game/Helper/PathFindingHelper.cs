// ------------------------------------------------------------
// @file       PathFindingHelper.cs
// @brief
// @author     zheliku
// @Modified   2025-04-14 20:04:08
// @Copyright  Copyright (c) 2025, zheliku
// ------------------------------------------------------------

namespace Game
{
    using System.Collections.Generic;
    using System.Linq;
    using Framework.Core;
    using Framework.Toolkits.FluentAPI;
    using Framework.Toolkits.GridKit;
    using Sirenix.OdinInspector;
    using UnityEngine;
    using UnityEngine.Pool;

    public partial class PathFindingHelper
    {
        public static void FindPath<T>(NodeBase<T> start, NodeBase<T> end, List<NodeBase<T>> path)
        {
            var toSearch = ListPool<NodeBase<T>>.Get();
            toSearch.Add(start);
            var processed = ListPool<NodeBase<T>>.Get();

            while (toSearch.Count > 0)
            {
                var current = toSearch[0];
                foreach (var t in toSearch)
                {
                    if (t.F < current.F || t.F.Approximately(current.F) && t.H < current.H)
                    {
                        current = t;
                    }
                }
                
                processed.Add(current);
                toSearch.Remove(current);

                if (current == end)
                {
                    var currentPathTile = end;
                    var count           = 100;
                    while (currentPathTile != start)
                    {
                        path.Add(currentPathTile);
                        currentPathTile = currentPathTile.Connection;
                        count--;
                        if (count < 0)
                        {
                            throw new FrameworkException("Pathfinding failed");
                        }
                    }

                    ListPool<NodeBase<T>>.Release(toSearch);
                    ListPool<NodeBase<T>>.Release(processed);
                    return;
                }

                foreach (var neighbor in current.Neighbors.Where(t => t.Walkable && !processed.Contains(t)))
                {
                    var inSearch = toSearch.Contains(neighbor);

                    var costToNeighbor = current.G + current.GetDistance(neighbor);

                    if (!inSearch || costToNeighbor < neighbor.G)
                    {
                        neighbor.G          = costToNeighbor;
                        neighbor.Connection = current;

                        if (!inSearch)
                        {
                            neighbor.H = neighbor.GetDistance(end);
                            toSearch.Add(neighbor);
                        }
                    }
                }
            }
            
            ListPool<NodeBase<T>>.Release(toSearch);
            ListPool<NodeBase<T>>.Release(processed);
        }
    }

    public partial class PathFindingHelper
    {
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        [ShowInInspector]
        public interface ICoords<T>
        {
            float GetDistance(ICoords<T> other);

            [ShowInInspector]
            T Pos { get; set; }
        }

        [ShowInInspector]
        public class TileCoords : ICoords<Vector3Int>
        {
            public float GetDistance(ICoords<Vector3Int> other)
            {
                var distance = new Vector3Int((Pos.x - other.Pos.x).Abs(), (Pos.y - other.Pos.y).Abs(), 0);

                var lowest  = distance.x.MinWith(distance.y);
                var highest = distance.x.MaxWith(distance.y);

                var horizontalMovesRequired = highest - lowest;

                return lowest * 14 + horizontalMovesRequired * 10;
            }

            [ShowInInspector]
            public Vector3Int Pos { get; set; }
        }

        [ShowInInspector]
        public abstract class NodeBase<T>
        {
            [ShowInInspector]
            public ICoords<T> Coords { get; set; }

            [ShowInInspector]
            public bool Walkable { get; set; }

            [ShowInInspector]
            public List<NodeBase<T>> Neighbors { get; protected set; }

            public NodeBase<T> Connection { get; set; }

            public float G { get; set; }

            public float H { get; set; }

            public float F
            {
                get => G + H;
            }

            public float GetDistance(NodeBase<T> other)
            {
                return Coords.GetDistance(other.Coords);
            }

            public virtual NodeBase<T> Init(bool walkable, ICoords<T> coords)
            {
                Walkable = walkable;
                Coords   = coords;
                return this;
            }

            public abstract void CacheNeighbors();
        }

        [ShowInInspector]
        public class TileNode : NodeBase<Vector3Int>
        {
            public static readonly List<Direction> DIRECTIONS = new List<Direction>()
            {
                Direction.Up,
                Direction.Down,
                Direction.Left,
                Direction.Right
            };

            private readonly DynamicGrid<TileNode> _grid;

            public TileNode(DynamicGrid<TileNode> grid)
            {
                _grid = grid;
            }

            public override void CacheNeighbors()
            {
                Neighbors = new List<NodeBase<Vector3Int>>();

                foreach (var direction in DIRECTIONS)
                {
                    var pos = Coords.Pos + direction.ToVector3Int();
                    if (_grid[pos.x, pos.y] != null)
                    {
                        Neighbors.Add(_grid[pos.x, pos.y]);
                    }
                }
            }
        }
    }
}