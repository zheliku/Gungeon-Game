// ------------------------------------------------------------
// @file       ActionKitMonoBehaviourEvents.cs
// @brief
// @author     zheliku
// @Modified   2024-10-24 22:10:30
// @Copyright  Copyright (c) 2024, zheliku
// ------------------------------------------------------------

namespace Framework.Toolkits.ActionKit
{
    using System;
    using System.Collections;
    using Framework.Core;
    using SingletonKit;
    using Sirenix.OdinInspector;

    /// <summary>
    /// 用于 ActionKit 的 MonoBehaviour 事件
    /// </summary>
    public class ActionKitMonoBehaviourEvent : MonoSingleton<ActionKitMonoBehaviourEvent>
    {
        [ShowInInspector]
        internal readonly EasyEvent OnUpdate = new EasyEvent();

        [ShowInInspector]
        internal readonly EasyEvent OnFixedUpdate = new EasyEvent();

        [ShowInInspector]
        internal readonly EasyEvent OnLateUpdate = new EasyEvent();

        [ShowInInspector]
        internal readonly EasyEvent OnGUIEvent = new EasyEvent();

        [ShowInInspector]
        internal readonly EasyEvent<bool> OnApplicationFocusEvent = new EasyEvent<bool>();

        [ShowInInspector]
        internal readonly EasyEvent<bool> OnApplicationPauseEvent = new EasyEvent<bool>();

        [ShowInInspector]
        internal readonly EasyEvent OnApplicationQuitEvent = new EasyEvent();

        private void Awake()
        {
            // hideFlags = HideFlags.HideInHierarchy;
        }

        protected override void Update()
        {
            base.Update();
            
            OnUpdate?.Trigger();
        }

        private void OnGUI()
        {
            OnGUIEvent?.Trigger();
        }

        private void FixedUpdate()
        {
            OnFixedUpdate?.Trigger();
        }

        private void LateUpdate()
        {
            OnLateUpdate?.Trigger();
        }

        private void OnApplicationFocus(bool hasFocus)
        {
            OnApplicationFocusEvent?.Trigger(hasFocus);
        }

        private void OnApplicationPause(bool pauseStatus)
        {
            OnApplicationPauseEvent?.Trigger(pauseStatus);
        }

        protected override void OnApplicationQuit()
        {
            OnApplicationQuitEvent?.Trigger();
            base.OnApplicationQuit();
        }

        public void ExecuteCoroutine(IEnumerator coroutine, Action onFinish)
        {
            StartCoroutine(DoExecuteCoroutine(coroutine, onFinish));
        }

        private IEnumerator DoExecuteCoroutine(IEnumerator coroutine, Action onFinish)
        {
            yield return coroutine;
            onFinish?.Invoke();
        }
    }
}