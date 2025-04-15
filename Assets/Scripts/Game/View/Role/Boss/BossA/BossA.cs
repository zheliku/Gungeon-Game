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
    using UnityEngine;

    public class BossA : Boss
    {
        public enum State
        {
            Follow,
            PrepareToShoot,
            Shoot
        }
        
        public float HpRatio
        {
            get => Property.Hp.Value * 1f / Property.MaxHp.Value;
        }

        public FSM<State> FSM = new FSM<State>();

        protected override void Awake()
        {
            base.Awake();

            FSM.AddState(State.Follow, new StateFollowBossA(FSM, this));
            FSM.AddState(State.PrepareToShoot, new StatePrepareToShootBossA(FSM, this));
            FSM.AddState(State.Shoot, new StateShootBossA(FSM, this));

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

        protected override IArchitecture _Architecture { get => Game.Architecture; }
    }
}