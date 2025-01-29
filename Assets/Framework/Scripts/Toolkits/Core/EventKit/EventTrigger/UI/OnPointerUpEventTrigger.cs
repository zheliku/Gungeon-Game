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

    public class OnPointerUpEventTrigger : MonoBehaviour, IPointerUpHandler
    {
        public readonly EasyEvent<PointerEventData> OnPointerUpEvent = new EasyEvent<PointerEventData>();

        public void OnPointerUp(PointerEventData eventData)
        {
            OnPointerUpEvent.Trigger(eventData);
        }
    }

    public static class OnPointerUpEventTriggerExtension
    {
        public static IUnRegister OnPointerUpEvent<T>(this T self, Action<PointerEventData> onPointerUpEvent)
            where T : Component
        {
            return self.GetOrAddComponent<OnPointerUpEventTrigger>().OnPointerUpEvent
                       .Register(onPointerUpEvent);
        }

        public static IUnRegister OnPointerUpEvent(this GameObject self, Action<PointerEventData> onPointerUpEvent)
        {
            return self.GetOrAddComponent<OnPointerUpEventTrigger>().OnPointerUpEvent
                       .Register(onPointerUpEvent);
        }
    }
}