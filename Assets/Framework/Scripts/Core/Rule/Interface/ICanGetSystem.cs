// ------------------------------------------------------------
// @file       ICanGetSystem.cs
// @brief
// @author     zheliku
// @Modified   2024-10-04 11:10:32
// @Copyright  Copyright (c) 2024, zheliku
// ------------------------------------------------------------

namespace Framework.Core
{
    /// <summary>
    /// 可获取 System，通过 Architecture 获取，由 CanGetSystemExtension 扩展实现
    /// </summary>
    public interface ICanGetSystem : IBelongToArchitecture
    {
        // *************** 继承的接口 ↓ *****************
        // IArchitecture Architecture { get; }
        // *************** 继承的接口 ↑ *****************
    }
}