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
    using Framework.Core;
    using Framework.Toolkits.FluentAPI;
    using UnityEngine;

    public enum RoomType
    {
        Init,
        Normal,
        Final
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

        private RoomConfig _config;

        private List<Enemy> _enemiesInRoom = new List<Enemy>();

        private List<EnemyWaveConfig> _enemyWaves = new List<EnemyWaveConfig>()
        {
            new EnemyWaveConfig(),
            new EnemyWaveConfig(),
            new EnemyWaveConfig()
        };

        private EnemyWaveConfig _currentWave;

        public RoomState State { get; private set; } = RoomState.Closed;

        private void Awake()
        {
            TypeEventSystem.GLOBAL.Register<EnemyDieEvent>(e =>
            {
                _enemiesInRoom.Remove(e.Enemy);

                // 检测房间内敌人数量是否为 0
                if (_enemiesInRoom.Count == 0)
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
                            door.DisableGameObject();
                        }
                    }
                }
            });
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag("Player"))
            {
                if (_config.RoomType == RoomType.Normal)
                {
                    if (State == RoomState.Closed) // 第一次进，状态变为 PlayerIn
                    {
                        State = RoomState.PlayerIn;

                        GenerateEnemies();

                        foreach (var door in _doors)
                        {
                            door.EnableGameObject();
                        }
                    }
                }
            }
        }

        public void AddEnemyGeneratePos(Vector3 generatePos)
        {
            _enemyGeneratePoses.Add(generatePos);
        }

        public void GenerateEnemies()
        {
            _enemyWaves.RemoveAt(0);

            foreach (var generatePos in _enemyGeneratePoses)
            {
                var enemy = LevelController.Instance.Enemy.Instantiate(keepName: true)
                   .EnableGameObject()
                   .SetPosition(generatePos); // +0.5f to the center grid

                _enemiesInRoom.Add(enemy);
            }
        }

        public Room AddDoor(Door door)
        {
            _doors.Add(door);
            return this;
        }

        public Room SetConfig(RoomConfig config)
        {
            _config = config;
            return this;
        }

        protected override IArchitecture _Architecture { get => Game.Interface; }
    }
}