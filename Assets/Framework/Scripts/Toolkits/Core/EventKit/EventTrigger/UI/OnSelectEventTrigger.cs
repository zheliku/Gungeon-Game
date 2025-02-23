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

    public class OnSelectEventTrigger : MonoBehaviour, ISelectHandler
    {
        public readonly EasyEvent<BaseEventData> OnSelectEvent = new EasyEvent<BaseEventData>();

        public void OnSelect(BaseEventData eventData)
        {
            OnSelectEvent.Trigger(eventData);
        }
    }

    public static class OnSelectEventTriggerTriggerExtension
    {
        public static IUnRegister OnSelectEvent<T>(this T self, Action<BaseEventData> onSelect, int priority = 0)
            where T : Component
        {
            return self.GetOrAddComponent<OnSelectEventTrigger>().OnSelectEvent.Register(onSelect, priority);
        }

        public static IUnRegister OnSelectEvent(this GameObject self, Action<BaseEventData> onSelect, int priority = 0)
        {
            return self.GetOrAddComponent<OnSelectEventTrigger>().OnSelectEvent.Register(onSelect, priority);
        }
    }
}