// ------------------------------------------------------------
// @file       PowerUpFactory.cs
// @brief
// @author     zheliku
// @Modified   2025-03-14 12:15:56
// @Copyright  Copyright (c) 2025, zheliku
// ------------------------------------------------------------

namespace Game
{
    using System;
    using Framework.Core;
    using Framework.Toolkits.SingletonKit;

    public class PowerUpFactory : MonoSingleton<PowerUpFactory>
    {
        [HierarchyPath("Coin")]
        public Coin Coin;

        private void Awake()
        {
            this.BindHierarchyComponent();
        }

        protected override IArchitecture _Architecture { get => Game.Interface; }
    }
}