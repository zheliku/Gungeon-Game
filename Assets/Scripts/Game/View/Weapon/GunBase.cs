// ------------------------------------------------------------
// @file       GunBase.cs
// @brief
// @author     zheliku
// @Modified   2025-06-21 05:35:30
// @Copyright  Copyright (c) 2025, zheliku
// ------------------------------------------------------------

namespace Game
{
    using System;
    using Framework.Core;
    using Framework.Toolkits.UIKit;
    using UnityEngine;

    public class GunBase : AbstractView
    {
        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag("Player"))
            {
                UIKit.ShowPanelAsync<UIGunList>();
            }
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            if (other.CompareTag("Player"))
            {
                UIKit.HidePanel<UIGunList>();
            }
        }

        protected override IArchitecture _Architecture { get => Game.Architecture; }
    }
}