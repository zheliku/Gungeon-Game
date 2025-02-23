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

    public class OnMoveEventTrigger : MonoBehaviour, IMoveHandler
    {
        public readonly EasyEvent<AxisEventData> OnMoveEvent = new EasyEvent<AxisEventData>();

        public void OnMove(AxisEventData eventData)
        {
            OnMoveEvent.Trigger(eventData);
        }
    }

    public static class OnMoveEventTriggerExtension
    {
        public static IUnRegister OnMoveEvent<T>(this T self, Action<AxisEventData> onMove, int priority = 0)
            where T : Component
        {
            return self.GetOrAddComponent<OnMoveEventTrigger>().OnMoveEvent.Register(onMove, priority);
        }

        public static IUnRegister OnMoveEvent(this GameObject self, Action<AxisEventData> onMove, int priority = 0)
        {
            return self.GetOrAddComponent<OnMoveEventTrigger>().OnMoveEvent.Register(onMove, priority);
        }
    }
}