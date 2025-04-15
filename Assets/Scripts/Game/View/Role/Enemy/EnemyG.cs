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
    using UnityEngine.Serialization;
    using Random = UnityEngine.Random;

    public class EnemyG : Enemy
    {
        public enum State
        {
            Follow,
            PrepareToShoot,
            Shoot
        }

        public (int min, int max) FireTimes = (1, 4 + 1); // 一次连续射击发射的子弹次数，一次发射 1s

        public float FireInterval = 0.25f; // 发射间隔时间

        private int _fireTimes;

        public int FireCount = 3; // 每次开枪射击发射的子弹个数

        public float IntervalAngle = 15; // 间隔角度

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
                    if (TimerKit.HasPassedInterval(this, 1f)) // 每秒计算一次路径
                    {
                        CalculateMovementPath();
                    }
                    
                    AutoMove();

                    AnimationHelper.UpDownAnimation(SpriteRenderer, FSM.SecondsOfCurrentState, 0.2f, _playerSpriteOriginLocalPos.y, 0.05f);
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
                    _fireTimes                 = FireTimes.RandomSelect();
                    Rigidbody2D.linearVelocity = Vector2.zero;
                })
               .OnUpdate(() =>
                {
                    if (TimerKit.HasPassedInterval(this, FireInterval))
                    {
                        Fire();
                    }

                    if (FSM.SecondsOfCurrentState >= _fireTimes)
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