// ------------------------------------------------------------
// @file       ShootEvent.cs
// @brief
// @author     zheliku
// @Modified   2025-02-01 00:02:49
// @Copyright  Copyright (c) 2025, zheliku
// ------------------------------------------------------------

namespace Game
{
    public struct GunBulletChangeEvent
    {
        public Gun Gun;

        public GunBulletChangeEvent(Gun gun)
        {
            Gun = gun;
        }
    }
}