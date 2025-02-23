// ------------------------------------------------------------
// @file       GunLoadBulletEvent.cs
// @brief
// @author     zheliku
// @Modified   2025-02-01 01:02:06
// @Copyright  Copyright (c) 2025, zheliku
// ------------------------------------------------------------

namespace Game
{
    public struct GunBulletLoadingEvent
    {
        public Gun Gun;

        public GunBulletLoadingEvent(Gun gun)
        {
            Gun = gun;
        }
    }
}