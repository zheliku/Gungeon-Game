// ------------------------------------------------------------
// @file       PowerUpHalfBullet.cs
// @brief
// @author     zheliku
// @Modified   2025-04-11 11:56:53
// @Copyright  Copyright (c) 2025, zheliku
// ------------------------------------------------------------

namespace Game
{
    using Framework.Core;
    using Framework.Toolkits.AudioKit;
    using Framework.Toolkits.FluentAPI;
    using UnityEngine;

    public class HalfBullet : AbstractView, IPowerUp
    {
        public SpriteRenderer SpriteRenderer { get => GetComponent<SpriteRenderer>(); }
        
        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag("Player"))
            {
                this.GetModel<LevelModel>().CurrentRoom.PowerUps.Remove(this); // 从房间中移除

                foreach (var gun in Player.Instance.Guns)
                {
                    var bag              = gun.Bag;
                    var bulletCountToAdd = bag.MaxBulletCount / 2;
                    bag.AddBulletCount(bulletCountToAdd);
                }
                
                TypeEventSystem.GLOBAL.Send(new GunBulletChangeEvent(Player.Instance.CurrentGun));
                AudioKit.PlaySound(Config.Sound.POWER_UP_HALF_BULLET);
                this.DestroyGameObject();
            }
        }

        protected override IArchitecture _Architecture { get => Game.Interface; }
    }
}