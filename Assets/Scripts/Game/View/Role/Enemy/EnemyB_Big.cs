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

    public class EnemyB_Big : Enemy
    {
        public enum State
        {
            Follow,
            PrepareToShoot,
            Shoot
        }

        public int   FireCount     = 9;  // 一次射击发射的子弹个数
        public float IntervalAngle = 15; // 间隔角度

        public FSM<State> FSM = new FSM<State>();

        protected override void Awake()
        {
            base.Awake();
            
            BulletSpeed = 8f;                     // 子弹速度更快
            _property.Hp.SetValueWithoutEvent(10); // 血量更高

            FSM.State(State.Follow)
               .OnEnter(() =>
                {
                    FollowSeconds = Random.Range(0.5f, 3f);
                })
               .OnUpdate(() =>
                {
                    AnimationHelper.UpDownAnimation(SpriteRenderer, FSM.SecondsOfCurrentState, 0.2f, _playerSpriteOriginLocalPos.y, 0.05f);
                    AnimationHelper.RotateAnimation(SpriteRenderer, FSM.SecondsOfCurrentState, 0.4f, 3);

                    if (FSM.SecondsOfCurrentState >= FollowSeconds)
                    {
                        FSM.ChangeState(State.PrepareToShoot);
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
            
            Vector2 onEnterPrepareToShootLocalPos = SpriteRenderer.GetLocalPosition();
            FSM.State(State.PrepareToShoot)
               .OnEnter(() =>
                {
                    onEnterPrepareToShootLocalPos = SpriteRenderer.GetLocalPosition();
                })
               .OnUpdate(() =>
                {
                    var shakeRate = (FSM.SecondsOfCurrentState / 0.25f).Lerp(0.05f, 0.1f);
                    var shakePos  = new Vector2(shakeRate.RandomTo0(), shakeRate.RandomTo0());
                    SpriteRenderer.SetLocalPosition(onEnterPrepareToShootLocalPos + shakePos);

                    if (FSM.SecondsOfCurrentState >= 0.3f)
                    {
                        FSM.ChangeState(State.Shoot);
                    }    
                })
               .OnExit(() =>
                {
                    SpriteRenderer.SetLocalPosition(onEnterPrepareToShootLocalPos);
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

            BulletHelper.SpreadShoot(
                fireCount: FireCount,
                center: transform.position,
                radius: 0.5f,
                direction: direction,
                intervalAngle: IntervalAngle,
                bulletPrefab: Bullet,
                damage: 1f,
                speed: BulletSpeed);

            AudioKit.PlaySound(ShootSounds.RandomTakeOne());
        }

        protected override IArchitecture _Architecture { get => Game.Architecture; }
    }
}