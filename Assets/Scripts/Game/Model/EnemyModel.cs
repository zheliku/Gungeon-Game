// ------------------------------------------------------------
// @file       EnemyModel.cs
// @brief
// @author     zheliku
// @Modified   2025-01-30 20:01:53
// @Copyright  Copyright (c) 2025, zheliku
// ------------------------------------------------------------

namespace Game
{
    using System.Collections.Generic;
    using Framework.Core;
    using Framework.Core.Model;

    public class EnemyModel : AbstractModel
    {
        public List<IEnemy> Enemies = new List<IEnemy>();
        
        protected override void OnInit()
        {
            TypeEventSystem.GLOBAL.Register<EnemyCreateEvent>(e =>
            {
                Enemies.Add(e.Enemy);
            });

            TypeEventSystem.GLOBAL.Register<EnemyDieEvent>(e =>
            {
                Enemies.Remove(e.Enemy);
            });
        }
    }
}