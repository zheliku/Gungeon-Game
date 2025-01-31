// ------------------------------------------------------------
// @file       EnemyModel.cs
// @brief
// @author     zheliku
// @Modified   2025-01-30 20:01:53
// @Copyright  Copyright (c) 2025, zheliku
// ------------------------------------------------------------

namespace Game
{
    using Framework.Core.Model;

    public class EnemyModel : AbstractModel
    {
        public Property Property = new Property();
        
        protected override void OnInit()
        {
            Property.MoveSpeed = 2;
        }
    }
}