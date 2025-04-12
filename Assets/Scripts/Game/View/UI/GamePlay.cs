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
    using Framework.Toolkits.ActionKit;
    using Framework.Toolkits.InputKit;
    using Framework.Toolkits.UIKit;
    using TMPro;
    using UnityEngine;

    public class GamePlay : UIPanel
    {
        [HierarchyPath("txtHp")]
        private TextMeshProUGUI _txtHp;
        
        [HierarchyPath("txtArmor")]
        private TextMeshProUGUI _txtArmor;

        [HierarchyPath("txtGun")]
        private TextMeshProUGUI _txtGun;

        [HierarchyPath("Coin/txtCoin")]
        private TextMeshProUGUI _textCoin;

        protected override void OnShow()
        {
            this.GetModel<PlayerModel>().Property.Hp.RegisterWithInitValue((oldValue, value) =>
            {
                _txtHp.text = $"HP:{value}";
            }).UnRegisterWhenGameObjectDisabled(gameObject);
            
            this.GetModel<PlayerModel>().Property.Armor.RegisterWithInitValue((oldValue, value) =>
            {
                _txtArmor.text = $"Armor:{value}";
            }).UnRegisterWhenGameObjectDisabled(gameObject);
            
            this.GetModel<PlayerModel>().Coin.RegisterWithInitValue((oldValue, value) =>
            {
                _textCoin.text = $"{value}";
            }).UnRegisterWhenGameObjectDisabled(gameObject);

            TypeEventSystem.GLOBAL.Register<GunShootEvent>(e =>
            {
                UpdateGunInfo(e.Gun);
            }).UnRegisterWhenGameObjectDisabled(this);

            TypeEventSystem.GLOBAL.Register<GunChangeEvent>(e =>
            {
                UpdateGunInfo(e.NewGun);
            }).UnRegisterWhenGameObjectDisabled(this);

            TypeEventSystem.GLOBAL.Register<GunBulletLoadingEvent>(e =>
            {
                UpdateGunInfo(e.Gun);
            }).UnRegisterWhenGameObjectDisabled(this);
            
            TypeEventSystem.GLOBAL.Register<GunBulletChangeEvent>(e =>
            {
                UpdateGunInfo(e.Gun);
            }).UnRegisterWhenGameObjectDisabled(this);

            InputKit.BindPerformed(AssetConfig.Action.OPEN_MAP, context =>
            {
                if (!UIKit.IsPanelShown<UIMap>())
                {
                    UIKit.ShowPanelAsync<UIMap>();
                }
                else
                {
                    UIKit.HidePanel<UIMap>();
                }
            }).UnBindAllPerformedWhenGameObjectDisabled(this);
        }

        private void UpdateGunInfo(Gun gun)
        {
            var clipInfo = $"{gun.Clip.RemainBulletCount}/{gun.Clip.MaxBulletCount}";
            var bagInfo  = gun.Bag.MaxBulletCount < 0 ? "(\u221e)" : $"({gun.Bag.RemainBulletCount}/{gun.Bag.MaxBulletCount})";
            _txtGun.text = $"Bullet: {clipInfo} {bagInfo} R Reload!";
        }

        protected override IArchitecture _Architecture { get => Game.Architecture; }
    }
}