// ------------------------------------------------------------
// @file       BulletHelper.cs
// @brief
// @author     zheliku
// @Modified   2025-03-12 00:03:37
// @Copyright  Copyright (c) 2025, zheliku
// ------------------------------------------------------------

namespace Game
{
    using Framework.Toolkits.FluentAPI;
    using UnityEngine;

    public class BulletHelper
    {
        public static void Shoot(Vector2 pos, Vector2 direction, GameObject bulletPrefab, float damage, float speed)
        {
            var bullet = bulletPrefab.Instantiate(pos)
               .Enable()
               .SetTransformRight(direction)
               .GetComponent<Bullet>();

            bullet.Damage   = damage;
            bullet.Velocity = direction * speed;
        }

        /// <summary>
        /// 圆圈攻击
        /// </summary>
        public static void CircleShoot(
            int        fireCount,
            Vector2    center,
            float      radius,
            GameObject bulletPrefab,
            float      damage,
            float      speed)
        {
            var angleOffset = (0f, 360f).RandomSelect();
            var stepAngle   = 360f / fireCount;

            for (int i = 0; i < fireCount; i++)
            {
                var direction = (angleOffset + stepAngle * i).Deg2Direction2D();
                var initPos   = center + direction * radius; // 中心偏移 radius 个单位
                var bullet = bulletPrefab.Instantiate(initPos)
                   .Enable()
                   .GetComponent<Bullet>();

                bullet.Damage   = damage;
                bullet.Velocity = direction * speed;
            }
        }

        /// <summary>
        /// 扩散攻击
        /// </summary>
        public static void SpreadShoot(
            int        fireCount,
            Vector2    center,
            float      radius,
            Vector2    direction,
            float      intervalAngle,
            GameObject bulletPrefab,
            float      damage,
            float      speed)
        {
            for (int i = -fireCount / 2; i <= fireCount / 2; i++)
            {
                var eachDir = direction.Rotate(i * intervalAngle);
                var initPos = center + eachDir * radius; // 中心偏移 radius 个单位

                var bullet = bulletPrefab.Instantiate(initPos)
                   .Enable()
                   .GetComponent<Bullet>();

                bullet.Damage   = damage;
                bullet.Velocity = eachDir * speed;
            }
        }
    }
}