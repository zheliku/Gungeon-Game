// ------------------------------------------------------------
// @file       PlayerPrefsBoolProperty.cs
// @brief
// @author     zheliku
// @Modified   2024-11-14 09:11:09
// @Copyright  Copyright (c) 2024, zheliku
// ------------------------------------------------------------

namespace Framework.Toolkits.BindableKit
{
    using Framework.Core;
    using UnityEngine;

    public class PlayerPrefsBoolProperty : BindableProperty<bool>
    {
        public PlayerPrefsBoolProperty(string saveKey, bool defaultValue = false)
        {
            var defaultIntValue = defaultValue ? 1 : 0;
            _value = PlayerPrefs.GetInt(saveKey, defaultIntValue) == 1;
            
            Register((oldValue, value) => PlayerPrefs.SetInt(saveKey, value ? 1 : 0));
        }
    }
}