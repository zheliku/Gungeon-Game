// ------------------------------------------------------------
// @file       Coroutine.cs
// @brief
// @author     zheliku
// @Modified   2024-10-29 11:10:20
// @Copyright  Copyright (c) 2024, zheliku
// ------------------------------------------------------------

namespace Framework.Toolkits.ActionKit
{
    using System;
    using System.Collections;
    using PoolKit;

    /// <summary>
    /// 协程 Action
    /// </summary>
    internal class Coroutine : AbstractAction<Coroutine>
    {
    #region Static

        public static Coroutine Create(Func<IEnumerator> coroutineGetter)
        {
            var coroutine = CreateInternal();
            coroutine._coroutineGetter = coroutineGetter;
            return coroutine;
        }

    #endregion

    #region 字段

        private Func<IEnumerator> _coroutineGetter;

    #endregion

    #region 接口

        public override void OnCreate() { }

        public override void OnStart()
        {
            // 协程执行完后，结束 Action
            ActionKitMonoBehaviourEvent.Instance.ExecuteCoroutine(_coroutineGetter(), () =>
            {
                this.Finish();
            });
        }

        public override void OnExecute(float deltaTime) { }

        public override void OnFinish() { }

        protected override void OnReset() { }

        protected override void OnDeinit()
        {
            _coroutineGetter = null;
        }

    #endregion
    }

    public static class CoroutineExtension
    {
        public static IAction ToAction(this IEnumerator self)
        {
            return Coroutine.Create(() => self);
        }
    }
}