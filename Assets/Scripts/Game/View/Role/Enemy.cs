// ------------------------------------------------------------
// @file       Enemy.cs
// @brief
// @author     zheliku
// @Modified   2025-03-09 12:48:36
// @Copyright  Copyright (c) 2025, zheliku
// ------------------------------------------------------------

namespace Game
{
    using System.Collections.Generic;
    using Framework.Core;
    using Framework.Toolkits.AudioKit;
    using Framework.Toolkits.FluentAPI;
    using Sirenix.OdinInspector;
    using UnityEngine;

    public abstract class Enemy : AbstractRole, IEnemy
    {
        [HierarchyPath("Bullet")]
        public GameObject Bullet;

        public float FollowSeconds = 3;

        public float BulletSpeed = 5;

        protected Vector2 _playerSpriteOriginLocalPos;

        public List<AudioClip> ShootSounds = new List<AudioClip>();

        [ShowInInspector]
        protected Property _property = new Property() { Hp = { Value = 2 } };

        public GameObject GameObject { get => gameObject; }

        public Transform Transform { get => transform; }
        
        private Room _room;

        [ShowInInspector]
        public Room Room
        {
            get => _room;
            set
            {
                _room = value;
                _room.EnemiesInRoom.Add(this);
            }
        }

        protected override void Awake()
        {
            base.Awake();
            
            _playerSpriteOriginLocalPos = SpriteRenderer.GetLocalPosition();

            Bullet.Disable();

            _property.Hp.Register((oldValue, value) =>
            {
                if (value <= 0)
                {
                    AudioKit.PlaySound(AssetConfig.Sound.ENEMY_DIE);

                    PowerUpFactory.GenPowerUp(this);
                    
                    this.DestroyGameObject();
                }
            }).UnRegisterWhenGameObjectDestroyed(gameObject);

            // 生成时发送生成事件
            TypeEventSystem.GLOBAL.Send(new EnemyCreateEvent(this));
        }

        protected virtual void OnDestroy()
        {
            // 死亡时发送死亡事件
            TypeEventSystem.GLOBAL.Send(new EnemyDieEvent(this));
        }
        
        public override void Hurt(float damage, HitInfo info)
        {
            _property.Hp.Value -= damage;

            FxFactory.PlayHurtFx(this.GetPosition(), Color.red);

            FxFactory.PlayEnemyBlood(this.GetPosition());

            if (_property.Hp.Value <= 0)
            {
                FxFactory.PlayDieBody(this.GetPosition(), GetType().Name.Substring(0, 6) + "Die", info, SpriteRenderer.GetLocalScaleX());
            }
        }
    }
}