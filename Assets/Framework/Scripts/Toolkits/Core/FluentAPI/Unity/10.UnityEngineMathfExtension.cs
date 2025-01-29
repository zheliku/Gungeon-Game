// ------------------------------------------------------------
// @file       10.UnityEngineOthersExtension.cs
// @brief
// @author     zheliku
// @Modified   2024-12-17 21:12:22
// @Copyright  Copyright (c) 2024, zheliku
// ------------------------------------------------------------

namespace Framework.Toolkits.FluentAPI
{
    using System.Collections.Generic;
    using System.Linq;
    using Core;
    using UnityEngine;

    public static class UnityEngineMathfExtension
    {
        public static float Lerp(this float self, float a, float b)
        {
            return Mathf.Lerp(a, b, self);
        }

        public static float Abs(this float self)
        {
            return Mathf.Abs(self);
        }

        public static float Abs(this int self)
        {
            return Mathf.Abs(self);
        }
        
        public static float AbsTo(this float self, float to)
        {
            return Mathf.Abs(self - to);
        }

        public static float AbsTo(this int self, float to)
        {
            return Mathf.Abs(self - to);
        }
        
        public static float AbsTo(this int self, int to)
        {
            return Mathf.Abs(self - to);
        }

        public static float AbsTo(this float self, int to)
        {
            return Mathf.Abs(self - to);
        }

        public static bool Approximately(this float self, float other)
        {
            return Mathf.Approximately(self, other);
        }
        
        public static bool Approximately(this float self, int other)
        {
            return Mathf.Approximately(self, other);
        }
        
        public static bool Approximately(this int self, int other)
        {
            return Mathf.Approximately(self, other);
        }
        
        public static bool Approximately(this int self, float other)
        {
            return Mathf.Approximately(self, other);
        }

        public static float Exp(this float self)
        {
            return Mathf.Exp(self);
        }

        public static float Exp(this int self)
        {
            return Mathf.Exp(self);
        }

        public static float Ln(this float self)
        {
            return Mathf.Log(self);
        }

        public static float Ln(this int self)
        {
            return Mathf.Log(self);
        }
        
        public static float Log10(this float self)
        {
            return Mathf.Log10(self);
        }

        public static float Log10(this int self)
        {
            return Mathf.Log10(self);
        }

        public static float Log(this float self, float newBase)
        {
            return Mathf.Log(self, newBase);
        }

        public static float Log(this int self, float newBase)
        {
            return Mathf.Log(self, newBase);
        }

        public static float Pow(this float self, float power)
        {
            return Mathf.Pow(self, power);
        }

        public static float Pow(this int self, float power)
        {
            return Mathf.Pow(self, power);
        }

        public static float Sqrt(this float self)
        {
            return Mathf.Sqrt(self);
        }

        public static float Sqrt(this int self)
        {
            return Mathf.Sqrt(self);
        }

        public static float Sign(this float self)
        {
            return self switch
            {
                < 0 => -1,
                > 0 => 1,
                _   => 0
            };
        }

        public static float Sign(this int self)
        {
            return self switch
            {
                < 0 => -1,
                > 0 => 1,
                _   => 0
            };
        }

        public static float Cos(this float self)
        {
            return Mathf.Cos(self);
        }

        public static float Cos(this int self)
        {
            return Mathf.Cos(self);
        }

        public static float Sin(this float self)
        {
            return Mathf.Sin(self);
        }

        public static float Sin(this int self)
        {
            return Mathf.Sin(self);
        }

        public static float Deg2Rad(this float self)
        {
            return self * Mathf.Deg2Rad;
        }

        public static float Deg2Rad(this int self)
        {
            return self * Mathf.Deg2Rad;
        }

        public static float Rad2Deg(this float self)
        {
            return self * Mathf.Rad2Deg;
        }

        public static float Rad2Deg(this int self)
        {
            return self * Mathf.Rad2Deg;
        }

        public static float ToAngle(this Vector2 self)
        {
            return Mathf.Atan2(self.y, self.x).Rad2Deg();
        }

        public static float Clamp(this float self, float min, float max)
        {
            return Mathf.Clamp(self, min, max);
        }
        
        public static float Clamp(this int self, int min, int max)
        {
            return Mathf.Clamp(self, min, max);
        }

        public static float Clamp01(this float self)
        {
            return Mathf.Clamp01(self);
        }
        
        public static float MinWith(this float self, float min)
        {
            return Mathf.Min(self, min);
        }
        
        public static float MinWith(this float self, int min)
        {
            return Mathf.Min(self, min);
        }
        
        public static float MinWith(this int self, float min)
        {
            return Mathf.Min(self, min);
        }
        
        public static int MinWith(this int self, int min)
        {
            return Mathf.Min(self, min);
        }
        
        public static float MaxWith(this float self, float max)
        {
            return Mathf.Max(self, max);
        }
        
        public static float MaxWith(this float self, int max)
        {
            return Mathf.Max(self, max);
        }
        
        public static float MaxWith(this int self, float max)
        {
            return Mathf.Max(self, max);
        }
        
        public static int MaxWith(this int self, int max)
        {
            return Mathf.Max(self, max);
        }
    }
}