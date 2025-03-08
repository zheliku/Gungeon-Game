// ------------------------------------------------------------
// @file       Room.cs
// @brief
// @author     zheliku
// @Modified   2025-02-22 15:02:10
// @Copyright  Copyright (c) 2025, zheliku
// ------------------------------------------------------------

namespace Game
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Framework.Core;
    using Framework.Toolkits.FluentAPI;
    using Sirenix.OdinInspector;
    using UnityEngine;

    public enum RoomType
    {
        Init,
        Normal,
        Final,
        Chest
    }

    public enum RoomState
    {
        Closed, // 初始状态为 Closed
        PlayerIn,
        Unlocked
    }

    public class Room : AbstractView
    {
        private List<Vector3> _enemyGeneratePoses = new List<Vector3>();

        private List<Door> _doors = new List<Door>();

        [ShowInInspector]
        private RoomGrid _grid;

        private List<Enemy> _enemiesInRoom = new List<Enemy>();

        private List<EnemyWaveConfig> _enemyWaves = new List<EnemyWaveConfig>();

        private EnemyWaveConfig _currentWave;

        [ShowInInspector]
        public RoomNode Node { get; set; }

        public RoomType RoomType { get => _grid.RoomType; }

        public RoomState State { get; private set; } = RoomState.Closed;
        
        public List<Enemy> EnemiesInRoom
        {
            get => _enemiesInRoom;
        }

        private void Awake()
        {
            TypeEventSystem.GLOBAL.Register<EnemyDieEvent>(e =>
            {
                _enemiesInRoom.Remove(e.Enemy);

                // 检测房间内敌人数量是否为 0
                if (_enemiesInRoom.Count == 0 && State == RoomState.PlayerIn)
                {
                    if (_enemyWaves.Count > 0) // 还有下一波敌人
                    {
                        GenerateEnemies();
                    }
                    else // 没有下一波敌人，房间状态变为 Unlocked
                    {
                        State = RoomState.Unlocked;

                        foreach (var door in _doors) // 开门
                        {
                            door.State.ChangeState(DoorState.Open);
                        }
                    }
                }
            }).UnRegisterWhenGameObjectDestroyed(this);
        }

        private void Start()
        {
            if (RoomType == RoomType.Init)
            {
                foreach (var door in _doors)
                {
                    door.State.ChangeState(DoorState.Open);
                }
            }
            else if (RoomType == RoomType.Normal)
            {
                var randomCount = (1, 3 + 1).RandomSelect(); // 随机生成 1 到 3 波敌人
                for (int i = 0; i < randomCount; i++)
                {
                    _enemyWaves.Add(new EnemyWaveConfig());
                }
            }
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag("Player"))
            {
                TypeEventSystem.GLOBAL.Send(new EnterRoomEvent(this));

                if (_grid.RoomType == RoomType.Normal)
                {
                    if (State == RoomState.Closed) // 第一次进，状态变为 PlayerIn
                    {
                        State = RoomState.PlayerIn;

                        GenerateEnemies();

                        foreach (var door in _doors)
                        {
                            door.State.ChangeState(DoorState.BattleClose);
                        }
                    }
                }
            }
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            if (other.CompareTag("Player"))
            {
                TypeEventSystem.GLOBAL.Send(new ExitRoomEvent(this));
            }
        }

        public void AddEnemyGeneratePos(Vector3 generatePos)
        {
            _enemyGeneratePoses.Add(generatePos);
        }

        public void GenerateEnemies()
        {
            _enemyWaves.RemoveAt(0);

            var enemyCount = (3, 5 + 1).RandomSelect();

            var posGen = _enemyGeneratePoses
               .OrderByDescending(p => Player.Instance.Distance(p))
               .Take(enemyCount)
               .ToList();

            for (int i = 0; i < posGen.Count; i++)
            {
                var enemy = LevelController.Instance.Enemy.Instantiate(keepName: true)
                   .EnableGameObject()
                   .SetPosition(posGen[i]);

                _enemiesInRoom.Add(enemy);
            }
        }

        public Room AddDoor(Door door)
        {
            _doors.Add(door);
            return this;
        }

        public Room SetGrid(RoomGrid grid)
        {
            _grid = grid;
            return this;
        }

        protected override IArchitecture _Architecture { get => Game.Interface; }
    }
}