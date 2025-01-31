// ------------------------------------------------------------
// @file       Enemy.cs
// @brief
// @author     zheliku
// @Modified   2025-01-29 16:01:14
// @Copyright  Copyright (c) 2025, zheliku
// ------------------------------------------------------------

namespace Game
{
    using System;
    using System.Collections.Generic;
    using Framework.Core;
    using Framework.Toolkits.AudioKit;
    using Framework.Toolkits.EventKit;
    using Framework.Toolkits.FluentAPI;
    using UnityEngine;
    using Random = UnityEngine.Random;

    public class Enemy : AbstractRole
    {
        public enum States
        {
            Follow,
            Shoot
        }

        [HierarchyPath("Bullet")]
        public GameObject Bullet;

        public States State;

        public float FollowSeconds = 3;

        public float CurrentSeconds = 0;

        public List<AudioClip> ShootSounds = new List<AudioClip>();

        private EnemyModel _enemyModel;

        private Property _Property = new Property() { Hp = { Value = 2 } };

        protected override void Awake()
        {
            base.Awake();

            _enemyModel = this.GetModel<EnemyModel>();
            
            _enemyModel.Enemies.Add(this);

            Bullet.Disable();

            _Property.Hp.Register((oldValue, value) =>
            {
                if (value <= 0)
                {
                    this.DestroyGameObject();
                }
            }).UnRegisterWhenGameObjectDestroyed(gameObject);
        }

        private void Update()
        {
            var player = Player.Instance;

            var directionToPlayer = player.Direction2DFrom(transform);

            if (directionToPlayer.x > 0)
            {
                SpriteRenderer.flipX = false;
            }
            else if (directionToPlayer.x < 0)
            {
                SpriteRenderer.flipX = true;
            }

            if (State == States.Follow)
            {
                if (CurrentSeconds >= FollowSeconds)
                {
                    State          = States.Shoot;
                    CurrentSeconds = 0;
                }

                CurrentSeconds += Time.deltaTime;
            }
            else if (State == States.Shoot)
            {
                CurrentSeconds += Time.deltaTime;

                if (CurrentSeconds >= 1)
                {
                    State          = States.Follow;
                    FollowSeconds  = Random.Range(2f, 4f);
                    CurrentSeconds = 0;
                }

                if (Time.frameCount % 20 == 0 && player)
                {
                    Fire();
                }
            }
        }

        private void FixedUpdate()
        {
            var player = Player.Instance;

            if (State == States.Follow)
            {
                if (player)
                {
                    Rigidbody2D.linearVelocity = player.Direction2DFrom(transform) * _Property.MoveSpeed;
                }
            }
        }

        private void OnDestroy()
        {
            _enemyModel.Enemies.Remove(this);
        }

        public void Fire()
        {
            var bullet = Bullet.Instantiate(transform.position)
               .Enable();
            var rigidbody2D = bullet.GetComponent<Rigidbody2D>();

            rigidbody2D.linearVelocity = Player.Instance.Direction2DFrom(bullet) * 3;
            bullet.OnFixedUpdateEvent(() => { }); // todo: 需要写在这里吗？

            bullet.OnCollisionEnter2DEvent(collider2D =>
            {
                var player = collider2D.gameObject.GetComponent<Player>();
                if (player)
                {
                    player.Hurt(1);
                    bullet.Destroy();
                }

                bullet.Destroy();
            });

            AudioKit.PlaySound(ShootSounds.RandomChoose());
        }

        public override void Hurt(float damage)
        {
            _Property.Hp.Value -= damage;
        }

        protected override IArchitecture _Architecture { get => Game.Interface; }
    }
}