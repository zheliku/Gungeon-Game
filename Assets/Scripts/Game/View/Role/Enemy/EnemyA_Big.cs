// ------------------------------------------------------------
// @file       Enemy.cs
// @brief
// @author     zheliku
// @Modified   2025-01-29 16:01:14
// @Copyright  Copyright (c) 2025, zheliku
// ------------------------------------------------------------

namespace Game
{
    using Framework.Core;
    using Framework.Toolkits.AudioKit;
    using Framework.Toolkits.FluentAPI;
    using Framework.Toolkits.FSMKit;
    using UnityEngine;
    using Random = UnityEngine.Random;

    public class EnemyA_Big : Enemy
    {
        public enum State
        {
            Follow,
            Shoot
        }

        public FSM<State> FSM = new FSM<State>();

        protected override void Awake()
        {
            base.Awake();

            BulletSpeed = 10f;                     // 子弹速度更快
            _property.Hp.SetValueWithoutEvent(10); // 血量更高

            FSM.State(State.Follow)
               .OnEnter(() =>
                {
                    FollowSeconds = Random.Range(0.5f, 3f);
                })
               .OnUpdate(() =>
                {
                    if (FSM.SecondsOfCurrentState >= FollowSeconds)
                    {
                        FSM.ChangeState(State.Shoot);
                    }
                })
               .OnFixedUpdate(() =>
                {
                    var player = Player.Instance;

                    if (player)
                    {
                        Rigidbody2D.linearVelocity = player.Direction2DFrom(transform) * _property.MoveSpeed;
                    }
                });

            FSM.State(State.Shoot)
               .OnEnter(() =>
                {
                    Fire();
                    Rigidbody2D.linearVelocity = Vector2.zero;
                })
               .OnUpdate(() =>
                {
                    if (FSM.SecondsOfCurrentState >= 1)
                    {
                        FSM.ChangeState(State.Follow);
                    }
                });

            FSM.StartState(State.Follow);
        }

        private void Update()
        {
            FSM.Update();

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
        }

        private void FixedUpdate()
        {
            FSM.FixedUpdate();
        }

        public void Fire()
        {
            var bullet = Bullet.Instantiate(transform.position)
               .Enable()
               .GetComponent<EnemyBullet>();

            bullet.Damage   = 1f;
            bullet.Velocity = Player.Instance.Direction2DFrom(bullet) * BulletSpeed;

            AudioKit.PlaySound(ShootSounds.RandomTakeOne());
        }

        protected override IArchitecture _Architecture { get => Game.Interface; }
    }
}