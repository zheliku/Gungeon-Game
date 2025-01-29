// ------------------------------------------------------------
// @file       CustomState.cs
// @brief
// @author     zheliku
// @Modified   2024-10-20 20:10:46
// @Copyright  Copyright (c) 2024, zheliku
// ------------------------------------------------------------

namespace Framework.Toolkits.FSMKit
{
    using System;
    using Sirenix.OdinInspector;

    public class FSMState : IState
    {
        [ShowInInspector]
        private Func<bool> _onCondition;

        [ShowInInspector]
        private Action _onEnter;

        [ShowInInspector]
        private Action _onUpdate;

        [ShowInInspector]
        private Action _onFixedUpdate;

        [ShowInInspector]
        private Action _onGUI;

        [ShowInInspector]
        private Action _onExit;

        public FSMState OnCondition(Func<bool> onCondition)
        {
            _onCondition = onCondition;
            return this;
        }

        public FSMState OnEnter(Action onEnter)
        {
            _onEnter = onEnter;
            return this;
        }

        public FSMState OnUpdate(Action onUpdate)
        {
            _onUpdate = onUpdate;
            return this;
        }


        public FSMState OnFixedUpdate(Action onFixedUpdate)
        {
            _onFixedUpdate = onFixedUpdate;
            return this;
        }

        public FSMState OnGUI(Action onGUI)
        {
            _onGUI = onGUI;
            return this;
        }

        public FSMState OnExit(Action onExit)
        {
            _onExit = onExit;
            return this;
        }

        public bool Condition()
        {
            return _onCondition == null || _onCondition.Invoke();
        }

        public void Enter()
        {
            _onEnter?.Invoke();
        }

        public void Update()
        {
            _onUpdate?.Invoke();
        }

        public void FixedUpdate()
        {
            _onFixedUpdate?.Invoke();
        }

        public void OnGUI()
        {
            _onGUI?.Invoke();
        }

        public void Exit()
        {
            _onExit?.Invoke();
        }
    }
}