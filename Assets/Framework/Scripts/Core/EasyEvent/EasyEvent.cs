// ------------------------------------------------------------
// @file       EasyEvent.cs
// @brief
// @author     zheliku
// @Modified   2024-10-04 17:10:19
// @Copyright  Copyright (c) 2024, zheliku
// ------------------------------------------------------------

namespace Framework.Core
{
    using global::System;
    using global::System.Collections.Generic;
    using Sirenix.OdinInspector;

    [HideReferenceObjectPicker]
    public class EasyEvent : IEasyEvent
    {
        // 定义一个 Action 类型的私有变量，初始值为空
        [ShowInInspector]
        private List<Action> _onEvent = new List<Action>();
        
        public int EventCount { get => _onEvent.Count; }

        // 注册事件，返回 IUnRegister 接口
        public IUnRegister Register(Action onEvent)
        {
            _onEvent.Add(onEvent);
            return new CustomUnRegister(() => { UnRegister(onEvent); }); // 返回自定义 UnRegister 接口，用于注销事件，lambda 表达式使用了闭包
        }
        
        // 注册并调用事件
        public IUnRegister RegisterWithTrigger(Action onEvent)
        {
            onEvent?.Invoke();
            return Register(onEvent);
        }

        // 注销事件
        public void UnRegister(Action onEvent)
        {
            _onEvent.Remove(onEvent);
        }
        
        // 注销所有事件
        public void UnRegisterAll()
        {
            _onEvent.Clear();
        }

        // 触发事件
        public void Trigger()
        {
            // 使用 for 循环，防止列表更改导致的报错
            for (int i = 0; i < _onEvent.Count; i++)
            {
                Action action = _onEvent[i];
                action.Invoke();
            }
        }
    }

    [HideReferenceObjectPicker]
    public class EasyEvent<TArg> : IEasyEvent
    {
        [ShowInInspector]
        private List<Action<TArg>> _onEvent = new List<Action<TArg>>();

        public int EventCount { get => _onEvent.Count; }

        public IUnRegister Register(Action<TArg> onEvent)
        {
            _onEvent.Add(onEvent);
            return new CustomUnRegister(() => { UnRegister(onEvent); });
        }
        
        public IUnRegister RegisterWithTrigger(Action<TArg> onEvent, TArg t)
        {
            onEvent?.Invoke(t);
            return Register(onEvent);
        }

        public void UnRegister(Action<TArg> onEvent)
        {
            _onEvent.Remove(onEvent);
        }
        
        // 注销所有事件
        public void UnRegisterAll()
        {
            _onEvent.Clear();
        }

        public void Trigger(TArg t)
        {
            // 使用 for 循环，防止列表更改导致的报错
            for (int i = 0; i < _onEvent.Count; i++)
            {
                Action<TArg> action = _onEvent[i];
                action.Invoke(t);
            }
        }

        // 仅能通过 IEasyEvent 接口使用 Register(Action onEvent) 方法
        IUnRegister IEasyEvent.Register(Action onEvent)
        {
            return Register((TArg _) => onEvent());
        }
    }

    [HideReferenceObjectPicker]
    public class EasyEvent<TArg1, TArg2> : IEasyEvent
    {
        [ShowInInspector]
        private List<Action<TArg1, TArg2>> _onEvent = new List<Action<TArg1, TArg2>>();

        public int EventCount { get => _onEvent.Count; }

        public IUnRegister Register(Action<TArg1, TArg2> onEvent)
        {
            _onEvent.Add(onEvent);
            return new CustomUnRegister(() => { UnRegister(onEvent); });
        }
        
        public IUnRegister RegisterWithTrigger(Action<TArg1, TArg2> onEvent, TArg1 t1, TArg2 t2)
        {
            onEvent?.Invoke(t1, t2);
            return Register(onEvent);
        }

        public void UnRegister(Action<TArg1, TArg2> onEvent)
        {
            _onEvent.Remove(onEvent);
        }
        
        // 注销所有事件
        public void UnRegisterAll()
        {
            _onEvent.Clear();
        }

        public void Trigger(TArg1 t1, TArg2 t2)
        {
            // 使用 for 循环，防止列表更改导致的报错
            for (int i = 0; i < _onEvent.Count; i++)
            {
                Action<TArg1, TArg2> action = _onEvent[i];
                action.Invoke(t1, t2);
            }
        }

        // 仅能通过 IEasyEvent 接口使用 Register(Action onEvent) 方法
        IUnRegister IEasyEvent.Register(Action onEvent)
        {
            return Register((TArg1 _, TArg2 _) => onEvent());
        }
    }

    [HideReferenceObjectPicker]
    public class EasyEvent<TArg1, TArg2, TArg3> : IEasyEvent
    {
        [ShowInInspector]
        private List<Action<TArg1, TArg2, TArg3>> _onEvent = new List<Action<TArg1, TArg2, TArg3>>();

        public int EventCount { get => _onEvent.Count; }

        public IUnRegister Register(Action<TArg1, TArg2, TArg3> onEvent)
        {
            _onEvent.Add(onEvent);
            return new CustomUnRegister(() => { UnRegister(onEvent); });
        }
        
        public IUnRegister RegisterWithTrigger(Action<TArg1, TArg2, TArg3> onEvent, TArg1 t1, TArg2 t2, TArg3 t3)
        {
            onEvent?.Invoke(t1, t2, t3);
            return Register(onEvent);
        }

        public void UnRegister(Action<TArg1, TArg2, TArg3> onEvent)
        {
            _onEvent.Remove(onEvent);
        }
        
        // 注销所有事件
        public void UnRegisterAll()
        {
            _onEvent.Clear();
        }

        public void Trigger(TArg1 t1, TArg2 t2, TArg3 t3)
        {
            // 使用 for 循环，防止列表更改导致的报错
            for (int i = 0; i < _onEvent.Count; i++)
            {
                Action<TArg1, TArg2, TArg3> action = _onEvent[i];
                action.Invoke(t1, t2, t3);
            }
        }

        // 仅能通过 IEasyEvent 接口使用 Register(Action onEvent) 方法
        IUnRegister IEasyEvent.Register(Action onEvent)
        {
            return Register((TArg1 _, TArg2 _, TArg3 _) => onEvent());
        }
    }
}