// ------------------------------------------------------------
// @file       GunLoadBulletEvent.cs
// @brief
// @author     zheliku
// @Modified   2025-02-01 01:02:06
// @Copyright  Copyright (c) 2025, zheliku
// ------------------------------------------------------------

namespace Game
{
    public struct GunBulletEmptyEvent
    {
        public Gun Gun;

        public GunBulletEmptyEvent(Gun gun)
        {
            Gun = gun;
        }
    }
}