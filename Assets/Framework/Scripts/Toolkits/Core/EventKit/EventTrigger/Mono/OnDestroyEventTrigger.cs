namespace Framework.Toolkits.EventKit
{
    using System;
    using FluentAPI;
    using Framework.Core;
    using UnityEngine;

    public class OnDestroyEventTrigger : MonoBehaviour
    {
        public readonly EasyEvent OnDestroyEvent = new EasyEvent();

        private void OnDestroy()
        {
            OnDestroyEvent.Trigger();
        }
    }

    public static class OnDestroyEventTriggerExtension
    {
        public static IUnRegister OnDestroyEvent<T>(this T self, Action onDestroy)
            where T : Component
        {
            return self.GetOrAddComponent<OnDestroyEventTrigger>().OnDestroyEvent
                       .Register(onDestroy);
        }

        public static IUnRegister OnDestroyEvent(this GameObject self, Action onDestroy)
        {
            return self.GetOrAddComponent<OnDestroyEventTrigger>().OnDestroyEvent
                       .Register(onDestroy);
        }
    }
}