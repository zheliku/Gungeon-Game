// ------------------------------------------------------------
// @file       Bullet.cs
// @brief
// @author     zheliku
// @Modified   2025-02-22 23:02:09
// @Copyright  Copyright (c) 2025, zheliku
// ------------------------------------------------------------

namespace Game
{
    using System;
    using Framework.Core;
    using Framework.Toolkits.FluentAPI;
    using UnityEngine;

    public class PlayerBullet : AbstractView
    {
        public float Damage;
        
        public Rigidbody2D Rigidbody; // 不用 HierarchyPath，因为有些子弹没有 Rigidbody

        public Vector2 Velocity
        {
            get => Rigidbody.linearVelocity;
            set => Rigidbody.linearVelocity = value;
        }

        private void Awake()
        {
            Rigidbody = GetComponent<Rigidbody2D>();
        }

        private void OnCollisionEnter2D(Collision2D other)
        {
            var enemy = other.gameObject.GetComponent<IEnemy>();
            if (enemy != null)
            {
                enemy.Hurt(Damage);
            }

            this.DestroyGameObject();
        }

        protected override IArchitecture _Architecture { get => Game.Interface; }
    }
}