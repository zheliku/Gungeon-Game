// ------------------------------------------------------------
// @file       AnimationHelper.cs
// @brief
// @author     zheliku
// @Modified   2025-04-12 03:04:01
// @Copyright  Copyright (c) 2025, zheliku
// ------------------------------------------------------------

namespace Game
{
    using Framework.Toolkits.FluentAPI;
    using UnityEngine;

    public class AnimationHelper
    {
        public static void UpDownAnimation(
            Component component,
            float     upDownTime,   // 动画周期时长
            float     upDownOffset, // 动画偏移量
            float     A)
        {
            var t    = (Time.time % upDownTime - upDownTime * 0.5f).Abs() / (upDownTime * 0.5f);
            var posY = t.Lerp(-A, A);

            component.SetLocalPosition(y: posY + upDownOffset);
        }
    }
}