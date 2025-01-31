// ------------------------------------------------------------
// @file       Pistol.cs
// @brief
// @author     zheliku
// @Modified   2025-01-31 16:01:18
// @Copyright  Copyright (c) 2025, zheliku
// ------------------------------------------------------------

namespace Game
{
    using System.Collections.Generic;
    using Framework.Core;
    using Framework.Toolkits.AudioKit;
    using Framework.Toolkits.EventKit;
    using Framework.Toolkits.FluentAPI;
    using UnityEngine;

    public class Pistol : AbstractView
    {
        public GameObject Bullet;

        public List<AudioClip> ShootSounds = new List<AudioClip>();

        public float BulletSpeed = 10;

        private void Awake()
        {
            Bullet = "Bullet".GetGameObjectInHierarchy(transform);
            Bullet.Disable();
        }

        public void ShootDown(Vector2 direction)
        {
            var bullet = Bullet.Instantiate(Bullet.transform.position)
               .Enable();
            var rigidbody2D = bullet.GetComponent<Rigidbody2D>();
            
            rigidbody2D.linearVelocity = direction * BulletSpeed;
            bullet.OnFixedUpdateEvent(() => { }); // todo: 需要写在这里吗？

            bullet.OnCollisionEnter2DEvent(collider2D =>
            {
                if (collider2D.gameObject.GetComponent<Enemy>())
                {
                    collider2D.gameObject.Destroy();
                }

                bullet.Destroy();
            });

            AudioKit.PlaySound(ShootSounds.RandomChoose());
        }

        public void Shooting(Vector2 direction)
        { }

        public void ShootUp(Vector2 direction)
        { }

        protected override IArchitecture Architecture { get => Game.Interface; }
    }
}