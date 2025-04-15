// ------------------------------------------------------------
// @file       BossCreateEvent.cs
// @brief
// @author     zheliku
// @Modified   2025-04-15 21:04:22
// @Copyright  Copyright (c) 2025, zheliku
// ------------------------------------------------------------

namespace Game
{
    public struct BossCreateEvent
    {
        public Boss Boss;

        public BossCreateEvent(Boss boss)
        {
            Boss = boss;
        }
    }
}