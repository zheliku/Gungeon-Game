// ------------------------------------------------------------
// @file       HitInfo.cs
// @brief
// @author     zheliku
// @Modified   2025-03-14 11:03:08
// @Copyright  Copyright (c) 2025, zheliku
// ------------------------------------------------------------

namespace Game
{
    using UnityEngine;

    public struct HitInfo
    {
        public Vector2 HitPoint { get; set; } // 击中位置

        public Vector2 HitNormal { get; set; } // 击中法线
    }

    public static class HitInfoExtension
    {
        public static HitInfo ToHitInfo(this Collision2D self)
        {
            return new HitInfo()
            {
                HitPoint  = self.contacts[0].point,
                HitNormal = self.relativeVelocity.normalized
            };
        }

        public static HitInfo ToHitInfo(this RaycastHit2D self)
        {
            return new HitInfo()
            {
                HitPoint  = self.point,
                HitNormal = self.normal
            };
        }
    }
}