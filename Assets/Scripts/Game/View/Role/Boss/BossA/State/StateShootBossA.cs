// ------------------------------------------------------------
// @file       StateFollow.cs
// @brief
// @author     zheliku
// @Modified   2025-04-16 00:04:13
// @Copyright  Copyright (c) 2025, zheliku
// ------------------------------------------------------------

namespace Game
{
    using Framework.Toolkits.ActionKit;
    using Framework.Toolkits.AudioKit;
    using Framework.Toolkits.FluentAPI;
    using Framework.Toolkits.FSMKit;
    using Framework.Toolkits.TimerKit;
    using UnityEngine;

    public class StateShootBossA : AbstractState<BossA.State, BossA>
    {
        public StateShootBossA(FSM<BossA.State> fsm, BossA owner) : base(fsm, owner)
        { }

        protected override void OnEnter()
        {
            _owner.Rigidbody2D.linearVelocity = Vector2.zero;
        }

        protected override void OnUpdate()
        {
            switch (_owner.HpRatio)
            {
                case > 0.7f: Stage1Update(); break;
                case > 0.3f: Stage2Update(); break;
                case >= 0f:  Stage3Update(); break;
            }
        }

        private void Stage1Update() // 阶段一，连续攻击 3 次
        {
            if (TimerKit.HasPassedInterval(this, 0.3f))
            {
                BulletHelper.CircleShoot(
                    30,
                    _owner.GetPosition(),
                    1f,
                    (0f, 360f).RandomSelect(),
                    _owner.Bullet,
                    1,
                    _owner.BulletSpeed);

                AudioKit.PlaySound(_owner.ShootSounds.RandomTakeOne());
            }

            if (_fsm.SecondsOfCurrentState >= 0.9f)
            {
                _fsm.ChangeState(BossA.State.Follow);
            }
        }

        private void Stage2Update() // 阶段二，圆圈跟踪攻击
        {
            if (TimerKit.HasPassedInterval(this, 0.5f))
            {
                var bullets = BulletHelper.CircleShoot(
                    15,
                    _owner.GetPosition(),
                    1f,
                    (0f, 360f).RandomSelect(),
                    _owner.Bullet,
                    1,
                    _owner.BulletSpeed / 4);
                
                ActionKit.Delay(0.8f, () =>
                {
                    var direction = _owner.Direction2DTo(Player.Instance);
                    foreach (var bullet in bullets)
                    {
                        if (bullet)
                        {
                            bullet.Velocity = direction * (_owner.BulletSpeed * 1.2f);
                        }
                    }
                }).Start(_owner);
                
                AudioKit.PlaySound(_owner.ShootSounds.RandomTakeOne());
            }

            if (_fsm.SecondsOfCurrentState >= 1.5f)
            {
                _fsm.ChangeState(BossA.State.Follow);
            }
        }

        private void Stage3Update() // 阶段三，持续弹幕攻击
        {
            if (TimerKit.HasPassedInterval(this, 0.3f))
            {
                BulletHelper.CircleShoot(
                    20,
                    _owner.GetPosition(),
                    1f,
                    (0f, 360f).RandomSelect(),
                    _owner.Bullet,
                    1,
                    _owner.BulletSpeed);

                AudioKit.PlaySound(_owner.ShootSounds.RandomTakeOne());
            }
        }
    }
}