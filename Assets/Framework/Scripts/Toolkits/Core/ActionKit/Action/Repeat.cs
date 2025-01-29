// ------------------------------------------------------------
// @file       Repeat.cs
// @brief
// @author     zheliku
// @Modified   2024-10-26 12:10:30
// @Copyright  Copyright (c) 2024, zheliku
// ------------------------------------------------------------

namespace Framework.Toolkits.ActionKit
{
    using PoolKit;

    public interface IRepeat : ISequence
    { }

    /// <summary>
    /// 重复 Action
    /// </summary>
    public class Repeat : AbstractAction<Repeat>, IRepeat
    {
    #region Static
        public static Repeat Create(int repeatCount = -1) // -1 表示无限次
        {
            var repeat = CreateInternal();
            repeat._repeatCount = repeatCount;
            return repeat;
        }

    #endregion

    #region 字段

        private Sequence _sequence;

        private int _repeatCount = -1;
        private int _currentRepeatCount;

    #endregion

    #region 接口

        public override void OnCreate()
        {
            _sequence = Sequence.Create();
        }
        
        public override void OnStart()
        {
            _currentRepeatCount = 0;
        }

        public override void OnExecute(float deltaTime)
        {
            if (_repeatCount is -1 or 0) // 无限次重复，不 Finish()
            {
                if (_sequence.Execute(deltaTime))
                {
                    // 重置序列，重新开始
                    _sequence.Reset();
                }
            }
            else if (_currentRepeatCount < _repeatCount) // 有限次重复，会 Finish()
            {
                if (_sequence.Execute(deltaTime))
                {
                    _currentRepeatCount++;

                    if (_currentRepeatCount >= _repeatCount)
                    {
                        this.Finish();
                    }
                    else
                    {
                        // 重置序列，重新开始
                        _sequence.Reset();
                    }
                }
            }
        }

        public override void OnFinish() { }

        protected override void OnReset()
        {
            _currentRepeatCount = 0;
            _sequence.Reset();
        }

        protected override void OnDeinit()
        {
            _sequence.Deinit();
        }

        public ISequence Append(IAction action)
        {
            // 将后面的 Action 添加到 _sequence 中执行
            _sequence.Append(action);
            return this;
        }

    #endregion
    }
}