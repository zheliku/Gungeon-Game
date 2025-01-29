// ------------------------------------------------------------
// @file       Lerp.cs
// @brief
// @author     zheliku
// @Modified   2024-10-30 10:10:18
// @Copyright  Copyright (c) 2024, zheliku
// ------------------------------------------------------------

namespace Framework.Toolkits.ActionKit
{
    using PoolKit;
    using UnityEngine;

    /// <summary>
    /// 线性插值 Action
    /// </summary>
    public class Lerp : AbstractAction<Lerp>
    {
    #region 常量

    #endregion

    #region Static

        public static Lerp Create(float a, float b, float duration, System.Action<float> onLerp, System.Action onLerpFinished)
        {
            var lerp = CreateInternal();
            lerp._a              = a;
            lerp._b              = b;
            lerp._duration       = duration;
            lerp._onLerp         = onLerp;
            lerp._onLerpFinished = onLerpFinished;
            return lerp;
        }

    #endregion

    #region 字段

        private float _a;
        private float _b;
        private float _duration;
        private float _currentTime;

        private System.Action<float> _onLerp;
        private System.Action        _onLerpFinished;

    #endregion

    #region 接口

        public override void OnCreate() { }

        public override void OnStart()
        {
            // 初始执行一次 _onLerp
            _currentTime = 0;
            _onLerp?.Invoke(Mathf.Lerp(_a, _b, 0));
        }

        public override void OnExecute(float deltaTime)
        {
            // 计时，并执行 _onLerp
            _currentTime += deltaTime;

            if (_currentTime < _duration)
            {
                _onLerp?.Invoke(Mathf.Lerp(_a, _b, _currentTime / _duration));
            }
            else
            {
                this.Finish();
            }
        }

        public override void OnFinish()
        {
            // 结束时执行一次 _onLerp
            _onLerp?.Invoke(Mathf.Lerp(_a, _b, 1));
            _onLerpFinished?.Invoke();
        }

        protected override void OnReset()
        {
            _currentTime = 0;
        }

        protected override void OnDeinit()
        {
            _onLerp         = null;
            _onLerpFinished = null;
        }

    #endregion
    }
}