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
        // 在给定的起始节点和结束节点之间找到路径
        public static List<NodeBase<T>> FindPath<T>(NodeBase<T> start, NodeBase<T> end)
        {
            List<NodeBase<T>> path = new List<NodeBase<T>>();
            
            // 获取待搜索的节点列表
            var toSearch = ListPool<NodeBase<T>>.Get();
            toSearch.Add(start);
            // 获取已处理的节点列表
            var processed = ListPool<NodeBase<T>>.Get();

            // 当待搜索的节点列表不为空时，继续搜索
            while (toSearch.Count > 0)
            {
                // 获取当前节点
                var current = toSearch[0];
                // 遍历待搜索的节点列表，找到F值最小的节点
                foreach (var t in toSearch)
                {
                    if (t.F < current.F || t.F.Approximately(current.F) && t.H < current.H)
                    {
                        current = t;
                    }
                }
                
                // 将当前节点添加到已处理的节点列表中
                processed.Add(current);
                // 从待搜索的节点列表中移除当前节点
                toSearch.Remove(current);

                // 如果当前节点是结束节点，则找到了路径
                if (current == end)
                {
                    // 从结束节点开始，逆向遍历路径，将路径添加到path列表中
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

                    // 释放待搜索的节点列表和已处理的节点列表
                    // ListPool<NodeBase<T>>.Release(toSearch);
                    // ListPool<NodeBase<T>>.Release(processed);
                    break;
                }

                // 遍历当前节点的邻居节点
                foreach (var neighbor in current.Neighbors.Where(t => t.Walkable && !processed.Contains(t)))
                {
                    // 判断邻居节点是否在待搜索的节点列表中
                    var inSearch = toSearch.Contains(neighbor);

                    // 计算从当前节点到邻居节点的代价
                    var costToNeighbor = current.G + current.GetDistance(neighbor);

                    // 如果邻居节点不在待搜索的节点列表中，或者从当前节点到邻居节点的代价小于邻居节点的代价，则更新邻居节点的代价和连接节点
                    if (!inSearch || costToNeighbor < neighbor.G)
                    {
                        neighbor.G          = costToNeighbor;
                        neighbor.Connection = current;

                        // 如果邻居节点不在待搜索的节点列表中，则将邻居节点添加到待搜索的节点列表中
                        if (!inSearch)
                        {
                            neighbor.H = neighbor.GetDistance(end);
                            toSearch.Add(neighbor);
                        }
                    }
                }
            }
            
            // 释放待搜索的节点列表和已处理的节点列表
            ListPool<NodeBase<T>>.Release(toSearch);
            ListPool<NodeBase<T>>.Release(processed);
            
            return path;
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
            // 节点坐标
            [ShowInInspector]
            public ICoords<T> Coords { get; set; }

            // 节点是否可通行
            [ShowInInspector]
            public bool Walkable { get; set; }

            // 节点的邻居节点
            [ShowInInspector]
            public List<NodeBase<T>> Neighbors { get; protected set; }

            // 节点的连接节点
            public NodeBase<T> Connection { get; set; }

            // G值，表示从起点到当前节点的实际代价
            public float G { get; set; }

            // H值，表示从当前节点到终点的估计代价
            public float H { get; set; }

            // F值，表示从起点到终点的总代价
            public float F
            {
                get => G + H;
            }

            // 计算当前节点与目标节点之间的距离
            public float GetDistance(NodeBase<T> other)
            {
                return Coords.GetDistance(other.Coords);
            }

            // 初始化节点
            public virtual NodeBase<T> Init(bool walkable, ICoords<T> coords)
            {
                Walkable = walkable;
                Coords   = coords;
                return this;
            }

            // 缓存邻居节点
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