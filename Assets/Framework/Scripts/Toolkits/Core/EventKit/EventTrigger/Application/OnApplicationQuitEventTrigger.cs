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

    public class OnApplicationQuitEventTrigger : MonoBehaviour
    {
        public readonly EasyEvent OnApplicationQuitEvent = new EasyEvent();

        private void OnApplicationQuit()
        {
            OnApplicationQuitEvent.Trigger();
        }
    }

    public static class OnApplicationQuitEventTriggerExtension
    {
        public static IUnRegister OnApplicationQuitEventEvent<T>(this T self, Action onApplicationQuitEvent)
            where T : Component
        {
            return self.GetOrAddComponent<OnApplicationQuitEventTrigger>().OnApplicationQuitEvent
                       .Register(onApplicationQuitEvent);
        }

        public static IUnRegister OnApplicationQuitEventEvent(this GameObject self, Action onApplicationQuitEvent)
        {
            return self.GetOrAddComponent<OnApplicationQuitEventTrigger>().OnApplicationQuitEvent
                       .Register(onApplicationQuitEvent);
        }
    }
}