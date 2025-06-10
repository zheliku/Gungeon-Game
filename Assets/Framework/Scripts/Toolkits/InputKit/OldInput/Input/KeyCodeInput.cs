// ------------------------------------------------------------
// @file       KeyCodeInput.cs
// @brief
// @author     zheliku
// @Modified   2024-08-23 23:08:33
// @Copyright  Copyright (c) 2024, zheliku
// ------------------------------------------------------------

using System.Collections.Generic;
using Framework.Toolkits.SingletonKit;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Events;

namespace Framework.Toolkits.InputKit
{
    using System;
    using Framework.Core;

    [MonoSingletonPath("Framework/InputKit/OldInput/KeyCodeInput")]
    public class KeyCodeInput : MonoSingleton<KeyCodeInput>
    {
    #region 常量

    #endregion

    #region Static

    #endregion

    #region 字段

        [ShowInInspector] [LabelText("KeyCode Press")]
        [DictionaryDrawerSettings(KeyLabel = "KeyCode", ValueLabel = "Value")]
        private Dictionary<KeyCode, BindableKeyCodeInputProperty> _keyCodePressProperties = new Dictionary<KeyCode, BindableKeyCodeInputProperty>();

        [ShowInInspector] [LabelText("KeyCode Hold")] [PropertySpace]
        [DictionaryDrawerSettings(KeyLabel = "KeyCode", ValueLabel = "Value")]
        private Dictionary<KeyCode, BindableKeyCodeInputProperty> _keyCodeHoldProperties = new Dictionary<KeyCode, BindableKeyCodeInputProperty>();

        [ShowInInspector] [LabelText("KeyCode Release")] [PropertySpace]
        [DictionaryDrawerSettings(KeyLabel = "KeyCode", ValueLabel = "Value")]
        private Dictionary<KeyCode, BindableKeyCodeInputProperty> _keyCodeReleaseProperties = new Dictionary<KeyCode, BindableKeyCodeInputProperty>();

    #endregion

    #region 属性

    #endregion

    #region 公共方法

        public void Register(KeyCode keyCode, Action<bool, bool> action, InputType inputType)
        {
            var dic = GetMouseDic(inputType);

            if (!dic.TryGetValue(keyCode, out var value))
            {
                value = new BindableKeyCodeInputProperty();

                dic.Add(keyCode, value);
            }

            value.Register(action).UnRegisterWhenGameObjectDestroyed(Instance);
        }
        
        public void UnRegister(KeyCode keyCode, Action<bool, bool> action, InputType inputType)
        {
            var dic = GetMouseDic(inputType);

            if (dic.TryGetValue(keyCode, out var value))
            {
                value.UnRegister(action);
                
                if (value.EventCount == 0)
                {
                    dic.Remove(keyCode);
                }
            }
        }
        
        public void UnRegister(KeyCode keyCode, InputType inputType)
        {
            var dic = GetMouseDic(inputType);

            if (dic.TryGetValue(keyCode, out var value))
            {
                value.UnRegisterAll();
                dic.Remove(keyCode);
            }
        }
        
        public void UnRegister(KeyCode keyCode)
        {
            if (_keyCodePressProperties.TryGetValue(keyCode, out var value))
            {
                value.UnRegisterAll();
                _keyCodePressProperties.Remove(keyCode);
            }
            
            if (_keyCodeHoldProperties.TryGetValue(keyCode, out value))
            {
                value.UnRegisterAll();
                _keyCodeHoldProperties.Remove(keyCode);
            }
            
            if (_keyCodeReleaseProperties.TryGetValue(keyCode, out value))
            {
                value.UnRegisterAll();
                _keyCodeReleaseProperties.Remove(keyCode);
            }
        }

        public void UnRegisterAll()
        {
            foreach (var pair in _keyCodePressProperties)
            {
                pair.Value.UnRegisterAll();
            }
            
            foreach (var pair in _keyCodeHoldProperties)
            {
                pair.Value.UnRegisterAll();
            }
            
            foreach (var pair in _keyCodeReleaseProperties)
            {
                pair.Value.UnRegisterAll();
            }
            
            _keyCodePressProperties.Clear();
            _keyCodeHoldProperties.Clear();
            _keyCodeReleaseProperties.Clear();
        }

    #endregion

    #region 其他方法

        private Dictionary<KeyCode, BindableKeyCodeInputProperty> GetMouseDic(InputType inputType)
        {
            return inputType switch
            {
                InputType.Press   => _keyCodePressProperties,
                InputType.Hold    => _keyCodeHoldProperties,
                InputType.Release => _keyCodeReleaseProperties,
                _                 => throw new ArgumentOutOfRangeException(nameof(inputType), inputType, null)
            };
        }

    #endregion

    #region Unity 事件

        protected override void Update()
        {
            base.Update();

            if (!InputKit.EnableKeyCode)
            {
                return;
            }
            
            foreach (var pair in _keyCodePressProperties)
            {
                var property = pair.Value;

                property.Value = Input.GetKeyDown(pair.Key);
            }
            
            foreach (var pair in _keyCodeHoldProperties)
            {
                var property = pair.Value;

                property.Value = Input.GetKey(pair.Key);
            }
            
            foreach (var pair in _keyCodeReleaseProperties)
            {
                var property = pair.Value;

                property.Value = Input.GetKeyUp(pair.Key);
            }
        }

    #endregion
    }
}