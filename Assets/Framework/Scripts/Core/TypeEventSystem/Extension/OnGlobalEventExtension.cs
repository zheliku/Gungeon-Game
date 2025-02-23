// ------------------------------------------------------------
// @file       OnGloableEventExtension.cs
// @brief
// @author     zheliku
// @Modified   2024-10-06 01:10:37
// @Copyright  Copyright (c) 2024, zheliku
// ------------------------------------------------------------

namespace Framework.Core
{
    /// <summary>
    /// IOnEvent 扩展
    /// </summary>
    public static class OnGlobalEventExtension
    {
        /// <summary>
        /// 注册事件
        /// </summary>
        /// <param name="self">IOnEvent 实例</param>
        /// <param name="priority">事件优先级</param>
        /// <typeparam name="TEvent">Event 类型</typeparam>
        /// <returns>注销器</returns>
        public static IUnRegister RegisterEvent<TEvent>(this IOnEvent<TEvent> self, int priority = 0)
        {
            return TypeEventSystem.GLOBAL.Register<TEvent>(self.OnEvent, priority);
        }

        /// <summary>
        /// 注销事件
        /// </summary>
        /// <param name="self">IOnEvent 实例</param>
        /// <typeparam name="TEvent">Event 类型</typeparam>
        public static void UnRegisterEvent<TEvent>(this IOnEvent<TEvent> self)
        {
            TypeEventSystem.GLOBAL.UnRegister<TEvent>(self.OnEvent);
        }
    }
}