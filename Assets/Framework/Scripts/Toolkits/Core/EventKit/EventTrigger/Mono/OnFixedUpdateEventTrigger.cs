namespace Framework.Toolkits.EventKit
{
    using System;
    using FluentAPI;
    using Core;
    using UnityEngine;

    public class OnFixedUpdateEventTrigger : MonoBehaviour
    {
        public readonly EasyEvent FixedUpdateEvent = new EasyEvent();

        private void FixedUpdate()
        {
            FixedUpdateEvent.Trigger();
        }
    }

    public static class OnFixedUpdateEventTriggerExtension
    {
        public static IUnRegister OnFixedUpdateEvent<T>(this T self, Action update, int priority = 0)
            where T : Component
        {
            return self.GetOrAddComponent<OnFixedUpdateEventTrigger>().FixedUpdateEvent
                       .Register(update, priority);
        }

        public static IUnRegister OnFixedUpdateEvent(this GameObject self, Action update, int priority = 0)
        {
            return self.GetOrAddComponent<OnFixedUpdateEventTrigger>().FixedUpdateEvent
                       .Register(update, priority);
        }
    }
}