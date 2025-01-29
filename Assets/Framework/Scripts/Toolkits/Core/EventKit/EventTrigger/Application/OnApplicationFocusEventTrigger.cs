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
        public static IUnRegister OnApplicationFocusEvent<T>(this T self, Action onApplicationFocus)
            where T : Component
        {
            return self.GetOrAddComponent<OnApplicationFocusEventTrigger>().OnApplicationFocusEvent
               .Register(onApplicationFocus);
        }

        public static IUnRegister OnApplicationFocusEvent(this GameObject self, Action onApplicationFocus)
        {
            return self.GetOrAddComponent<OnApplicationFocusEventTrigger>().OnApplicationFocusEvent
               .Register(onApplicationFocus);
        }
        
        public static IUnRegister OnApplicationUnFocusEvent<T>(this T self, Action onApplicationUnFocus)
            where T : Component
        {
            return self.GetOrAddComponent<OnApplicationFocusEventTrigger>().OnApplicationUnFocusEvent
               .Register(onApplicationUnFocus);
        }

        public static IUnRegister OnApplicationUnFocusEvent(this GameObject self, Action onApplicationUnFocus)
        {
            return self.GetOrAddComponent<OnApplicationFocusEventTrigger>().OnApplicationUnFocusEvent
               .Register(onApplicationUnFocus);
        }
    }
}