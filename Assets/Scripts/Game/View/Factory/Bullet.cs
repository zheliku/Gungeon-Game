// ------------------------------------------------------------
// @file       Bullet.cs
// @brief
// @author     zheliku
// @Modified   2025-03-14 12:03:25
// @Copyright  Copyright (c) 2025, zheliku
// ------------------------------------------------------------

using System;
using System.Collections.Generic;

namespace Game
{
    using Framework.Core;
    using UnityEngine;

    public abstract class Bullet : AbstractView
    {
        public float Damage;

        [HierarchyPath(false)]
        public Rigidbody2D Rigidbody; // 不用 HierarchyPath，因为有些子弹没有 Rigidbody

        public Vector2 Velocity
        {
            get => Rigidbody.linearVelocity;
            set => Rigidbody.linearVelocity = value;
        }

        protected virtual void Awake()
        {
            this.BindHierarchyComponent();
        }

        protected abstract void OnCollisionEnter2D(Collision2D other);

        protected override IArchitecture _Architecture { get => Game.Architecture; }
    }

    public abstract class PlayerBullet : Bullet
    {
        public List<AudioClip> HitWallSounds = new List<AudioClip>();
        public List<AudioClip> HitEnemySounds = new List<AudioClip>();
    }

    public abstract class EnemyBullet : Bullet
    {
    }
}