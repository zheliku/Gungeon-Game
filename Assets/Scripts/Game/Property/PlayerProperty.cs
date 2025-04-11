// ------------------------------------------------------------
// @file       PlayerProperty.cs
// @brief
// @author     zheliku
// @Modified   2025-04-11 11:04:04
// @Copyright  Copyright (c) 2025, zheliku
// ------------------------------------------------------------

namespace Game
{
    using Framework.Core;

    public class PlayerProperty : Property
    {
        public BindableProperty<int> Armor = new BindableProperty<int>();
    }
}