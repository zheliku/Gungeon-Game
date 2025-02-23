// ------------------------------------------------------------
// @file       3.CollectionsExtension.cs
// @brief
// @author     zheliku
// @Modified   2024-10-18 00:10:23
// @Copyright  Copyright (c) 2024, zheliku
// ------------------------------------------------------------

namespace Framework.Toolkits.FluentAPI
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    /// 针对 System.Collections 提供的链式扩展，理论上任何集合都可以使用
    /// </summary>
    public static class SystemCollectionsExtension
    {
        /// <summary>
        /// 判断数组是否为空
        /// </summary>
        /// <example> <code>
        /// <![CDATA[
        /// int[] testArray = null;
        /// Debug.Log(testArray.IsNullOrEmpty()); // true
        /// testArray = new int[0];
        /// Debug.Log(testArray.IsNullOrEmpty()); // true
        /// testArray = new int[] { 1, 2, 3 };
        /// Debug.Log(testArray.IsNullOrEmpty()); // false
        /// ]]>
        /// </code> </example>        
        public static bool IsNullOrEmpty<T>(this T[] collection)
        {
            return collection == null || collection.Length == 0;
        }

        /// <summary>
        /// 判断数组是否不为空
        /// </summary>
        public static bool IsNotNullAndNotEmpty<T>(this T[] collection)
        {
            return !IsNullOrEmpty(collection);
        }

        /// <summary>
        /// 判断 IList 是否为空
        /// </summary>
        /// <example> <code>
        /// <![CDATA[
        /// List<int> testList = null;
        /// Debug.Log(testList.IsNullOrEmpty()); // true
        /// testList = new List<int>();
        /// Debug.Log(testList.IsNullOrEmpty()); // true
        /// testList = new List<int>() { 1, 2, 3 };
        /// Debug.Log(testList.IsNullOrEmpty()); // false
        /// ]]>
        /// </code> </example>   
        public static bool IsNullOrEmpty<T>(this IList<T> collection)
        {
            return collection == null || collection.Count == 0;
        }

        /// <summary>
        /// 判断 IList 是否不为空
        /// </summary>
        public static bool IsNotNullAndNotEmpty<T>(this IList<T> collection)
        {
            return !IsNullOrEmpty(collection);
        }

        /// <summary>
        /// 判断 IEnumerable 是否为空
        /// </summary>
        public static bool IsNullOrEmpty<T>(this IEnumerable<T> collection)
        {
            return collection == null || !collection.Any();
        }

        /// <summary>
        /// 判断 IEnumerable 是否不为空
        /// </summary>
        public static bool IsNotNullAndNotEmpty<T>(this IEnumerable<T> collection)
        {
            return !IsNullOrEmpty(collection);
        }

        /// <summary>
        /// 遍历 IEnumerable
        /// </summary>
        /// <example> <code>
        /// <![CDATA[
        /// IEnumerable<int> testIEnumerable = new List<int> { 1, 2, 3 };
        /// testIEnumerable.ForEach(number => Debug.Log(number));
        /// // output:
        /// // 1
        /// // 2
        /// // 3
        /// ]]> </code> </example>
        /// <example> <code> <![CDATA[
        /// new Dictionary<string, string>()
        /// {
        ///     {"name","DaMing"},
        ///     {"company","DaMingGame" }
        /// }.ForEach(keyValue => Debug.LogFormat(""key: {0}, value: {1}"", keyValue.Key, keyValue.Value));
        /// // key: name, value: DaMing
        /// // key: company, value: DaMingGame")]
        /// ]]>
        /// </code> </example>
        public static IEnumerable<T> ForEach<T>(this IEnumerable<T> self, Action<T> action)
        {
            var forEach = self.ToList();
            foreach (var item in forEach)
            {
                action(item);
            }

            return forEach;
        }

        /// <summary>
        /// 遍历 List
        /// </summary>
        /// <example> <code>
        /// <![CDATA[
        /// List<int> testList = new List<int> { 1, 2, 3 };
        /// testList.ForEach(number => Debug.Log(number));
        /// // output
        /// // 1
        /// // 2
        /// // 3
        /// ]]>
        /// </code> </example>
        public static IEnumerable<T> ForEach<T>(this List<T> self, Action<T> action)
        {
            foreach (var item in self)
            {
                action(item);
            }

            return self;
        }

        /// <summary>
        /// 遍历 List
        /// </summary>
        /// <example> <code>
        /// <![CDATA[
        /// List<int> testList = new List<int> { 1, 2, 3 };
        /// testList.ForEach(number => Debug.Log(number));
        /// // output:
        /// // 3
        /// // 2
        /// // 1
        /// ]]>
        /// </code> </example>
        public static IEnumerable<T> ForEachReverse<T>(this List<T> self, Action<T> action)
        {
            for (var i = self.Count - 1; i >= 0; --i)
                action(self[i]);

            return self;
        }

        /// <summary>
        /// 遍历 Dictionary
        /// </summary>
        /// <example> <code>
        /// <![CDATA[
        /// new Dictionary<string, string>()
        /// {
        ///     {"name","DaMing"},
        ///     {"company","DaMingGame" }
        /// }.ForEach(keyValue => Debug.LogFormat(""key: {0}, value: {1}"", keyValue.Key, keyValue.Value));
        /// // key: name, value: DaMing
        /// // key: company, value: DaMingGame")]
        /// ]]>
        /// </code> </example>
        public static void ForEach<TKey, TValue>(this Dictionary<TKey, TValue> dict, Action<TKey, TValue> action)
        {
            var dictE = dict.GetEnumerator();

            while (dictE.MoveNext())
            {
                var current = dictE.Current;
                action(current.Key, current.Value);
            }

            dictE.Dispose();
        }

        /// <summary>
        /// 合并字典（不支持重复的 key）
        /// </summary>
        /// <example> <code>
        /// <![CDATA[
        /// var dictionary1 = new Dictionary<string, string> { { "1", "2" } };
        /// var dictionary2 = new Dictionary<string, string> { { "3", "4" } };
        /// var dictionary3 = dictionary1.Merge(dictionary2);
        /// dictionary3.ForEach(pair => Debug.LogFormat(""{0}: {1}"", pair.Key, pair.Value));
        /// // 1: 2
        /// // 3: 4
        /// ]]>
        /// </code> </example>
        public static Dictionary<TKey, TValue> Merge<TKey, TValue>(this   Dictionary<TKey, TValue>   dictionary,
                                                                   params Dictionary<TKey, TValue>[] dictionaries)
        {
            return dictionaries.Aggregate(dictionary,
                                          (current, dict) =>
                                              Enumerable.Union(current, dict).ToDictionary(kv => kv.Key, kv => kv.Value));
        }

        /// <summary>
        /// 添加新的字典（支持重复的 key）
        /// </summary>
        /// <example> <code>
        /// <![CDATA[
        /// var dictionary1 = new Dictionary<string, string> { { "1", "2" } };
        /// var dictionary2 = new Dictionary<string, string> { { "1", "4" } };
        /// var dictionary3 = dictionary1.AddRange(dictionary2, true);
        /// dictionary3.ForEach(pair => Debug.LogFormat(""{0}: {1}"", pair.Key, pair.Value));
        /// // 1: 4
        /// ]]>
        /// </code> </example>
        public static void AddRange<TKey, TValue>(this Dictionary<TKey, TValue> dict, Dictionary<TKey, TValue> addInDict, bool isOverride = false)
        {
            var enumerator = addInDict.GetEnumerator();

            while (enumerator.MoveNext())
            {
                var current = enumerator.Current;
                if (dict.ContainsKey(current.Key))
                {
                    if (isOverride)
                        dict[current.Key] = current.Value;
                    continue;
                }

                dict.Add(current.Key, current.Value);
            }

            enumerator.Dispose();
        }

        /// <summary>
        /// 生成从 (from.x, from.y) 到 (to.x, to.y) 的坐标（[from, to]）
        /// </summary>
        /// <param name="from">起始坐标</param>
        /// <param name="to">终止坐标</param>
        /// <returns>每个坐标值，依次 +1</returns>
        public static IEnumerable<(int i, int j)> StepTo(this (int x, int y) from, (int x, int y) to)
        {
            for (int i = from.x; i <= to.x; i++)
            {
                for (int j = from.y; j <= to.y; j++)
                {
                    yield return (i, j);
                }
            }
        }

        /// <summary>
        /// 生成从 (from.x, from.y) 到 (toX, toY) 的坐标（[from, to]）
        /// </summary>
        /// <param name="from">起始坐标</param>
        /// <param name="toX">终止 X 坐标</param>
        /// <param name="toY">终止 Y 坐标</param>
        /// <returns>每个坐标值，依次 +1</returns>
        public static IEnumerable<(int i, int j)> StepTo(this (int x, int y) from, int toX, int toY)
        {
            for (int i = from.x; i <= toX; i++)
            {
                for (int j = from.y; j <= toY; j++)
                {
                    yield return (i, j);
                }
            }
        }

        /// <summary>
        /// 生成从 from 到 to 的步长为 1 的序列（[from, to]）
        /// </summary>
        /// <param name="from">起始数</param>
        /// <param name="to">中止数</param>
        /// <returns>序列</returns>
        public static IEnumerable<int> StepTo(this int from, int to)
        {
            for (int i = from; i <= to; i++)
            {
                yield return i;
            }
        }
    }
}