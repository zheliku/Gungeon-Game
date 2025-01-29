// ------------------------------------------------------------
// @file       CanSendQueryExtension.cs
// @brief      ICanSendQuery 的扩展实现，用于发送查询
// @author     zheliku
// @Modified   2024-10-04 16:10:09
// @Copyright  Copyright (c) 2024, zheliku
// ------------------------------------------------------------

namespace Framework.Core
{
    /// <summary>
    /// ICanSendQuery 的扩展实现
    /// </summary>
    public static class CanSendQueryExtension
    {
        /// <summary>
        /// 发送 Query 并返回结果
        /// </summary>
        /// <typeparam name="TResult">查询结果的类型</typeparam>
        /// <param name="self">ICanSendQuery 实例</param>
        /// <param name="query">要发送的查询实例</param>
        /// <returns>TResult 类型的查询结果</returns>
        public static TResult SendQuery<TResult>(this ICanSendQuery self, IQuery<TResult> query)
        {
            return self.Architecture.SendQuery(query); // 调用 Architecture 的 SendQuery 方法
        }
    }
}