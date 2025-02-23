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

    public class OnEndDragEventTrigger : MonoBehaviour, IEndDragHandler
    {
        public readonly EasyEvent<PointerEventData> OnEndDragEvent = new EasyEvent<PointerEventData>();

        public void OnEndDrag(PointerEventData eventData)
        {
            OnEndDragEvent.Trigger(eventData);
        }
    }

    public static class OnEndDragEventTriggerExtension
    {
        public static IUnRegister OnEndDragEvent<T>(this T self, Action<PointerEventData> onEndDrag, int priority = 0)
            where T : Component
        {
            return self.GetOrAddComponent<OnEndDragEventTrigger>().OnEndDragEvent.Register(onEndDrag, priority);
        }

        public static IUnRegister OnEndDragEvent(this GameObject self, Action<PointerEventData> onEndDrag, int priority = 0)
        {
            return self.GetOrAddComponent<OnEndDragEventTrigger>().OnEndDragEvent.Register(onEndDrag, priority);
        }
    }
}