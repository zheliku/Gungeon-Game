// ------------------------------------------------------------
// @file       IBelongToArchitecture.cs
// @brief
// @author     zheliku
// @Modified   2024-10-04 10:10:45
// @Copyright  Copyright (c) 2024, zheliku
// ------------------------------------------------------------

namespace Framework.Core
{
    /// <summary>
    /// 继承该接口，即可获取 Architecture
    /// </summary>
    public interface IBelongToArchitecture
    {
        /// <summary>
        /// 获取 Architecture
        /// </summary>
        IArchitecture Architecture { get; }
    }
}