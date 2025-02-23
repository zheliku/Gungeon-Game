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

    public class OnPointerEnterEventTrigger : MonoBehaviour, IPointerEnterHandler
    {
        public readonly EasyEvent<PointerEventData> OnPointerEnterEvent = new EasyEvent<PointerEventData>();

        public void OnPointerEnter(PointerEventData eventData)
        {
            OnPointerEnterEvent.Trigger(eventData);
        }
    }

    public static class OnPointerEnterEventTriggerExtension
    {
        public static IUnRegister OnPointerEnterEvent<T>(this T self, Action<PointerEventData> onPointerEnter, int priority = 0)
            where T : Component
        {
            return self.GetOrAddComponent<OnPointerEnterEventTrigger>().OnPointerEnterEvent.Register(onPointerEnter, priority);
        }

        public static IUnRegister OnPointerEnterEvent(this GameObject self, Action<PointerEventData> onPointerEnter, int priority = 0)
        {
            return self.GetOrAddComponent<OnPointerEnterEventTrigger>().OnPointerEnterEvent.Register(onPointerEnter, priority);
        }
    }
}