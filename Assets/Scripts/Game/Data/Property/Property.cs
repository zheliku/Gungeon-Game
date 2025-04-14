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
        public BindableProperty<int> Hp = new BindableProperty<int>();
        
        public BindableProperty<int> MaxHp = new BindableProperty<int>();
        
        public float MoveSpeed = 3;
        
        public bool IsFullHp
        {
            get => Mathf.Approximately(Hp.Value, MaxHp.Value);
        }
    }
}