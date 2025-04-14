// ------------------------------------------------------------
// @file       7.UnityEngineVectorExtension.cs
// @brief
// @author     zheliku
// @Modified   2024-10-18 22:10:55
// @Copyright  Copyright (c) 2024, zheliku
// ------------------------------------------------------------

namespace Framework.Toolkits.FluentAPI
{
    using UnityEngine;

    /// <summary>
    /// 针对 <see cref="UnityEngine.Vector4"/>、<see cref="UnityEngine.Vector3"/>、<see cref="UnityEngine.Vector2"/> 提供的链式扩展
    /// </summary>
    public static class UnityEngineVectorExtension
    {
        public static Vector4 Set(this Vector4 self, float? x = null, float? y = null, float? z = null, float? w = null)
        {
            return new Vector4(x ?? self.x, y ?? self.y, z ?? self.z, w ?? self.w);
        }

        public static Vector4 Set(this Vector4 self, Vector3 value)
        {
            return new Vector4(value.x, value.y, value.z, self.w);
        }

        public static Vector4 Set(this Vector4 self, Vector2 value)
        {
            return new Vector4(value.x, value.y, self.z, self.w);
        }

        public static Vector3 Set(this Vector3 self, float? x = null, float? y = null, float? z = null)
        {
            return new Vector3(x ?? self.x, y ?? self.y, z ?? self.z);
        }

        public static Vector3 Set(this Vector3 self, Vector2 value)
        {
            return new Vector3(value.x, value.y, self.z);
        }

        public static Vector2 Set(this Vector2 self, float? x = null, float? y = null)
        {
            return new Vector2(x ?? self.x, y ?? self.y);
        }

        public static Vector4 Add(this Vector4 self, float? x = null, float? y = null, float? z = null, float? w = null)
        {
            return new Vector4(self.x + (x ?? 0), self.y + (y ?? 0), self.z + (z ?? 0), self.w + (w ?? 0));
        }
        
        public static Vector4 Add(this Vector4 self, Vector4 value)
        {
            return self + value;
        }
        
        public static Vector4 Add(this Vector4 self, Vector3 value)
        {
            return self + value.ToVector4();
        }
        
        public static Vector4 Add(this Vector4 self, Vector2 value)
        {
            return self + value.ToVector4();
        }

        public static Vector3 Add(this Vector3 self, float? x = null, float? y = null, float? z = null)
        {
            return new Vector3(self.x + (x ?? 0), self.y + (y ?? 0), self.z + (z ?? 0));
        }
        
        public static Vector3 Add(this Vector3 self, Vector3 value)
        {
            return self + value;
        }
        
        public static Vector3 Add(this Vector3 self, Vector2 value)
        {
            return self + value.ToVector3();
        }

        public static Vector2 Add(this Vector2 self, float? x = null, float? y = null)
        {
            return new Vector2(self.x + (x ?? 0), self.y + (y ?? 0));
        }
        
        public static Vector2 Add(this Vector2 self, Vector2 value)
        {
            return self + value;
        }

        public static Vector4 Multiply(this Vector4 self, float? x = null, float? y = null, float? z = null, float? w = null)
        {
            return new Vector4(self.x * (x ?? 1), self.y * (y ?? 1), self.z * (z ?? 1), self.w * (w ?? 1));
        }

        public static Vector3 Multiply(this Vector3 self, float? x = null, float? y = null, float? z = null)
        {
            return new Vector3(self.x * (x ?? 1), self.y * (y ?? 1), self.z * (z ?? 1));
        }

        public static Vector2 Multiply(this Vector2 self, float? x = null, float? y = null)
        {
            return new Vector2(self.x * (x ?? 1), self.y * (y ?? 1));
        }

        public static Vector2 ToVector2(this Vector3 self)
        {
            return new Vector2(self.x, self.y);
        }

        public static Vector3 ToVector3(this Vector2 self, float z = 0)
        {
            return new Vector3(self.x, self.y, z);
        }

        public static Vector3 ToVector3(this Vector4 self)
        {
            return new Vector3(self.x, self.y, self.z);
        }

        public static Vector4 ToVector4(this Vector3 self, float w = 0)
        {
            return new Vector4(self.x, self.y, self.z, w);
        }

        public static Vector2 ToVector2(this Vector4 self)
        {
            return new Vector2(self.x, self.y);
        }

        public static Vector4 ToVector4(this Vector2 self, float z = 0, float w = 0)
        {
            return new Vector4(self.x, self.y, z, w);
        }

        public static Vector3 PositionTo(this Component self, Component to)
        {
            return to.transform.position - self.transform.position;
        }

        public static Vector3 PositionTo(this GameObject self, GameObject to)
        {
            return to.transform.position - self.transform.position;
        }

        public static Vector3 PositionTo(this Component self, GameObject to)
        {
            return to.transform.position - self.transform.position;
        }

        public static Vector3 PositionTo(this GameObject self, Component to)
        {
            return to.transform.position - self.transform.position;
        }
        
        public static Vector3 PositionTo(this Component self, Vector3 to)
        {
            return to - self.transform.position;
        }

        public static Vector3 PositionTo(this GameObject self, Vector3 to)
        {
            return to - self.transform.position;
        }

        public static Vector3 PositionFrom(this Component self, Component from)
        {
            return self.transform.position - from.transform.position;
        }

        public static Vector3 PositionFrom(this GameObject self, GameObject from)
        {
            return self.transform.position - from.transform.position;
        }

        public static Vector3 PositionFrom(this GameObject self, Component from)
        {
            return self.transform.position - from.transform.position;
        }

        public static Vector3 PositionFrom(this Component self, GameObject from)
        {
            return self.transform.position - from.transform.position;
        }
        
        public static Vector3 PositionFrom(this GameObject self, Vector3 from)
        {
            return self.transform.position - from;
        }

        public static Vector3 PositionFrom(this Component self, Vector3 from)
        {
            return self.transform.position - from;
        }

        public static Vector2 Position2DTo(this Component self, Component to)
        {
            return to.transform.position - self.transform.position;
        }

        public static Vector2 Position2DTo(this GameObject self, GameObject to)
        {
            return to.transform.position - self.transform.position;
        }

        public static Vector2 Position2DTo(this Component self, GameObject to)
        {
            return to.transform.position - self.transform.position;
        }

        public static Vector2 Position2DTo(this GameObject self, Component to)
        {
            return to.transform.position - self.transform.position;
        }
        
        public static Vector2 Position2DTo(this Component self, Vector2 to)
        {
            return to - self.transform.position.ToVector2();
        }

        public static Vector2 Position2DTo(this GameObject self, Vector2 to)
        {
            return to - self.transform.position.ToVector2();
        }

        public static Vector2 Position2DFrom(this Component self, Component from)
        {
            return self.transform.position - from.transform.position;
        }

        public static Vector2 Position2DFrom(this GameObject self, GameObject from)
        {
            return self.transform.position - from.transform.position;
        }

        public static Vector2 Position2DFrom(this GameObject self, Component from)
        {
            return self.transform.position - from.transform.position;
        }

        public static Vector2 Position2DFrom(this Component self, GameObject from)
        {
            return self.transform.position - from.transform.position;
        }
        
        public static Vector2 Position2DFrom(this GameObject self, Vector2 from)
        {
            return self.transform.position.ToVector2() - from;
        }

        public static Vector2 Position2DFrom(this Component self, Vector2 from)
        {
            return self.transform.position.ToVector2() - from;
        }

        public static Vector3 DirectionTo(this Component self, Component to)
        {
            return (to.transform.position - self.transform.position).normalized;
        }

        public static Vector3 DirectionTo(this GameObject self, GameObject to)
        {
            return (to.transform.position - self.transform.position).normalized;
        }

        public static Vector3 DirectionTo(this Component self, GameObject to)
        {
            return (to.transform.position - self.transform.position).normalized;
        }

        public static Vector3 DirectionTo(this GameObject self, Component to)
        {
            return (to.transform.position - self.transform.position).normalized;
        }
        
        public static Vector3 DirectionTo(this Component self, Vector3 to)
        {
            return (to - self.transform.position).normalized;
        }

        public static Vector3 DirectionTo(this GameObject self, Vector3 to)
        {
            return (to - self.transform.position).normalized;
        }

        public static Vector3 DirectionFrom(this Component self, Component from)
        {
            return (self.transform.position - from.transform.position).normalized;
        }

        public static Vector3 DirectionFrom(this GameObject self, GameObject from)
        {
            return (self.transform.position - from.transform.position).normalized;
        }

        public static Vector3 DirectionFrom(this GameObject self, Component from)
        {
            return (self.transform.position - from.transform.position).normalized;
        }

        public static Vector3 DirectionFrom(this Component self, GameObject from)
        {
            return (self.transform.position - from.transform.position).normalized;
        }
        
        public static Vector3 DirectionFrom(this GameObject self, Vector3 from)
        {
            return (self.transform.position - from).normalized;
        }

        public static Vector3 DirectionFrom(this Component self, Vector3 from)
        {
            return (self.transform.position - from).normalized;
        }

        public static Vector2 Direction2DTo(this Component self, Component to)
        {
            return (to.transform.position - self.transform.position).normalized;
        }

        public static Vector2 Direction2DTo(this GameObject self, GameObject to)
        {
            return (to.transform.position - self.transform.position).normalized;
        }

        public static Vector2 Direction2DTo(this Component self, GameObject to)
        {
            return (to.transform.position - self.transform.position).normalized;
        }

        public static Vector2 Direction2DTo(this GameObject self, Component to)
        {
            return (to.transform.position - self.transform.position).normalized;
        }
        
        public static Vector2 Direction2DTo(this Component self, Vector2 to)
        {
            return (to - self.transform.position.ToVector2()).normalized;
        }

        public static Vector2 Direction2DTo(this GameObject self, Vector2 to)
        {
            return (to - self.transform.position.ToVector2()).normalized;
        }

        public static Vector2 Direction2DFrom(this Component self, Component from)
        {
            return (self.transform.position - from.transform.position).normalized;
        }

        public static Vector2 Direction2DFrom(this GameObject self, GameObject from)
        {
            return (self.transform.position - from.transform.position).normalized;
        }

        public static Vector2 Direction2DFrom(this GameObject self, Component from)
        {
            return (self.transform.position - from.transform.position).normalized;
        }

        public static Vector2 Direction2DFrom(this Component self, GameObject from)
        {
            return (self.transform.position - from.transform.position).normalized;
        }
        
        public static Vector2 Direction2DFrom(this GameObject self, Vector2 from)
        {
            return (self.transform.position.ToVector2() - from).normalized;
        }

        public static Vector2 Direction2DFrom(this Component self, Vector2 from)
        {
            return (self.transform.position.ToVector2() - from).normalized;
        }

        public static float Distance(this GameObject self, GameObject other)
        {
            return Vector3.Distance(self.GetPosition(), other.GetPosition());
        }

        public static float Distance(this Component self, GameObject other)
        {
            return Vector3.Distance(self.GetPosition(), other.GetPosition());
        }

        public static float Distance(this GameObject self, Component other)
        {
            return Vector3.Distance(self.GetPosition(), other.GetPosition());
        }

        public static float Distance(this Component self, Component other)
        {
            return Vector3.Distance(self.GetPosition(), other.GetPosition());
        }
        
        public static float Distance(this GameObject self, Vector3 other)
        {
            return Vector3.Distance(self.GetPosition(), other);
        }

        public static float Distance(this Component self, Vector3 other)
        {
            return Vector3.Distance(self.GetPosition(), other);
        }

        public static float Distance2D(this GameObject self, GameObject other)
        {
            return Vector2.Distance(self.GetPosition(), other.GetPosition());
        }

        public static float Distance2D(this Component self, GameObject other)
        {
            return Vector2.Distance(self.GetPosition(), other.GetPosition());
        }

        public static float Distance2D(this GameObject self, Component other)
        {
            return Vector2.Distance(self.GetPosition(), other.GetPosition());
        }

        public static float Distance2D(this Component self, Component other)
        {
            return Vector2.Distance(self.GetPosition(), other.GetPosition());
        }
        
        public static float Distance2D(this GameObject self, Vector2 other)
        {
            return Vector2.Distance(self.GetPosition(), other);
        }

        public static float Distance2D(this Component self, Vector2 other)
        {
            return Vector2.Distance(self.GetPosition(), other);
        }

        public static Vector2 Lerp(this Vector2 self, Vector2 target, float t)
        {
            return Vector2.Lerp(self, target, t);
        }
        
        public static Vector2 LerpWithSpeed(this Vector2 self, Vector2 target, float speed)
        {
            return Vector2.Lerp(self, target, 1 - Mathf.Exp(-speed));
        }
        
        public static Vector3 Lerp(this Vector3 self, Vector3 target, float t)
        {
            return Vector3.Lerp(self, target, t);
        }
        
        public static Vector3 LerpWithSpeed(this Vector3 self, Vector3 target, float speed)
        {
            return Vector3.Lerp(self, target, 1 - Mathf.Exp(-speed));
        }

        public static Vector2 Rotate(this Vector2 self, float angle)
        {
            return Quaternion.Euler(0, 0, angle) * self;
        }
    }
}