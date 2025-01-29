// ------------------------------------------------------------
// @file       PlayerPrefsIntProperty.cs
// @brief
// @author     zheliku
// @Modified   2024-11-14 09:11:20
// @Copyright  Copyright (c) 2024, zheliku
// ------------------------------------------------------------

namespace Framework.Toolkits.BindableKit
{
    using Framework.Core;
    using UnityEngine;

    public class PlayerPrefsIntProperty : BindableProperty<int>
    {
        public PlayerPrefsIntProperty(string saveKey, int defaultValue = 0)
        {
            _value = PlayerPrefs.GetInt(saveKey, defaultValue);
            
            Register((oldValue, value) => PlayerPrefs.SetInt(saveKey, value));
        }
    }
}