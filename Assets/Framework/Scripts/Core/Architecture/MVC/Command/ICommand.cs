// ------------------------------------------------------------
// @file       ICommand.cs
// @brief
// @author     zheliku
// @Modified   2024-10-04 11:10:52
// @Copyright  Copyright (c) 2024, zheliku
// ------------------------------------------------------------

namespace Framework.Core.Command
{
    /// <summary>
    /// Command 接口
    /// </summary>
    public interface ICommand : // IBelongToArchitecture,
        ICanGetModel,           // ICommand 可获取 Model
        ICanGetSystem,          // ICommand 可获取 System
        ICanGetUtility,         // ICommand 可获取 Utility
        ICanSetArchitecture,    // ICommand 可设置 Architecture
        ICanSendCommand,        // ICommand 可发送 Command
        ICanSendEvent,          // ICommand 可发送 Event
        ICanSendQuery           // ICommand 可发送 Query
    {
        // *************** 继承的接口 ↓ *****************
        // IArchitecture Architecture { get; }
        // void SetArchitecture(IArchitecture architecture);
        // *************** 继承的接口 ↑ *****************

        /// <summary>
        /// 执行 Command
        /// </summary>
        void Execute();
    }

    /// <summary>
    /// Command 接口
    /// </summary>
    public interface ICommand<out TResult> : // IBelongToArchitecture, 
        ICanGetModel,                        // ICommand 可获取 Model
        ICanGetSystem,                       // ICommand 可获取 System
        ICanGetUtility,                      // ICommand 可获取 Utility
        ICanSetArchitecture,                 // ICommand 可设置 Architecture
        ICanSendCommand,                     // ICommand 可发送 Command
        ICanSendEvent,                       // ICommand 可发送 Event
        ICanSendQuery                        // ICommand 可发送 Query
    {
        // *************** 继承的接口 ↓ *****************
        // IArchitecture Architecture { get; }
        // void SetArchitecture(IArchitecture architecture);
        // *************** 继承的接口 ↑ *****************

        /// <summary>
        /// 执行 Command
        /// </summary>
        /// <returns>返回值</returns>
        TResult Execute();
    }
}