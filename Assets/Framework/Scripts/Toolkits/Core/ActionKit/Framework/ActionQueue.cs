// ------------------------------------------------------------
// @file       ActionQueue.cs
// @brief
// @author     zheliku
// @Modified   2024-10-24 20:10:32
// @Copyright  Copyright (c) 2024, zheliku
// ------------------------------------------------------------

namespace Framework.Toolkits.ActionKit
{
    using System.Collections.Generic;
    using SingletonKit;
    using Sirenix.OdinInspector;
    using UnityEngine;

    [MonoSingletonPath("Framework/ActionKit/Queue")]
    public class ActionQueue : MonoBehaviour, ISingleton
    {
        [ShowInInspector]
        private static ActionQueue _Instance { get => MonoSingletonProperty<ActionQueue>.Instance; }

        /// <summary>
        /// 回收列表
        /// </summary>
        [ShowInInspector]
        private readonly List<IActionQueueCallback> _actionQueueCallbacks = new List<IActionQueueCallback>();

        public static void AddCallback(IActionQueueCallback actionQueueCallback)
        {
            _Instance._actionQueueCallbacks.Add(actionQueueCallback);
        }

        // Update is called once per frame
        private void Update()
        {
#if UNITY_EDITOR

            // 强制刷新 Inspector GUI
            UnityEditor.EditorUtility.SetDirty(this);
#endif

            // 如果回调列表不为空，则立即回调
            if (_actionQueueCallbacks.Count > 0)
            {
                foreach (var actionQueueCallback in _actionQueueCallbacks)
                {
                    actionQueueCallback.Call();
                }

                _actionQueueCallbacks.Clear();
            }
        }

        void ISingleton.OnSingletonInit() { }
    }
}