// ------------------------------------------------------------
// @file       EnemyFactory.cs
// @brief
// @author     zheliku
// @Modified   2025-04-11 16:50:46
// @Copyright  Copyright (c) 2025, zheliku
// ------------------------------------------------------------

namespace Game
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Framework.Core;
    using Framework.Toolkits.FluentAPI;
    using Framework.Toolkits.SingletonKit;
    using Sirenix.OdinInspector;

    public class EnemyFactory : MonoSingleton<EnemyFactory>
    {
        [ShowInInspector]
        public List<Enemy> Enemies = new List<Enemy>();

        public List<Boss> Bosses = new List<Boss>();

        public override void OnSingletonInit()
        {
            for (int i = 0; i < transform.childCount; i++)
            {
                var child = transform.GetChild(i).gameObject;
                var enemy = child.GetComponent<Enemy>();
                if (enemy != null)
                {
                    Enemies.Add(enemy);
                }
                
                var boss = child.GetComponent<Boss>();
                if (boss != null)
                {
                    Bosses.Add(boss);
                }
            }
        }

        public static Enemy GetEnemyByName(string name)
        {
            var testName = new[]
            {
                "EnemyG",
                "EnemyH",
            }.RandomTakeOne();
            return Instance.Enemies.First(enemy => testName == enemy.GameObject.name);

            return Instance.Enemies.FirstOrDefault(e => e.GameObject.name == name);
        }

        public static string GetEnemyByScore(int enemyScore)
        {
            return enemyScore switch
            {
                2  => AssetConfig.EnemyName.ENEMY_A,
                3  => new[] { AssetConfig.EnemyName.ENEMY_B, AssetConfig.EnemyName.ENEMY_C }.RandomTakeOne(),
                4  => new[] { AssetConfig.EnemyName.ENEMY_D, AssetConfig.EnemyName.ENEMY_E }.RandomTakeOne(),
                5  => AssetConfig.EnemyName.ENEMY_F,
                6  => AssetConfig.EnemyName.ENEMY_G,
                7  => AssetConfig.EnemyName.ENEMY_H,
                8  => AssetConfig.EnemyName.ENEMY_A_BIG,
                9  => new[] { AssetConfig.EnemyName.ENEMY_B_BIG, AssetConfig.EnemyName.ENEMY_C_BIG }.RandomTakeOne(),
                10 => AssetConfig.EnemyName.ENEMY_D_BIG,
                _  => null
            };
        }

        public static int GenTargetEnemyScore()
        {
            var currentLevelData = Instance.GetModel<LevelModel>().CurrentLevel;

            if (currentLevelData == Level1.DATA)
            {
                return new[] { 2, 2, 3, 3, 8 }.RandomTakeOne();
            }
            if (currentLevelData == Level2.DATA)
            {
                return new[] { 2, 2, 3, 3, 4, 4, 8, 9 }.RandomTakeOne();
            }
            if (currentLevelData == Level3.DATA)
            {
                return new[] { 2, 3, 4, 5, 9, }.RandomTakeOne();
            }
            if (currentLevelData == Level4.DATA)
            {
                return new[] { 3, 4, 5, 6, 7, 9, 10 }.RandomTakeOne();
            }

            return new[] { 2, 3, 4, 5, 6, 7, 8, 9, 10 }.RandomTakeOne();
        }

        protected override IArchitecture _Architecture { get => Game.Architecture; }
    }
}