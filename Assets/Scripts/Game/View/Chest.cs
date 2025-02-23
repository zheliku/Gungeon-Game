// ------------------------------------------------------------
// @file       Chest.cs
// @brief
// @author     zheliku
// @Modified   2025-02-23 21:02:17
// @Copyright  Copyright (c) 2025, zheliku
// ------------------------------------------------------------

namespace Game
{
    using Framework.Core;
    using Framework.Toolkits.AudioKit;
    using Framework.Toolkits.FluentAPI;
    using UnityEngine;

    public class Chest : AbstractView
    {
        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag("Player"))
            {
                LevelController.Instance.Hp1Template
                   .Instantiate(this.GetPosition())
                   .EnableGameObject();
                
                AudioKit.PlaySound(Config.Sound.CHEST, 0.6f);
                this.DestroyGameObject();
            }
        }
        
        protected override IArchitecture _Architecture { get => Game.Interface; }
    }
}