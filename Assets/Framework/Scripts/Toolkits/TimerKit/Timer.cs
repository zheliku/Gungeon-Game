// ------------------------------------------------------------
// @file       AudioTimer.cs
// @brief
// @author     zheliku
// @Modified   2024-11-14 15:11:52
// @Copyright  Copyright (c) 2024, zheliku
// ------------------------------------------------------------

namespace Framework.Toolkits.TimerKit
{
    using System;
    using System.Collections.Generic;
    using PoolKit;
    using Sirenix.OdinInspector;
    using UnityEngine;

    public enum TimerType
    {
        Scaled,
        Unscaled
    }

    [HideReferenceObjectPicker]
    public class Timer : IPoolable, IPoolType
    {
    #region Static

        public static Timer Spawn(Action<Timer> onTick, float duration, int repeatCount = 1, TimerType timerType = TimerType.Scaled)
        {
            var timer = SingletonObjectPool<Timer>.Instance.Get();
            timer.Enabled      = true;
            timer.TickCount    = 0;
            timer._onTickAction = onTick;
            timer.DelayTime     = duration;
            timer.RepeatCount  = repeatCount;
            timer.TimerType    = timerType;
            timer.CreateTime   = timer.CurrentTime;
            timer.LastTickTime = timer.CurrentTime;

            return timer;
        }

    #endregion

    #region 字段

        [ShowInInspector]
        private Action<Timer> _onTickAction; // Timer 的触发事件

        private bool _paused;

        // [ShowInInspector]
        private float _pausedProgress;

    #endregion

    #region 属性

        [ShowInInspector]
        public bool Enabled { get; private set; } = true;

        // [ShowInInspector]
        public bool IsInPool { get; set; }

        /// <summary>
        /// 延迟时间，即 Timer 的持续时间
        /// </summary>
        [ShowInInspector]
        public float DelayTime { get; set; }

        /// <summary>
        /// 创造时间
        /// </summary>
        // [ShowInInspector]
        public float CreateTime { get; private set; }

        /// <summary>
        /// 上一次触发的时间
        /// </summary>
        // [ShowInInspector]
        [ShowInInspector]
        public float LastTickTime { get; private set; }

        /// <summary>
        /// 下一次将要触发的时间
        /// </summary>
        // [ShowInInspector]
        [ShowInInspector]
        public float TargetTime { get => LastTickTime + DelayTime; }

        [ShowInInspector]
        [ProgressBar(nameof(LastTickTime), nameof(TargetTime))]
        public float CurrentTime
        {
            get => TimerType == TimerType.Scaled
                ? Time.time
                : Time.unscaledTime;
        }

        /// <summary>
        /// Timer 类型
        /// </summary>
        [ShowInInspector]
        public TimerType TimerType { get; private set; }

        /// <summary>
        /// 是否循环
        /// </summary>
        [ShowInInspector]
        public bool Loop { get => RepeatCount < 0; }

        [ShowInInspector]
        public bool Paused
        {
            get
            {
                return _paused;
            }
            set
            {
                
                if (value)
                {
                    _pausedProgress = CurrentTime - LastTickTime;
                }
                else
                {
                    LastTickTime = CurrentTime - _pausedProgress;
                }
            }
        }

        /// <summary>
        /// 已触发次数
        /// </summary>
        [ShowInInspector] 
        public int TickCount { get; private set; }

        /// <summary>
        /// 循环次数 <br/>
        /// 小于 0 : 无限循环 <br/>
        /// = 0 : 被回收 <br/>
        /// 大于 0 : 循环次数
        /// </summary>
        [ShowInInspector]
        public int RepeatCount { get; private set; }

    #endregion

    #region 公共方法

        public void Tick()
        {
            ++TickCount;
            _onTickAction?.Invoke(this);
        }

        public void Cancel()
        {
            if (Enabled)
            {
                Enabled      = false;
                _onTickAction = null;
            }
        }

        public bool TryRepeat()
        {
            if (RepeatCount < 0 || TickCount < RepeatCount)
            {
                LastTickTime += DelayTime;
                return true;
            }

            return false;
        }

        public void OnGet() { }

        public void OnRelease()
        {
            _onTickAction = null;
            DelayTime     = 0;
            RepeatCount  = 0;
            TickCount    = 0;
            Enabled      = false;
        }

        public void OnDestroy() { }


        public void RecycleToCache()
        {
            SingletonObjectPool<Timer>.Instance.Release(this);
        }

    #endregion
    }

    public class TimerComparer : IComparer<Timer>
    {
        public int Compare(Timer x, Timer y)
        {
            if (ReferenceEquals(x, y)) return 0;
            if (y is null) return 1;
            if (x is null) return -1;

            // if (x.Paused && !y.Paused) return 1;
            // if (!x.Paused && y.Paused) return -1;
            return x.TargetTime.CompareTo(y.TargetTime);
        }
    }
}