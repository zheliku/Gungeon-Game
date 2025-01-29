// ------------------------------------------------------------
// @file       IAction.cs
// @brief
// @author     zheliku
// @Modified   2024-10-24 19:10:27
// @Copyright  Copyright (c) 2024, zheliku
// ------------------------------------------------------------

namespace Framework.Toolkits.ActionKit
{
    public interface IAction : IAction<ActionStatus>
    { }

    public interface IAction<TStatus>
    {
        ulong ActionID { get; set; }

        TStatus Status { get; set; }

        void OnStart();

        void OnExecute(float deltaTime);

        void OnFinish();

        /// <summary>
        /// 从初始化状态变为未初始化状态，即 Action 是否已经执行完毕，同 Finished <br/>
        /// 在 Deinited 状态下，Deinit() 已经执行完成，该对象还有未完成的 unown 引用（至少是初始值：1），等待被释放
        /// </summary>
        bool Deinited { get; set; }

        bool Paused { get; set; }

        void Reset();

        /// <summary>
        /// Action 执行完成后触发的方法
        /// </summary>
        void Deinit();
    }
}