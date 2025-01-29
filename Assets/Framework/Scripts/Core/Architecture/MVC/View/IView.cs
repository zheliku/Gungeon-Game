// ------------------------------------------------------------
// @file       IController.cs
// @brief
// @author     zheliku
// @Modified   2024-10-04 11:10:01
// @Copyright  Copyright (c) 2024, zheliku
// ------------------------------------------------------------

namespace Framework.Core.View
{
    /// <summary>
    /// View 接口
    /// </summary>
    public interface IView : // IBelongToArchitecture, 
        ICanGetModel,        // IView 可获取 Model
        ICanGetSystem,       // IView 可获取 System
        ICanGetUtility,      // IView 可获取 Utility 
        ICanRegisterEvent,   // IView 可注册 Event
        ICanSendCommand,     // IView 可发送 Command
        ICanSendQuery        // IView 可发送 Query
    {
        // *************** 继承的接口 ↓ *****************
        // IArchitecture Architecture { get; }
        // *************** 继承的接口 ↑ *****************
    }
}