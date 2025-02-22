// ------------------------------------------------------------
// @file       GunShootEvent.cs
// @brief
// @author     zheliku
// @Modified   2025-02-01 01:02:12
// @Copyright  Copyright (c) 2025, zheliku
// ------------------------------------------------------------

namespace Game
{
    public struct GunShootEvent
    {
        public Gun Gun;

        public GunShootEvent(Gun gun)
        {
            Gun = gun;
        }
    }
}