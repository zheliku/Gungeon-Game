// ------------------------------------------------------------
// @file       HP1.cs
// @brief
// @author     zheliku
// @Modified   2025-02-23 21:02:35
// @Copyright  Copyright (c) 2025, zheliku
// ------------------------------------------------------------

namespace Game
{
    using Framework.Core;
    using Framework.Toolkits.AudioKit;
    using Framework.Toolkits.FluentAPI;
    using UnityEngine;

    public class Hp1 : AbstractView, IPowerUp
    {
        public SpriteRenderer SpriteRenderer { get => GetComponent<SpriteRenderer>(); }
        
        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag("Player"))
            {
                this.GetModel<LevelModel>().CurrentRoom.PowerUps.Remove(this); // 从房间中移除
                
                this.GetModel<PlayerModel>().Property.Hp.Value++;
                AudioKit.PlaySound(Config.Sound.HP1, 0.6f);
                this.DestroyGameObject();
            }
        }

        protected override IArchitecture _Architecture { get => Game.Interface; }
    }
}