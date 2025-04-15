// ------------------------------------------------------------
// @file       Enemy.cs
// @brief
// @author     zheliku
// @Modified   2025-01-29 16:01:14
// @Copyright  Copyright (c) 2025, zheliku
// ------------------------------------------------------------

namespace Game
{
    using System.Collections.Generic;
    using System.Linq;
    using Framework.Core;
    using Framework.Toolkits.AudioKit;
    using Framework.Toolkits.FluentAPI;
    using Framework.Toolkits.FSMKit;
    using Framework.Toolkits.TimerKit;
    using Sirenix.OdinInspector;
    using UnityEngine;
    using Random = UnityEngine.Random;

    public class BossA : Enemy
    {
        public enum State
        {
            Follow,
            PrepareToShoot,
            Shoot
        }

        public FSM<State> FSM = new FSM<State>();

        protected override void Awake()
        {
            base.Awake();

            // _property.Hp.SetValueWithoutEvent(10);    // 血量更高
            // _property.MaxHp.SetValueWithoutEvent(10); // 血量更高
            
            TypeEventSystem.GLOBAL.Send(new BossCreateEvent(this));

            FSM.State(State.Follow)
               .OnEnter(() =>
                {
                    FollowSeconds = Random.Range(0.5f, 3f);
                })
               .OnUpdate(() =>
                {
                    if (TimerKit.HasPassedInterval(this, 1f)) // 每秒计算一次路径
                    {
                        Debug.Log("HasPassedInterval");
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

        protected override void OnDestroy()
        {
            base.OnDestroy();
            
            TypeEventSystem.GLOBAL.Send(new BossDieEvent(this));
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

        public override void Hurt(float damage, HitInfo info)
        {
            _property.Hp.Value -= (int) damage;

            FxFactory.PlayHurtFx(this.GetPosition(), Color.red);

            FxFactory.PlayEnemyBlood(this.GetPosition());
            
            TypeEventSystem.GLOBAL.Send(new BossHpChangeEvent(_property.Hp.Value * 1f / _property.MaxHp.Value));
        }

        protected override IArchitecture _Architecture { get => Game.Architecture; }
    }
}