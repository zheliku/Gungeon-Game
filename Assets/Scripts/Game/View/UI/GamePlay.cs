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
    using Framework.Toolkits.FluentAPI;
    using Framework.Toolkits.InputKit;
    using Framework.Toolkits.UIKit;
    using TMPro;
    using UnityEngine;
    using UnityEngine.UI;

    public class GamePlay : UIPanel
    {
        public static void PlayHurtFlashScreen()
        {
            ActionKit.ScreenTransition.FadeOut(0.2f, Color.red)
               .FromAlpha(0.3f)
               .StartCurrentScene()
               .IgnoreTimeScale();
        }

        [HierarchyPath("HPAndArmors/Content")]
        private Transform _hpAndArmors;

        [HierarchyPath("HPAndArmors/Template/Hp")]
        private Image _imgHp;

        [HierarchyPath("HPAndArmors/Template/Armor")]
        private Image _imgArmor;

        [HierarchyPath("txtGun")]
        private TextMeshProUGUI _txtGun;

        [HierarchyPath("Coin/txtCoin")]
        private TextMeshProUGUI _textCoin;

        [HierarchyPath("Key/txtKey")]
        private TextMeshProUGUI _textKey;

        protected override void OnLoad()
        {
            _imgHp.DisableGameObject();
            _imgArmor.DisableGameObject();
        }

        protected override void OnShow()
        {
            this.GetModel<PlayerModel>().Property.Hp.RegisterWithInitValue((oldValue, value) =>
            {
                UpdateHpAndArmorView();
            }).UnRegisterWhenGameObjectDisabled(gameObject);

            this.GetModel<PlayerModel>().Property.Armor.RegisterWithInitValue((oldValue, value) =>
            {
                UpdateHpAndArmorView();
            }).UnRegisterWhenGameObjectDisabled(gameObject);

            this.GetModel<PlayerModel>().Coin.RegisterWithInitValue((oldValue, value) =>
            {
                _textCoin.text = $"{value}";
            }).UnRegisterWhenGameObjectDisabled(gameObject);

            this.GetModel<PlayerModel>().Key.RegisterWithInitValue((oldValue, value) =>
            {
                _textKey.text = $"{value}";
            }).UnRegisterWhenGameObjectDisabled(gameObject);

            TypeEventSystem.GLOBAL.Register<GunShootEvent>(e =>
            {
                UpdateGunView(e.Gun);
            }).UnRegisterWhenGameObjectDisabled(this);

            TypeEventSystem.GLOBAL.Register<GunChangeEvent>(e =>
            {
                UpdateGunView(e.NewGun);
            }).UnRegisterWhenGameObjectDisabled(this);

            TypeEventSystem.GLOBAL.Register<GunBulletLoadingEvent>(e =>
            {
                UpdateGunView(e.Gun);
            }).UnRegisterWhenGameObjectDisabled(this);

            TypeEventSystem.GLOBAL.Register<GunBulletChangeEvent>(e =>
            {
                UpdateGunView(e.Gun);
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

        private void UpdateHpAndArmorView()
        {
            _hpAndArmors.DestroyChildren();

            var playerProperty = this.GetModel<PlayerModel>().Property;

            for (int i = 0; i < playerProperty.MaxHp.Value / 2; i++)
            {
                var hp = _imgHp.Instantiate(_hpAndArmors)
                   .EnableGameObject();

                var result = playerProperty.Hp.Value - i * 2;
                var value  = hp.transform.Find("Value").GetComponent<Image>();

                if (result > 0)
                {
                    value.fillAmount = result == 1 ? 0.5f : 1;
                }
                else
                {
                    value.fillAmount = 0;
                }
            }
            
            for (int i = 0; i < playerProperty.Armor.Value; i++)
            {
                var armor = _imgArmor.Instantiate(_hpAndArmors)
                   .EnableGameObject();
            }
        }

        private void UpdateGunView(Gun gun)
        {
            var clipInfo = $"{gun.Clip.RemainBulletCount}/{gun.Clip.MaxBulletCount}";
            var bagInfo  = gun.Bag.MaxBulletCount < 0 ? "(\u221e)" : $"({gun.Bag.RemainBulletCount}/{gun.Bag.MaxBulletCount})";
            _txtGun.text = $"Bullet: {clipInfo} {bagInfo} R Reload!";
        }

        protected override IArchitecture _Architecture { get => Game.Architecture; }
    }
}