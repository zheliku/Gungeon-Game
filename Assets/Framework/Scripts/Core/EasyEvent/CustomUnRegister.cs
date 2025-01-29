// ------------------------------------------------------------
// @file       CustomUnRegister.cs
// @brief
// @author     zheliku
// @Modified   2024-10-04 16:10:04
// @Copyright  Copyright (c) 2024, zheliku
// ------------------------------------------------------------

namespace Framework.Core
{
    using System;
    using Sirenix.OdinInspector;

    /// <summary>
    /// 自定义注销器
    /// </summary>
    [HideReferenceObjectPicker]
    public struct CustomUnRegister : IUnRegister
    {
        [ShowInInspector]
        private Action _onUnRegister;

        public CustomUnRegister(Action onUnRegister)
        {
            _onUnRegister = onUnRegister;
        }

        public void UnRegister()
        {
            _onUnRegister?.Invoke();
            _onUnRegister = null;
        }
    }
}