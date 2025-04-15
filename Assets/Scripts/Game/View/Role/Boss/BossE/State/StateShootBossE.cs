// ------------------------------------------------------------
// @file       StateFollow.cs
// @brief
// @author     zheliku
// @Modified   2025-04-16 00:04:13
// @Copyright  Copyright (c) 2025, zheliku
// ------------------------------------------------------------

namespace Game
{
    using Framework.Toolkits.AudioKit;
    using Framework.Toolkits.FluentAPI;
    using Framework.Toolkits.FSMKit;
    using Framework.Toolkits.TimerKit;
    using UnityEngine;

    public class StateShootBossE : AbstractState<BossE.State, BossE>
    {
        public StateShootBossE(FSM<BossE.State> fsm, BossE owner) : base(fsm, owner)
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

        private void Stage1Update() // 阶段一，只攻击一次
        {
            if (TimerKit.HasPassedInterval(this, 0.1f))
            {
                BulletHelper.CircleShoot(
                    3, 
                    _owner.GetPosition(), 
                    1.5f, 
                    _fsm.FrameCountOfCurrentState,
                    _owner.Bullet, 
                    1, 
                    20);

                AudioKit.PlaySound(_owner.ShootSounds.RandomTakeOne());
            }

            if (_fsm.SecondsOfCurrentState >= 1.2f)
            {
                _fsm.ChangeState(BossE.State.Follow);
            }
        }

        private void Stage2Update() // 阶段二，持续攻击 1 s
        {
            if (TimerKit.HasPassedInterval(this, 0.08f))
            {
                BulletHelper.CircleShoot(
                    5, 
                    _owner.GetPosition(), 
                    1.5f, 
                    _fsm.FrameCountOfCurrentState,
                    _owner.Bullet, 
                    1, 
                    15);

                AudioKit.PlaySound(_owner.ShootSounds.RandomTakeOne());
            }

            if (_fsm.SecondsOfCurrentState >= 1.5f)
            {
                _fsm.ChangeState(BossE.State.Follow);
            }
        }

        private void Stage3Update() // 阶段二，持续攻击 1 s
        {
            if (TimerKit.HasPassedInterval(this, 0.06f))
            {
                BulletHelper.CircleShoot(
                    8, 
                    _owner.GetPosition(), 
                    1.5f, 
                    _fsm.FrameCountOfCurrentState,
                    _owner.Bullet, 
                    1, 
                    12);

                AudioKit.PlaySound(_owner.ShootSounds.RandomTakeOne());
            }
            
            if (_fsm.SecondsOfCurrentState >= 2.5f)
            {
                _fsm.ChangeState(BossE.State.Follow);
            }
        }
    }
}