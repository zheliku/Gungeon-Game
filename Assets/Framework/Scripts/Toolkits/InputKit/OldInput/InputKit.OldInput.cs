// ------------------------------------------------------------
// @file       InputKit.cs
// @brief
// @author     zheliku
// @Modified   2024-12-03 14:12:30
// @Copyright  Copyright (c) 2024, zheliku
// ------------------------------------------------------------

namespace Framework.Toolkits.InputKit
{
    using System;
    using UnityEngine;
    using UnityEngine.InputSystem;

    public enum InputType
    {
        Press,
        Hold,
        Release
    }

    public partial class InputKit
    {
        public static bool EnableAxis { get; set; } = true;

        public static bool EnableMouse { get; set; } = true;

        public static bool EnableKeyCode { get; set; } = true;

        public static bool EnableAll
        {
            get => EnableKeyCode && EnableMouse && EnableAxis;
            set
            {
                EnableKeyCode = value;
                EnableMouse   = value;
                EnableAxis    = value;
            }
        }

        public static void RegisterAxis(string axisName, Action<float, float> action)
        {
            AxisInput.Instance.Register(axisName, action);
        }

        public static void RegisterHorizontalAndVertical(Action<Vector2, Vector2> action, bool isRaw = false)
        {
            AxisInput.Instance.RegisterHorizontalAndVertical(action, isRaw);
        }

        public static void UnRegisterAxis(string axisName, Action<float, float> action)
        {
            AxisInput.Instance.UnRegister(axisName, action);
        }

        public static void UnRegisterAxis(string axisName)
        {
            AxisInput.Instance.UnRegister(axisName);
        }

        public static void UnRegisterAxisAll()
        {
            AxisInput.Instance.UnRegisterAll();
        }

        public void UnRegisterHorizontalAndVertical(Action<Vector2, Vector2> action, bool isRaw = false)
        {
            AxisInput.Instance.UnRegisterHorizontalAndVertical(action, isRaw);
        }

        public void UnRegisterHorizontalAndVertical(bool isRaw)
        {
            AxisInput.Instance.UnRegisterHorizontalAndVertical(isRaw);
        }

        public void UnRegisterHorizontalAndVertical()
        {
            AxisInput.Instance.UnRegisterHorizontalAndVertical();
        }

        public static void RegisterMouse(MouseInputType mouseType, Action<bool, bool> action, InputType inputType = InputType.Press)
        {
            MouseInput.Instance.Register(mouseType, action, inputType);
        }

        public static void UnRegisterMouse(MouseInputType mouseType, Action<bool, bool> action, InputType inputType = InputType.Press)
        {
            MouseInput.Instance.UnRegister(mouseType, action, inputType);
        }

        public static void UnRegisterMouse(MouseInputType mouseType, InputType inputType)
        {
            MouseInput.Instance.UnRegister(mouseType, inputType);
        }

        public static void UnRegisterMouse(MouseInputType mouseType)
        {
            MouseInput.Instance.UnRegister(mouseType);
        }

        public static void UnRegisterMouseAll()
        {
            MouseInput.Instance.UnRegisterAll();
        }

        public static void RegisterKeyCode(KeyCode keyCode, Action<bool, bool> action, InputType inputType = InputType.Press)
        {
            KeyCodeInput.Instance.Register(keyCode, action, inputType);
        }

        public static void UnRegisterKeyCode(KeyCode keyCode, Action<bool, bool> action, InputType inputType = InputType.Press)
        {
            KeyCodeInput.Instance.UnRegister(keyCode, action, inputType);
        }

        public static void UnRegisterKeyCode(KeyCode keyCode, InputType inputType)
        {
            KeyCodeInput.Instance.UnRegister(keyCode, inputType);
        }

        public static void UnRegisterKeyCode(KeyCode keyCode)
        {
            KeyCodeInput.Instance.UnRegister(keyCode);
        }

        public static void UnRegisterKeyCodeAll()
        {
            KeyCodeInput.Instance.UnRegisterAll();
        }
    }
}