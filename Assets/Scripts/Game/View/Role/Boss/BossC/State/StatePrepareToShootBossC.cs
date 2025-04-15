// ------------------------------------------------------------
// @file       StateFollow.cs
// @brief
// @author     zheliku
// @Modified   2025-04-16 00:04:13
// @Copyright  Copyright (c) 2025, zheliku
// ------------------------------------------------------------

namespace Game
{
    using Framework.Toolkits.FluentAPI;
    using Framework.Toolkits.FSMKit;
    using UnityEngine;

    public class StatePrepareToShootBossC : AbstractState<BossC.State, BossC>
    {
        private Vector2 _onEnterPrepareToShootLocalPos;
            

        public StatePrepareToShootBossC(FSM<BossC.State> fsm, BossC owner) : base(fsm, owner)
        { }

        protected override void OnEnter()
        {
            _onEnterPrepareToShootLocalPos = _owner.SpriteRenderer.GetLocalPosition();
        }

        protected override void OnUpdate()
        {
            var shakeRate = (_fsm.SecondsOfCurrentState / 0.25f).Lerp(0.05f, 0.1f);
            var shakePos  = new Vector2(shakeRate.RandomTo0(), shakeRate.RandomTo0());
            _owner.SpriteRenderer.SetLocalPosition(_onEnterPrepareToShootLocalPos + shakePos);

            if (_fsm.SecondsOfCurrentState >= 0.3f)
            {
                _fsm.ChangeState(BossC.State.Shoot);
            }
        }

        protected override void OnExit()
        {
            _owner.SpriteRenderer.SetLocalPosition(_onEnterPrepareToShootLocalPos);
        }
    }
}