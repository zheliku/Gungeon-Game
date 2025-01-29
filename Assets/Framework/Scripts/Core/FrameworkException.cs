// ------------------------------------------------------------
// @file       FrameworkException.cs
// @brief
// @author     zheliku
// @Modified   2024-10-06 02:10:05
// @Copyright  Copyright (c) 2024, zheliku
// ------------------------------------------------------------

namespace Framework.Core
{
    using global::System;

    /// <summary>
    /// 框架异常类
    /// </summary>
    public sealed class FrameworkException : Exception
    {
        /// <summary>
        /// 初始化游戏框架异常类的新实例。
        /// </summary>
        public FrameworkException() { }

        /// <summary>
        /// 使用指定错误消息初始化游戏框架异常类的新实例。
        /// </summary>
        /// <param name="message">描述错误的消息。</param>
        public FrameworkException(string message) : base(message) { }

        /// <summary>
        /// 使用指定错误消息和对作为此异常原因的内部异常的引用来初始化游戏框架异常类的新实例。
        /// </summary>
        /// <param name="message">解释异常原因的错误消息。</param>
        /// <param name="inner">导致当前异常的异常。如果 innerException 参数不为空引用，则在处理内部异常的 catch 块中引发当前异常。</param>
        public FrameworkException(string message, Exception inner) : base(message, inner) { }
    }
}