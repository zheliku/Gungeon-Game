// ------------------------------------------------------------
// @file       PlayerModel.cs
// @brief
// @author     zheliku
// @Modified   2025-01-30 20:01:35
// @Copyright  Copyright (c) 2025, zheliku
// ------------------------------------------------------------

using System.Collections.Generic;

namespace Game
{
    using Framework.Core;
    using Framework.Core.Model;
    using Framework.Toolkits.BindableKit;

    public class PlayerModel : AbstractModel
    {
        public PlayerProperty Property = new PlayerProperty();

        public BindableProperty<int> Coin = new BindableProperty<int>();
        
        public BindableProperty<int> Key = new BindableProperty<int>();
        
        public PlayerPrefsIntProperty Palette = new PlayerPrefsIntProperty(nameof(Palette));

        public List<int> BossList = new List<int>() // 剩余待击败的 Boss 下标
        {
            0, // Level1
            1, // Level2
            2, // Level3
            3, // Level4
            4, // Level5
        };
        
        protected override void OnInit()
        {
            Reset();
        }

        public void Reset()
        {
            Property.Hp.SetValueWithoutEvent(6);
            Property.MaxHp.SetValueWithoutEvent(6);
            Coin.SetValueWithoutEvent(100);
            Key.SetValueWithoutEvent(20);
            // Palette.SetValueWithoutEvent(0);
            Property.MoveSpeed = 5;
            Property.Armor.SetValueWithoutEvent(1);
        }
    }
}