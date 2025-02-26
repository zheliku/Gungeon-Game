// ------------------------------------------------------------
// @file       LevelGenHelper.cs
// @brief
// @author     zheliku
// @Modified   2025-02-27 03:02:19
// @Copyright  Copyright (c) 2025, zheliku
// ------------------------------------------------------------

namespace Game
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using UnityEngine;

    public class LevelGenHelper
    {
        /// <summary>
        /// 获取 index 周围的可连接方向
        /// </summary>
        /// <param name="index"></param>
        /// <param name="existIndex"></param>
        /// <returns></returns>
        public static List<Direction> GetAvailableDirections(Vector2Int index, HashSet<Vector2Int> existIndex)
        {
            return Enum.GetValues(typeof(Direction)).Cast<Direction>()
               .Where(dir => !existIndex.Contains(index + dir.ToVector2Int())).ToList();
        }
        
        
        /// <summary>
        /// 获取 index 4 个方向的可连接方向
        /// </summary>
        /// <param name="index">位置</param>
        /// <param name="existIndex">已生成网格</param>
        /// <returns></returns>
        public static Dictionary<Direction, int> Predict(Vector2Int index, HashSet<Vector2Int> existIndex)
        {
            var availableDirection = GetAvailableDirections(index, existIndex);
            var result = new Dictionary<Direction, int>();
            foreach (var dir in availableDirection)
            {
                result.Add(dir, GetAvailableDirections(index + dir.ToVector2Int(), existIndex).Count);
            }
            return result;
        }
    }
}