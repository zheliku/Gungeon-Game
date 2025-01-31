// ------------------------------------------------------------
// @file       ShootEvent.cs
// @brief
// @author     zheliku
// @Modified   2025-02-01 00:02:49
// @Copyright  Copyright (c) 2025, zheliku
// ------------------------------------------------------------

namespace Game
{
    public struct GunChangeEvent
    {
        public Gun OldGun;
        
        public Gun NewGun;

        public GunChangeEvent(Gun oldGun, Gun newGun)
        {
            OldGun = oldGun;
            NewGun = newGun;
        }
    }
}