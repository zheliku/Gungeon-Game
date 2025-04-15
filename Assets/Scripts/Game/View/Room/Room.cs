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
    using Framework.Toolkits.GridKit;
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
        Closed,   // 初始状态为 Closed
        PlayerIn, // 玩家正在 Fighting
        Unlocked
    }

    public class Room : AbstractView
    {
        public DynamicGrid<PathFindingHelper.TileNode> PathFindingGrid;

        public Vector3Int LB;
        public Vector3Int RT;

        public List<Vector3> EnemyGeneratePoses { get; } = new List<Vector3>();

        public List<Vector3> ShopGeneratePoses { get; } = new List<Vector3>();

        private List<Door> _doors = new List<Door>();

        [ShowInInspector]
        private RoomGrid _grid;

        public RoomGrid Grid
        {
            get => _grid;
        }

        private List<Enemy> _enemiesInRoom = new List<Enemy>();

        private List<EnemyWaveData> _enemyWaves = new List<EnemyWaveData>();

        private EnemyWaveData _currentWave;

        [ShowInInspector]
        public RoomNode Node { get; set; }

        public RoomType RoomType { get => Node.RoomType; }

        [ShowInInspector]
        public RoomState State { get; private set; } = RoomState.Closed;

        public HashSet<IPowerUp> PowerUps { get; private set; } = new HashSet<IPowerUp>();

        public Final Final;

        [ShowInInspector]
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

            TypeEventSystem.GLOBAL.Register<BossDieEvent>(e =>
            {
                if (Final != null)
                {
                    Final.EnableGameObject();
                }
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
            else if (RoomType == RoomType.Final)
            {
                Final.DisableGameObject();
            }
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag("Player"))
            {
                TypeEventSystem.GLOBAL.Send(new EnterRoomEvent(this));

                if (State == RoomState.Closed) // 第一次进
                {
                    if (Node.RoomType == RoomType.Normal)
                    {
                        OnFirstEnterNormalRoom();
                    }
                    else if (Node.RoomType == RoomType.Final)
                    {
                        State = RoomState.PlayerIn;

                        var boss = EnemyFactory.Instance.Bosses[0].GameObject.Instantiate()
                           .SetPosition(EnemyGeneratePoses.RandomTakeOne())
                           .Enable()
                           .GetComponent<Boss>();

                        boss.Room = this;
                        
                        foreach (var door in _doors)
                        {
                            door.State.ChangeState(DoorState.BattleClose);
                        }
                    }
                    else
                    {
                        if (Node.RoomType == RoomType.Shop)
                        {
                            OnFirstEnterShopRoom();
                        }

                        State = RoomState.Unlocked; // 进入房间直接解锁
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

        private void OnFirstEnterNormalRoom()
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

                while (targetScore > 0 && waveConfig.EnemyNames.Count < EnemyGeneratePoses.Count)
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

        private void OnFirstEnterShopRoom()
        {
            var normalShopItem = this.GetSystem<ShopSystem>().CalculateNormalShopItems();

            // 必须生成一个钥匙
            var keyItem     = normalShopItem.First(item => ReferenceEquals(item.Item1, PowerUpFactory.Instance.Key));
            var generatePos = ShopGeneratePoses.RandomTakeOneAndRemove();
            LevelController.Instance.ShopItemTemplate.Instantiate()
               .EnableGameObject()
               .Self(self =>
                {
                    self.PowerUp = keyItem.Item1;
                    self.Price   = keyItem.Item2;
                    self.UpdateView();
                })
               .SetPosition(generatePos);

            var takeCount = (2, normalShopItem.Count + 1).RandomSelect();
            takeCount = takeCount.MinWith(ShopGeneratePoses.Count); // 不能超过生成位置数量

            // 随机生成其他
            for (int i = 0; i < takeCount; i++)
            {
                var item = normalShopItem.RandomTakeOne();
                generatePos = ShopGeneratePoses.RandomTakeOneAndRemove();

                var shopItem = LevelController.Instance.ShopItemTemplate.Instantiate()
                   .EnableGameObject()
                   .Self(self =>
                    {
                        self.PowerUp = item.Item1;
                        self.Price   = item.Item2;
                        self.UpdateView();
                    })
                   .SetPosition(generatePos);
            }
        }

        public void GenerateEnemies(EnemyWaveData waveData)
        {
            var posGen = EnemyGeneratePoses
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
                   .GetComponent<Enemy>();

                enemy.Room = this;
            }
        }

        // 准备A*节点
        public void PrepareAStarNodes()
        {
            // 如果节点类型是最终房间或普通房间
            if (Node.RoomType == RoomType.Final || Node.RoomType == RoomType.Normal)
            {
                // 创建动态网格
                PathFindingGrid = new DynamicGrid<PathFindingHelper.TileNode>();

                // 遍历网格中的每个节点
                for (var i = LB.x; i <= RT.x; i++)
                {
                    for (var j = LB.y; j <= RT.y; j++)
                    {
                        // 判断节点是否可通行
                        var walkable =
                            LevelController.Instance.WallTilemap.GetTile(new Vector3Int(i, j, 0)) == null;

                        // 初始化节点
                        PathFindingGrid[i, j] = new PathFindingHelper.TileNode(PathFindingGrid);
                        PathFindingGrid[i, j].Init(walkable, new PathFindingHelper.TileCoords()
                        {
                            Pos = new Vector3Int(i, j, 0)
                        });
                    }
                }

                // 缓存每个节点的邻居节点
                foreach (var pair in PathFindingGrid)
                {
                    pair.Value.CacheNeighbors();
                }
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