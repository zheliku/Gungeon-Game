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
    using UnityEngine;

    public static class TimerKit
    {
        public static Timer CreateScaled(Action<Timer> onTick, float duration, int repeat = 1)
        {
            return TimerMgr.Instance.CreateTimer(onTick, duration, repeat, TimerType.Scaled);
        }
        
        public static Timer CreateUnscaled(Action<Timer> onTick, float duration, int repeat = 1)
        {
            return TimerMgr.Instance.CreateTimer(onTick, duration, repeat, TimerType.Unscaled);
        }
        
        public static Timer Create(Action<Timer> onTick, float duration, int repeat = 1, TimerType timerType = TimerType.Scaled)
        {
            return TimerMgr.Instance.CreateTimer(onTick, duration, repeat, timerType);
        }

        public static bool HasPassedInterval(int id, float interval)
        {
            var timeDict = TimerMgr.Instance.TimeDict;
            if (timeDict.TryGetValue(id, out var time))
            {
                if (time + interval <= Time.time)
                {
                    timeDict[id] = Time.time;
                    return true;
                }
                return false;
            }
            
            timeDict[id] = Time.time;
            return false;
        }

        public static bool HasPassedInterval(object id, float interval)
        {
            return HasPassedInterval(id.GetHashCode(), interval);
        }
    }
}