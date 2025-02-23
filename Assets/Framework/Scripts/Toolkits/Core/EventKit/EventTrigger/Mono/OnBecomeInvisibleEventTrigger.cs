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
    using Framework.Core;
    using UnityEngine;

    public class OnBecomeInvisibleEventTrigger : MonoBehaviour
    {
        public readonly EasyEvent OnBecameInvisibleEvent = new EasyEvent();

        private void OnBecameInvisible()
        {
            OnBecameInvisibleEvent.Trigger();
        }
    }

    public static class OnBecameInvisibleEventTriggerExtension
    {
        public static IUnRegister OnBecameInvisibleEvent<T>(this T self, Action onBecameInvisible, int priority = 0)
            where T : Component
        {
            return self.GetOrAddComponent<OnBecomeInvisibleEventTrigger>().OnBecameInvisibleEvent
                       .Register(onBecameInvisible, priority);
        }

        public static IUnRegister OnBecameInvisibleEvent(this GameObject self, Action onBecameInvisible, int priority = 0)
        {
            return self.GetOrAddComponent<OnBecomeInvisibleEventTrigger>().OnBecameInvisibleEvent
                       .Register(onBecameInvisible, priority);
        }
    }
}