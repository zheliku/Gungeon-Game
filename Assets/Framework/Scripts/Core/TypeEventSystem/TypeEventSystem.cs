// ------------------------------------------------------------
// @file       TypeEventSystem.cs
// @brief
// @author     zheliku
// @Modified   2024-10-04 17:10:56
// @Copyright  Copyright (c) 2024, zheliku
// ------------------------------------------------------------

namespace Framework.Core
{
    using global::System;
    using Sirenix.OdinInspector;

    /// <summary>
    /// 类型事件系统
    /// </summary>
    public sealed class TypeEventSystem
    {
        [ShowInInspector]
        private readonly EasyEvents _events = new EasyEvents();

        public static readonly TypeEventSystem GLOBAL = new TypeEventSystem();

        /// <summary>
        /// 发送 Event，参数使用默认构造函数 new() 传入
        /// </summary>
        /// <typeparam name="TEvent">Event 类型</typeparam>
        public void Send<TEvent>() where TEvent : new()
        {
            _events.GetEvent<EasyEvent<TEvent>>()?.Trigger(new TEvent());
        }

        /// <summary>
        /// 发送 Event，参数使用指定的 e 传入
        /// </summary>
        /// <param name="e">Event 实例</param>
        /// <typeparam name="TEvent">Event 类型</typeparam>
        public void Send<TEvent>(TEvent e)
        {
            _events.GetEvent<EasyEvent<TEvent>>()?.Trigger(e);
        }

        /// <summary>
        /// 注册事件监听
        /// </summary>
        /// <typeparam name="TEvent">事件类型</typeparam>
        /// <param name="onEvent">事件触发时的回调函数</param>
        /// <param name="priority">事件优先级</param>
        /// <returns>IUnRegister 接口，用于取消注册</returns>
        public IUnRegister Register<TEvent>(Action<TEvent> onEvent, int priority = 0)
        {
            return _events.GetOrAddEvent<EasyEvent<TEvent>>().Register(onEvent, priority);
        }

        /// <summary>
        /// 注销事件监听
        /// </summary>
        /// <typeparam name="TEvent">事件类型</typeparam>
        /// <param name="onEvent">要注销的回调函数</param>
        public void UnRegister<TEvent>(Action<TEvent> onEvent)
        {
            var e = _events.GetEvent<EasyEvent<TEvent>>();
            e?.UnRegister(onEvent);
        }
    }
}