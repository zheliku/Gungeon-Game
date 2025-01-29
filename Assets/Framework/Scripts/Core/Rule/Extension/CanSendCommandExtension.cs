// ------------------------------------------------------------
// @file       CanSendCommandExtension.cs
// @brief      ICanSendCommand 的扩展实现，用于发送命令
// @author     zheliku
// @Modified   2024-10-04 16:10:58
// @Copyright  Copyright (c) 2024, zheliku
// ------------------------------------------------------------

namespace Framework.Core
{
    using Command;

    /// <summary>
    /// ICanSendCommand 的扩展实现
    /// </summary>
    public static class CanSendCommandExtension
    {
        /// <summary>
        /// 发送 Command，参数使用默认构造函数 new() 传入
        /// </summary>
        /// <typeparam name="TCommand">要发送的命令类型</typeparam>
        /// <param name="self">ICanSendCommand 实例</param>
        public static void SendCommand<TCommand>(this ICanSendCommand self) where TCommand : ICommand, new()
        {
            self.Architecture.SendCommand<TCommand>(new TCommand()); // 调用 Architecture 的 SendCommand 方法
        }

        /// <summary>
        /// 发送 Command，参数使用指定的 command 传入
        /// </summary>
        /// <typeparam name="TCommand">要发送的命令类型</typeparam>
        /// <param name="self">ICanSendCommand 实例</param>
        /// <param name="command">命令实例</param>
        public static void SendCommand<TCommand>(this ICanSendCommand self, TCommand command) where TCommand : ICommand
        {
            self.Architecture.SendCommand<TCommand>(command); // 调用 Architecture 的 SendCommand 方法
        }

        /// <summary>
        /// 发送 Command 并返回执行结果
        /// </summary>
        /// <typeparam name="TResult">结果类型</typeparam>
        /// <param name="self">ICanSendCommand 实例</param>
        /// <param name="command">ICommand 实例</param>
        /// <returns>TResult 类型的命令执行结果</returns>
        public static TResult SendCommand<TResult>(this ICanSendCommand self, ICommand<TResult> command)
        {
            return self.Architecture.SendCommand(command); // 调用 Architecture 的 SendCommand 方法
        }
    }
}