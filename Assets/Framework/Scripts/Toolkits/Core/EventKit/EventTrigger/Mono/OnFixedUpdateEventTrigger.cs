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
        public static IUnRegister OnFixedUpdateEvent<T>(this T self, Action update)
            where T : Component
        {
            return self.GetOrAddComponent<OnFixedUpdateEventTrigger>().FixedUpdateEvent
                       .Register(update);
        }

        public static IUnRegister OnFixedUpdateEvent(this GameObject self, Action update)
        {
            return self.GetOrAddComponent<OnFixedUpdateEventTrigger>().FixedUpdateEvent
                       .Register(update);
        }
    }
}