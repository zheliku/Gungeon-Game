// ------------------------------------------------------------
// @file       State.cs
// @brief
// @author     zheliku
// @Modified   2024-10-20 20:10:36
// @Copyright  Copyright (c) 2024, zheliku
// ------------------------------------------------------------

namespace Framework.Toolkits.FSMKit
{
    public abstract class AbstractState<TStateId, TOwner> : IState
    {
        protected FSM<TStateId> _fsm;
        protected TOwner       _owner;

        public AbstractState(FSM<TStateId> fsm, TOwner owner)
        {
            _fsm    = fsm;
            _owner = owner;
        }

        bool IState.Condition()
        {
            return OnCondition();
        }

        void IState.Enter()
        {
            OnEnter();
        }

        void IState.Update()
        {
            OnUpdate();
        }

        void IState.FixedUpdate()
        {
            OnFixedUpdate();
        }

        void IState.OnGUI()
        {
            OnGUI();
        }

        void IState.Exit()
        {
            OnExit();
        }

        protected virtual bool OnCondition()
        {
            return true;
        }

        protected virtual void OnEnter()
        { }

        protected virtual void OnUpdate()
        { }

        protected virtual void OnFixedUpdate()
        { }

        public virtual void OnGUI()
        { }

        protected virtual void OnExit()
        { }
    }
}