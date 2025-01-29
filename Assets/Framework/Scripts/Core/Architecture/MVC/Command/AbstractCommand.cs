// ------------------------------------------------------------
// @file       Command.cs
// @brief
// @author     zheliku
// @Modified   2024-10-04 11:10:21
// @Copyright  Copyright (c) 2024, zheliku
// ------------------------------------------------------------

namespace Framework.Core.Command
{
    using Sirenix.OdinInspector;

    /// <summary>
    /// Command 基类，执行后无返回值
    /// </summary>
    [HideReferenceObjectPicker]
    public abstract class AbstractCommand : ICommand
    {
        private IArchitecture _architecture;

        // 仅能通过 IBelongToArchitecture 接口访问 Architecture 属性
        IArchitecture IBelongToArchitecture.Architecture => _architecture;

        // 仅能通过 ICanSetArchitecture 接口设置 Architecture 属性
        void ICanSetArchitecture.SetArchitecture(IArchitecture architecture) { _architecture = architecture; }

        // 仅能通过 ICommand 接口使用 Execute 方法
        void ICommand.Execute() { OnExecute(); }

        /// <summary>
        /// 执行方法，需要由子类实现
        /// </summary>
        protected abstract void OnExecute();
    }

    /// <summary>
    /// Command 基类，执行后有返回值
    /// </summary>
    [HideReferenceObjectPicker]
    public abstract class AbstractCommand<TResult> : ICommand<TResult>
    {
        private IArchitecture _architecture;

        // 仅能通过 IBelongToArchitecture 接口访问 Architecture 属性
        IArchitecture IBelongToArchitecture.Architecture => _architecture;

        // 仅能通过 ICanSetArchitecture 接口设置 Architecture 属性
        void ICanSetArchitecture.SetArchitecture(IArchitecture architecture) { _architecture = architecture; }

        // 仅能通过 ICommand 接口使用 Execute 方法
        TResult ICommand<TResult>.Execute() { return OnExecute(); }

        /// <summary>
        /// 执行方法，需要由子类实现
        /// </summary>
        protected abstract TResult OnExecute();
    }
}