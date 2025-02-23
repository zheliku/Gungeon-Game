/****************************************************************************
 * Copyright (c) 2016 - 2023 liangxiegame UNDER MIT License
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
    using UnityEngine.EventSystems;

    public class OnCancelEventTrigger : MonoBehaviour, ICancelHandler
    {
        public readonly EasyEvent<BaseEventData> OnCancelEvent = new EasyEvent<BaseEventData>();

        public void OnCancel(BaseEventData eventData)
        {
            OnCancelEvent.Trigger(eventData);
        }
    }

    public static class OnCancelEventTriggerExtension
    {
        public static IUnRegister OnCancelEvent<T>(this T self, Action<BaseEventData> onCancel, int priority = 0)
            where T : Component
        {
            return self.GetOrAddComponent<OnCancelEventTrigger>().OnCancelEvent.Register(onCancel, priority);
        }

        public static IUnRegister OnCancelEvent(this GameObject self, Action<BaseEventData> onCancel, int priority = 0)
        {
            return self.GetOrAddComponent<OnCancelEventTrigger>().OnCancelEvent.Register(onCancel, priority);
        }
    }
}