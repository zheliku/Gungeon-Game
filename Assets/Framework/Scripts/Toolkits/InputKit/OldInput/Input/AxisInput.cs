// ------------------------------------------------------------
// @file       InputManager.cs
// @brief
// @author     zheliku
// @Modified   2024-08-23 23:08:33
// @Copyright  Copyright (c) 2024, zheliku
// ------------------------------------------------------------

using System.Collections.Generic;
using Framework.Toolkits.SingletonKit;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Framework.Toolkits.InputKit
{
    using System;
    using Framework.Core;

    [MonoSingletonPath("Framework/InputKit/OldInput/AxisInput")]
    public class AxisInput : MonoSingleton<AxisInput>
    {
    #region 常量

    #endregion

    #region Static

        public static string Horizontal = "Horizontal";
        public static string Vertical   = "Vertical";
        public static string Jump       = "Jump";
        public static string MouseX     = "Mouse X";
        public static string MouseY     = "Mouse Y";

    #endregion

    #region 字段

        [ShowInInspector] [LabelText("Axis")] [PropertySpace]
        [DictionaryDrawerSettings(KeyLabel = "Axis", ValueLabel = "Value")]
        private Dictionary<string, BindableAxisInputProperty> _axisInputProperties = new Dictionary<string, BindableAxisInputProperty>();

        [ShowInInspector]
        private BindableTwoAxisInputProperty _horizontalAndVerticalProperty = new BindableTwoAxisInputProperty(false);

        [ShowInInspector]
        private BindableTwoAxisInputProperty _horizontalAndVerticalRawProperty = new BindableTwoAxisInputProperty(true);

    #endregion

    #region 属性

    #endregion

    #region 公共方法

        public void Register(string axisName, Action<float, float> action, bool isRaw = false)
        {
            if (!_axisInputProperties.TryGetValue(axisName, out var value))
            {
                value = new BindableAxisInputProperty(isRaw);

                _axisInputProperties[axisName] = value;
            }

            value.Register(action).UnRegisterWhenGameObjectDestroyed(Instance);
        }

        public void UnRegister(string axisName, Action<float, float> action)
        {
            if (_axisInputProperties.TryGetValue(axisName, out var value))
            {
                value.UnRegister(action);

                if (value.EventCount == 0)
                {
                    _axisInputProperties.Remove(axisName);
                }
            }
        }

        public void UnRegister(string axisName)
        {
            if (_axisInputProperties.TryGetValue(axisName, out var value))
            {
                value.UnRegisterAll();
                _axisInputProperties.Remove(axisName);
            }
        }

        public void RegisterHorizontalAndVertical(Action<Vector2, Vector2> action, bool isRaw = false)
        {
            var value = isRaw
                ? _horizontalAndVerticalRawProperty
                : _horizontalAndVerticalProperty;

            value.Register(action).UnRegisterWhenGameObjectDestroyed(Instance);
        }

        public void UnRegisterHorizontalAndVertical(Action<Vector2, Vector2> action, bool isRaw = false)
        {
            var value = isRaw
                ? _horizontalAndVerticalRawProperty
                : _horizontalAndVerticalProperty;

            value.UnRegister(action);
        }

        public void UnRegisterHorizontalAndVertical(bool isRaw)
        {
            var value = isRaw
                ? _horizontalAndVerticalRawProperty
                : _horizontalAndVerticalProperty;

            value.UnRegisterAll();
        }

        public void UnRegisterHorizontalAndVertical()
        {
            _horizontalAndVerticalRawProperty.UnRegisterAll();
            _horizontalAndVerticalProperty.UnRegisterAll();
        }

        public void UnRegisterAll()
        {
            foreach (var pair in _axisInputProperties)
            {
                pair.Value.UnRegisterAll();
            }

            _axisInputProperties.Clear();
        }

    #endregion

    #region 其他方法

    #endregion

    #region Unity 事件

        protected override void Update()
        {
            base.Update();

            if (!InputKit.EnableAxis)
            {
                return;
            }

            foreach (var pair in _axisInputProperties)
            {
                var property = pair.Value;
                property.Value = property.IsRaw ? Input.GetAxisRaw(pair.Key) : Input.GetAxis(pair.Key);
            }

            _horizontalAndVerticalProperty.Value    = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
            _horizontalAndVerticalRawProperty.Value = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        }

    #endregion
    }
}