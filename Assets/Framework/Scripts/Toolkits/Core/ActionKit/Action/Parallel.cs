// ------------------------------------------------------------
// @file       Parallel.cs
// @brief
// @author     zheliku
// @Modified   2024-10-29 10:10:51
// @Copyright  Copyright (c) 2024, zheliku
// ------------------------------------------------------------

namespace Framework.Toolkits.ActionKit
{
    using System.Collections.Generic;
    using PoolKit;

    /// <summary>
    /// 并行 Action
    /// </summary>
    public interface IParallel : ISequence
    { }

    internal class Parallel : AbstractAction<Parallel>, IParallel
    {
    #region Static

        public static Parallel Create()
        {
            return CreateInternal();
        }

    #endregion

    #region 字段

        private List<IAction> _actions = new List<IAction>();

        private int _finishedCount; // 已完成的 Action 数量

    #endregion

    #region 接口

        public override void OnCreate() { }

        public override void OnStart() { }

        public override void OnExecute(float deltaTime)
        {
            for (int i = _finishedCount; i < _actions.Count; i++) // 从未完成的 Action 开始执行
            {
                if (!_actions[i].Execute(deltaTime)) continue;

                _finishedCount++;

                if (_finishedCount == _actions.Count)
                {
                    this.Finish();
                }
                else
                {
                    // 交换顺序，将已完成的 Action 放在最前面
                    (_actions[i], _actions[_finishedCount - 1]) = (_actions[_finishedCount - 1], _actions[i]);
                }
            }
        }

        public override void OnFinish() { }

        protected override void OnReset()
        {
            _finishedCount = 0;

            foreach (var action in _actions)
            {
                action.Reset();
            }
        }

        protected override void OnDeinit()
        {
            foreach (var action in _actions)
            {
                action.Deinit();
            }

            _actions.Clear();
        }

        public ISequence Append(IAction action)
        {
            _actions.Add(action);
            return this;
        }

    #endregion
    }
}