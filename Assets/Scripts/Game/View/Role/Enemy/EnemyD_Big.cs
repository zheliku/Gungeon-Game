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
    using Framework.Toolkits.TimerKit;
    using UnityEngine;
    using Random = UnityEngine.Random;

    public class EnemyD_Big : Enemy
    {
        public enum State
        {
            Follow,
            PrepareToShoot,
            Shoot
        }

        public (int min, int max) FireCount = (12, 18 + 1); // 一次射击发射的子弹个数区间

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
                    if (TimerKit.HasPassedInterval(this, 1f)) // 每秒计算一次路径
                    {
                        CalculateMovementPath();
                    }
                    
                    AutoMove();

                    AnimationHelper.UpDownAnimation(SpriteRenderer, FSM.SecondsOfCurrentState, 0.2f, PlayerSpriteOriginLocalPos.y, 0.05f);
                    AnimationHelper.RotateAnimation(SpriteRenderer, FSM.SecondsOfCurrentState, 0.4f, 3);

                    if (FSM.SecondsOfCurrentState >= FollowSeconds)
                    {
                        FSM.ChangeState(State.PrepareToShoot);
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

        protected override void Update()
        {
            base.Update();
            
            FSM.Update();
        }

        private void FixedUpdate()
        {
            FSM.FixedUpdate();
        }

        public void Fire()
        {
            var fireCount = FireCount.RandomSelect();

            BulletHelper.CircleShoot(
                fireCount: fireCount,
                center: transform.position,
                radius: 0.5f,
                angleOffset: (0f, 360f).RandomSelect(),
                bulletPrefab: Bullet,
                damage: 1f,
                speed: BulletSpeed);

            AudioKit.PlaySound(ShootSounds.RandomTakeOne());
        }

        protected override IArchitecture _Architecture { get => Game.Architecture; }
    }
}