// ------------------------------------------------------------
// @file       IArchitecture.cs
// @brief
// @author     zheliku
// @Modified   2024-10-07 22:10:18
// @Copyright  Copyright (c) 2024, zheliku
// ------------------------------------------------------------

namespace Framework.Core
{
    using Command;
    using global::System;
    using Model;

    /// <summary>
    /// Architecture 接口，用于规范 System、Model、Utility 的架构
    /// </summary>
    public interface IArchitecture
    {
        /// <summary>
        /// 注册 System
        /// </summary>
        /// <param name="system">TSystem 实例</param>
        /// <typeparam name="TSystem">System 类型</typeparam>
        void RegisterSystem<TSystem>(TSystem system) where TSystem : ISystem;

        /// <summary>
        /// 注册 Model
        /// </summary>
        /// <param name="model">TModel 实例</param>
        /// <typeparam name="TModel">Model 类型</typeparam>
        void RegisterModel<TModel>(TModel model) where TModel : IModel;

        /// <summary>
        /// 注册 Utility
        /// </summary>
        /// <param name="utility">TUtility 实例</param>
        /// <typeparam name="TUtility">Utility 类型</typeparam>
        void RegisterUtility<TUtility>(TUtility utility) where TUtility : IUtility;

        /// <summary>
        /// 获取 System
        /// </summary>
        /// <typeparam name="TSystem">System 类型</typeparam>
        /// <returns>TSystem 实例</returns>
        TSystem GetSystem<TSystem>() where TSystem : class, ISystem;

        /// <summary>
        /// 获取 Model
        /// </summary>
        /// <typeparam name="TModel">Model 类型</typeparam>
        /// <returns>TModel 实例</returns>
        TModel GetModel<TModel>() where TModel : class, IModel;

        /// <summary>
        /// 获取 Utility
        /// </summary>
        /// <typeparam name="TUtility">Utility 类型</typeparam>
        /// <returns>TUtility 实例</returns>
        TUtility GetUtility<TUtility>() where TUtility : class, IUtility;

        /// <summary>
        /// 发送 Command
        /// </summary>
        /// <param name="command">TCommand 实例</param>
        /// <typeparam name="TCommand">Command 类型</typeparam>
        void SendCommand<TCommand>(TCommand command) where TCommand : ICommand;

        /// <summary>
        /// 发送 Command 并返回执行结果
        /// </summary>
        /// <typeparam name="TResult">结果类型</typeparam>
        /// <param name="command">ICommand 实例</param>
        /// <returns>TResult 类型的 Command 执行结果</returns>
        TResult SendCommand<TResult>(ICommand<TResult> command);

        /// <summary>
        /// 发送 Query 并返回查询结果
        /// </summary>
        /// <typeparam name="TResult">查询结果类型</typeparam>
        /// <param name="query">IQuery 实例</param>
        /// <returns>TResult 类型的查询结果</returns>
        TResult SendQuery<TResult>(IQuery<TResult> query);

        /// <summary>
        /// 发送 Event，参数使用默认构造函数 new() 传入
        /// </summary>
        /// <typeparam name="TEvent">Event 类型</typeparam>
        void SendEvent<TEvent>() where TEvent : new();

        /// <summary>
        /// 发送 Event，参数使用指定的 e 传入
        /// </summary>
        /// <typeparam name="TEvent">Event 类型</typeparam>
        /// <param name="e">Event 实例</param>
        void SendEvent<TEvent>(TEvent e);

        /// <summary>
        /// 注册事件监听
        /// </summary>
        /// <typeparam name="TEvent">事件类型</typeparam>
        /// <param name="onEvent">事件触发时的回调函数</param>
        /// <param name="priority">事件优先级</param>
        /// <returns>IUnRegister 接口，用于取消注册</returns>
        IUnRegister RegisterEvent<TEvent>(Action<TEvent> onEvent, int priority);

        /// <summary>
        /// 注销事件监听
        /// </summary>
        /// <typeparam name="TEvent">事件类型</typeparam>
        /// <param name="onEvent">要注销的回调函数</param>
        void UnRegisterEvent<TEvent>(Action<TEvent> onEvent);

        /// <summary>
        /// 反初始化，用于清理或关闭架构
        /// </summary>
        void Deinit();
    }
}