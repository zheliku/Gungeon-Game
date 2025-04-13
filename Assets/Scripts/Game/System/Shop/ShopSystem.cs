// ------------------------------------------------------------
// @file       ShopSystem.cs
// @brief
// @author     zheliku
// @Modified   2025-04-13 13:04:46
// @Copyright  Copyright (c) 2025, zheliku
// ------------------------------------------------------------

namespace Game
{
    using System;
    using System.Collections.Generic;
    using Framework.Core;
    using Framework.Toolkits.FluentAPI;

    public class ShopSystem : AbstractSystem
    {
        public List<Tuple<IPowerUp, int>> CalculateNormalShopItems()
        {
            var normalShopItem = new List<Tuple<IPowerUp, int>>()
            {
                new(PowerUpFactory.Instance.Armor1, (3, 6 + 1).RandomSelect()),
                new(PowerUpFactory.Instance.Hp1, (3, 6 + 1).RandomSelect()),
                new(PowerUpFactory.Instance.SingleGunFullBullet, (15, 20 + 1).RandomSelect()),
                new(PowerUpFactory.Instance.AllGunHalfBullet, (15, 25 + 1).RandomSelect()),
                new(PowerUpFactory.Instance.Key, (5, 10 + 1).RandomSelect()),
            };

            return normalShopItem;
        }

        protected override void OnInit()
        { }
    }
}