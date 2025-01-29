// ------------------------------------------------------------
// @file       DelayFrame.cs
// @brief
// @author     zheliku
// @Modified   2024-10-26 11:10:53
// @Copyright  Copyright (c) 2024, zheliku
// ------------------------------------------------------------

namespace Framework.Toolkits.ActionKit
{
    using System;
    using PoolKit;
    using UnityEngine;

    /// <summary>
    /// 延迟帧 Action
    /// </summary>
    public class DelayFrame : AbstractAction<DelayFrame>
    {
    #region Static
        
        public static DelayFrame Create(int frameCount, Action onDelayFinish = null)
        {
            var delayFrame = CreateInternal();
            delayFrame._delayFrameCount = frameCount;
            delayFrame._onDelayFinish   = onDelayFinish;
            return delayFrame;
        }

    #endregion

    #region 字段

        private Action _onDelayFinish;

        private int _startFrameCount;

        private int _delayFrameCount;

    #endregion

    #region 接口

        public override void OnCreate() { }

        public override void OnStart()
        {
            // 记录当前帧数
            _startFrameCount = Time.frameCount;
        }

        public override void OnExecute(float deltaTime)
        {
            // 到了延迟帧后，结束 Action
            if (Time.frameCount >= _startFrameCount + _delayFrameCount)
            {
                _onDelayFinish?.Invoke();
                this.Finish();
            }
        }

        public override void OnFinish() { }

        protected override void OnReset()
        {
            _startFrameCount = 0;
        }

        protected override void OnDeinit()
        {
            _onDelayFinish = null;
        }

    #endregion
    }
}