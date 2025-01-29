// ------------------------------------------------------------
// @file       Task.cs
// @brief
// @author     zheliku
// @Modified   2024-10-30 20:10:21
// @Copyright  Copyright (c) 2024, zheliku
// ------------------------------------------------------------

namespace Framework.Toolkits.ActionKit
{
    using System;
    using PoolKit;
    using SystemTask = System.Threading.Tasks;

    public class Task : AbstractAction<Task>
    {
    #region Static

        public static Task Create(Func<SystemTask.Task> taskGetter)
        {
            var task = CreateInternal();
            task._taskGetter = taskGetter;
            return task;
        }

    #endregion

    #region 字段

        private Func<SystemTask.Task> _taskGetter;

        private SystemTask.Task _executingTask;

    #endregion

    #region 接口

        public override void OnCreate() { }

        public override void OnStart()
        {
            StartTask();
        }

        public override void OnExecute(float deltaTime) { }

        public override void OnFinish() { }

        protected override void OnReset() { }

        protected override void OnDeinit()
        {
            _taskGetter = null;

            if (_executingTask != null)
            {
                _executingTask.Dispose();
                _executingTask = null;
            }
        }

    #endregion

    #region 方法

        private async void StartTask()
        {
            _executingTask = _taskGetter();
            await _executingTask;

            // task 完成后，结束 Action
            this.Finish();
            _executingTask = null;
        }

    #endregion
    }
}