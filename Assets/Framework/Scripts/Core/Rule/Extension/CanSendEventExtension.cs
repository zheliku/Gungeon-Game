// ------------------------------------------------------------
// @file       CanSendEventExtension.cs
// @brief      ICanSendEvent 的扩展实现，用于发送事件
// @author     zheliku
// @Modified   2024-10-04 16:10:04
// @Copyright  Copyright (c) 2024, zheliku
// ------------------------------------------------------------

namespace Framework.Core
{
    /// <summary>
    /// ICanSendEvent 的扩展实现
    /// </summary>
    public static class CanSendEventExtension
    {
        /// <summary>
        /// 发送 Event，参数使用默认构造函数 new() 传入
        /// </summary>
        /// <typeparam name="TEvent">事件类型</typeparam>
        /// <param name="self">ICanSendEvent 实例</param>
        public static void SendEvent<TEvent>(this ICanSendEvent self) where TEvent : new()
        {
            self.Architecture.SendEvent<TEvent>(); // 调用 Architecture 的 SendEvent 方法
        }

        /// <summary>
        /// 发送 Event，参数使用指定的 e 传入
        /// </summary>
        /// <typeparam name="TEvent">事件类型</typeparam>
        /// <param name="self">ICanSendEvent 实例</param>
        /// <param name="e">事件实例</param>
        public static void SendEvent<TEvent>(this ICanSendEvent self, TEvent e)
        {
            self.Architecture.SendEvent<TEvent>(e); // 调用 Architecture 的 SendEvent 方法
        }
    }
}