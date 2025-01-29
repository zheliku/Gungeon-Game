// ------------------------------------------------------------
// @file       IBindableProperty.cs
// @brief
// @author     zheliku
// @Modified   2024-10-05 11:10:19
// @Copyright  Copyright (c) 2024, zheliku
// ------------------------------------------------------------

namespace Framework.Core
{
    /// <summary>
    /// 可绑定事件监听的 Property 接口
    /// </summary>
    /// <typeparam name="TProperty">被绑定的 Property 类型</typeparam>
    public interface IBindableProperty<TProperty> : IReadonlyBindableProperty<TProperty>
    {
        /// <summary>
        /// Property
        /// </summary>
        new TProperty Value { get; set; }

        /// <summary>
        /// 设置 Property 值，但不触发绑定事件
        /// </summary>
        /// <param name="newValue">新值</param>
        void SetValueWithoutEvent(TProperty newValue);
    }
}