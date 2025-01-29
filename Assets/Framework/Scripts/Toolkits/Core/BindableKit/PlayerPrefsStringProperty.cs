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

    public class PlayerPrefsStringProperty : BindableProperty<string>
    {
        public PlayerPrefsStringProperty(string saveKey, string defaultValue = "")
        {
            _value = PlayerPrefs.GetString(saveKey, defaultValue);
            
            Register((oldValue, value) => PlayerPrefs.SetString(saveKey, value));
        }
    }
}