// ------------------------------------------------------------
// @file       Bullet.cs
// @brief
// @author     zheliku
// @Modified   2025-02-22 23:02:09
// @Copyright  Copyright (c) 2025, zheliku
// ------------------------------------------------------------

using System;

namespace Game
{
    using Framework.Toolkits.AudioKit;
    using Framework.Toolkits.FluentAPI;
    using UnityEngine;

    public class RocketBullet : PlayerBullet
    {
        public void Update()
        {
            // 自动跟踪最近的敌人
            var enemy = AimHelper.GetClosestEnemy(transform, this.GetPosition2D());
            if (enemy != null)
            {
                var targetDirection = (enemy.Position - this.GetPosition()).normalized;
                var currentAngle = Velocity.ToAngle();
                var angle = Vector2.SignedAngle(Velocity, targetDirection);
                var sign = angle.Sign();
                var newAngle = currentAngle + sign * Time.deltaTime * 90; // 每秒转 90 度
                Velocity        = newAngle.Deg2Direction2D() * Velocity.magnitude;
                transform.right = Velocity;
            }
        }

        protected override void OnCollisionEnter2D(Collision2D other)
        {
            FxFactory.Instance.Explosion.Instantiate()
                .SetPosition(this.GetPosition2D())
                .EnableGameObject()
                .Damage = Damage;

            if (other.gameObject.CompareTag("Enemy"))
            {
                this.DisableGameObject();
                var enemy = other.gameObject.GetComponent<Enemy>();
                enemy.Hurt(Damage, other.ToHitInfo());
                AudioKit.PlaySound(HitEnemySounds.RandomTakeOne(), 0.5f, onPlayFinish: _ =>
                {
                    this.DestroyGameObjectGracefully(); // 可能重复删除
                });
            }
            else if (other.gameObject.CompareTag("Boss"))
            {
                this.DisableGameObject();
                var enemy = other.gameObject.GetComponent<Boss>();
                enemy.Hurt(Damage, other.ToHitInfo());
                AudioKit.PlaySound(HitEnemySounds.RandomTakeOne(), 0.5f, onPlayFinish: _ =>
                {
                    this.DestroyGameObjectGracefully(); // 可能重复删除
                });
            }
            else if (other.gameObject.CompareTag("Wall"))
            {
                this.DisableGameObject();
                AudioKit.PlaySound(HitWallSounds.RandomTakeOne(), 0.5f, onPlayFinish: _ =>
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