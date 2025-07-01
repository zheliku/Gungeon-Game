// ------------------------------------------------------------
// @file       UIGunList.cs
// @brief
// @author     zheliku
// @Modified   2025-06-11 01:29:27
// @Copyright  Copyright (c) 2025, zheliku
// ------------------------------------------------------------

namespace Game
{
    using Framework.Core;
    using Framework.Toolkits.AudioKit;
    using Framework.Toolkits.FluentAPI;
    using Framework.Toolkits.UIKit;
    using UnityEngine;

    public class UIGunList : UIPanel
    {
        [HierarchyPath("Scroll View/Viewport/GunItemRoot")]
        private Transform _gunItemRoot;

        [HierarchyPath("Scroll View/GunItem")] private GunItem _gunItemInfoTemplate;

        private void Awake()
        {
            this.BindHierarchyComponent();
        }

        private void OnEnable()
        {
            _gunItemRoot.DestroyChildren();

            foreach (var gunData in this.GetSystem<GunSystem>().AllGuns)
            {
                var itemObj = _gunItemInfoTemplate.Instantiate(_gunItemRoot).EnableGameObject();

                itemObj.TxtName.text = gunData.Config.Description;
                itemObj.ImgIcon.sprite = Player.Instance.GetGun(gunData).GunSprite.sprite;

                if (gunData.IsUnlocked)
                {
                    itemObj.TxtPrice.DisableGameObject();
                    itemObj.ImgPalette.DisableGameObject();
                    itemObj.BtnUnlock.DisableGameObject();
                    itemObj.ImgIcon.color = Color.white;
                }
                else
                {
                    itemObj.TxtPrice.text = "x" + gunData.Config.Price;
                    itemObj.ImgIcon.color = new Color(0.5f, 0.5f, 0.5f, 0.3f);

                    itemObj.BtnUnlock.onClick.AddListener(() =>
                    {
                        if (gunData.Config.Price <= this.GetModel<PlayerModel>().Palette)
                        {
                            gunData.IsUnlocked.Value = true;
                            this.GetModel<PlayerModel>().Palette.Value -= gunData.Config.Price;

                            itemObj.TxtPrice.DisableGameObject();
                            itemObj.ImgPalette.DisableGameObject();
                            itemObj.BtnUnlock.DisableGameObject();
                            itemObj.ImgIcon.color = new Color(0.5f, 0.5f, 0.5f, 0.3f);
                            AudioKit.PlaySound(AssetConfig.Sound.UNLOCK_GUN);
                            
                            var showText = $"<color=yellow>{gunData.Key}</color>解锁成功！";
                            Player.DisplayText(showText, 1f);
                        }
                        else
                        {
                            Player.DisplayText("你的颜色不够", 1f);
                        }
                    });
                }
            }
        }

        protected override IArchitecture _Architecture
        {
            get => Game.Architecture;
        }
    }
}