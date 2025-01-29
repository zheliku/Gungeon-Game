// ------------------------------------------------------------
// @file       Callback.cs
// @brief
// @author     zheliku
// @Modified   2024-10-24 20:10:16
// @Copyright  Copyright (c) 2024, zheliku
// ------------------------------------------------------------

namespace Framework.Toolkits.ActionKit
{
    using System;

    /// <summary>
    /// 回调 Action
    /// </summary>
    internal class Callback : AbstractAction<Callback>
    {
    #region Static

        public static Callback Create(Action func)
        {
            var callback = CreateInternal();
            callback._callback = func;
            return callback;
        }

    #endregion

    #region 字段

        private Action _callback;

    #endregion

    #region 接口

        public override void OnCreate()
        {
            // 执行回调后即结束
            _callback?.Invoke();
            this.Finish();
        }
        
        public override void OnStart()
        {
            // 执行回调后即结束
            _callback?.Invoke();
            this.Finish();
        }

        public override void OnExecute(float deltaTime) { }

        public override void OnFinish() { }

        protected override void OnReset() { }

        protected override void OnDeinit()
        {
            _callback = null;
        }

    #endregion
    }
}