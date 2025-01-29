// ------------------------------------------------------------
// @file       Sequence.cs
// @brief
// @author     zheliku
// @Modified   2024-10-25 21:10:23
// @Copyright  Copyright (c) 2024, zheliku
// ------------------------------------------------------------

namespace Framework.Toolkits.ActionKit
{
    using System.Collections.Generic;
    using PoolKit;
    using Sirenix.OdinInspector;

    public interface ISequence : IAction
    {
        ISequence Append(IAction action);
    }

    public class Sequence : AbstractAction<Sequence>, ISequence
    {
    #region Static

        public static Sequence Create()
        {
            return CreateInternal();
        }

    #endregion

    #region 字段

        private IAction _currentAction;

        private int _currentActionIndex;

        [ShowInInspector]
        private readonly List<IAction> _actions = new List<IAction>();

    #endregion

    #region 接口

        public override void OnCreate() { }

        public override void OnStart()
        {
            if (_actions.Count > 0)
            {
                _currentActionIndex = 0;
                _currentAction      = _actions[0];
                _currentAction.Reset();

                // 执行到第一个未完成的 Action
                TryExecuteUntilNextNotFinished();
            }
            else
            {
                this.Finish();
            }
        }

        public override void OnExecute(float deltaTime)
        {
            if (_currentAction == null)
            {
                this.Finish();
                return;
            }

            if (_currentAction.Execute(deltaTime)) // 当前 Action 已经执行完毕
            {
                _currentActionIndex++; // 更新索引为下一个 Action

                if (_currentActionIndex < _actions.Count) // 如果数组没有越界
                {
                    // 更新 Action 为下一个
                    _currentAction = _actions[_currentActionIndex];
                    _currentAction.Reset();

                    // 继续执行，直到有未完成的 Action 停止
                    TryExecuteUntilNextNotFinished();
                }
                else
                {
                    this.Finish();
                }
            }
        }

        public override void OnFinish() { }

        protected override void OnReset()
        {
            // 将序列中的每个 Action 重置为初始状态
            _currentActionIndex = 0;

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

    #region 方法

        /// <summary>
        /// 执行到下一个未完成的 Action
        /// </summary>
        private void TryExecuteUntilNextNotFinished()
        {
            while (_currentAction != null &&
                   _currentAction.Execute(0)) // 前一个执行完成后，才会进入下一个 Action，执行时帧间隔为 0，因为在同一帧的 while 循环中执行的
            {
                _currentActionIndex++;

                if (_currentActionIndex < _actions.Count) // 数组未越界
                {
                    _currentAction = _actions[_currentActionIndex]; // 更新当前 Action 为下一个
                    _currentAction.Reset();                         // 重置当前 Action，准备执行
                }
                else // 数组已越界
                {
                    _currentAction = null;
                    this.Finish();
                }
            }
        }

    #endregion
    }
}