// ------------------------------------------------------------
// @file       FSM.cs
// @brief
// @author     zheliku
// @Modified   2024-10-20 20:10:55
// @Copyright  Copyright (c) 2024, zheliku
// ------------------------------------------------------------

namespace Framework.Toolkits.FSMKit
{
    using System;
    using System.Collections.Generic;
    using Sirenix.OdinInspector;
    using UnityEngine;

    [HideReferenceObjectPicker]
    public sealed class FSM<TStateId>
    {
    #region 字段

        [ShowInInspector]
        private readonly Dictionary<TStateId, IState> _states = new Dictionary<TStateId, IState>();

        [ShowInInspector]
        private IState _currentState;

        [ShowInInspector]
        private TStateId _currentStateId;

        [ShowInInspector]
        private TStateId _previousStateId;

        private Action<TStateId, TStateId> _onStateChanged = (_, _) => { };

        [ShowInInspector]
        private long _frameCountOfCurrentState = 1;

        [ShowInInspector]
        private float _secondsOfCurrentState = 0.0f;

    #endregion

    #region 属性

        public IState CurrentState => _currentState;

        public TStateId CurrentStateId => _currentStateId;

        public TStateId PreviousStateId => _previousStateId;

        public long FrameCountOfCurrentState => _frameCountOfCurrentState;

        public float SecondsOfCurrentState => _secondsOfCurrentState;

    #endregion

    #region 方法

        public FSMState State(TStateId id)
        {
            if (_states.TryGetValue(id, out IState value))
            {
                return value as FSMState;
            }

            var state = new FSMState();
            _states.Add(id, state);
            return state;
        }

        public void AddState(TStateId id, IState state)
        {
            _states.Add(id, state);
        }

        public void ChangeState(TStateId id)
        {
            if (Equals(id, _currentStateId)) return;

            if (_states.TryGetValue(id, out var state))
            {
                if (_currentState != null && state.Condition())
                {
                    _currentState.Exit();
                    _previousStateId = _currentStateId;
                    _currentState    = state;
                    _currentStateId  = id;
                    _onStateChanged?.Invoke(_previousStateId, _currentStateId);
                    _frameCountOfCurrentState = 1;
                    _secondsOfCurrentState    = 0.0f;
                    _currentState.Enter();
                }
            }
        }

        public void OnStateChanged(Action<TStateId, TStateId> onStateChanged)
        {
            _onStateChanged = onStateChanged;
        }

        public void StartState(TStateId id)
        {
            if (_states.TryGetValue(id, out var state))
            {
                _previousStateId          = id;
                _currentState             = state;
                _currentStateId           = id;
                _frameCountOfCurrentState = 0;
                _secondsOfCurrentState    = 0.0f;
                _currentState.Enter();
            }
        }

        public void FixedUpdate()
        {
            _currentState?.FixedUpdate();
        }

        public void Update()
        {
            _currentState?.Update();
            _frameCountOfCurrentState++;
            _secondsOfCurrentState += Time.deltaTime;
        }

        public void OnGUI()
        {
            _currentState?.OnGUI();
        }

        public void Clear()
        {
            _currentState    = null;
            _currentStateId  = default;
            _previousStateId = default;
            _states.Clear();
        }

    #endregion
    }
}