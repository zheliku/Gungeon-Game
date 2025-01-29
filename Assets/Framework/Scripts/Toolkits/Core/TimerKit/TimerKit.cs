// ------------------------------------------------------------
// @file       TimerKit.cs
// @brief
// @author     zheliku
// @Modified   2024-11-14 16:11:34
// @Copyright  Copyright (c) 2024, zheliku
// ------------------------------------------------------------

namespace Framework.Toolkits.TimerKit
{
    using System;

    public static class TimerKit
    {
        public static Timer CreateScaled(Action<Timer> onTick, float duration, int repeat = 1)
        {
            return TimerManager.Instance.CreateTimer(onTick, duration, repeat, TimerType.Scaled);
        }
        
        public static Timer CreateUnscaled(Action<Timer> onTick, float duration, int repeat = 1)
        {
            return TimerManager.Instance.CreateTimer(onTick, duration, repeat, TimerType.Unscaled);
        }
        
        public static Timer Create(Action<Timer> onTick, float duration, int repeat = 1, TimerType timerType = TimerType.Scaled)
        {
            return TimerManager.Instance.CreateTimer(onTick, duration, repeat, timerType);
        }
    }
}