// ------------------------------------------------------------
// @file       CanRegisterEventExtension.cs
// @brief      ICanRegisterEvent 的扩展实现，用于注册和注销事件
// @author     zheliku
// @Modified   2024-10-04 16:10:52
// @Copyright  Copyright (c) 2024, zheliku
// ------------------------------------------------------------

namespace Framework.Core
{
    using global::System;

    /// <summary>
    /// ICanRegisterEvent 的扩展实现
    /// </summary>
    public static class CanRegisterEventExtension
    {
        /// <summary>
        /// 注册指定类型的事件
        /// </summary>
        /// <typeparam name="TEvent">事件类型</typeparam>
        /// <param name="self">ICanRegisterEvent 实例</param>
        /// <param name="onEvent">事件触发时的回调函数</param>
        /// <returns>IUnRegister 实例，用于取消注册事件</returns>
        public static IUnRegister RegisterEvent<TEvent>(this ICanRegisterEvent self, Action<TEvent> onEvent)
        {
            return self.Architecture.RegisterEvent<TEvent>(onEvent); // 调用 Architecture 类的 RegisterEvent 方法
        }

        /// <summary>
        /// 注销指定类型的事件
        /// </summary>
        /// <typeparam name="TEvent">事件类型</typeparam>
        /// <param name="self">ICanRegisterEvent 实例</param>
        /// <param name="onEvent">要注销的回调函数</param>
        public static void UnRegisterEvent<TEvent>(this ICanRegisterEvent self, Action<TEvent> onEvent)
        {
            self.Architecture.UnRegisterEvent<TEvent>(onEvent); // 调用 Architecture 类的 UnRegisterEvent 方法
        }
    }
}