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
        public static IUnRegister OnDisableEvent<T>(this T self, Action onDisable)
            where T : Component
        {
            return self.GetOrAddComponent<OnDisableEventTrigger>().OnDisableEvent
                       .Register(onDisable);
        }

        public static IUnRegister OnDisableEvent(this GameObject self, Action onDisable)
        {
            return self.GetOrAddComponent<OnDisableEventTrigger>().OnDisableEvent
                       .Register(onDisable);
        }
    }
}