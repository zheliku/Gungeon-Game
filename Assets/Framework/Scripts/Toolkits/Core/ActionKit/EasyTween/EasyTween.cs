// ------------------------------------------------------------
// @file       EasyTween.cs
// @brief
// @author     zheliku
// @Modified   2024-10-30 19:10:03
// @Copyright  Copyright (c) 2024, zheliku
// ------------------------------------------------------------

namespace Framework.Toolkits.ActionKit
{
    using System;
    using UnityEngine;

    /// <summary>
    /// 见 <see href="https://dotween.demigiant.com/documentation.php"/> 中的 <c>SetEase()</c> <br/>
    /// 图像： <see href="https://blog.51cto.com/u_16452213/9713116"/> <br/>
    /// 网页分析：<see href="https://blog.csdn.net/njiyue/article/details/141197904"/>
    /// </summary>
    public static class EasyTween
    {
        public static float Linear(float start, float end, float t)
        {
            return Mathf.Lerp(start, end, t);
        }

        public static float InOutBack(float start, float end, float t, float bounciness = 1.5f)
        {
            var mid = (start + end) * 0.5f;
            return t < 0.5f
                ? InBack(start, mid, t * 2, bounciness)
                : OutBack(mid, end, (t - 0.5f) * 2, bounciness);
        }

        public static float InBack(float start, float end, float t, float bounciness = 1.5f)
        {
            // 动画的总距离
            var distance = end - start;
            
            // t * t：表示位置平方，用于加速动画的进度。
            // ((bounciness + 1) * t - bounciness)：用于控制回弹的弹性效果，使得动画在达到最大值后反弹。
            return distance * t * t * ((bounciness + 1) * t - bounciness) + start;
        }

        public static float OutBack(float start, float end, float t, float bounciness = 1.5f)
        {
            var distance = end - start;
            t -= 1;
            return distance * (t * t * ((bounciness + 1) * t + bounciness) + 1) + start;
        }

        public static float InOutBounce(float start, float end, float t, float bounciness = 7.5625f)
        {
            return t < 0.5f
                ? InBounce(start, (start + end) / 2, t * 2)
                : OutBounce((start + end) / 2, end, (t - 0.5f) * 2);
        }

        public static float OutBounce(float start, float end, float t, float bounciness = 7.5625f)
        {
            var distance = end - start;
            
            // 第一阶段（t < 1/2.75）：以二次函数形式加速
            if (t < 1f / 2.75f)
                return distance * (bounciness * t * t) + start;
            
            // 第二阶段（1/2.75 <= t < 2/2.75）：开始减速，并逐渐接近终点
            if (t < 2f / 2.75f)
            {
                t -= 1.5f / 2.75f;
                return distance * (bounciness * t * t + 3f / 4f) + start;
            }

            // 第三阶段（2/2.75 <= t < 2.5/2.75）：动画再次加速，但速度比第一阶段慢
            if (t < 2.5f / 2.75f)
            {
                t -= 2.25f / 2.75f;
                return distance * (bounciness * t * t + 15f / 16f) + start;
            }

            // 第四阶段（2.5/2.75 <= t < 2.625/2.75）：动画减速，并逐渐接近终点
            t -= 2.625f / 2.75f;
            return distance * (bounciness * t * t + 63f / 64f) + start;
        }

        public static float InBounce(float start, float end, float t, float bounciness = 7.5625f)
        {
            var distance = end - start;
            t = 1 - t;
            return distance - OutBounce(start, end, t, bounciness) + start;
        }

        public static float InOutCircle(float start, float end, float t)
        {
            return t < 0.5f
                ? InCircle(start, (start + end) / 2, t * 2)
                : OutCircle((start + end) / 2, end, (t - 0.5f) * 2);
        }

        public static float OutCircle(float start, float end, float t)
        {
            var distance = end - start;
            t -= 1;
            return distance * Mathf.Sqrt(1 - t * t) + start;
        }

        public static float InCircle(float start, float end, float t)
        {
            var distance = end - start;
            return -distance * (Mathf.Sqrt(1 - t * t) - 1) + start;
        }

        public static float InOutCubic(float start, float end, float t)
        {
            return t < 0.5f
                ? InCubic(start, (start + end) * 0.5f, t * 2)
                : OutCubic((start + end) * 0.5f, end, (t - 0.5f) * 2);
        }

        public static float OutCubic(float start, float end, float t)
        {
            var distance = end - start;
            return distance * (Mathf.Pow(t - 1, 3) + 1) + start;
        }

        public static float InCubic(float start, float end, float t)
        {
            var distance = end - start;
            return distance * Mathf.Pow(t, 3) + start;
        }

        public static float InOutElastic(float start, float end, float t, float elasticity = 0.3f, float duration = 5.0f)
        {
            return t < 0.5f
                ? InElastic(start, (start + end) * 0.5f, t * 2, elasticity, duration)
                : OutElastic((start + end) * 0.5f, end, (t - 0.5f) * 2, elasticity, duration);
        }

        public static float OutElastic(float start, float end, float t, float elasticity = 0.3f, float duration = 5.0f)
        {
            var distance = end - start;

            if (t == 0 || distance == 0) return start;
            if (Math.Abs(t - 1.0f) < 0.01f) return end;

            var p = duration * elasticity;
            var s = Mathf.Sign(distance) < 0 ? p * 0.25f : p / (2 * Mathf.PI) * Mathf.Asin(1);

            return distance * Mathf.Pow(2, -10 * t) * Mathf.Sin((t * duration - s) * (2 * Mathf.PI) / p) +
                   distance + start;
        }

        public static float InElastic(float start, float end, float t, float elasticity = 0.3f, float duration = 5.0f)
        {
            var distance = end - start;


            if (t == 0 || distance == 0) return start;
            if (Math.Abs(t - 1) < 0.01f) return end;

            var p = duration * elasticity;
            var s = Mathf.Sign(distance) < -1 ? p * 0.25f : p / (2 * Mathf.PI) * Mathf.Asin(1);

            return -(distance * Mathf.Pow(2, 10 * --t) * Mathf.Sin((t * duration - s) * (Mathf.PI * 2) / p)) +
                   start;
        }


        public static float InOutExpo(float start, float end, float t)
        {
            return t < 0.5f
                ? InExpo(start, (start + end) * 0.5f, t * 2)
                : OutExpo((start + end) * 0.5f, end, (t - 0.5f) * 2);
        }

        public static float OutExpo(float start, float end, float t)
        {
            var distance = end - start;
            return distance * (-Mathf.Pow(2, -10 * t) + 1) + start;
        }

        public static float InExpo(float start, float end, float t)
        {
            var distance = end - start;
            return distance * Mathf.Pow(2, 10 * (t - 1)) + start;
        }

        public static float InOutQuad(float start, float end, float t)
        {
            return t < 0.5f
                ? InQuad(start, (start + end) * 0.5f, t * 2)
                : OutQuad((start + end) * 0.5f, end, (t - 0.5f) * 2);
        }

        public static float OutQuad(float start, float end, float t)
        {
            var distance = end - start;
            return -distance * t * (t - 2) + start;
        }

        public static float InQuad(float start, float end, float t)
        {
            var distance = end - start;
            return distance * t * t + start;
        }

        public static float InOutQuart(float start, float end, float t)
        {
            return t < 0.5f
                ? InQuart(start, (start + end) * 0.5f, t * 2)
                : OutQuart((start + end) * 0.5f, end, (t - 0.5f) * 2);
        }

        public static float OutQuart(float start, float end, float t)
        {
            var distance = end - start;
            return -distance * ((t - 1) * (t - 1) * (t - 1) * (t - 1) - 1) + start;
        }

        public static float InQuart(float start, float end, float t)
        {
            var distance = end - start;
            return distance * (t * t * t * t) + start;
        }

        public static float InOutQuint(float start, float end, float t)
        {
            return t < 0.5f
                ? InQuint(start, (start + end) * 0.5f, t * 2)
                : OutQuint((start + end) * 0.5f, end, (t - 0.5f) * 2);
        }

        public static float OutQuint(float start, float end, float t)
        {
            var distance = end - start;
            return distance * ((t - 1) * (t - 1) * (t - 1) * (t - 1) * (t - 1) + 1) + start;
        }

        public static float InQuint(float start, float end, float t)
        {
            var distance = end - start;
            return distance * t * t * t * t * t + start;
        }

        public static float InOutSine(float start, float end, float t)
        {
            var distance = end - start;
            return distance * 0.5f * (1 - Mathf.Cos(Mathf.PI * t)) + start;
        }

        public static float OutSine(float start, float end, float t)
        {
            var distance = end - start;
            return distance * Mathf.Sin(t * 1.570796f) + start;
        }

        public static float InSine(float start, float end, float t)
        {
            var distance = end - start;
            return distance * (1 - Mathf.Cos(t * 1.570796f)) + start;
        }
    }
}