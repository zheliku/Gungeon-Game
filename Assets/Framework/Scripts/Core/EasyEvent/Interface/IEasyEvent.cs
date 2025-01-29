// ------------------------------------------------------------
// @file       IEasyEvent.cs
// @brief
// @author     zheliku
// @Modified   2024-10-04 17:10:39
// @Copyright  Copyright (c) 2024, zheliku
// ------------------------------------------------------------

namespace Framework.Core
{
    using global::System;

    /// <summary>
    /// 轻量级事件
    /// </summary>
    public interface IEasyEvent
    {
        /// <summary>
        /// 注册事件
        /// </summary>
        /// <param name="onEvent">事件方法</param>
        /// <returns>注销器</returns>
        IUnRegister Register(Action onEvent);

        /// <summary>
        /// 注销所有事件
        /// </summary>
        void UnRegisterAll();
        
        int EventCount { get; }
    }
}