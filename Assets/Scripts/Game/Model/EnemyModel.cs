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
    using Framework.Core.Model;

    public class EnemyModel : AbstractModel
    {
        public List<Enemy> Enemies = new List<Enemy>();
        
        protected override void OnInit()
        {
            
        }
    }
}