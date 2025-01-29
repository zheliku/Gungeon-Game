// ------------------------------------------------------------
// @file       IQuery.cs
// @brief
// @author     zheliku
// @Modified   2024-10-04 11:10:17
// @Copyright  Copyright (c) 2024, zheliku
// ------------------------------------------------------------

namespace Framework.Core
{
    /// <summary>
    /// Query 接口
    /// </summary>
    /// <typeparam name="TResult">查询结果类型</typeparam>
    public interface IQuery<out TResult> : // IBelongToArchitecture, 
        ICanGetModel,                      // IQuery 可获取 Model
        ICanGetSystem,                     // IQuery 可获取 System
        ICanSetArchitecture,               // IQuery 可设置 Architecture
        ICanSendQuery                      // IQuery 可发送 Query
    {
        // *************** 继承的接口 ↓ *****************
        // IArchitecture Architecture { get; }
        // void SetArchitecture(IArchitecture architecture);
        // *************** 继承的接口 ↑ *****************

        /// <summary>
        /// 执行查询
        /// </summary>
        /// <returns>查询结果</returns>
        TResult Do();
    }
}