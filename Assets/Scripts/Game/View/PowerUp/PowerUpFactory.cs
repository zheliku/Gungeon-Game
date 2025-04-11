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
    using UnityEngine.Serialization;

    public class PowerUpFactory : MonoSingleton<PowerUpFactory>
    {
        [HierarchyPath("Coin")]
        public Coin Coin;
        
        [HierarchyPath("Armor1")]
        public Armor1 Armor1;
        
        [HierarchyPath("AllGunHalfBullet")]
        public AllGunHalfBullet AllGunHalfBullet;
        
        [HierarchyPath("SingleGunFullBullet")]
        public SingleGunFullBullet SingleGunFullBullet;

        private void Awake()
        {
            this.BindHierarchyComponent();
        }

        protected override IArchitecture _Architecture { get => Game.Interface; }
    }
}