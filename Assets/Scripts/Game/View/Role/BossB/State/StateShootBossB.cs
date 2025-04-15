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
                case > 0.7f: Stage1Update(); break;
                case > 0.3f: Stage2Update(); break;
                case >= 0f:  Stage3Update(); break;
            }
        }

        private void Stage1Update() // 阶段一，只攻击一次
        {
            BulletHelper.CircleShoot(24, _owner.GetPosition(), 1.5f, _owner.Bullet, 1, 15);

            AudioKit.PlaySound(_owner.ShootSounds.RandomTakeOne());

            _fsm.ChangeState(BossB.State.Follow);
        }

        private void Stage2Update() // 阶段二，持续攻击 1 s
        {
            if (TimerKit.HasPassedInterval(this, 0.25f))
            {
                BulletHelper.CircleShoot(20, _owner.GetPosition(), 1.5f, _owner.Bullet, 1, 12);

                AudioKit.PlaySound(_owner.ShootSounds.RandomTakeOne());
            }

            if (_fsm.SecondsOfCurrentState >= 1.2f)
            {
                _fsm.ChangeState(BossB.State.Follow);
            }
        }

        private void Stage3Update() // 阶段二，持续攻击 1 s
        {
            if (TimerKit.HasPassedInterval(this, 0.22f))
            {
                BulletHelper.CircleShoot(30, _owner.GetPosition(), 1.5f, _owner.Bullet, 1, 8);

                AudioKit.PlaySound(_owner.ShootSounds.RandomTakeOne());
            }
        }
    }
}