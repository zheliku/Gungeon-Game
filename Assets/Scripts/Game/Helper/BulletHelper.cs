// ------------------------------------------------------------
// @file       BulletHelper.cs
// @brief
// @author     zheliku
// @Modified   2025-03-12 00:03:37
// @Copyright  Copyright (c) 2025, zheliku
// ------------------------------------------------------------

namespace Game
{
    using System.Collections.Generic;
    using Framework.Toolkits.FluentAPI;
    using UnityEngine;

    public class BulletHelper
    {
        public static Bullet Shoot(
            Vector2 pos,
            Vector2 direction,
            Bullet bullet,
            float damage,
            float speed,
            float unstableAngle = 0)
        {
            var shootAngle = direction.ToAngle() + (-unstableAngle, unstableAngle).RandomSelect();
            var unstableDir = shootAngle.Deg2Direction2D();

            bullet = bullet.Instantiate(pos)
                .EnableGameObject()
                .SetTransformRight(unstableDir);

            bullet.Damage   = damage;
            bullet.Velocity = unstableDir * speed;

            return bullet;
        }

        /// <summary>
        /// 圆圈攻击
        /// </summary>
        public static List<Bullet> CircleShoot(
            int fireCount,
            Vector2 center,
            float radius,
            float angleOffset,
            Bullet bullet,
            float damage,
            float speed,
            float unstableAngle = 0)
        {
            var stepAngle = 360f / fireCount;

            var bullets = new List<Bullet>(fireCount);

            for (int i = 0; i < fireCount; i++)
            {
                var direction = (angleOffset + stepAngle * i).Deg2Direction2D();
                var shootAngle = direction.ToAngle() + (-unstableAngle, unstableAngle).RandomSelect();
                var unstableDir = shootAngle.Deg2Direction2D();

                var initPos = center + unstableDir * radius; // 中心偏移 radius 个单位
                bullet = bullet.Instantiate(initPos)
                    .EnableGameObject();

                bullet.Damage   = damage;
                bullet.Velocity = unstableDir * speed;

                bullets.Add(bullet);
            }

            return bullets;
        }

        /// <summary>
        /// 扩散攻击
        /// </summary>
        public static List<Bullet> SpreadShoot(
            int fireCount,
            Vector2 center,
            float radius,
            Vector2 direction,
            float intervalAngle,
            Bullet bullet,
            float damage,
            float speed,
            float unstableAngle = 0)
        {
            var bullets = new List<Bullet>(fireCount);

            for (int i = -fireCount / 2; i <= fireCount / 2; i++)
            {
                var eachDir = direction.Rotate(i * intervalAngle);
                var shootAngle = eachDir.ToAngle() + (-unstableAngle, unstableAngle).RandomSelect();
                var unstableDir = shootAngle.Deg2Direction2D();

                var initPos = center + unstableDir * radius; // 中心偏移 radius 个单位

                bullet = bullet.Instantiate(initPos)
                    .EnableGameObject();

                bullet.Damage   = damage;
                bullet.Velocity = unstableDir * speed;

                bullets.Add(bullet);
            }

            return bullets;
        }

        /// <summary>
        /// 圆圈攻击
        /// </summary>
        public static List<Bullet> FocusShoot(
            int fireCount,
            Vector2 center,
            float radius,
            float angleOffset,
            Bullet bullet,
            float damage,
            float speed,
            float unstableAngle = 0)
        {
            var stepAngle = 360f / fireCount;

            var bullets = new List<Bullet>(fireCount);

            for (int i = 0; i < fireCount; i++)
            {
                var direction = (angleOffset + stepAngle * i).Deg2Direction2D();
                var shootAngle = direction.ToAngle() + (-unstableAngle, unstableAngle).RandomSelect();
                var unstableDir = shootAngle.Deg2Direction2D();

                var initPos = center + unstableDir * radius; // 中心偏移 radius 个单位
                bullet = bullet.Instantiate(initPos)
                    .EnableGameObject();

                bullet.Damage   = damage;
                bullet.Velocity = -unstableDir * speed;

                bullets.Add(bullet);
            }

            return bullets;
        }
    }
}