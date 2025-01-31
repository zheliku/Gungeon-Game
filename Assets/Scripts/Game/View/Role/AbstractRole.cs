// ------------------------------------------------------------
// @file       Role.cs
// @brief
// @author     zheliku
// @Modified   2025-01-31 14:01:01
// @Copyright  Copyright (c) 2025, zheliku
// ------------------------------------------------------------

namespace Game
{
    using Framework.Core;
    using Framework.Toolkits.FluentAPI;
    using UnityEngine;

    public abstract class AbstractRole : AbstractView
    {
        public SpriteRenderer SpriteRenderer;
        
        public Rigidbody2D Rigidbody2D;


        protected virtual void Awake()
        {
            SpriteRenderer = "Sprite".GetComponentInHierarchy<SpriteRenderer>(transform);
            Rigidbody2D    = GetComponent<Rigidbody2D>();
        }

        public abstract void Hurt(float damage);

        protected override IArchitecture Architecture { get => Game.Interface; }
    }
}