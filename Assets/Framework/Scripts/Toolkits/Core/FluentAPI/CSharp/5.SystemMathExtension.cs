// ------------------------------------------------------------
// @file       5.SystemMathExtension.cs
// @brief
// @author     zheliku
// @Modified   2025-02-27 01:02:26
// @Copyright  Copyright (c) 2025, zheliku
// ------------------------------------------------------------

namespace Framework.Toolkits.FluentAPI
{
    using System.Collections.Generic;

    public static class SystemMathExtension
    {
        /// <summary>
        /// 获取 1 到 n 的 k 个数的全排列
        /// </summary>
        /// <param name="self">(n, k)</param>
        /// <returns>全排列列表</returns>
        public static List<List<int>> GetPermutations(this (int n, int k) self)
        {
            var result = new List<List<int>>();
            var path   = new List<int>();
            var used   = new bool[self.n + 1]; // 标记数组，记录哪些数已经被使用

            Backtrack(self.n, self.k, used, path, result);

            return result;
        }

        private static void Backtrack(int n, int k, bool[] used, List<int> path, List<List<int>> result)
        {
            // 如果当前排列的长度等于 k，将其加入结果列表
            if (path.Count == k)
            {
                result.Add(new List<int>(path));
                return;
            }

            // 遍历 1 到 n 的所有数
            for (int i = 1; i <= n; i++)
            {
                if (!used[i])
                {
                    // 选择当前数
                    used[i] = true;
                    path.Add(i);

                    // 递归选择下一个数
                    Backtrack(n, k, used, path, result);

                    // 回溯，撤销选择
                    path.RemoveAt(path.Count - 1);
                    used[i] = false;
                }
            }
        }
    }
}