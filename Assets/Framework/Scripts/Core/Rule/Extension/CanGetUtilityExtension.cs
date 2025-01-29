// ------------------------------------------------------------
// @file       CanGetUtilityExtension.cs
// @brief      ICanGetUtility 的扩展实现，用于获取指定类型的 Utility
// @author     zheliku
// @Modified   2024-10-04 16:10:17
// @Copyright  Copyright (c) 2024, zheliku
// ------------------------------------------------------------

namespace Framework.Core
{
    /// <summary>
    /// ICanGetUtility 的扩展实现
    /// </summary>
    public static class CanGetUtilityExtension
    {
        /// <summary>
        /// 获取指定类型的 Utility
        /// </summary>
        /// <typeparam name="TUtility">要获取的 Utility 类型</typeparam>
        /// <param name="self">ICanGetUtility 实例</param>
        /// <returns>指定类型的 Utility</returns>
        public static TUtility GetUtility<TUtility>(this ICanGetUtility self) where TUtility : class, IUtility
        {
            return self.Architecture.GetUtility<TUtility>(); // 调用 Architecture 类的 GetUtility 方法
        }
    }
}