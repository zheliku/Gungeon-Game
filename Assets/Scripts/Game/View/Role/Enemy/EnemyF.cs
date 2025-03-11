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
    using Framework.Toolkits.ActionKit;
    using Framework.Toolkits.AudioKit;
    using Framework.Toolkits.FluentAPI;
    using Framework.Toolkits.FSMKit;
    using UnityEngine;
    using Random = UnityEngine.Random;

    public class EnemyF : Enemy
    {
        public enum State
        {
            Follow,
            Shoot
        }

        public int   FireCount    = 15;   // 一次射击发射的子弹个数
        public float IntervalTime = 0.2f; // 发射间隔时间

        public FSM<State> FSM = new FSM<State>();

        protected override void Awake()
        {
            base.Awake();

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
            ActionKit.Repeat(3)
               .Callback(() =>
                {
                    BulletHelper.CircleShoot(
                        fireCount: FireCount,
                        center: transform.position,
                        radius: 0.5f,
                        bulletPrefab: Bullet,
                        speed: BulletSpeed);
                    AudioKit.PlaySound(ShootSounds.RandomTakeOne());
                })
               .Delay(IntervalTime)
               .Start(this);
        }

        protected override IArchitecture _Architecture { get => Game.Interface; }
    }
}