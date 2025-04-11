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
    using Sirenix.OdinInspector;
    using UnityEngine;

    public abstract class PowerUp : AbstractView, IPowerUp
    {
        public SpriteRenderer SpriteRenderer { get => this.GetComponent<SpriteRenderer>(); }

        private Room _room;

        [ShowInInspector]
        public Room Room
        {
            get => _room;
            set
            {
                _room = value;
                _room.PowerUps.Add(this);
            }
        }

        protected virtual void Awake()
        {
            this.BindHierarchyComponent();
        }

        protected override IArchitecture _Architecture { get => Game.Architecture; }
    }
}