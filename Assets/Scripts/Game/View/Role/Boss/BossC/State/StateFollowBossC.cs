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
    using Framework.Toolkits.FluentAPI;
    using Framework.Toolkits.FSMKit;
    using Framework.Toolkits.TimerKit;
    using UnityEngine;

    public class StateFollowBossC : AbstractState<BossC.State, BossC>
    {
        public float FollowSeconds;

        public StateFollowBossC(FSM<BossC.State> fsm, BossC owner) : base(fsm, owner)
        { }

        protected override void OnEnter()
        {
            FollowSeconds = Random.Range(0.5f, 3f);
        }

        protected override void OnUpdate()
        {
            if (TimerKit.HasPassedInterval(this, 1f)) // 每秒计算一次路径
            {
                Debug.Log("HasPassedInterval");
                _owner.CalculateMovementPath();
            }

            _owner.AutoMove();

            AnimationHelper.UpDownAnimation(_owner.SpriteRenderer, _fsm.SecondsOfCurrentState, 0.2f, _owner.PlayerSpriteOriginLocalPos.y, 0.05f);
            AnimationHelper.RotateAnimation(_owner.SpriteRenderer, _fsm.SecondsOfCurrentState, 0.4f, 3);

            if (_fsm.SecondsOfCurrentState >= FollowSeconds)
            {
                _fsm.ChangeState(BossC.State.PrepareToShoot);
            }
        }
    }
}