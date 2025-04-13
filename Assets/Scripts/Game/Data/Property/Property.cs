// ------------------------------------------------------------
// @file       Property.cs
// @brief
// @author     zheliku
// @Modified   2025-01-30 20:01:11
// @Copyright  Copyright (c) 2025, zheliku
// ------------------------------------------------------------

namespace Game
{
    using Framework.Core;
    using UnityEngine;

    public class Property
    {
        public BindableProperty<float> Hp = new BindableProperty<float>();
        
        public BindableProperty<float> MaxHp = new BindableProperty<float>();
        
        public float MoveSpeed = 3;
        
        public bool IsFullHp
        {
            get => Mathf.Approximately(Hp.Value, MaxHp.Value);
        }
    }
}