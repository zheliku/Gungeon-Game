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
    using UnityEngine;

    public interface IRole
    {
        GameObject GameObject { get; }

        Vector3 Position
        {
            get => GameObject.transform.position;
        }
        
        public void Hurt(float damage, HitInfo info);
    }

    public abstract class AbstractRole : AbstractView
    {
        [HierarchyPath("Sprite")]
        public SpriteRenderer SpriteRenderer;

        [HierarchyPath]
        public Rigidbody2D Rigidbody2D;

        protected virtual void Awake()
        {
            this.BindHierarchyComponent();
        }

        public abstract void Hurt(float damage, HitInfo info);

        protected override IArchitecture _Architecture { get => Game.Architecture; }
    }
}