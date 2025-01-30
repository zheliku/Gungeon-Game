// ------------------------------------------------------------
// @file       LevelModel.cs
// @brief
// @author     zheliku
// @Modified   2025-01-30 20:01:32
// @Copyright  Copyright (c) 2025, zheliku
// ------------------------------------------------------------

namespace Game
{
    using System.Collections.Generic;
    using Framework.Core.Model;

    public class LevelModel : AbstractModel
    {
        /// <summary>
        /// 1：地块
        /// @：主角
        /// e：敌人
        /// </summary>
        public List<string> InitRoom = new List<string>()
        {
            "1111111111",
            "1        1",
            "1 @      1",
            "1        1",
            "1        1",
            "1        1",
            "1        1",
            "1     e  1",
            "1        1",
            "1111111111",
        };
        
        protected override void OnInit()
        {
            
        }
    }
}