// ------------------------------------------------------------
// @file       IReadonlyBindableProperty.cs
// @brief
// @author     zheliku
// @Modified   2024-10-05 11:10:48
// @Copyright  Copyright (c) 2024, zheliku
// ------------------------------------------------------------

namespace Framework.Core
{
    using global::System;

    /// <summary>
    /// 可绑定事件监听的只读 Property 接口
    /// </summary>
    /// <typeparam name="TProperty">被绑定的只读 Property 类型</typeparam>
    public interface IReadonlyBindableProperty<TProperty> : IEasyEvent
    {
        /// <summary>
        /// Property
        /// </summary>
        TProperty Value { get; }

        /// <summary>
        /// 注册绑定事件
        /// </summary>
        /// <param name="onValueChanged">Property 变化时触发的监听事件</param>
        /// <returns>注销器</returns>
        IUnRegister Register(Action<TProperty, TProperty> onValueChanged);

        /// <summary>
        /// 注册绑定事件并立即触发一次
        /// </summary>
        /// <param name="onValueChanged">Property 变化时触发的监听事件</param>
        /// <returns>注销器</returns>
        IUnRegister RegisterWithInitValue(Action<TProperty, TProperty> onValueChanged);

        /// <summary>
        /// 注销绑定事件
        /// </summary>
        /// <param name="onValueChanged">Property 变化时触发的监听事件</param>
        void UnRegister(Action<TProperty, TProperty> onValueChanged);
    }
}