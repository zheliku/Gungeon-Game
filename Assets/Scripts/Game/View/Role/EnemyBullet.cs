// ------------------------------------------------------------
// @file       EnemyBullet.cs
// @brief
// @author     zheliku
// @Modified   2025-03-09 12:37:44
// @Copyright  Copyright (c) 2025, zheliku
// ------------------------------------------------------------

namespace Game
{
    using System;
    using Framework.Core;
    using Framework.Toolkits.FluentAPI;
    using UnityEngine;

    public class EnemyBullet : AbstractView
    {
        public float Damage;

        [HierarchyPath]
        public Rigidbody2D Rigidbody;

        public Vector2 Velocity
        {
            get => Rigidbody.linearVelocity;
            set => Rigidbody.linearVelocity = value;
        }

        private void Awake()
        {
            this.BindHierarchyComponent();
        }

        private void OnCollisionEnter2D(Collision2D other)
        {
            var player = other.gameObject.GetComponent<Player>();
            if (player)
            {
                player.Hurt(Damage, other.ToHitInfo());
            }

            this.DestroyGameObject();
        }

        protected override IArchitecture _Architecture { get => Game.Interface; }
    }
}