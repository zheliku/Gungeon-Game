// ------------------------------------------------------------
// @file       Delay.cs
// @brief
// @author     zheliku
// @Modified   2024-10-24 23:10:32
// @Copyright  Copyright (c) 2024, zheliku
// ------------------------------------------------------------

namespace Framework.Toolkits.ActionKit
{
    using System;
    using PoolKit;

    /// <summary>
    /// 延迟 Action
    /// </summary>
    public class Delay : AbstractAction<Delay>
    {
    #region Static

        public static Delay Create(float delayTime, Action onDelayFinish = null)
        {
            var delay = CreateInternal();
            delay._delayTime     = delayTime;
            delay.OnDelayFinish  = onDelayFinish;
            delay.CurrentSeconds = 0;
            return delay;
        }

        public static Delay Create(Func<float> delayTimeFactory, Action onDelayFinish = null)
        {
            var delay = CreateInternal();
            delay._delayTimeFactory = delayTimeFactory;
            delay.OnDelayFinish     = onDelayFinish;
            delay.CurrentSeconds    = 0;
            return delay;
        }

    #endregion

    #region 字段

        private float _delayTime;

        private Func<float> _delayTimeFactory;

    #endregion

    #region 属性

        public Action OnDelayFinish { get; set; }

        public float CurrentSeconds { get; set; } = 0;

    #endregion

    #region 接口

        public override void OnCreate() { }

        public override void OnStart()
        {
            // 计算延迟时间
            if (_delayTimeFactory != null)
            {
                _delayTime = _delayTimeFactory();
            }
        }

        public override void OnExecute(float deltaTime)
        {
            // 到了延迟时间后，结束 Action
            if (CurrentSeconds >= _delayTime)
            {
                OnDelayFinish?.Invoke();
                this.Finish();
            }

            CurrentSeconds += deltaTime;
        }

        public override void OnFinish() { }

        protected override void OnReset()
        {
            CurrentSeconds = 0;
        }

        protected override void OnDeinit()
        {
            OnDelayFinish = null;
        }

    #endregion
    }
}