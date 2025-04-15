// ------------------------------------------------------------
// @file       LevelController.cs
// @brief
// @author     zheliku
// @Modified   2025-01-30 20:01:15
// @Copyright  Copyright (c) 2025, zheliku
// ------------------------------------------------------------

namespace Game
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Framework.Core;
    using Framework.Toolkits.FluentAPI;
    using Framework.Toolkits.SingletonKit;
    using Framework.Toolkits.TreeKit;
    using Sirenix.OdinInspector;
    using UnityEngine;
    using UnityEngine.Tilemaps;

    public class LevelController : MonoSingleton<LevelController>
    {
        public TileBase WallTile;

        [SerializeField]
        private List<TileBase> _floorTiles = new List<TileBase>();

        public TileBase FloorTile
        {
            get
            {
                var levelId = this.GetModel<LevelModel>().CurrentLevel.LevelId;
                return _floorTiles[levelId - 1];
            }
        }

        [HierarchyPath("Grid/Wall")]
        public Tilemap WallTilemap;

        [HierarchyPath("Grid/Floor")]
        public Tilemap FloorTilemap;

        [ShowInInspector]
        public List<Enemy> TemplateEnemies = new List<Enemy>();

        public Enemy Enemy
        {
            get => TemplateEnemies.RandomTakeOne();
        }

        [HierarchyPath("Template/Final")]
        public Final FinalTemplate;

        [HierarchyPath("Template/Room")]
        public Room RoomTemplate;

        [HierarchyPath("Template/Door")]
        public Door DoorTemplate;

        [HierarchyPath("Template/ShopItem")]
        public ShopItem ShopItemTemplate;

        protected void Awake()
        {
            this.BindHierarchyComponent();

            FinalTemplate.DisableGameObject();
            RoomTemplate.DisableGameObject();
            DoorTemplate.DisableGameObject();
            ShopItemTemplate.DisableGameObject();

            // DontDestroyOnLoad(this); // 防止被销毁
        }

        private void Start()
        {
            GenerateRoomMap(this.GetModel<LevelModel>().CurrentLevel);
        }

        private RoomNode GenerateRoomMap(LevelData level)
        {
            var roomNode = new RoomNode(level.RoomTree.Root.Data, GetRoomGridByType(RoomType.Init));

            var levelModel = this.GetModel<LevelModel>();

            // 基于权重生成网格
            var predictWeight = 0;
            while (!GenerateRoomMapBFS(level.RoomTree, roomNode, predictWeight))
            {
                predictWeight++;
                roomNode.ClearConnect(); // 每次重新生成需要清除上次的结果
            }

            Debug.Log(predictWeight + " weight!");

            var maxRoomWidth  = 0;
            var maxRoomHeight = 0;
            level.RoomTree.Traverse(type =>
            {
                maxRoomWidth  = maxRoomWidth.MaxWith(GetRoomGridByType(type).Column);
                maxRoomHeight = maxRoomHeight.MaxWith(GetRoomGridByType(type).Row);
            });

            var queue          = new Queue<RoomNode>();
            var visitedRoom    = new HashSet<RoomNode>(); // 记录已生成的房间
            var generatedRooms = levelModel.GeneratedRooms;
            queue.Enqueue(roomNode);

            while (queue.Count > 0)
            {
                var node = queue.Dequeue();
                visitedRoom.Add(node);
                var room = GenerateRoomByNode(node, maxRoomWidth, maxRoomHeight);
                generatedRooms[node.Index] = room;

                foreach (var pair in node.ConnectNodes)
                {
                    if (!visitedRoom.Contains(pair.Value)) // 防止重复生成
                    {
                        queue.Enqueue(pair.Value);
                    }
                }
            }

            return roomNode;
        }

        private bool GenerateRoomMapBFS(Tree<RoomType> tree, RoomNode roomRoot, int predictWeight = 0)
        {
            var roomQueue  = new Queue<RoomNode>();
            var treeQueue  = new Queue<TreeNode<RoomType>>();
            var existIndex = new HashSet<Vector2Int>(); // 记录已生成的房间位置
            roomQueue.Enqueue(roomRoot);
            treeQueue.Enqueue(tree.Root);
            existIndex.Add(Vector2Int.zero);

            while (treeQueue.Count > 0)
            {
                var roomNode = roomQueue.Dequeue();
                var treeNode = treeQueue.Dequeue();

                var availableDirections = LevelGenHelper.GetAvailableDirections(roomNode.Index, existIndex);

                if (availableDirections.Count < treeNode.Degree) // 没有足够的方向可连接
                {
                    Debug.LogWarning("房间度数与树节点度数不匹配");
                    return false;
                }

                var predictDic       = LevelGenHelper.Predict(roomNode.Index, existIndex);
                var sortedDirections = predictDic.OrderBy(pair => pair.Value).ToList(); // 最优方案：按可选择方向数从大到小排序

                // 是否选择最优方案
                var choosePerfect = (0, 100 + 1).RandomSelect() < predictWeight;

                foreach (var child in treeNode.Children)
                {
                    Direction nextRoomDirection;
                    if (choosePerfect) // 选择最优方案
                    {
                        nextRoomDirection = sortedDirections.First().Key;
                        sortedDirections.RemoveAt(0);
                    }
                    else // 随机生成方向
                    {
                        nextRoomDirection = availableDirections.RandomTakeOneAndRemove();
                    }

                    var connectNode = new RoomNode(child.Data, GetRoomGridByType(child.Data))
                    {
                        Index = roomNode.Index + nextRoomDirection.ToVector2Int()
                    };

                    existIndex.Add(connectNode.Index);
                    var connectSuccess = roomNode.Connect(nextRoomDirection, connectNode); // 连接房间

                    if (!connectSuccess)
                    {
                        return false;
                    }

                    treeQueue.Enqueue(child);
                    roomQueue.Enqueue(connectNode);
                }
            }

            return true;
        }

        private Room GenerateRoomByNode(RoomNode node, int maxRoomWidth, int maxRoomHeight)
        {
            var roomGrid = node.RoomGrid;

            var roomWidth  = roomGrid.Column;
            var roomHeight = roomGrid.Row;

            var pos = new Vector2Int(
                node.Index.x * (maxRoomWidth + LevelConfig.ROOM_INTERVAL.x),
                node.Index.y * (maxRoomHeight + LevelConfig.ROOM_INTERVAL.y)
            );

            var room = RoomTemplate.Instantiate(this)
               .SetGrid(roomGrid)
               .SetPosition(x: pos.x + 0.5f, y: pos.y + 0.5f) // +0.5f to the center grid
               .EnableGameObject();
            room.Node = node;

            var roomTrigger = room.GetComponent<BoxCollider2D>();
            roomTrigger.size = new Vector2(roomWidth - 4, roomHeight - 4); // 减去 4 是为了防止玩家卡在墙壁上

            // 绘制墙体与背景
            for (int i = -roomHeight / 2; i <= roomHeight / 2; i++)
            {
                for (int j = -roomWidth / 2; j <= roomWidth / 2; j++)
                {
                    var code = roomGrid[-i + roomHeight / 2, j + roomWidth / 2]; // 从左下角开始

                    var x = pos.x + j;
                    var y = pos.y + i;

                    FloorTilemap.SetTile(new Vector3Int(x, y, 0), FloorTile);

                    if (code == '1')
                    {
                        WallTilemap.SetTile(new Vector3Int(x, y, 0), WallTile);
                    }
                    else if (code == '@')
                    {
                        Player.Instance.SetPosition(x + 0.5f, y + 0.5f, 0); // +0.5f to the center grid
                    }
                    else if (code == 'e')
                    {
                        room.EnemyGeneratePoses.Add(new Vector3(x + 0.5f, y + 0.5f, 0));
                    }
                    else if (code == '#')
                    {
                        var final = FinalTemplate.Instantiate(keepName: true)
                           .SetPosition(x + 0.5f, y + 0.5f, 0); // +0.5f to the center grid

                        room.Final = final;
                    }
                    else if (code == 'd')
                    {
                        // var door = DoorTemplate.Instantiate(parent: room)
                        //    .EnableGameObject()
                        //    .SetPosition(x + 0.5f, y + 0.5f, 0); // +0.5f to the center grid
                        //
                        // room.AddDoor(door);
                    }
                    else if (code == 'c')
                    {
                        var chest = PowerUpFactory.Instance.Chest.Instantiate(parent: room)
                           .EnableGameObject()
                           .SetPosition(x + 0.5f, y + 0.5f, 0); // +0.5f to the center grid

                        chest.Room = room;
                    }
                    else if (code == 's')
                    {
                        room.ShopGeneratePoses.Add(new Vector3(x + 0.5f, y + 0.5f, 0));
                    }
                }
            }

            // return room;

            var openDirections  = node.ConnectNodes.Keys.ToList();
            var closeDirections = Enum.GetValues(typeof(Direction)).Cast<Direction>().Except(openDirections).ToList();

            // 开启的方向绘制连接道路
            foreach (var direction in openDirections)
            {
                var connectNode = node.ConnectNodes[direction]; // 连接的节点
                var connectNodePos = new Vector2Int(
                    connectNode.Index.x * (maxRoomWidth + LevelConfig.ROOM_INTERVAL.x),
                    connectNode.Index.y * (maxRoomHeight + LevelConfig.ROOM_INTERVAL.y) // 每个房间目前间距为 2
                );
                var dirVector    = direction.ToVector2Int();                  // 门朝向的向量
                var normalVector = new Vector2Int(dirVector.y, -dirVector.x); // 门朝向的垂直向量

                // 开始位置与结束位置的门位置
                var startPos = new Vector2Int(
                    direction.ToVector2Int().x * roomWidth / 2 + pos.x,
                    direction.ToVector2Int().y * roomHeight / 2 + pos.y
                );
                var endPos = new Vector2Int(
                    direction.Opposite().ToVector2Int().x * connectNode.RoomGrid.Column / 2 + connectNodePos.x,
                    direction.Opposite().ToVector2Int().y * connectNode.RoomGrid.Row / 2 + connectNodePos.y
                );

                // 绘制门
                var startDoor = DoorTemplate.Instantiate(parent: room)
                   .EnableGameObject()
                   .SetPosition(startPos.x + 0.5f, startPos.y + 0.5f, 0) // +0.5f to the center grid
                   .SetLocalScale(x: 3)
                   .SetTransformRight(new Vector3(normalVector.x, normalVector.y, 0));
                room.AddDoor(startDoor);

                // 绘制连接道路
                for (Vector2Int p = startPos + dirVector; p != endPos; p += dirVector)
                {
                    for (int i = -2; i <= 2; i++)
                    {
                        // 绘制地板
                        FloorTilemap.SetTile((Vector3Int) (p + normalVector * i), FloorTile);
                    }

                    // 绘制两侧墙壁
                    WallTilemap.SetTile((Vector3Int) (p + normalVector * 2), WallTile);
                    WallTilemap.SetTile((Vector3Int) (p - normalVector * 2), WallTile);
                }

                // 取消开始与结束位置门朝向的墙壁
                WallTilemap.SetTile((Vector3Int) (startPos + normalVector), null);
                WallTilemap.SetTile((Vector3Int) (startPos - normalVector), null);
                WallTilemap.SetTile((Vector3Int) (endPos + normalVector), null);
                WallTilemap.SetTile((Vector3Int) (endPos - normalVector), null);
            }

            // 关闭的方向绘制墙壁
            foreach (var direction in closeDirections)
            {
                var doorPosX = direction.ToVector2Int().x * roomWidth / 2 + pos.x;
                var doorPosY = direction.ToVector2Int().y * roomHeight / 2 + pos.y;
                WallTilemap.SetTile(new Vector3Int(doorPosX, doorPosY, 0), WallTile);
            }

            room.LB = new Vector3Int(pos.x - roomWidth / 2, pos.y - roomHeight / 2, 0);
            room.RT = new Vector3Int(pos.x + roomWidth / 2, pos.y + roomHeight / 2, 0);

            room.PrepareAStarNodes();

            return room;
        }

        private RoomGrid GetRoomGridByType(RoomType type)
        {
            return type switch
            {
                RoomType.Init   => RoomConfig.INIT_ROOM,
                RoomType.Normal => this.GetModel<LevelModel>().CurrentLevel.NormalRoomTemplates.RandomTakeOne(),
                RoomType.Final  => RoomConfig.FINAL_ROOM,
                RoomType.Chest  => RoomConfig.CHEST_ROOM,
                RoomType.Shop   => RoomConfig.SHOP_ROOM,
                _               => throw new ArgumentOutOfRangeException()
            };
        }

        protected override IArchitecture _Architecture { get => Game.Architecture; }
    }
}