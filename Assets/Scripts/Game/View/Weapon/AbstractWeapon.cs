// ------------------------------------------------------------
// @file       AbstractWeapon.cs
// @brief
// @author     zheliku
// @Modified   2025-01-31 18:01:29
// @Copyright  Copyright (c) 2025, zheliku
// ------------------------------------------------------------

namespace Game
{
    using System.Collections.Generic;
    using Framework.Core;
    using Framework.Toolkits.FluentAPI;
    using UnityEngine;

    public abstract class AbstractWeapon : AbstractView
    {
        public GameObject Bullet;

        public List<AudioClip> ShootSounds = new List<AudioClip>();

        public float BulletSpeed = 10;

        protected virtual void Awake()
        {
            Bullet = "Bullet".GetGameObjectInHierarchy(transform);
            Bullet.Disable();
        }

        public abstract void ShootDown(Vector2 direction);

        public abstract void Shooting(Vector2 direction);

        public abstract void ShootUp(Vector2 direction);

        protected override IArchitecture Architecture { get => Game.Interface; }
    }
}