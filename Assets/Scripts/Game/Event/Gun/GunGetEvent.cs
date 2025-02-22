// ------------------------------------------------------------
// @file       GunGetEvent.cs
// @brief
// @author     zheliku
// @Modified   2025-02-01 01:02:04
// @Copyright  Copyright (c) 2025, zheliku
// ------------------------------------------------------------

namespace Game
{
    public struct GunGetEvent
    {
        public Gun Gun;

        public GunGetEvent(Gun gun)
        {
            Gun = gun;
        }
    }
}