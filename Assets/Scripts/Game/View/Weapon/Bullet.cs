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

    public class Bullet : AbstractView
    {
        public float Damage;

        private void Awake()
        {
            
        }

        private void OnCollisionEnter2D(Collision2D other)
        {
            var enemy = other.gameObject.GetComponent<Enemy>();
            if (enemy)
            {
                enemy.Hurt(Damage);
            }

            this.DestroyGameObject();
        }

        protected override IArchitecture _Architecture { get => Game.Interface; }
    }
}