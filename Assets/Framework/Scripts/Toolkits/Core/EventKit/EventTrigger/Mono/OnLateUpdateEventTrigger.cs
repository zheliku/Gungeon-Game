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
        public static IUnRegister OnLateUpdateEvent<T>(this T self, Action update)
            where T : Component
        {
            return self.GetOrAddComponent<OnLateUpdateEventTrigger>().LateUpdateEvent
                       .Register(update);
        }

        public static IUnRegister OnLateUpdateEvent(this GameObject self, Action update)
        {
            return self.GetOrAddComponent<OnLateUpdateEventTrigger>().LateUpdateEvent
                       .Register(update);
        }
    }
}