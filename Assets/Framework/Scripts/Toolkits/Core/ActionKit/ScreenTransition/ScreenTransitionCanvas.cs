// ------------------------------------------------------------
// @file       ScreenTransitionCanvas.cs
// @brief
// @author     zheliku
// @Modified   2024-10-31 15:10:24
// @Copyright  Copyright (c) 2024, zheliku
// ------------------------------------------------------------

namespace Framework.Toolkits.ActionKit
{
    using SingletonKit;
    using UnityEngine;
    using UnityEngine.UI;

    [MonoSingletonPath("Framework/ActionKit/ScreenTransitionCanvas")]
    internal class ScreenTransitionCanvas : MonoBehaviour, ISingleton
    {
        internal static ScreenTransitionCanvas Instance
        {
            get => PrefabSingletonProperty<ScreenTransitionCanvas>.InstanceWithLoader(Resources.Load<GameObject>);
        }

        public Image ColorImage;

        public void OnSingletonInit()
        { }
    }
}