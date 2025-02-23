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

    public class OnApplicationFocusEventTrigger : MonoBehaviour
    {
        public readonly EasyEvent OnApplicationFocusEvent = new EasyEvent();

        public readonly EasyEvent OnApplicationUnFocusEvent = new EasyEvent();

        public bool IsFocused;

        private void OnApplicationFocus(bool focusStatus)
        {
            if (IsFocused == focusStatus)
            {
                return;
            }

            if (focusStatus)
            {
                OnApplicationFocusEvent.Trigger();
            }
            else
            {
                OnApplicationUnFocusEvent.Trigger();
            }
            
            IsFocused = focusStatus;
        }
    }

    public static class OnApplicationFocusEventTriggerExtension
    {
        public static IUnRegister OnApplicationFocusEvent<T>(this T self, Action onApplicationFocus, int priority = 0)
            where T : Component
        {
            return self.GetOrAddComponent<OnApplicationFocusEventTrigger>().OnApplicationFocusEvent
               .Register(onApplicationFocus, priority);
        }

        public static IUnRegister OnApplicationFocusEvent(this GameObject self, Action onApplicationFocus, int priority = 0)
        {
            return self.GetOrAddComponent<OnApplicationFocusEventTrigger>().OnApplicationFocusEvent
               .Register(onApplicationFocus, priority);
        }
        
        public static IUnRegister OnApplicationUnFocusEvent<T>(this T self, Action onApplicationUnFocus, int priority = 0)
            where T : Component
        {
            return self.GetOrAddComponent<OnApplicationFocusEventTrigger>().OnApplicationUnFocusEvent
               .Register(onApplicationUnFocus, priority);
        }

        public static IUnRegister OnApplicationUnFocusEvent(this GameObject self, Action onApplicationUnFocus, int priority = 0)
        {
            return self.GetOrAddComponent<OnApplicationFocusEventTrigger>().OnApplicationUnFocusEvent
               .Register(onApplicationUnFocus, priority);
        }
    }
}