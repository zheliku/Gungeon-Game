// ------------------------------------------------------------
// @file       BossCreateEvent.cs
// @brief
// @author     zheliku
// @Modified   2025-04-15 21:04:22
// @Copyright  Copyright (c) 2025, zheliku
// ------------------------------------------------------------

namespace Game
{
    public struct BossDieEvent
    {
        public Boss Boss;

        public BossDieEvent(Boss boss)
        {
            Boss = boss;
        }
    }
}