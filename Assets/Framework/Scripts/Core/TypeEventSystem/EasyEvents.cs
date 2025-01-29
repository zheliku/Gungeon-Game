// ------------------------------------------------------------
// @file       EasyEvents.cs
// @brief
// @author     zheliku
// @Modified   2024-10-04 17:10:29
// @Copyright  Copyright (c) 2024, zheliku
// ------------------------------------------------------------

namespace Framework.Core
{
    using global::System;
    using global::System.Collections.Generic;
    using Sirenix.OdinInspector;

    /// <summary>
    /// EasyEvent 管理器
    /// </summary>
    public sealed class EasyEvents
    {
        private static readonly EasyEvents GLOBAL_EVENTS = new EasyEvents();

        [ShowInInspector]
        private readonly Dictionary<Type, IEasyEvent> _typeEvents = new Dictionary<Type, IEasyEvent>();

        /// <summary>
        /// 获取 EasyEvent，用于 GLOBAL_EVENTS
        /// </summary>
        /// <typeparam name="TEasyEvent">EasyEvent 类型</typeparam>
        /// <returns>EasyEvent 实例</returns>
        public static TEasyEvent Get<TEasyEvent>() where TEasyEvent : IEasyEvent
        {
            return GLOBAL_EVENTS.GetEvent<TEasyEvent>();
        }

        /// <summary>
        /// 注册 EasyEvent，用于 GLOBAL_EVENTS
        /// </summary>
        /// <typeparam name="TEasyEvent">EasyEvent 类型</typeparam>
        public static void Register<TEasyEvent>() where TEasyEvent : IEasyEvent, new()
        {
            GLOBAL_EVENTS.AddEvent<TEasyEvent>();
        }

        /// <summary>
        /// 添加 EasyEvent，用于新实例
        /// </summary>
        /// <typeparam name="TEasyEvent">EasyEvent 类型</typeparam>
        public void AddEvent<TEasyEvent>() where TEasyEvent : IEasyEvent, new()
        {
            _typeEvents.Add(typeof(TEasyEvent), new TEasyEvent());
        }

        /// <summary>
        /// 获取 EasyEvent，用于新实例
        /// </summary>
        /// <typeparam name="TEasyEvent">EasyEvent 类型</typeparam>
        /// <returns>EasyEvent 实例</returns>
        public TEasyEvent GetEvent<TEasyEvent>() where TEasyEvent : IEasyEvent
        {
            return _typeEvents.TryGetValue(typeof(TEasyEvent), out var e) ? (TEasyEvent) e : default(TEasyEvent);
        }

        /// <summary>
        /// 获取 EasyEvent，不存在则添加，用于新实例
        /// </summary>
        /// <typeparam name="TEasyEvent">EasyEvent 类型</typeparam>
        /// <returns>EasyEvent 实例</returns>
        public TEasyEvent GetOrAddEvent<TEasyEvent>() where TEasyEvent : IEasyEvent, new()
        {
            var eType = typeof(TEasyEvent);
            if (_typeEvents.TryGetValue(eType, out var e))
            {
                return (TEasyEvent) e;
            }

            var t = new TEasyEvent();
            _typeEvents.Add(eType, t);
            return t;
        }
    }
}