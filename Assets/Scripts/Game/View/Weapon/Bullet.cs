// ------------------------------------------------------------
// @file       Bullet.cs
// @brief
// @author     zheliku
// @Modified   2025-03-14 12:03:25
// @Copyright  Copyright (c) 2025, zheliku
// ------------------------------------------------------------

namespace Game
{
    using Framework.Core;
    using UnityEngine;

    public abstract class Bullet : AbstractView
    {
        public float Damage;

        public Rigidbody2D Rigidbody; // 不用 HierarchyPath，因为有些子弹没有 Rigidbody

        public Vector2 Velocity
        {
            get => Rigidbody.linearVelocity;
            set => Rigidbody.linearVelocity = value;
        }

        protected virtual void Awake()
        {
            Rigidbody = GetComponent<Rigidbody2D>();
        }
        
        protected override IArchitecture _Architecture { get => Game.Interface; }
    }
}