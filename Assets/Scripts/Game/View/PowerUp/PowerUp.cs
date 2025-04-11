// ------------------------------------------------------------
// @file       PowUp.cs
// @brief
// @author     zheliku
// @Modified   2025-04-11 15:04:24
// @Copyright  Copyright (c) 2025, zheliku
// ------------------------------------------------------------

namespace Game
{
    using System;
    using Framework.Core;
    using UnityEngine;

    public abstract class PowerUp : AbstractView, IPowerUp
    {
        public SpriteRenderer SpriteRenderer { get => this.GetComponent<SpriteRenderer>(); }

        public Room Room { get; set; }

        protected virtual void Awake()
        {
            this.BindHierarchyComponent();
        }

        protected override IArchitecture _Architecture { get => Game.Interface; }
    }
}