// ------------------------------------------------------------
// @file       10.UnityEngineOthersExtension.cs
// @brief
// @author     zheliku
// @Modified   2024-12-17 21:12:22
// @Copyright  Copyright (c) 2024, zheliku
// ------------------------------------------------------------

namespace Framework.Toolkits.FluentAPI
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Core;
    using UnityEngine;
    using Random = UnityEngine.Random;

    public static class UnityEngineRandomExtension
    {
        public static T RandomTakeOne<T>(this IList<T> list)
        {
            return list[Random.Range(0, list.Count)];
        }

        public static T RandomTakeOne<T>(this IEnumerable<T> enumerable)
        {
            return enumerable.ToList().RandomTakeOne();
        }

        public static T RandomTakeOneAndRemove<T>(this IList<T> list)
        {
            var randomIndex = Random.Range(0, list.Count);
            var randomItem  = list[randomIndex];
            list.RemoveAt(randomIndex);
            return randomItem;
        }

        public static IList<T> RandomTakeRange<T>(this IList<T> list, int count)
        {
            // 如果要求的数量超过了总元素数量，则返回全部元素
            count = Math.Min(count, list.Count);

            var newList = new List<T>(list);

            // 随机移除元素，直到剩余元素数量等于 count
            for (int i = 0; i < list.Count - count; i++)
            {
                newList.RandomTakeOneAndRemove();
            }

            return newList;
        }

        public static IEnumerable<T> RandomTakeRange<T>(this IEnumerable<T> enumerable, int count)
        {
            var list = enumerable.ToList(); // 将源转换为列表

            // 如果要求的数量超过了总元素数量，则返回全部元素
            count = Math.Min(count, list.Count);

            for (int i = 0; i < count; i++)
            {
                yield return list.RandomTakeOneAndRemove();
            }
        }

        public static IList<T> RandomTakeRangeAndRemove<T>(this IList<T> list, int count)
        {
            // 如果要求的数量超过了总元素数量，则返回全部元素
            count = Math.Min(count, list.Count);

            var newList = new List<T>(count);

            for (int i = 0; i < count; i++)
            {
                newList.Add(list.RandomTakeOneAndRemove());
            }

            return newList;
        }
        
        /// <summary>
        /// 从区间 [vec2.x, vec2.y] 随机选择一个数
        /// </summary>
        public static float RandomSelect(this Vector2 vec2)
        {
            return Random.Range(vec2.x, vec2.y);
        }

        /// <summary>
        /// 从区间 [0, x] 随机选择一个数
        /// </summary>
        public static float RandomTo0(this float x)
        {
            return Random.Range(0, x);
        }

        /// <summary>
        /// 从区间 [x, y] 随机选择一个数
        /// </summary>
        public static float RandomToY(this float x, float y)
        {
            return Random.Range(x, y);
        }
        
        /// <summary>
        /// 从区间 [-x, x] 随机选择一个数
        /// </summary>
        public static float RandomToNeg(this float x)
        {
            return Random.Range(-x, x);
        }

        /// <summary>
        /// 从区间 [0, 1] 随机选择一个数
        /// </summary>
        public static bool Random0To1(this float value)
        {
            return Random.Range(0f, 1f) <= value;
        }
    }

    public class RandomUtility
    {
        public static float Range(params (float a, float b)[] ranges)
        {
            var list = ranges.Where(r => r.a <= r.b).ToList();

            if (!list.Any())
            {
                throw new FrameworkException("RandomUtility.Range: ranges is empty");
            }

            var item = list.RandomTakeOne();
            return Random.Range(item.a, item.b);
        }

        public static Vector2 RangeRadius(float r)
        {
            return new Vector2(r.RandomToNeg(), r.RandomToNeg());
        }
    }
}