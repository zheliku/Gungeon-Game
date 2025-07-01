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

    public class StateFollowBossB : AbstractState<BossB.State, BossB>
    {
        public float FollowSeconds;

        public bool IsPlayingInAnimation;
        public bool IsPlayingOutAnimation;

        public Vector3 InitLocalScale;

        public StateFollowBossB(FSM<BossB.State> fsm, BossB owner) : base(fsm, owner)
        { }

        protected override void OnEnter()
        {
            FollowSeconds = Random.Range(0.5f, 3f);

            InitLocalScale = _owner.GetLocalScale();

            // _owner.Collider2D.Disable();
            IsPlayingInAnimation = true;

            ActionKit.Lerp(1, 0, 1, f =>
            {
                _owner.SetLocalScale(InitLocalScale * f);
            }, () =>
            {
                _owner.Collider2D.Disable();
                IsPlayingInAnimation = false;
            }).Start(_owner);
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

            if (_fsm.SecondsOfCurrentState >= FollowSeconds + 1 && !IsPlayingOutAnimation)
            {
                // _owner.Collider2D.Disable();
                IsPlayingOutAnimation = true;
                _owner.Collider2D.Enable();

                ActionKit.Lerp(0f, 1f, 1, f =>
                {
                    _owner.SetLocalScale(InitLocalScale * f);
                }, () =>
                {
                    IsPlayingOutAnimation = false;
                    _fsm.ChangeState(BossB.State.PrepareToShoot);
                }).Start(_owner);
            }
        }
    }
}