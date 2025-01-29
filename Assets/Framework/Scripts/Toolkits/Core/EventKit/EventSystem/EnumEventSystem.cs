// ------------------------------------------------------------
// @file       EnumEventSystem.cs
// @brief
// @author     zheliku
// @Modified   2024-10-23 16:10:34
// @Copyright  Copyright (c) 2024, zheliku
// ------------------------------------------------------------

namespace Framework.Toolkits.EventKit
{
    using System;
    using System.Collections.Generic;
    using FluentAPI;
    using Framework.Core;
    using Sirenix.OdinInspector;

    public class EnumEventSystem
    {
        public static readonly EnumEventSystem GLOBAL = new EnumEventSystem();

        [ShowInInspector]
        private readonly Dictionary<Enum, IEasyEvent> _events = new Dictionary<Enum, IEasyEvent>(50);

        protected EnumEventSystem() { }

        public IUnRegister Register<TEnum>(TEnum key, Action<TEnum, object[]> onEvent) where TEnum : Enum
        {
            if (_events.TryGetValue(key, out var e))
            {
                var easyEvent = e.As<EasyEvent<TEnum, object[]>>();
                return easyEvent.Register(onEvent);
            }
            else
            {
                var easyEvent = new EasyEvent<TEnum, object[]>();
                _events.Add(key, easyEvent);
                return easyEvent.Register(onEvent);
            }
        }

        public bool UnRegister<TEnum>(TEnum key, Action<TEnum, object[]> onEvent) where TEnum : Enum
        {
            if (_events.TryGetValue(key, out var e))
            {
                e.As<EasyEvent<TEnum, object[]>>()?.UnRegister(onEvent);
                return true;
            }
            
            return false;
        }

        public bool UnRegister<TEnum>(TEnum key) where TEnum : Enum
        {
            return _events.Remove(key);
        }

        public void UnRegisterAll()
        {
            _events.Clear();
        }

        public void Send<TEnum>(TEnum key, params object[] args) where TEnum : Enum
        {
            if (_events.TryGetValue(key, out var e))
            {
                e.As<EasyEvent<TEnum, object[]>>().Trigger(key, args);
            }
        }
    }
}