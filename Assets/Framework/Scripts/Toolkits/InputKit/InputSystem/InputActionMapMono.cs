// ------------------------------------------------------------
// @file       InputActionMap.cs
// @brief
// @author     zheliku
// @Modified   2024-12-26 22:12:12
// @Copyright  Copyright (c) 2024, zheliku
// ------------------------------------------------------------

using UnityEngine;

namespace Framework.Toolkits.InputKit
{
    using Sirenix.OdinInspector;
    using UnityEngine.InputSystem;

    /// <summary>
    /// 用于在 Inspector 中显示 InputAction 的 MapMono 对象
    /// </summary>
    [HideReferenceObjectPicker]
    public class InputActionMapMono : MonoBehaviour
    {
        public void Init(InputActionMap map)
        {
            Map ??= map;
        }

        [ShowInInspector] [HideReferenceObjectPicker]
        public InputActionMap Map { get; private set; }
    }
}