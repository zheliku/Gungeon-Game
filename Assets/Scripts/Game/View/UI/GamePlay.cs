// ------------------------------------------------------------
// @file       GamePlay.cs
// @brief
// @author     zheliku
// @Modified   2025-01-31 14:01:37
// @Copyright  Copyright (c) 2025, zheliku
// ------------------------------------------------------------

namespace Game
{
    using Framework.Core;
    using Framework.Toolkits.FluentAPI;
    using Framework.Toolkits.UIKit;
    using TMPro;

    public class GamePlay : UIPanel
    {
        private TextMeshProUGUI _txtHp;

        protected override void OnLoad()
        {
            _txtHp = "txtHp".GetComponentInHierarchy<TextMeshProUGUI>(transform);
        }

        protected override void OnShow()
        {
            this.GetModel<PlayerModel>().Property.Hp.RegisterWithInitValue((oldValue, value) =>
            {
                _txtHp.text = $"HP:{value}";
            }).UnRegisterWhenGameObjectDisabled(gameObject);
        }

        protected override IArchitecture Architecture { get => Game.Interface; }
    }
}