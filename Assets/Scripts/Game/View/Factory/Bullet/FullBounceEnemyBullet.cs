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

    public class FullBounceEnemyBullet : Bullet
    {
        public int ReflectCount = 3;

        public Color SpriteColor
        {
            get => GetComponent<SpriteRenderer>().color;
            set => GetComponent<SpriteRenderer>().color = value;
        }

        private void OnCollisionEnter2D(Collision2D other)
        {
            var player = other.gameObject.GetComponent<Player>();
            if (player)
            {
                player.Hurt(Damage, other.ToHitInfo());
                this.DestroyGameObject();
            }

            ReflectCount--;
            if (ReflectCount < 0)
            {
                this.DestroyGameObject();
            }
        }

        protected override IArchitecture _Architecture { get => Game.Architecture; }
    }
}