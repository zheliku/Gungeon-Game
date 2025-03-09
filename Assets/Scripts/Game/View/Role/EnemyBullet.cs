// ------------------------------------------------------------
// @file       EnemyBullet.cs
// @brief
// @author     zheliku
// @Modified   2025-03-09 12:37:44
// @Copyright  Copyright (c) 2025, zheliku
// ------------------------------------------------------------

namespace Game
{
    using Framework.Core;
    using Framework.Toolkits.FluentAPI;
    using UnityEngine;

    public class EnemyBullet : AbstractView
    {
        public float Damage;

        private void OnCollisionEnter2D(Collision2D other)
        {
            var player = other.gameObject.GetComponent<Player>();
            if (player)
            {
                player.Hurt(Damage);
            }

            this.DestroyGameObject();
        }
        
        protected override IArchitecture _Architecture { get => Game.Interface; }
    }
}