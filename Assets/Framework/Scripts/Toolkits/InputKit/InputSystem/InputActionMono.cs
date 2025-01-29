// ------------------------------------------------------------
// @file       InputKitAction.cs
// @brief
// @author     zheliku
// @Modified   2024-12-26 23:12:47
// @Copyright  Copyright (c) 2024, zheliku
// ------------------------------------------------------------

using UnityEngine;

namespace Framework.Toolkits.InputKit
{
    using System;
    using System.Collections.Generic;
    using Sirenix.OdinInspector;
    using UnityEngine.InputSystem;

    /// <summary>
    /// 用于在 Inspector 中显示 InputAction 的 Mono 对象
    /// </summary>
    [HideReferenceObjectPicker]
    public class InputActionMono : MonoBehaviour
    {
        [ShowInInspector]
        public List<Action<InputAction.CallbackContext>> StartedActions = new List<Action<InputAction.CallbackContext>>();

        [ShowInInspector]
        public List<Action<InputAction.CallbackContext>> PerformedActions = new List<Action<InputAction.CallbackContext>>();
        
        [ShowInInspector]
        public List<Action<InputAction.CallbackContext>> CanceledActions = new List<Action<InputAction.CallbackContext>>();
    }
}
