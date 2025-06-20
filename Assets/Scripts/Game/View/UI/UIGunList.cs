// ------------------------------------------------------------
// @file       UIGunList.cs
// @brief
// @author     zheliku
// @Modified   2025-06-11 01:29:27
// @Copyright  Copyright (c) 2025, zheliku
// ------------------------------------------------------------

namespace Game
{
    using System;
    using System.Collections.Generic;
    using Framework.Core;
    using Framework.Toolkits.AudioKit;
    using Framework.Toolkits.FluentAPI;
    using Framework.Toolkits.UIKit;
    using UnityEngine;

    public partial class UIGunList : UIPanel
    {
        private List<GunItemInfo> _gunItems;

        [HierarchyPath("Scroll View/Viewport/GunItemRoot")]
        private Transform _gunItemRoot;

        [HierarchyPath("Scroll View/GunItem")]
        private GunItem _gunItemInfoTemplate;

        private void Awake()
        {
            this.BindHierarchyComponent();

            _gunItems = new List<GunItemInfo>()
            {
                new() { Name = "ShotGun 喷子", Key = GunConfig.ShotGun.Key, Price   = 0, InitUnlockState = true },
                new() { Name = "MP5 冲锋枪", Key    = GunConfig.MP5.Key, Price       = 0, InitUnlockState = true },
                new() { Name = "AK47 自动步枪", Key  = GunConfig.AK.Key, Price        = 5, },
                new() { Name = "AWP 狙击枪", Key    = GunConfig.AWP.Key, Price       = 6, },
                new() { Name = "Laser 激光枪", Key  = GunConfig.LaserGun.Key, Price  = 7, },
                new() { Name = "Bow 弓箭", Key     = GunConfig.Bow.Key, Price       = 10, },
                new() { Name = "Rocket 火箭", Key  = GunConfig.RocketGun.Key, Price = 15, },
            };

            Load();
        }

        private void OnEnable()
        {
            _gunItemRoot.DestroyChildren();

            foreach (var gunItem in _gunItems)
            {
                var itemObj = _gunItemInfoTemplate.Instantiate(_gunItemRoot).EnableGameObject();

                itemObj.TxtName.text = gunItem.Name;

                // item.ImgIcon.sprite = gunItem.Icon;

                if (gunItem.IsUnlocked)
                {
                    itemObj.TxtPrice.DisableGameObject();
                    itemObj.ImgPalette.DisableGameObject();
                    itemObj.BtnUnlock.DisableGameObject();
                    itemObj.ImgIcon.color = Color.white;
                }
                else
                {
                    itemObj.TxtPrice.text = "x" + gunItem.Price;
                    itemObj.ImgIcon.color = Color.gray;

                    itemObj.BtnUnlock.onClick.AddListener(() =>
                    {
                        if (gunItem.Price <= this.GetModel<PlayerModel>().Palette)
                        {
                            gunItem.IsUnlocked = true;
                            var showText = $"<color=yellow>{gunItem.Name}</color>解锁成功！";
                            Player.DisplayText(showText, 1f);
                            itemObj.BtnUnlock.DisableGameObject();
                            itemObj.ImgIcon.color = Color.gray;
                            AudioKit.PlaySound(AssetConfig.Sound.UNLOCK_GUN);

                            this.GetModel<PlayerModel>().Palette.Value -= gunItem.Price;
                            Save();
                        }
                        else
                        {
                            Player.DisplayText("你的颜色不够", 1f);
                        }
                    });
                }
            }
        }

        private void Load()
        {
            foreach (var gunItem in _gunItems)
            {
                gunItem.IsUnlocked = PlayerPrefs.GetInt(gunItem.Key, gunItem.InitUnlockState ? 1 : 0) == 1;
            }
        }

        private void Save()
        {
            foreach (var gunItem in _gunItems)
            {
                PlayerPrefs.SetInt(gunItem.Key, gunItem.IsUnlocked ? 1 : 0);
            }
        }

        protected override IArchitecture _Architecture { get => Game.Architecture; }
    }

    public partial class UIGunList
    {
        public class GunItemInfo
        {
            public string Name;
            public string Key;
            public int    Price;
            public bool   IsUnlocked;
            public Sprite Icon;
            public bool   InitUnlockState;
        }
    }
}