// ------------------------------------------------------------
// @file       ResMgr.cs
// @brief
// @author     zheliku
// @Modified   2024-12-09 20:12:36
// @Copyright  Copyright (c) 2024, zheliku
// ------------------------------------------------------------

namespace Framework.Toolkits.ResKit
{
    using System;
    using System.Collections.Generic;
    using FluentAPI;
    using SingletonKit;
    using Sirenix.OdinInspector;
    using UnityEngine;
    using UnityEngine.ResourceManagement.AsyncOperations;
    using Object = UnityEngine.Object;

    [MonoSingletonPath("Framework/ResKit")]
    public class ResMgr : MonoSingleton<ResMgr>
    {
    #region 常量

    #endregion

    #region Static

        /// <summary>
        /// 记录 AsyncOperationHandle 的附属信息：资源名称
        /// </summary>
        [ShowInInspector]
        public static Dictionary<AsyncOperationHandle, string> HandleAssetNameMap = new Dictionary<AsyncOperationHandle, string>();

        /// <summary>
        /// 记录 AsyncOperationHandle 的附属信息：资源类型
        /// </summary>
        [ShowInInspector]
        public static Dictionary<AsyncOperationHandle, Type> HandleAssetTypeMap = new Dictionary<AsyncOperationHandle, Type>();

        /// <summary>
        /// 记录 Resources 的附属信息：资源加载路径
        /// </summary>
        [ShowInInspector]
        public static Dictionary<Object, string> ResourceAssetPathMap = new Dictionary<Object, string>();

    #endregion

    #region 字段

        public Transform ResourcesMonoParent;   // 用于挂载 ResourcesMono 的父节点
        public Transform AddressableMonoParent; // 用于挂载 AddressableMono 的父节点

    #endregion

    #region 属性

    #endregion

    #region 公共方法

        public override void OnSingletonInit()
        {
            ResourcesMonoParent = new GameObject("Resources").transform;
            ResourcesMonoParent.SetParent(transform);

            AddressableMonoParent = new GameObject("Addressable").transform;
            AddressableMonoParent.SetParent(transform);
        }

        public AddressableMono GetAddressableMono(AsyncOperationHandle handle)
        {
            return $"{handle.AssetType().Name}".GetOrAddComponentInHierarchy<AddressableMono>(AddressableMonoParent);
        }

        public ResourcesMono GetResourcesMono(Object res)
        {
            return $"{res.GetType().Name}".GetOrAddComponentInHierarchy<ResourcesMono>(ResourcesMonoParent);
        }

    #endregion
    }
}