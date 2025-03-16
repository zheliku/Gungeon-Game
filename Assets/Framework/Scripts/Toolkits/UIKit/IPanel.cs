// ------------------------------------------------------------
// @file       IUIPanel.cs
// @brief
// @author     zheliku
// @Modified   2024-12-12 14:12:47
// @Copyright  Copyright (c) 2024, zheliku
// ------------------------------------------------------------

namespace Framework.Toolkits.UIKit
{
    using UnityEngine;

    public enum PanelState
    {
        Loaded,
        Shown,
        Hide,
        Unloaded,
    }

    public enum UILevel
    {
        Bg     = -100, // 背景层
        Bottom = -99, // 底部层
        Common = 0,  // 普通层
        Top    = 99, // 顶部层
    }

    public interface IPanel
    {
        /// <summary>
        /// Panel 依附的 Transform
        /// </summary>
        Transform Transform { get; }

        /// <summary>
        /// Panel 状态
        /// </summary>
        PanelState State { get; }
        
        /// <summary>
        /// Panel 层级
        /// </summary>
        UILevel Level { get; set; }

        /// <summary>
        ///  加载 Panel
        /// </summary>
        void Load();

        /// <summary>
        /// 显示 Panel
        /// </summary>
        void Show();

        /// <summary>
        /// 隐藏 Panel
        /// </summary>
        void Hide();

        /// <summary>
        /// 卸载 Panel
        /// </summary>
        void Unload();
    }
}