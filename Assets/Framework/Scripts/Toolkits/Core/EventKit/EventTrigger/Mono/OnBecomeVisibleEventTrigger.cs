﻿namespace Framework.Toolkits.EventKit
{
    using System;
    using FluentAPI;
    using Framework.Core;
    using UnityEngine;

    public class OnBecomeVisibleEventTrigger : MonoBehaviour
    {
        public readonly EasyEvent OnBecameVisibleEvent = new EasyEvent();

        private void OnBecameVisible()
        {
            OnBecameVisibleEvent.Trigger();
        }
    }

    public static class OnBecameVisibleEventTriggerExtension
    {
        public static IUnRegister OnBecameVisibleEvent<T>(this T self, Action onBecameVisible)
            where T : Component
        {
            return self.GetOrAddComponent<OnBecomeVisibleEventTrigger>().OnBecameVisibleEvent
                       .Register(onBecameVisible);
        }

        public static IUnRegister OnBecameVisibleEvent(this GameObject self, Action onBecameVisible)
        {
            return self.GetOrAddComponent<OnBecomeVisibleEventTrigger>().OnBecameVisibleEvent
                       .Register(onBecameVisible);
        }
    }
}