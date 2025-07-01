// ------------------------------------------------------------
// @file       Bullet.cs
// @brief
// @author     zheliku
// @Modified   2025-02-22 23:02:09
// @Copyright  Copyright (c) 2025, zheliku
// ------------------------------------------------------------

namespace Game
{
    using Framework.Toolkits.AudioKit;
    using Framework.Toolkits.FluentAPI;
    using UnityEngine;

    public class GunBullet : PlayerBullet
    {
        protected override void OnCollisionEnter2D(Collision2D other)
        {
            if (other.gameObject.CompareTag("Enemy"))
            {
                this.DisableGameObject();
                var enemy = other.gameObject.GetComponent<Enemy>();
                enemy.Hurt(Damage, other.ToHitInfo());
                AudioKit.PlaySound(HitEnemySounds.RandomTakeOne(), 0.3f, onPlayFinish: _ =>
                {
                    this.DestroyGameObjectGracefully(); // 可能重复删除
                });
            }
            else if (other.gameObject.CompareTag("Boss"))
            {
                this.DisableGameObject();
                var enemy = other.gameObject.GetComponent<Boss>();
                enemy.Hurt(Damage, other.ToHitInfo());
                AudioKit.PlaySound(HitEnemySounds.RandomTakeOne(), 0.3f, onPlayFinish: _ =>
                {
                    this.DestroyGameObjectGracefully(); // 可能重复删除
                });
            }
            else if (other.gameObject.CompareTag("Wall"))
            {
                this.DisableGameObject();
                AudioKit.PlaySound(HitWallSounds.RandomTakeOne(), 0.3f, onPlayFinish: _ =>
                {
                    this.DestroyGameObjectGracefully(); // 可能重复删除
                });
            }
            
            this.DestroyGameObjectGracefully();
        }
    }
}