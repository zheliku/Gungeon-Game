// ------------------------------------------------------------
// @file       BulletFactory.cs
// @brief
// @author     zheliku
// @Modified   2025-03-12 22:28:54
// @Copyright  Copyright (c) 2025, zheliku
// ------------------------------------------------------------

namespace Game
{
    using System.Collections.Generic;
    using Framework.Core;
    using Framework.Toolkits.SingletonKit;
    using UnityEngine;
    using UnityEngine.Serialization;

    public class BulletFactory : MonoSingleton<BulletFactory>
    {
        public List<AudioClip> HitWallSounds  = new List<AudioClip>();
        public List<AudioClip> HitEnemySounds = new List<AudioClip>();

        [HierarchyPath("GunBullet")]
        public PlayerBullet GunBullet;

        [HierarchyPath("RocketBullet")]
        public PlayerBullet RocketBullet;

        [HierarchyPath("BowArrow")]
        public PlayerBullet BowArrow;

        private void Awake()
        {
            this.BindHierarchyComponent();
        }
    }
}