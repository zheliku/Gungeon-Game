// ------------------------------------------------------------
// @file       PlayerModel.cs
// @brief
// @author     zheliku
// @Modified   2025-01-30 20:01:35
// @Copyright  Copyright (c) 2025, zheliku
// ------------------------------------------------------------

namespace Game
{
    using Framework.Core.Model;

    public class PlayerModel : AbstractModel
    {
        public Property Property = new Property();
        
        protected override void OnInit()
        {
            Property.Hp.SetValueWithoutEvent(3);
            Property.MoveSpeed = 5;
        }
    }
}