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

    public class OnScrollEventTrigger : MonoBehaviour, IScrollHandler
    {
        public readonly EasyEvent<PointerEventData> OnScrollEvent = new EasyEvent<PointerEventData>();

        public void OnScroll(PointerEventData eventData)
        {
            OnScrollEvent.Trigger(eventData);
        }
    }

    public static class OnScrollEventTriggerExtension
    {
        public static IUnRegister OnScrollEvent<T>(this T self, Action<PointerEventData> onScroll, int priority = 0)
            where T : Component
        {
            return self.GetOrAddComponent<OnScrollEventTrigger>().OnScrollEvent.Register(onScroll, priority);
        }

        public static IUnRegister OnScrollEvent(this GameObject self, Action<PointerEventData> onScroll, int priority = 0)
        {
            return self.GetOrAddComponent<OnScrollEventTrigger>().OnScrollEvent.Register(onScroll, priority);
        }
    }
}