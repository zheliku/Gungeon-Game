// ------------------------------------------------------------
// @file       AudioTimer.cs
// @brief
// @author     zheliku
// @Modified   2024-11-14 14:11:30
// @Copyright  Copyright (c) 2024, zheliku
// ------------------------------------------------------------

namespace Framework.Toolkits.TimerKit
{
    using System;
    using System.Collections.Generic;
    using PoolKit;
    using SingletonKit;
    using Sirenix.OdinInspector;
    using UnityEngine;

    [MonoSingletonPath("Framework/TimerKit/TimerManager")]
    public class TimerManager : MonoSingleton<TimerManager>
    {
    #region 字段

        [ShowInInspector]
        private readonly List<Timer> _timers = new List<Timer>();

        [ShowInInspector]
        public float ScaleTime
        {
            get => Time.time;
        }

        [ShowInInspector]
        public float UnScaledTime
        {
            get => Time.unscaledTime;
        }

    #endregion

        [ShowInInspector]
        public SingletonObjectPool<Timer> TimerPool { get => SingletonObjectPool<Timer>.Instance; }
        
        [ShowInInspector]
        public Dictionary<string, float> TimeDict { get; } = new Dictionary<string, float>();

    #region 公共方法

        public Timer CreateTimer(Action<Timer> onTick, float duration, int repeat = 1, TimerType timerType = TimerType.Scaled)
        {
            var timer = Timer.Spawn(onTick, duration, repeat, timerType);
            _timers.Add(timer);
            return timer;
        }

    #endregion

    #region Unity 事件

        protected override void Update()
        {
            base.Update();

            for (int i = _timers.Count - 1; i >= 0; i--)
            {
                var timer = _timers[i];

                if (!timer.Enabled)
                {
                    _timers.Remove(timer);
                    timer.RecycleToCache();
                    continue;
                }

                if (timer.Paused)
                {
                    continue;
                }

                if (timer.TargetTime <= timer.CurrentTime)
                {
                    timer.Tick();

                    if (!timer.TryRepeat())
                    {
                        _timers.Remove(timer);
                        timer.RecycleToCache();
                    }
                }
            }
        }

    #endregion
    }
}