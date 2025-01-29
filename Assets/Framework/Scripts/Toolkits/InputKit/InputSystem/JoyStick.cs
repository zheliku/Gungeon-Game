// ------------------------------------------------------------
// @file       JoyStick.cs
// @brief
// @author     zheliku
// @Modified   2025-01-05 03:01:01
// @Copyright  Copyright (c) 2025, zheliku
// ------------------------------------------------------------

using UnityEngine;

namespace Game
{
    using Framework.Core;
    using Framework.Toolkits.EventKit;
    using Framework.Toolkits.FluentAPI;
    using UnityEngine.EventSystems;
    using UnityEngine.InputSystem.OnScreen;

    /// <summary>
    /// 显示 Android 手柄
    /// </summary>
    public class JoyStick : MonoBehaviour, IPointerDownHandler, IDragHandler, IPointerUpHandler
    {
        public OnScreenStick Handle;
        public GameObject    Bg;

        private void Awake()
        {
            Handle = "Bg/Handle".GetComponentInHierarchy<OnScreenStick>(transform);
            Bg     = "Bg".GetGameObjectInHierarchy(transform);

#if !UNITY_ANDROID
            Bg.Disable();
#endif
            Handle.movementRange = Bg.GetComponent<RectTransform>().rect.width / 2;
        }

        public void OnDrag(PointerEventData eventData)
        {
            Handle.OnDrag(eventData);
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            Bg.Enable();

            Handle.OnPointerUpEvent((e) =>
            {
                Bg.Disable();
            }).UnRegisterWhenGameObjectDisabled(Bg);

            Bg.SetPosition(eventData.position);

            Handle.OnPointerDown(eventData);
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            Handle.OnPointerUp(eventData);
        }
    }
}