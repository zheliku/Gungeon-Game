namespace Framework.Toolkits.EventKit
{
    using System;
    using FluentAPI;
    using Framework.Core;
    using UnityEngine;

    public class OnEnableEventTrigger : MonoBehaviour
    {
        public readonly EasyEvent OnEnableEvent = new EasyEvent();

        private void OnEnable()
        {
            OnEnableEvent.Trigger();
        }
    }

    public static class OnEnableEventTriggerExtension
    {
        public static IUnRegister OnEnableEvent<T>(this T self, Action onEnable, int priority = 0)
            where T : Component
        {
            return self.GetOrAddComponent<OnEnableEventTrigger>().OnEnableEvent
                       .Register(onEnable, priority);
        }

        public static IUnRegister OnEnableEvent(this GameObject self, Action onEnable, int priority = 0)
        {
            return self.GetOrAddComponent<OnEnableEventTrigger>().OnEnableEvent
                       .Register(onEnable, priority);
        }
    }
}