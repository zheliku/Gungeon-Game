namespace Framework.Toolkits.EventKit
{
    using System;
    using FluentAPI;
    using Core;
    using UnityEngine;

    public class OnUpdateEventTrigger : MonoBehaviour
    {
        public readonly EasyEvent UpdateEvent = new EasyEvent();

        private void Update()
        {
            UpdateEvent.Trigger();
        }
    }

    public static class OnUpdateEventTriggerExtension
    {
        public static IUnRegister OnUpdateEvent<T>(this T self, Action update)
            where T : Component
        {
            return self.GetOrAddComponent<OnUpdateEventTrigger>().UpdateEvent
                       .Register(update);
        }

        public static IUnRegister OnUpdateEvent(this GameObject self, Action update)
        {
            return self.GetOrAddComponent<OnUpdateEventTrigger>().UpdateEvent
                       .Register(update);
        }
    }
}