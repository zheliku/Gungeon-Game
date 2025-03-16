// ------------------------------------------------------------
// @file       UIPanel.cs
// @brief
// @author     zheliku
// @Modified   2024-12-12 19:12:14
// @Copyright  Copyright (c) 2024, zheliku
// ------------------------------------------------------------

using UnityEngine;

namespace Framework.Toolkits.UIKit
{
    using System;
    using Core;
    using Sirenix.OdinInspector;

    public abstract class UIPanel : AbstractView, IPanel
    {
        public Transform Transform { get => transform; }

        [ShowInInspector] [EnumToggleButtons]
        public PanelState State
        {
            get;
            protected set;
        }
        
        public UILevel Level
        {
            get => Enum.Parse<UILevel>(transform.parent?.name); // 父物体名称是该 Panel 的 Level
            set
            {
                if (Enum.TryParse<UILevel>(transform.parent?.name, out var level))
                {
                    if (level == value) // 相同的 level 不做处理
                    {
                        return;
                    }
                }
                UIRoot.Instance.SetLevelOfPanel(value, this);
            }
        }

        void IPanel.Load() // 对自己隐藏
        {
            State = PanelState.Loaded;
            this.BindHierarchyComponent();
            OnLoad();
            gameObject.SetActive(false);
        }

        public void Show()
        {
            State = PanelState.Shown;
            gameObject.SetActive(true);
            OnShow();
        }

        public void Hide()
        {
            State = PanelState.Hide;
            gameObject.SetActive(false);
            OnHide();
        }

        void IPanel.Unload() // 对自己隐藏
        {
            State = PanelState.Unloaded;
            OnUnload();
        }

        protected virtual void OnLoad() { }

        protected virtual void OnShow() { }

        protected virtual void OnHide() { }

        protected virtual void OnUnload() { }
    }
}