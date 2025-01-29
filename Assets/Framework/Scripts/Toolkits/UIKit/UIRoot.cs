// ------------------------------------------------------------
// @file       UIRoot.cs
// @brief
// @author     zheliku
// @Modified   2024-12-12 14:12:32
// @Copyright  Copyright (c) 2024, zheliku
// ------------------------------------------------------------

namespace Framework.Toolkits.UIKit
{
    using ResKit;
    using SingletonKit;
    using UnityEngine;
    using UnityEngine.UI;

    [MonoSingletonPath(nameof(UIRoot))]
    public class UIRoot : MonoBehaviour, ISingleton
    {
        private static GameObject _Asset;

        private static UIRoot _Instance;

        public static UIRoot Instance
        {
            get
            {
                if (!_Instance)
                {
                    _Instance = FindAnyObjectByType<UIRoot>(); // 先找场景中的 UIRoot
                }

                if (!_Instance)
                {
                    if (_Asset == null) // 找不到 UIRoot，则加载 _Asset
                    {
                        _Asset = ResKit.LoadFromResources<GameObject>("UIRoot");
                    }

                    _Instance      = Instantiate(_Asset).GetComponent<UIRoot>(); // 实例化 UIRoot
                    _Instance.name = "UIRoot";
                    DontDestroyOnLoad(_Instance);
                }

                return _Instance;
            }
        }

        public Camera           UICamera;
        public Canvas           Canvas;
        public CanvasScaler     CanvasScaler;
        public GraphicRaycaster GraphicRaycaster;

        // Level 层级对应的父物体
        public RectTransform Bg;
        public RectTransform Bottom;
        public RectTransform Common;
        public RectTransform Top;

        /// <summary>
        /// 设置参考分辨率
        /// </summary>
        public Vector2 ReferenceResolution
        {
            get => CanvasScaler.referenceResolution;
            set => CanvasScaler.referenceResolution = value;
        }

        /// <summary>
        /// 设置宽高适配比例
        /// </summary>
        public float MatchWidthOrHeight
        {
            get => CanvasScaler.matchWidthOrHeight;
            set => CanvasScaler.matchWidthOrHeight = value;
        }

        /// <summary>
        /// 设置 ScreenSpaceOverlay 渲染模式
        /// </summary>
        public void ScreenSpaceOverlayRenderMode()
        {
            Canvas.renderMode = RenderMode.ScreenSpaceOverlay;
            UICamera.gameObject.SetActive(false);
        }

        /// <summary>
        /// 设置 ScreenSpaceCamera 渲染模式
        /// </summary>
        public void ScreenSpaceCameraRenderMode()
        {
            Canvas.renderMode = RenderMode.ScreenSpaceCamera;
            UICamera.gameObject.SetActive(true);
            Canvas.worldCamera = UICamera;
        }

        /// <summary>
        /// 设置 Panel 层级
        /// </summary>
        /// <param name="level">层级</param>
        /// <param name="panel">哪个 Panel</param>
        public void SetLevelOfPanel(UILevel level, IPanel panel)
        {
            switch (level)
            {
                case UILevel.Bg:
                    panel.Transform.SetParent(Bg, false);
                    break;
                case UILevel.Bottom:
                    panel.Transform.SetParent(Bottom, false);
                    break;
                case UILevel.Common:
                    panel.Transform.SetParent(Common, false);
                    break;
                case UILevel.Top:
                    panel.Transform.SetParent(Top, false);
                    break;
            }
        }

        public void OnSingletonInit() { }

        private void OnDestroy()
        {
            // _Asset?.Release();
        }
    }
}