// ------------------------------------------------------------
// @file       BindableKeyCodeProperty.cs
// @brief
// @author     zheliku
// @Modified   2024-12-03 14:12:20
// @Copyright  Copyright (c) 2024, zheliku
// ------------------------------------------------------------

namespace Framework.Toolkits.InputKit
{
    using Framework.Core;

    public class BindableKeyCodeInputProperty : BindableProperty<bool>
    {
        public BindableKeyCodeInputProperty() : base(false, true)
        { }
    }
}