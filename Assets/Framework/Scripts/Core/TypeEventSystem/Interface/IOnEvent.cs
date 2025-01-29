// ------------------------------------------------------------
// @file       IOnEvent.cs
// @brief
// @author     zheliku
// @Modified   2024-10-06 01:10:22
// @Copyright  Copyright (c) 2024, zheliku
// ------------------------------------------------------------


namespace Framework.Core
{
    /// <summary>
    /// 简易事件监听接口，可快速实现事件监听
    /// </summary>
    /// <typeparam name="TEvent">Event 类型</typeparam>
    public interface IOnEvent<in TEvent>
    {
        /// <summary>
        /// 监听的事件
        /// </summary>
        /// <param name="e">Event 实例</param>
        void OnEvent(TEvent e);
    }
}