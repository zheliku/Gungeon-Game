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

    public class Property
    {
        public BindableProperty<float> Hp = new BindableProperty<float>();
        
        public float MoveSpeed = 3;
    }
}