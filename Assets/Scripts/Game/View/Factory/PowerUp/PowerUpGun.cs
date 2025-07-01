// ------------------------------------------------------------
// @file       PowerUpGun.cs
// @brief
// @author     zheliku
// @Modified   2025-07-01 13:15:14
// @Copyright  Copyright (c) 2025, zheliku
// ------------------------------------------------------------

using Framework.Core;
using Framework.Toolkits.FluentAPI;
using UnityEngine;

namespace Game
{
    public class PowerUpGun : PowerUp
    {
        public GunData Data;

        protected override void Awake()
        {
            SpriteRenderer.sprite = Player.Instance.GetGun(Data).GunSprite.sprite;
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag("Player"))
            {
                Room.PowerUps.Remove(this); // 从房间中移除
                
                Data.Owned = true;
                Player.Instance.UseGun(Data); // 使用最后一把枪
                
                this.DestroyGameObject();
            }
        }
    }
}