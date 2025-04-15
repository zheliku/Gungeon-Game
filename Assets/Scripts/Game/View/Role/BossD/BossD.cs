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

    public class BossD : Enemy
    {
        public enum State
        {
            Follow,
            PrepareToShoot,
            Shoot
        }
        
        public float HpRatio
        {
            get => _property.Hp.Value * 1f / _property.MaxHp.Value;
        }

        public FSM<State> FSM = new FSM<State>();

        protected override void Awake()
        {
            base.Awake();

            _property.Hp.SetValueWithoutEvent(150);    // 血量更高
            _property.MaxHp.SetValueWithoutEvent(150); // 血量更高

            TypeEventSystem.GLOBAL.Send(new BossCreateEvent(this));

            FSM.AddState(State.Follow, new StateFollowBossD(FSM, this));
            FSM.AddState(State.PrepareToShoot, new StatePrepareToShootBossD(FSM, this));
            FSM.AddState(State.Shoot, new StateShootBossD(FSM, this));

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

        protected override void OnDestroy()
        {
            base.OnDestroy();

            TypeEventSystem.GLOBAL.Send(new BossDieEvent(this));
        }

        public override void Hurt(float damage, HitInfo info)
        {
            _property.Hp.Value -= (int) damage;

            FxFactory.PlayHurtFx(this.GetPosition(), Color.red);

            FxFactory.PlayEnemyBlood(this.GetPosition());

            TypeEventSystem.GLOBAL.Send(new BossHpChangeEvent(_property.Hp.Value * 1f / _property.MaxHp.Value));
        }

        protected override IArchitecture _Architecture { get => Game.Architecture; }
    }
}