// ------------------------------------------------------------
// @file       Bullet.cs
// @brief
// @author     zheliku
// @Modified   2025-02-22 23:02:09
// @Copyright  Copyright (c) 2025, zheliku
// ------------------------------------------------------------

namespace Game
{
    using Framework.Core;
    using Framework.Toolkits.AudioKit;
    using Framework.Toolkits.FluentAPI;
    using UnityEngine;

    public class PlayerBullet : Bullet
    {
        private void OnCollisionEnter2D(Collision2D other)
        {
            Debug.Log(other.gameObject.tag);
            
            if (other.gameObject.CompareTag("Enemy"))
            {
                this.DisableGameObject();
                var enemy = other.gameObject.GetComponent<Enemy>();
                enemy.Hurt(Damage, other.ToHitInfo());
                var soundClip = BulletFactory.Instance.HitEnemySounds.RandomTakeOne();
                AudioKit.PlaySound(soundClip, 0.3f, onPlayFinish: _ =>
                {
                    this.DestroyGameObjectGracefully(); // 可能重复删除
                });
            }
            else if (other.gameObject.CompareTag("Boss"))
            {
                this.DisableGameObject();
                var enemy = other.gameObject.GetComponent<Boss>();
                enemy.Hurt(Damage, other.ToHitInfo());
                var soundClip = BulletFactory.Instance.HitEnemySounds.RandomTakeOne();
                AudioKit.PlaySound(soundClip, 0.3f, onPlayFinish: _ =>
                {
                    this.DestroyGameObjectGracefully(); // 可能重复删除
                });
            }
            else if (other.gameObject.CompareTag("Wall"))
            {
                this.DisableGameObject();
                var soundClip = BulletFactory.Instance.HitWallSounds.RandomTakeOne();
                AudioKit.PlaySound(soundClip, 0.3f, onPlayFinish: _ =>
                {
                    this.DestroyGameObjectGracefully(); // 可能重复删除
                });
            }
            
            this.DestroyGameObjectGracefully();
        }

        protected override IArchitecture _Architecture { get => Game.Architecture; }
    }
}