// ------------------------------------------------------------
// @file       View.cs
// @brief
// @author     zheliku
// @Modified   2024-10-06 02:10:20
// @Copyright  Copyright (c) 2024, zheliku
// ------------------------------------------------------------

namespace Framework.Core
{
    using System;
    using Sirenix.OdinInspector;
    using UnityEngine;

    /// <summary>
    /// View 基类
    /// </summary>
    [HideReferenceObjectPicker]
    public abstract class AbstractView : MonoBehaviour, IView
    {
        // 仅能通过 IBelongToArchitecture 接口访问 Architecture 属性
        IArchitecture IBelongToArchitecture.Architecture
        {
            get => _Architecture;
        }

        /// <summary>
        /// 子类需要指定该 View 属于哪个 Architecture
        /// </summary>
        [ShowInInspector]
        protected abstract IArchitecture _Architecture { get; }
    }

    /// <summary>
    /// 空路径：表示自己 <br/>
    /// 以 "/" 开头：表示从根节点开始查找 <br/>
    /// 其余情况：以自己为根结点的相对路径
    /// </summary>
    public class HierarchyPathAttribute : Attribute
    {
        public readonly string HierarchyPath;
        
        public readonly bool LogErrorIfNotFound = true;
        
        public HierarchyPathAttribute()
        {
            HierarchyPath = string.Empty;
        }
        
        public HierarchyPathAttribute(bool logErrorIfNotFound)
        {
            LogErrorIfNotFound = logErrorIfNotFound;
        }

        public HierarchyPathAttribute(string hierarchyPath, bool logErrorIfNotFound = true)
        {
            HierarchyPath = hierarchyPath;
            LogErrorIfNotFound = logErrorIfNotFound;
        }
    }
}