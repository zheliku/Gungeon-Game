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

    public class OnSubmitEventTrigger : MonoBehaviour, ISubmitHandler
    {
        public readonly EasyEvent<BaseEventData> OnSubmitEvent = new EasyEvent<BaseEventData>();

        public void OnSubmit(BaseEventData eventData)
        {
            OnSubmitEvent.Trigger(eventData);
        }
    }

    public static class OnSubmitEventTriggerExtension
    {
        public static IUnRegister OnSubmitEvent<T>(this T self, Action<BaseEventData> onSubmit, int priority = 0)
            where T : Component
        {
            return self.GetOrAddComponent<OnSubmitEventTrigger>().OnSubmitEvent.Register(onSubmit, priority);
        }

        public static IUnRegister OnSubmitEvent(this GameObject self, Action<BaseEventData> onSubmit, int priority = 0)
        {
            return self.GetOrAddComponent<OnSubmitEventTrigger>().OnSubmitEvent.Register(onSubmit, priority);
        }
    }
}