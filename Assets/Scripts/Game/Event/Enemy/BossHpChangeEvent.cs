// ------------------------------------------------------------
// @file       BossHpChangeEvent.cs
// @brief
// @author     zheliku
// @Modified   2025-04-15 22:04:50
// @Copyright  Copyright (c) 2025, zheliku
// ------------------------------------------------------------

namespace Game
{
    public struct BossHpChangeEvent
    {
        public float HpRatio;

        public BossHpChangeEvent(float hpRatio)
        {
            HpRatio = hpRatio;
        }
    }
}