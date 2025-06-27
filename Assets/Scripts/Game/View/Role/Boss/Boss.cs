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
    using System.Linq;
    using Framework.Core;
    using Framework.Toolkits.AudioKit;
    using Framework.Toolkits.FluentAPI;
    using Sirenix.OdinInspector;
    using UnityEngine;

    public interface IBoss : IEnemy
    {
        Room Room { get; set; }
    }

    public abstract class Boss : AbstractRole, IBoss
    {
        [HierarchyPath("Bullet")]
        public GameObject Bullet;

        public Vector2 PlayerSpriteOriginLocalPos;

        public List<AudioClip> ShootSounds = new List<AudioClip>();

        [ShowInInspector]
        public List<PathFindingHelper.NodeBase<Vector3Int>> MovementPath =
            new List<PathFindingHelper.NodeBase<Vector3Int>>();

        [ShowInInspector]
        public EnemyProperty Property { get; } = new EnemyProperty();

        public Vector2 FollowTimeRange { get; private set; }

        public float BulletSpeed { get; private set; }

        private Room _room;

        [ShowInInspector]
        public Room Room
        {
            get => _room;
            set => _room = value;
        }

        protected override void Awake()
        {
            base.Awake();

            var bgConfig = BG_BossTable.GetEntity(GetType().Name);
            if (bgConfig != null)
            {
                Property.Hp.SetValueWithoutEvent(bgConfig.Hp);
                Property.MaxHp.SetValueWithoutEvent(bgConfig.Hp);
                Property.MoveSpeed = bgConfig.MoveSpeed;
                Property.Damage = bgConfig.Damage;
                BulletSpeed = bgConfig.BulletSpeed;
            }

            PlayerSpriteOriginLocalPos = SpriteRenderer.GetLocalPosition();

            Bullet.Disable();

            Property.Hp.Register((oldValue, value) =>
            {
                if (value <= 0)
                {
                    AudioKit.PlaySound(AssetConfig.Sound.ENEMY_DIE);
                    
                    PowerUpFactory.GenBossPowerUp(this);

                    this.DestroyGameObject();
                }
            }).UnRegisterWhenGameObjectDestroyed(gameObject);

            // 生成时发送生成事件
            TypeEventSystem.GLOBAL.Send(new BossCreateEvent(this));
        }

        protected virtual void Update()
        {
            var directionToPlayer = Player.Instance.Direction2DFrom(transform);

            if (directionToPlayer.x > 0)
            {
                SpriteRenderer.flipX = false;
            }
            else if (directionToPlayer.x < 0)
            {
                SpriteRenderer.flipX = true;
            }
        }

        protected virtual void OnDestroy()
        {
            // 死亡时发送死亡事件
            TypeEventSystem.GLOBAL.Send(new BossDieEvent(this));
        }

        public override void Hurt(float damage, HitInfo info)
        {
            Property.Hp.Value -= (int)damage;

            FxFactory.PlayHurtFx(this.GetPosition(), Color.red);

            FxFactory.PlayEnemyBlood(this.GetPosition());

            TypeEventSystem.GLOBAL.Send(new BossHpChangeEvent(Property.Hp.Value * 1f / Property.MaxHp.Value));
        }

        /// <summary>
        /// 自动寻路
        /// </summary>
        /// <returns></returns>
        public void AutoMove()
        {
            var directionToPlayer = Player.Instance.DirectionFrom(this); // 直线移动

            if (MovementPath.Count > 0)
            {
                // 自动寻路计算
                var pathPos = MovementPath.Last().Coords.Pos;
                var moveVec = new Vector2(pathPos.x + 0.5f, pathPos.y + 0.5f) - (Vector2)this.GetPosition();
                directionToPlayer = moveVec.normalized;

                if (moveVec.magnitude < 0.1f) // 如果距离目标点小于等于0.1，则到达目标位置，删除上一个位置
                {
                    MovementPath.RemoveAt(MovementPath.Count - 1);
                }
            }

            Rigidbody2D.linearVelocity = directionToPlayer.normalized * Property.MoveSpeed;
        }

        public void CalculateMovementPath()
        {
            var grid = LevelController.Instance.WallTilemap.layoutGrid;
            var myCellPos = grid.WorldToCell(this.GetPosition());
            var playerCellPos = grid.WorldToCell(Player.Instance.GetPosition());

            MovementPath = PathFindingHelper.FindPath(
                Room.PathFindingGrid[myCellPos.x, myCellPos.y],
                Room.PathFindingGrid[playerCellPos.x, playerCellPos.y]);
        }
    }
}