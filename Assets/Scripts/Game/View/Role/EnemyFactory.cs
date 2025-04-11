// ------------------------------------------------------------
// @file       EnemyFactory.cs
// @brief
// @author     zheliku
// @Modified   2025-04-11 16:50:46
// @Copyright  Copyright (c) 2025, zheliku
// ------------------------------------------------------------

namespace Game
{
    using System.Collections.Generic;
    using System.Linq;
    using Framework.Core;
    using Framework.Toolkits.FluentAPI;
    using Framework.Toolkits.SingletonKit;

    public class EnemyFactory : MonoSingleton<EnemyFactory>
    {
        public List<IEnemy> Enemies = new List<IEnemy>();

        public override void OnSingletonInit()
        {
            for (int i = 0; i < transform.childCount; i++)
            {
                var child = transform.GetChild(i).gameObject;
                var enemy = child.GetComponent<IEnemy>();
                if (enemy != null)
                {
                    Enemies.Add(enemy);
                }
            }
        }

        public static IEnemy GetEnemyByName(string name)
        {
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
    }
}