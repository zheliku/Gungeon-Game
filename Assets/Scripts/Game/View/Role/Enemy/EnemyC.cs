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

    public class EnemyC : Enemy
    {
        public enum State
        {
            Follow,
            Shoot
        }

        public int   FireCount    = 3;    // 一次射击发射的子弹个数
        public float IntervalTime = 0.2f; // 间隔角度

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
            var direction = Player.Instance.Direction2DFrom(this);

            ActionKit.Repeat(3)
               .Callback(() =>
                {
                    var bullet = Bullet.Instantiate(transform.position)
                       .Enable()
                       .GetComponent<EnemyBullet>();

                    bullet.Damage = 1f;

                    var rigidbody2D = bullet.GetComponent<Rigidbody2D>();
                    rigidbody2D.linearVelocity = direction * BulletSpeed;
                    AudioKit.PlaySound(ShootSounds.RandomTakeOne());
                })
               .Delay(IntervalTime)
               .Start(this);
        }

        protected override IArchitecture _Architecture { get => Game.Interface; }
    }
}