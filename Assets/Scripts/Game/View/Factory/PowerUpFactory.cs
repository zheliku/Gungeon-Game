// ------------------------------------------------------------
// @file       PowerUpFactory.cs
// @brief
// @author     zheliku
// @Modified   2025-03-14 12:15:56
// @Copyright  Copyright (c) 2025, zheliku
// ------------------------------------------------------------

namespace Game
{
    using System.Collections.Generic;
    using Framework.Core;
    using Framework.Toolkits.SingletonKit;
    using Framework.Toolkits.FluentAPI;
    using UnityEngine;

    public class PowerUpFactory : MonoSingleton<PowerUpFactory>
    {
        [HierarchyPath("Coin")]
        public Coin Coin;

        [HierarchyPath("Palette")]
        public Palette Palette;

        [HierarchyPath("Key")]
        public Key Key;

        [HierarchyPath("Hp1")]
        public Hp1 Hp1;

        [HierarchyPath("Chest")]
        public Chest Chest;

        [HierarchyPath("Armor1")]
        public Armor1 Armor1;

        [HierarchyPath("AllGunHalfBullet")]
        public AllGunHalfBullet AllGunHalfBullet;

        [HierarchyPath("SingleGunFullBullet")]
        public SingleGunFullBullet SingleGunFullBullet;

        private void Awake()
        {
            this.BindHierarchyComponent();
        }

        public static void GenEnemyPowerUp(IEnemy enemy)
        {
            var powerUps = new List<IPowerUp>();

            if (Instance.GetModel<PlayerModel>().Property.Hp.Value < 6)
            {
                if (0.2f.IsLessThanRandom01()) { powerUps.Add(Instance.Hp1); }
                else if (0.05f.IsLessThanRandom01()) { powerUps.Add(Instance.Hp1); }

                if (0.05f.IsLessThanRandom01()) { powerUps.Add(Instance.Armor1); }
            }

            if (0.5f.IsLessThanRandom01()) { powerUps.Add(Instance.Coin); }

            if (powerUps.Count > 0)
            {
                var angle = (0f, 360f).RandomSelect();
                var powerUp = powerUps.RandomTakeOne();
                powerUp.SpriteRenderer.Instantiate()
                    .SetPosition(enemy.GameObject.GetPosition() +
                                 (Vector3)angle.Deg2Direction2D() * (0.25f, 0.5f).RandomSelect())
                    .EnableGameObject()
                    .GetComponent<IPowerUp>()
                    .Room = Instance.GetModel<LevelModel>().CurrentRoom;
            }
        }

        public static void GenBossPowerUp(IBoss boss)
        {
            var colorCount = (3, 5 + 1).RandomSelect();

            for (int i = 0; i < colorCount; i++)
            {
                var angle = (0f, 360f).RandomSelect();
                var palette = Instance.Palette.SpriteRenderer.Instantiate()
                    .SetPosition(boss.GameObject.GetPosition() +
                                 (Vector3)angle.Deg2Direction2D() * (0.25f, 0.5f).RandomSelect())
                    .EnableGameObject()
                    .GetComponent<Palette>()
                    .Room = Instance.GetModel<LevelModel>().CurrentRoom;
            }

            // todo 枪械生成 

            // 补给生成
            var powerUpCount = (1, 3 + 1).RandomSelect();
            var powerUps = new List<IPowerUp>()
            {
                Instance.Hp1,
                Instance.Hp1,
                Instance.Armor1,
                Instance.Armor1,
                Instance.SingleGunFullBullet,
                Instance.AllGunHalfBullet
            };
            
            for (int i = 0; i < powerUpCount; i++)
            {
                var angle = (0f, 360f).RandomSelect();
                var powerUp = powerUps.RandomTakeOne();
                powerUp.SpriteRenderer.Instantiate()
                    .SetPosition(boss.GameObject.GetPosition() +
                                 (Vector3)angle.Deg2Direction2D() * (0.25f, 0.5f).RandomSelect())
                    .EnableGameObject()
                    .GetComponent<IPowerUp>()
                    .Room = Instance.GetModel<LevelModel>().CurrentRoom;
            }
        }

        protected override IArchitecture _Architecture { get => Game.Architecture; }
    }
}