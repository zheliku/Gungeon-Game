// ------------------------------------------------------------
// @file       Bullet.cs
// @brief
// @author     zheliku
// @Modified   2025-02-22 23:02:09
// @Copyright  Copyright (c) 2025, zheliku
// ------------------------------------------------------------

using Framework.Core;

namespace Game
{
    using Framework.Toolkits.AudioKit;
    using Framework.Toolkits.FluentAPI;
    using UnityEngine;

    public class ArrowBullet : PlayerBullet
    {
        [HierarchyPath("HalfArrow")]
        public Transform HalfArrow;

        protected override void OnCollisionEnter2D(Collision2D other)
        {
            var otherGo = other.gameObject;

            if (otherGo.CompareTag("Enemy"))
            {
                HalfArrow.SetParent(otherGo)
                    .SetPosition(this.GetPosition2D() - other.GetContact(0).normal * 0.05f)
                    .EnableGameObject();

                this.DisableGameObject();
                var enemy = otherGo.GetComponent<Enemy>();
                enemy.Hurt(Damage, other.ToHitInfo());
                AudioKit.PlaySound(HitEnemySounds.RandomTakeOne(), 0.3f, onPlayFinish: _ =>
                {
                    this.DestroyGameObjectGracefully(); // 可能重复删除
                });
            }
            else if (otherGo.CompareTag("Boss"))
            {
                HalfArrow.SetParent(otherGo)
                    .SetPosition(this.GetPosition2D() - other.GetContact(0).normal * 0.05f)
                    .SetLocalScaleIdentity()
                    .EnableGameObject();

                this.DisableGameObject();
                var enemy = otherGo.GetComponent<Boss>();
                enemy.Hurt(Damage, other.ToHitInfo());
                AudioKit.PlaySound(HitEnemySounds.RandomTakeOne(), 0.3f, onPlayFinish: _ =>
                {
                    this.DestroyGameObjectGracefully(); // 可能重复删除
                });
            }
            else if (otherGo.CompareTag("Wall"))
            {
                HalfArrow.EnableGameObject()
                    .SetParent(null);

                this.DisableGameObject();
                AudioKit.PlaySound(HitWallSounds.RandomTakeOne(), 0.3f, onPlayFinish: _ =>
                {
                    this.DestroyGameObjectGracefully(); // 可能重复删除
                });
            }
            else
            {
                this.DestroyGameObjectGracefully();
            }
        }
    }
}