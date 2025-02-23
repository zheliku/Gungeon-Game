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

    public class OnDragEventTrigger : MonoBehaviour, IDragHandler
    {
        public readonly EasyEvent<PointerEventData> OnDragEvent = new EasyEvent<PointerEventData>();

        public void OnDrag(PointerEventData eventData)
        {
            OnDragEvent.Trigger(eventData);
        }
    }

    public static class OnDragEventTriggerExtension
    {
        public static IUnRegister OnDragEvent<T>(this T self, Action<PointerEventData> onDrag, int priority = 0)
            where T : Component
        {
            return self.GetOrAddComponent<OnDragEventTrigger>().OnDragEvent.Register(onDrag, priority);
        }

        public static IUnRegister OnDragEvent(this GameObject self, Action<PointerEventData> onDrag, int priority = 0)
        {
            return self.GetOrAddComponent<OnDragEventTrigger>().OnDragEvent.Register(onDrag, priority);
        }
    }
}