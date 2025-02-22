// ------------------------------------------------------------
// @file       EnemyDieEvent.cs
// @brief
// @author     zheliku
// @Modified   2025-02-23 01:02:13
// @Copyright  Copyright (c) 2025, zheliku
// ------------------------------------------------------------

namespace Game
{
    public struct EnemyCreateEvent
    {
        public Enemy Enemy;

        public EnemyCreateEvent(Enemy enemy)
        {
            Enemy = enemy;
        }
    }
}