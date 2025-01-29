// ------------------------------------------------------------
// @file       View.cs
// @brief
// @author     zheliku
// @Modified   2024-10-06 02:10:20
// @Copyright  Copyright (c) 2024, zheliku
// ------------------------------------------------------------

namespace Framework.Core.View
{
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
            get => Architecture;
        }

        /// <summary>
        /// 子类需要指定该 View 属于哪个 Architecture
        /// </summary>
        [ShowInInspector]
        protected abstract IArchitecture Architecture { get; }
    }
}