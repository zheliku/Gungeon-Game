// ------------------------------------------------------------
// @file       PlayerPrefsFloatProperty.cs
// @brief
// @author     zheliku
// @Modified   2024-11-14 09:11:12
// @Copyright  Copyright (c) 2024, zheliku
// ------------------------------------------------------------

namespace Framework.Toolkits.BindableKit
{
    using Framework.Core;
    using UnityEngine;

    public class PlayerPrefsFloatProperty : BindableProperty<float>
    {
        public PlayerPrefsFloatProperty(string saveKey, float defaultValue = 0f)
        {
            _value = PlayerPrefs.GetFloat(saveKey, defaultValue);
            
            Register((oldValue, value) => PlayerPrefs.SetFloat(saveKey, value));
        }
    }
}