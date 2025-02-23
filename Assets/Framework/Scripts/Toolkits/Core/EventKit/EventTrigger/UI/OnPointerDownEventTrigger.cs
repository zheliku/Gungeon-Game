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

    public class OnPointerDownEventTrigger : MonoBehaviour, IPointerDownHandler
    {
        public readonly EasyEvent<PointerEventData> OnPointerDownEvent = new EasyEvent<PointerEventData>();

        public void OnPointerDown(PointerEventData eventData)
        {
            OnPointerDownEvent.Trigger(eventData);
        }
    }

    public static class OnPointerDownEventTriggerExtension
    {
        public static IUnRegister OnPointerDownEvent<T>(this T self, Action<PointerEventData> onPointerDownEvent, int priority = 0)
            where T : Component
        {
            return self.GetOrAddComponent<OnPointerDownEventTrigger>().OnPointerDownEvent
                       .Register(onPointerDownEvent, priority);
        }

        public static IUnRegister OnPointerDownEvent(this GameObject self, Action<PointerEventData> onPointerDownEvent, int priority = 0)
        {
            return self.GetOrAddComponent<OnPointerDownEventTrigger>().OnPointerDownEvent
                       .Register(onPointerDownEvent, priority);
        }
    }
}