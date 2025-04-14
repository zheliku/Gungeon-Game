// ------------------------------------------------------------
// @file       Enemy.cs
// @brief
// @author     zheliku
// @Modified   2025-01-29 16:01:14
// @Copyright  Copyright (c) 2025, zheliku
// ------------------------------------------------------------

namespace Game
{
    using System.Collections.Generic;
    using System.Linq;
    using Framework.Core;
    using Framework.Toolkits.AudioKit;
    using Framework.Toolkits.FluentAPI;
    using Framework.Toolkits.FSMKit;
    using Sirenix.OdinInspector;
    using UnityEngine;
    using Random = UnityEngine.Random;

    public class EnemyA : Enemy
    {
        public enum State
        {
            Follow,
            PrepareToShoot,
            Shoot
        }

        public FSM<State> FSM = new FSM<State>();

        [ShowInInspector]
        public List<PathFindingHelper.NodeBase<Vector3Int>> MovementPath = new List<PathFindingHelper.NodeBase<Vector3Int>>();

        protected override void Awake()
        {
            base.Awake();

            Vector2? posToMove = null;

            Vector2 Move()
            {
                if (posToMove == null)
                {
                    if (MovementPath.Count > 0)
                    {
                        var pathPos = MovementPath.Last().Coords.Pos;
                        posToMove = new Vector2(pathPos.x + 0.5f, pathPos.y + 0.5f);
                        MovementPath.RemoveAt(MovementPath.Count - 1);
                    }
                }

                var directionToPlayer = Player.Instance.DirectionFrom(this);

                if (posToMove == null)
                {
                    Rigidbody2D.linearVelocity = directionToPlayer * _property.MoveSpeed;
                }
                else
                {
                    var direction = posToMove.Value - (Vector2) this.GetPosition();
                    Rigidbody2D.linearVelocity = direction.normalized * _property.MoveSpeed;

                    if (direction.magnitude < 0.2f)
                    {
                        posToMove = null;
                    }
                }

                return directionToPlayer;
            }

            FSM.State(State.Follow)
               .OnEnter(() =>
                {
                    FollowSeconds = Random.Range(0.5f, 3f);
                    MovementPath.Clear();
                })
               .OnUpdate(() =>
                {
                    var directionToPlayer = Move();

                    if (directionToPlayer.x > 0)
                    {
                        SpriteRenderer.flipX = false;
                    }
                    else if (directionToPlayer.x < 0)
                    {
                        SpriteRenderer.flipX = true;
                    }

                    if (MovementPath.Count == 0)
                    {
                        var grid          = LevelController.Instance.WallTilemap.layoutGrid;
                        var myCellPos     = grid.WorldToCell(this.GetPosition());
                        var playerCellPos = grid.WorldToCell(Player.Instance.GetPosition());

                        PathFindingHelper.FindPath(
                            Room.PathFindingGrid[myCellPos.x, myCellPos.y],
                            Room.PathFindingGrid[playerCellPos.x, playerCellPos.y],
                            MovementPath);
                    }

                    AnimationHelper.UpDownAnimation(SpriteRenderer, FSM.SecondsOfCurrentState, 0.2f, _playerSpriteOriginLocalPos.y, 0.05f);
                    AnimationHelper.RotateAnimation(SpriteRenderer, FSM.SecondsOfCurrentState, 0.4f, 3);

                    if (FSM.SecondsOfCurrentState >= FollowSeconds)
                    {
                        FSM.ChangeState(State.PrepareToShoot);
                    }
                });

            Vector2 onEnterPrepareToShootLocalPos = SpriteRenderer.GetLocalPosition();
            FSM.State(State.PrepareToShoot)
               .OnEnter(() =>
                {
                    onEnterPrepareToShootLocalPos = SpriteRenderer.GetLocalPosition();
                })
               .OnUpdate(() =>
                {
                    var shakeRate = (FSM.SecondsOfCurrentState / 0.25f).Lerp(0.05f, 0.1f);
                    var shakePos  = new Vector2(shakeRate.RandomTo0(), shakeRate.RandomTo0());
                    SpriteRenderer.SetLocalPosition(onEnterPrepareToShootLocalPos + shakePos);

                    if (FSM.SecondsOfCurrentState >= 0.3f)
                    {
                        FSM.ChangeState(State.Shoot);
                    }
                })
               .OnExit(() =>
                {
                    SpriteRenderer.SetLocalPosition(onEnterPrepareToShootLocalPos);
                });

            FSM.State(State.Shoot)
               .OnEnter(() =>
                {
                    Fire();
                    Rigidbody2D.linearVelocity = Vector2.zero;
                })
               .OnUpdate(() =>
                {
                    if (FSM.SecondsOfCurrentState >= 1)
                    {
                        FSM.ChangeState(State.Follow);
                    }
                });

            FSM.StartState(State.Follow);
        }

        private void Update()
        {
            FSM.Update();
        }

        private void FixedUpdate()
        {
            FSM.FixedUpdate();
        }

        public void Fire()
        {
            var bullet = Bullet.Instantiate(transform.position)
               .Enable()
               .GetComponent<EnemyBullet>();

            bullet.Damage   = 1f;
            bullet.Velocity = Player.Instance.Direction2DFrom(bullet) * BulletSpeed;

            AudioKit.PlaySound(ShootSounds.RandomTakeOne());
        }

        protected override IArchitecture _Architecture { get => Game.Architecture; }
    }
}