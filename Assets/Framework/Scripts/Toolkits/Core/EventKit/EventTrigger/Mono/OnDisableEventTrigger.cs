namespace Framework.Toolkits.EventKit
{
    using System;
    using FluentAPI;
    using Framework.Core;
    using UnityEngine;

    public class OnDisableEventTrigger : MonoBehaviour
    {
        public readonly EasyEvent OnDisableEvent = new EasyEvent();

        private void OnDisable()
        {
            OnDisableEvent.Trigger();
        }
    }

    public static class OnDisableEventTriggerExtension
    {
        public static IUnRegister OnDisableEvent<T>(this T self, Action onDisable, int priority = 0)
            where T : Component
        {
            return self.GetOrAddComponent<OnDisableEventTrigger>().OnDisableEvent
                       .Register(onDisable, priority);
        }

        public static IUnRegister OnDisableEvent(this GameObject self, Action onDisable, int priority = 0)
        {
            return self.GetOrAddComponent<OnDisableEventTrigger>().OnDisableEvent
                       .Register(onDisable, priority);
        }
    }
}