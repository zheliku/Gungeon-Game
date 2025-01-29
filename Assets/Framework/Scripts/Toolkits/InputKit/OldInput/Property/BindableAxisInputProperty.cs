// ------------------------------------------------------------
// @file       BindableAxisProperty.cs
// @brief
// @author     zheliku
// @Modified   2024-12-03 14:12:21
// @Copyright  Copyright (c) 2024, zheliku
// ------------------------------------------------------------

namespace Framework.Toolkits.InputKit
{
    using Framework.Core;
    using UnityEngine;

    public class BindableAxisInputProperty : BindableProperty<float>
    {
        public bool IsRaw;
        
        public BindableAxisInputProperty(bool isRaw) : base(0, true)
        {
            IsRaw = isRaw;
        }
    }
    
    public class BindableTwoAxisInputProperty : BindableProperty<Vector2>
    {
        public bool IsRaw;
        
        public BindableTwoAxisInputProperty(bool isRaw) : base(Vector2.zero, true)
        {
            IsRaw = isRaw;
        }
    }
}