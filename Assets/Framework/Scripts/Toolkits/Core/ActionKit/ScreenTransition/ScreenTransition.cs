// ------------------------------------------------------------
// @file       ScreenTransition.cs
// @brief
// @author     zheliku
// @Modified   2024-10-31 15:10:42
// @Copyright  Copyright (c) 2024, zheliku
// ------------------------------------------------------------

namespace Framework.Toolkits.ActionKit
{
    using UnityEngine;

    public partial class ActionKit
    {
        public static class ScreenTransition
        {
            public static ScreenTransitionFade FadeIn(float duration = 1.0f, Color color = default)
            {
                return ScreenTransitionFade.Create()
                   .FromAlpha(0)
                   .ToAlpha(1)
                   .Duration(duration)
                   .Color(color);
            }

            public static ScreenTransitionFade FadeOut(float duration = 1.0f, Color color = default)
            {
                return ScreenTransitionFade.Create()
                   .FromAlpha(1)
                   .ToAlpha(0)
                   .Duration(duration)
                   .Color(color);
            }

            public static ScreenTransitionFadeInOut FadeInOut(
                float fadeInDuration = 1.0f,    float fadeOutDuration = 1.0f,
                Color fadeColor      = default, float intervalTime    = 0)
            {
                return ScreenTransitionFadeInOut.Create(
                    FadeIn(fadeInDuration, fadeColor),
                    FadeOut(fadeOutDuration, fadeColor)
                ).IntervalTime(intervalTime);
            }
        }
    }
}