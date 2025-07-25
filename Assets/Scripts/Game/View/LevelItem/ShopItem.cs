// ------------------------------------------------------------
// @file       ShopItem.cs
// @brief
// @author     zheliku
// @Modified   2025-04-11 12:44:49
// @Copyright  Copyright (c) 2025, zheliku
// ------------------------------------------------------------

namespace Game
{
    using System;
    using Framework.Core;
    using Framework.Toolkits.FluentAPI;
    using Framework.Toolkits.InputKit;
    using TMPro;
    using UnityEngine;

    public class ShopItem : AbstractView
    {
        [HierarchyPath("Icon")]
        public SpriteRenderer Icon;
        
        [HierarchyPath("Price")]
        public TextMeshPro TxtPrice;

        [HierarchyPath("Keyboard")]
        public TextMeshPro TxtKeyboard;

        public IPowerUp PowerUp { get; set; }

        public int Price { get; set; }

        private void Awake()
        {
            this.BindHierarchyComponent();
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag("Player"))
            {
                TxtKeyboard.EnableGameObject();
            }
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            if (other.CompareTag("Player"))
            {
                TxtKeyboard.DisableGameObject();
            }
        }

        private void Update()
        {
            if (TxtKeyboard.IsGameObjectEnabledSelf())
            {
                if (InputKit.WasPressedThisFrame(AssetConfig.Action.BUY))
                {
                    if (this.GetModel<PlayerModel>().Coin >= Price)
                    {
                        var powerUp = PowerUp.SpriteRenderer.Instantiate()
                           .SetPosition(transform.position)
                           .EnableGameObject()
                           .GetComponent<IPowerUp>();
                        this.GetModel<PlayerModel>().Coin.Value -= Price;

                        powerUp.Room = this.GetModel<LevelModel>().CurrentRoom;
                        this.DestroyGameObject();
                    }
                    else
                    {
                        // 金币不足的提示
                        Player.DisplayText("金币不足", 1f);
                    }
                }
            }
        }

        public ShopItem UpdateView()
        {
            TxtPrice.text = $"${Price}";
            Icon.sprite = PowerUp.SpriteRenderer.sprite;
            return this;
        }

        protected override IArchitecture _Architecture { get => Game.Architecture; }
    }
}