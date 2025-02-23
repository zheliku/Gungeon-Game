namespace Framework.Toolkits.EventKit
{
    using System;
    using FluentAPI;
    using Core;
    using UnityEngine;

    public class OnLateUpdateEventTrigger : MonoBehaviour
    {
        public readonly EasyEvent LateUpdateEvent = new EasyEvent();

        private void LateUpdate()
        {
            LateUpdateEvent.Trigger();
        }
    }

    public static class OnLateUpdateEventTriggerExtension
    {
        public static IUnRegister OnLateUpdateEvent<T>(this T self, Action update, int priority = 0)
            where T : Component
        {
            return self.GetOrAddComponent<OnLateUpdateEventTrigger>().LateUpdateEvent
                       .Register(update, priority);
        }

        public static IUnRegister OnLateUpdateEvent(this GameObject self, Action update, int priority = 0)
        {
            return self.GetOrAddComponent<OnLateUpdateEventTrigger>().LateUpdateEvent
                       .Register(update, priority);
        }
    }
}