/****************************************************************************
 * Copyright (c) 2015 - 2023 liangxiegame UNDER MIT License
 *
 * https://qframework.cn
 * https://github.com/liangxiegame/QFramework
 * https://gitee.com/liangxiegame/QFramework
 ****************************************************************************/

namespace Framework.Toolkits.EventKit
{
    using System;
    using FluentAPI;
    using Core;
    using UnityEngine;

    public class OnApplicationPauseEventTrigger : MonoBehaviour
    {
        public readonly EasyEvent OnApplicationPauseEvent = new EasyEvent();

        public readonly EasyEvent OnApplicationUnPauseEvent = new EasyEvent();

        public bool IsPaused;

        private void OnApplicationPause(bool pauseStatus)
        {
            if (IsPaused == pauseStatus)
            {
                return;
            }

            if (pauseStatus)
            {
                OnApplicationPauseEvent.Trigger();
            }
            else
            {
                OnApplicationUnPauseEvent.Trigger();
            }

            IsPaused = pauseStatus;
        }
    }

    public static class OnApplicationPauseEventTriggerExtension
    {
        public static IUnRegister OnApplicationPauseEvent<T>(this T self, Action onApplicationPause)
            where T : Component
        {
            return self.GetOrAddComponent<OnApplicationPauseEventTrigger>().OnApplicationPauseEvent
               .Register(onApplicationPause);
        }

        public static IUnRegister OnApplicationPauseEvent(this GameObject self, Action onApplicationPause)
        {
            return self.GetOrAddComponent<OnApplicationPauseEventTrigger>().OnApplicationPauseEvent
               .Register(onApplicationPause);
        }

        public static IUnRegister OnApplicationUnPauseEvent<T>(this T self, Action onApplicationUnPause)
            where T : Component
        {
            return self.GetOrAddComponent<OnApplicationPauseEventTrigger>().OnApplicationUnPauseEvent
               .Register(onApplicationUnPause);
        }

        public static IUnRegister OnApplicationUnPauseEvent(this GameObject self, Action onApplicationUnPause)
        {
            return self.GetOrAddComponent<OnApplicationPauseEventTrigger>().OnApplicationUnPauseEvent
               .Register(onApplicationUnPause);
        }
    }
}