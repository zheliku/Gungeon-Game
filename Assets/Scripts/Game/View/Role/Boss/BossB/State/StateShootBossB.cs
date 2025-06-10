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
    using Framework.Toolkits.EventKit;
    using Framework.Toolkits.FluentAPI;
    using Framework.Toolkits.FSMKit;
    using Framework.Toolkits.TimerKit;
    using UnityEngine;

    public class StateShootBossB : AbstractState<BossB.State, BossB>
    {
        public StateShootBossB(FSM<BossB.State> fsm, BossB owner) : base(fsm, owner)
        { }

        protected override void OnEnter()
        {
            _owner.Rigidbody2D.linearVelocity = Vector2.zero;
        }

        protected override void OnUpdate()
        {
            switch (_owner.HpRatio)
            {
                case > 0.8f: Stage1Update(); break;
                case > 0.4f: Stage2Update(); break;
                case >= 0f:  Stage3Update(); break;
            }
        }

        private void Stage1Update() // 阶段一，只攻击一次
        {
            BulletHelper.SpreadShoot(
                15, 
                _owner.GetPosition(), 
                1.5f, 
                _owner.DirectionTo(Player.Instance),
                5,
                _owner.Bullet, 
                1, 
                _owner.BulletSpeed);

            AudioKit.PlaySound(_owner.ShootSounds.RandomTakeOne());

            _fsm.ChangeState(BossB.State.Follow);
        }

        private void Stage2Update() // 阶段二，持续攻击 1 s
        {
            if (TimerKit.HasPassedInterval(this, 0.2f))
            {
                var bullets = BulletHelper.SpreadShoot(
                    3, 
                    _owner.GetPosition(), 
                    1.5f, 
                    _owner.DirectionTo(Player.Instance),
                    10,
                    _owner.Bullet, 
                    1, 
                    _owner.BulletSpeed * 1.5f);

                AudioKit.PlaySound(_owner.ShootSounds.RandomTakeOne());
            }

            if (_fsm.SecondsOfCurrentState >= 1.2f)
            {
                _fsm.ChangeState(BossB.State.Follow);
            }
        }

        private void Stage3Update() // 阶段二，持续攻击 1 s
        {
            if (TimerKit.HasPassedInterval(this, 0.8f))
            {
                var bullets = BulletHelper.FocusShoot(
                    15, 
                    Player.Instance.GetPosition(), 
                    6f, 
                    (0f, 360f).RandomSelect(),
                    _owner.Bullet, 
                    1, 
                    _owner.BulletSpeed * 0.8f);
                
                foreach (var bullet in bullets)
                {
                    var initVelocity = bullet.Rigidbody.linearVelocity;
                    bullet.Rigidbody.linearVelocity = Vector2.zero;
                    
                    var initLocalScale = _owner.GetLocalScale();

                    ActionKit.Lerp(0, 1, 0.6f, f =>
                    {
                        bullet.SetLocalScale(initLocalScale * f);
                    }, () =>
                    {
                        bullet.Rigidbody.linearVelocity = initVelocity;
                    }).Start(bullet);
                }

                AudioKit.PlaySound(_owner.ShootSounds.RandomTakeOne());
            }
            
            if (_fsm.SecondsOfCurrentState >= 2.4f)
            {
                _fsm.ChangeState(BossB.State.Follow);
            }
        }
    }
}