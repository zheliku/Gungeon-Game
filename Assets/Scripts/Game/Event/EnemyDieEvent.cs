// ------------------------------------------------------------
// @file       EnemyDieEvent.cs
// @brief
// @author     zheliku
// @Modified   2025-02-23 01:02:13
// @Copyright  Copyright (c) 2025, zheliku
// ------------------------------------------------------------

namespace Game
{
    public struct EnemyDieEvent
    {
        public IEnemy Enemy;

        public EnemyDieEvent(IEnemy enemy)
        {
            Enemy = enemy;
        }
    }
}