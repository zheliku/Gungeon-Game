// ------------------------------------------------------------
// @file       ResourcesMono.cs
// @brief
// @author     zheliku
// @Modified   2024-12-27 20:12:15
// @Copyright  Copyright (c) 2024, zheliku
// ------------------------------------------------------------

using UnityEngine;

namespace Framework.Toolkits.ResKit
{
    using System.Collections.Generic;
    using Sirenix.OdinInspector;

    /// <summary>
    /// 用于在 Inspector 中显示 Resources 资源的 Mono 对象
    /// </summary>
    public class ResourcesMono : MonoBehaviour
    {
        [ShowInInspector]
        public Dictionary<string, ResourcesMonoInfo> ResMap = new Dictionary<string, ResourcesMonoInfo>();
        
        public void BindRes(Object res)
        {
            if (ResMap.TryAdd(res.AssetPath(), new ResourcesMonoInfo(res.AssetPath(), res)))
            {
                AddRef(res);
            }
        }
        
        public void AddRef(Object res)
        {
            ResMap[res.AssetPath()].RefCount++;
        }

        public void SubRef(Object res)
        {
            if (ResMap.TryGetValue(res.AssetPath(), out var info))
            {
                info.RefCount--;
                if (info.RefCount <= 0) // 资源引用计数为 0，则清空记录
                {
                    ResMap.Remove(res.AssetPath());
                    ResMgr.ResourceAssetPathMap.Remove(res);
                }
            }
        }
    }

    /// <summary>
    /// 显示在 Inspector 中的 Resources Info
    /// </summary>
    [HideReferenceObjectPicker]
    public class ResourcesMonoInfo
    {
        /// <summary>
        /// 显示 Resources 的资源加载路径
        /// </summary>
        [ShowInInspector] [LabelWidth(75)]
        public string AssetPath { get; private set; }
        
        /// <summary>
        /// 显示 Resources 的资源
        /// </summary>
        [ShowInInspector] [LabelWidth(75)]
        public Object Asset { get; private set; }
        
        /// <summary>
        /// 显示 Resources 的引用计数
        /// </summary>
        [ShowInInspector] [LabelWidth(75)]
        public int RefCount { get; set; }

        public ResourcesMonoInfo(string assetPath, Object asset)
        {
            AssetPath = assetPath;
            Asset = asset;
        }
    }
}
