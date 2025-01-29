// ------------------------------------------------------------
// @file       OrEvent.cs
// @brief
// @author     zheliku
// @Modified   2024-10-05 11:10:01
// @Copyright  Copyright (c) 2024, zheliku
// ------------------------------------------------------------

namespace Framework.Core
{
    using global::System;
    using global::System.Collections.Generic;

    /// <summary>
    /// 级联事件
    /// </summary>
    public sealed class OrEvent : IUnRegisterList
    {
        public List<IUnRegister> UnregisterList { get; } = new List<IUnRegister>(); // 待注销列表

        private Action _onEvent = () => { }; // OrEvent 事件

        /// <summary>
        /// 绑定 EasyEvent
        /// </summary>
        /// <param name="easyEvent">IEasyEvent 实例</param>
        /// <returns>OrEvent 自身</returns>
        public OrEvent Or(IEasyEvent easyEvent)
        {
            easyEvent.Register(Trigger)          // 给 easyEvent 绑定 OrEvent 自己的事件
                     .AddToUnregisterList(this); // 登记注销
            return this;
        }

        /// <summary>
        /// 注册事件
        /// </summary>
        /// <param name="onEvent">事件</param>
        /// <returns>注销器</returns>
        public IUnRegister Register(Action onEvent)
        {
            _onEvent += onEvent;
            return new CustomUnRegister(() => { UnRegister(onEvent); });
        }
        
        /// <summary>
        /// 注册并触发一次事件
        /// </summary>
        /// <param name="onEvent">事件</param>
        /// <returns>注销器</returns>
        public IUnRegister RegisterWithTrigger(Action onEvent)
        {
            onEvent?.Invoke();
            return Register(onEvent);
        }

        /// <summary>
        /// 注销事件
        /// </summary>
        /// <param name="onEvent">事件</param>
        public void UnRegister(Action onEvent)
        {
            _onEvent -= onEvent;
            this.UnRegisterAll();
        }

        private void Trigger()
        {
            _onEvent?.Invoke();
        }
    }
}