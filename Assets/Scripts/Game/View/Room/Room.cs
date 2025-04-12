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
    using Framework.Toolkits.ActionKit;
    using Framework.Toolkits.FluentAPI;
    using Sirenix.OdinInspector;
    using UnityEngine;

    public enum RoomType
    {
        Init,
        Normal,
        Final,
        Shop,
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

        public RoomGrid Grid
        {
            get => _grid;
        }

        private List<IEnemy> _enemiesInRoom = new List<IEnemy>();

        private List<EnemyWaveData> _enemyWaves = new List<EnemyWaveData>();

        private EnemyWaveData _currentWave;

        [ShowInInspector]
        public RoomNode Node { get; set; }

        public RoomType RoomType { get => _grid.RoomType; }

        [ShowInInspector]
        public RoomState State { get; private set; } = RoomState.Closed;

        public HashSet<IPowerUp> PowerUps { get; private set; } = new HashSet<IPowerUp>();

        [ShowInInspector]
        public List<IEnemy> EnemiesInRoom
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
                        _currentWave = _enemyWaves[0];
                        GenerateEnemies(_currentWave);
                        _enemyWaves.RemoveAt(0);
                    }
                    else // 没有下一波敌人
                    {
                        State = RoomState.Unlocked;

                        foreach (var door in _doors) // 开门
                        {
                            door.State.ChangeState(DoorState.Open);
                        }

                        if (RoomType == RoomType.Normal)
                        {
                            TypeEventSystem.GLOBAL.Send(new RoomClearEvent(this)); // 房间被清空
                        }
                    }
                }
            }).UnRegisterWhenGameObjectDestroyed(this);

            TypeEventSystem.GLOBAL.Register<RoomClearEvent>(e =>
            {
                if (e.Room != this)
                {
                    return;
                }

                // foreach (var powerUp in PowerUps)
                // {
                //     var cachedPowerUp = powerUp;
                //     ActionKit.OnFixedUpdate.Register(() =>
                //     {
                //         var spriteRenderer = cachedPowerUp.SpriteRenderer;
                //         spriteRenderer.transform.Translate(
                //             spriteRenderer.Direction2DTo(Player.Instance) * Time.fixedDeltaTime * 5);
                //     }).UnRegisterWhenGameObjectDestroyed(powerUp.SpriteRenderer);
                // }
            }).UnRegisterWhenGameObjectDestroyed(gameObject);
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

            // else if (RoomType == RoomType.Normal)
            // {
            //     var randomCount = (1, 3 + 1).RandomSelect(); // 随机生成 1 到 3 波敌人
            //     for (int i = 0; i < randomCount; i++)
            //     {
            //         _enemyWaves.Add(new EnemyWaveConfig());
            //     }
            // }
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

                        // 填充敌人
                        var difficultyLevel = this.GetModel<LevelModel>().PacingQueue.Dequeue();
                        var difficultyScore = 10 + difficultyLevel * 3;
                        var waveCount       = 0;
                        if (difficultyLevel <= 3)
                        {
                            waveCount = (1, difficultyLevel + 1).RandomSelect();
                        }
                        else
                        {
                            waveCount = (difficultyLevel / 3, difficultyLevel / 2).RandomSelect();
                        }

                        for (int i = 0; i < waveCount; i++)
                        {
                            var targetScore = difficultyScore / waveCount +
                                              (-difficultyScore / 10 * 2 + 1, difficultyScore / 20 + 1 + 1).RandomSelect();
                            var waveConfig = new EnemyWaveData();

                            while (targetScore > 0 && waveConfig.EnemyNames.Count < _enemyGeneratePoses.Count)
                            {
                                var enemyScore = EnemyFactory.GenTargetEnemyScore();
                                targetScore -= enemyScore;
                                waveConfig.EnemyNames.Add(EnemyFactory.GetEnemyByScore(enemyScore));
                            }
                            
                            _enemyWaves.Add(waveConfig);
                        }
                        
                        _currentWave = _enemyWaves[0];
                        GenerateEnemies(_currentWave);
                        _enemyWaves.RemoveAt(0);

                        foreach (var door in _doors)
                        {
                            door.State.ChangeState(DoorState.BattleClose);
                        }
                    }
                }
                else
                {
                    State = RoomState.Unlocked; // 进入房间直接解锁
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

        public void GenerateEnemies(EnemyWaveData waveData)
        {
            var posGen = _enemyGeneratePoses
               .OrderByDescending(p => Player.Instance.Distance(p))
               .ToList();

            foreach (var enemyName in waveData.EnemyNames)
            {
                if (posGen.Count == 0)
                {
                    break;
                }
                
                var enemy = EnemyFactory.GetEnemyByName(enemyName).GameObject
                   .Instantiate(keepName: true)
                   .SetPosition(posGen.RandomTakeOneAndRemove())
                   .Enable()
                   .GetComponent<IEnemy>();

                enemy.Room = this;
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

        protected override IArchitecture _Architecture { get => Game.Architecture; }
    }
}