// ------------------------------------------------------------
// @file       Camera.cs
// @brief
// @author     zheliku
// @Modified   2025-01-29 16:01:35
// @Copyright  Copyright (c) 2025, zheliku
// ------------------------------------------------------------

using UnityEngine;

namespace Game.View
{
    using System;
    using Framework.Core;
    using Framework.Core.View;
    using Framework.Toolkits.FluentAPI;

    public class Camera : AbstractView
    {
        public Player Player;

        private void Update()
        {
            transform.position = Player.GetPosition().Set(z: -1);
        }

        protected override IArchitecture Architecture { get => Game.Interface; }
    }
}
