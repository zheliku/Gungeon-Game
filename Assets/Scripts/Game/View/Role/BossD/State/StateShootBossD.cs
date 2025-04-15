// ------------------------------------------------------------
// @file       StateFollow.cs
// @brief
// @author     zheliku
// @Modified   2025-04-16 00:04:13
// @Copyright  Copyright (c) 2025, zheliku
// ------------------------------------------------------------

namespace Game
{
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
            switch (_owner.HpRatio)
            {
                case > 0.7f: Stage1Update(); break;
                case > 0.3f: Stage2Update(); break;
                case >= 0f:  Stage3Update(); break;
            }
        }

        private void Stage1Update() // 阶段一，只攻击一次
        {
            _owner.Fire();

            _fsm.ChangeState(BossD.State.Follow);
        }

        private void Stage2Update() // 阶段二，持续攻击 1 s
        {
            if (TimerKit.HasPassedInterval(this, 0.3f))
            {
                _owner.Fire();
            }

            if (_fsm.SecondsOfCurrentState >= 1.2f)
            {
                _fsm.ChangeState(BossD.State.Follow);
            }
        }

        private void Stage3Update() // 阶段二，持续攻击 1 s
        {
            if (TimerKit.HasPassedInterval(this, 0.25f))
            {
                _owner.Fire();
            }
        }
    }
}