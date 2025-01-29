// ------------------------------------------------------------
// @file       BindableMouseInputProperty.cs
// @brief
// @author     zheliku
// @Modified   2024-12-03 15:12:05
// @Copyright  Copyright (c) 2024, zheliku
// ------------------------------------------------------------

namespace Framework.Toolkits.InputKit
{
    using Core;
    
    public enum MouseInputType
    {
        Left,
        Right,
        Middle
    }

    public class BindableMouseInputProperty : BindableProperty<bool>
    {
        public BindableMouseInputProperty() : base(false, true)
        { }
    }
}