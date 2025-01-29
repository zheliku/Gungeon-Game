// ------------------------------------------------------------
// @file       ICanSendCommand.cs
// @brief
// @author     zheliku
// @Modified   2024-10-04 11:10:55
// @Copyright  Copyright (c) 2024, zheliku
// ------------------------------------------------------------

namespace Framework.Core
{
    /// <summary>
    /// 可发送 Command，通过 Architecture 发送，由 CanSendCommandExtension 扩展实现
    /// </summary>
    public interface ICanSendCommand : IBelongToArchitecture
    {
        // *************** 继承的接口 ↓ *****************
        // IArchitecture Architecture { get; }
        // *************** 继承的接口 ↑ *****************
    }
}