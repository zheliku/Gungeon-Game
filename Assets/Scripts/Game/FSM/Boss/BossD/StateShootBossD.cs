// ------------------------------------------------------------
// @file       StateFollow.cs
// @brief
// @author     zheliku
// @Modified   2025-04-16 00:04:13
// @Copyright  Copyright (c) 2025, zheliku
// ------------------------------------------------------------

namespace Game
{
    using Framework.Core;
    using Framework.Toolkits.ActionKit;
    using Framework.Toolkits.AudioKit;
    using Framework.Toolkits.EventKit;
    using Framework.Toolkits.FluentAPI;
    using Framework.Toolkits.FSMKit;
    using Framework.Toolkits.TimerKit;
    using UnityEngine;

    public class StateShootBossD : AbstractState<BossD.State, BossD>
    {
        public StateShootBossD(FSM<BossD.State> fsm, BossD owner) : base(fsm, owner)
        { }

        protected override void OnEnter()
        {
            _owner.Rigidbody2D.linearVelocity = Vector2.zero;
        }

        protected override void OnUpdate()
        {
            // Stage3Update();
            // return;
            
            switch (_owner.HpRatio)
            {
                case > 0.8f: Stage1Update(); break;
                case > 0.5f: Stage2Update(); break;
                case >= 0f:  Stage3Update(); break;
            }
        }

        private void Stage1Update() // 阶段一，只攻击一次
        {
            BulletHelper.SpreadShoot(
                8,
                _owner.GetPosition(),
                1.5f,
                _owner.DirectionTo(Player.Instance),
                10,
                _owner.Bullet,
                1,
                _owner.BulletSpeed);

            AudioKit.PlaySound(_owner.ShootSounds.RandomTakeOne());

            _fsm.ChangeState(BossD.State.Follow);
        }

        private void Stage2Update() // 阶段二，持续攻击 1 s
        {
            if (TimerKit.HasPassedInterval(this, 0.5f))
            {
                BulletHelper.SpreadShoot(
                    8,
                    _owner.GetPosition(),
                    1.5f,
                    _owner.DirectionTo(Player.Instance),
                    10,
                    _owner.Bullet,
                    1,
                    _owner.BulletSpeed);

                AudioKit.PlaySound(_owner.ShootSounds.RandomTakeOne());
            }

            if (_fsm.SecondsOfCurrentState >= 1f)
            {
                _fsm.ChangeState(BossD.State.Follow);
            }
        }

        private void Stage3Update() // 阶段二，持续攻击 1 s
        {
            var fullBounceBullet = _owner.Bullet.GetComponent<FullBounceEnemyBullet>();

            if (fullBounceBullet != null)
            {
                fullBounceBullet.ReflectCount = 3;
                fullBounceBullet.SpriteColor  = Color.green;
            }
            
            if (TimerKit.HasPassedInterval(this, 0.8f))
            {
                var playerSpeed = _owner.GetModel<PlayerModel>().Property.MoveSpeed;
                
                var bullets = BulletHelper.SpreadShoot(
                    7,
                    _owner.GetPosition(),
                    1.5f,
                    _owner.DirectionTo(Player.Instance),
                    20,
                    _owner.Bullet,
                    1,
                    playerSpeed / 2);

                foreach (var bullet in bullets)
                {
                    ActionKit.Sequence()
                       .Delay(1f)
                       .Callback(() =>
                        {
                            var followTime   = 3f;
                            bullet.OnFixedUpdateEvent(() =>
                            {
                                followTime -= Time.deltaTime;

                                if (followTime > 0)
                                {
                                    var targetDir = bullet.Direction2DTo(Player.Instance);
                                    var angle     = Vector2.SignedAngle(bullet.Velocity, targetDir);
                                    
                                    Debug.Log(angle);
                                    
                                    var limitedAngle = Mathf.Clamp(angle, -2f, 2f); // 限制旋转角度
                                    bullet.Velocity = Quaternion.Euler(0, 0, limitedAngle) * bullet.Velocity.normalized * playerSpeed * 1.1f;
                                }
                            });
                        })
                       .Start(bullet);
                }

                AudioKit.PlaySound(_owner.ShootSounds.RandomTakeOne());
            }

            if (_fsm.SecondsOfCurrentState >= 1.6f)
            {
                _fsm.ChangeState(BossD.State.Follow);
            }
        }
    }
}