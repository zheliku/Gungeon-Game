// ------------------------------------------------------------
// @file       ISystem.cs
// @brief
// @author     zheliku
// @Modified   2024-10-04 11:10:35
// @Copyright  Copyright (c) 2024, zheliku
// ------------------------------------------------------------

namespace Framework.Core
{
    /// <summary>
    /// System 接口
    /// </summary>
    public interface ISystem : // IBelongToArchitecture,
        ICanInit,              // ISystem 可初始化
        ICanGetSystem,         // ISystem 可获取 System
        ICanGetModel,          // ISystem 可获取 Model
        ICanGetUtility,        // ISystem 可获取 Utility
        ICanRegisterEvent,     // ISystem 可注册 Event
        ICanSetArchitecture,   // ISystem 可设置 Architecture
        ICanSendEvent          // ISystem 可发送 Event
    {
        // *************** 继承的接口 ↓ *****************
        // IArchitecture Architecture { get; }
        // void SetArchitecture(IArchitecture architecture);
        // bool Initialized { get; set; }
        // void Init();
        // void Deinit();
        // *************** 继承的接口 ↑ *****************
    }
}