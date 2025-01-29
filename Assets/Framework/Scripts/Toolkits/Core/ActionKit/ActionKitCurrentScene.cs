// ------------------------------------------------------------
// @file       ActionKitCurrentScene.cs
// @brief
// @author     zheliku
// @Modified   2024-10-24 22:10:44
// @Copyright  Copyright (c) 2024, zheliku
// ------------------------------------------------------------

namespace Framework.Toolkits.ActionKit
{
    using UnityEngine;

    /// <summary>
    /// 用于当前场景的 Action 单例
    /// </summary>
    public class ActionKitCurrentScene : MonoBehaviour
    {
        private static ActionKitCurrentScene _SceneComponent = null;

        public static ActionKitCurrentScene SceneComponent
        {
            get
            {
                if (!_SceneComponent)
                {
                    _SceneComponent = new GameObject(nameof(ActionKitCurrentScene)).AddComponent<ActionKitCurrentScene>();
                }

                return _SceneComponent;
            }
        }

        private void Awake()
        {
            // hideFlags = HideFlags.HideInHierarchy;
        }

        private void OnDestroy()
        {
            _SceneComponent = null;
        }
    }
}