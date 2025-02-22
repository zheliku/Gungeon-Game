// ------------------------------------------------------------
// @file       GunLoadBulletEvent.cs
// @brief
// @author     zheliku
// @Modified   2025-02-01 01:02:06
// @Copyright  Copyright (c) 2025, zheliku
// ------------------------------------------------------------

namespace Game
{
    public struct GunLoadBulletEvent
    {
        public Gun Gun;

        public GunLoadBulletEvent(Gun gun)
        {
            Gun = gun;
        }
    }
}