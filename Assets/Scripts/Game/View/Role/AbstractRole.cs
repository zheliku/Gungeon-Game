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
        
        Collider2D Collider2D { get; }

        Vector3 Position
        {
            get => GameObject.transform.position;
        }
        
        public void Hurt(float damage, HitInfo info);
    }

    public abstract class AbstractRole : AbstractView, IRole
    {
        [HierarchyPath("Sprite")]
        public SpriteRenderer SpriteRenderer;

        [HierarchyPath]
        public Rigidbody2D Rigidbody2D;
        
        [HierarchyPath]
        public Collider2D Collider2D;

        protected virtual void Awake()
        {
            this.BindHierarchyComponent();
        }

        public GameObject GameObject { get => gameObject; }

        Collider2D IRole.Collider2D { get => Collider2D; }

        public abstract void Hurt(float damage, HitInfo info);

        protected override IArchitecture _Architecture { get => Game.Architecture; }
    }
}