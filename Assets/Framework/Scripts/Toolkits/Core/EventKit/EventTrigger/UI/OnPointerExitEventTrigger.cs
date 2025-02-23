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

    public class OnPointerExitEventTrigger : MonoBehaviour, IPointerExitHandler
    {
        public readonly EasyEvent<PointerEventData> OnPointerExitEvent = new EasyEvent<PointerEventData>();

        public void OnPointerExit(PointerEventData eventData)
        {
            OnPointerExitEvent.Trigger(eventData);
        }
    }

    public static class OnPointerExitEventTriggerExtension
    {
        public static IUnRegister OnPointerExitEvent<T>(this T self, Action<PointerEventData> onPointerExit, int priority = 0)
            where T : Component
        {
            return self.GetOrAddComponent<OnPointerExitEventTrigger>().OnPointerExitEvent.Register(onPointerExit, priority);
        }

        public static IUnRegister OnPointerExitEvent(this GameObject self, Action<PointerEventData> onPointerExit, int priority = 0)
        {
            return self.GetOrAddComponent<OnPointerExitEventTrigger>().OnPointerExitEvent.Register(onPointerExit, priority);
        }
    }
}