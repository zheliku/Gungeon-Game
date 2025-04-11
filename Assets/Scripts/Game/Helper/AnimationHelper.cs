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
            float     totalTime,    // 游戏总时长
            float     upDownTime,   // 动画周期时长
            float     upDownOffset, // 动画偏移量
            float     A)
        {
            var t    = (totalTime % upDownTime - upDownTime * 0.5f).Abs() / (upDownTime * 0.5f);
            var posY = t.Lerp(-A, A);

            component.SetLocalPosition(y: posY + upDownOffset);
        }

        public static void RotateAnimation(
            Component component,
            float     totalTime,  // 游戏总时长
            float     rotateTime, // 动画周期时长
            float     angle)
        {
            var t      = (totalTime % rotateTime - rotateTime * 0.5f).Abs() / (rotateTime * 0.5f);
            var angleZ = t.Lerp(-angle, angle);

            component.SetLocalEulerAngles(z: angleZ);
        }
    }
}