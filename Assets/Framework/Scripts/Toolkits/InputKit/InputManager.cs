// ------------------------------------------------------------
// @file       InputManager.cs
// @brief
// @author     zheliku
// @Modified   2024-12-26 22:12:33
// @Copyright  Copyright (c) 2024, zheliku
// ------------------------------------------------------------

namespace Framework.Toolkits.InputKit
{
    using System.Collections.Generic;
    using FluentAPI;
    using SingletonKit;
    using Sirenix.OdinInspector;
    using UnityEngine.InputSystem;

    [MonoSingletonPath("Framework/InputKit")]
    public class InputManager : MonoSingleton<InputManager>
    {
    #region 常量

    #endregion

    #region Static

    #endregion

    #region 字段

        [ShowInInspector]
        public Dictionary<string, InputActionMap> ActionMaps { get; } = new Dictionary<string, InputActionMap>();

    #endregion

    #region 属性

    #endregion

    #region 公共方法

        public override void OnSingletonInit()
        {
            // 初始化时，将所有 inputAction 记录到 ActionMaps 中
            var inputActionAsset = InputSystem.actions;
            foreach (var map in inputActionAsset.actionMaps)
            {
                ActionMaps.TryAdd(map.name, map);
            }
        }

        /// <summary>
        /// 获取 InputAction 对应的 Mono
        /// </summary>
        /// <param name="action">输入行为</param>
        /// <returns>InputAction 对应的 Mono</returns>
        public InputActionMono GetInputActionMono(InputAction action)
        {
            return $"InputSystem/{action.actionMap.name}/{action.name}".GetOrAddComponentInHierarchy<InputActionMono>(transform);
        }
        
        /// <summary>
        /// 获取 InputAction 对应的 MapMono
        /// </summary>
        /// <param name="action">输入行为</param>
        /// <returns>InputAction 对应的 MapMono</returns>
        public InputActionMapMono GetInputActionMapMono(InputAction action)
        {
            return $"InputSystem/{action.actionMap.name}".GetOrAddComponentInHierarchy<InputActionMapMono>(transform);
        }
        
        /// <summary>
        /// 获取 InputActionMap 对应的 MapMono
        /// </summary>
        /// <param name="actionMap">输入行为地图</param>
        /// <returns>InputAction 对应的 MapMono</returns>
        public InputActionMapMono GetInputActionMapMono(InputActionMap actionMap)
        {
            return $"InputSystem/{actionMap.name}".GetOrAddComponentInHierarchy<InputActionMapMono>(transform);
        }

    #endregion

    #region 其他方法

    #endregion

    #region Unity 事件

    #endregion
    }
}