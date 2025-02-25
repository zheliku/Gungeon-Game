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
    using UnityEngine;
    using UnityEngine.Tilemaps;

    public class LevelController : MonoSingleton<LevelController>
    {
        public TileBase WallTile;
        public TileBase FloorTile;

        [HierarchyPath("Grid/Wall")]
        public Tilemap WallTilemap;

        [HierarchyPath("Grid/Floor")]
        public Tilemap FloorTilemap;

        [HierarchyPath("Template/Enemy")]
        public Enemy Enemy;

        [HierarchyPath("Template/Final")]
        public Final FinalTemplate;

        [HierarchyPath("Template/Room")]
        public Room RoomTemplate;

        [HierarchyPath("Template/Door")]
        public Door DoorTemplate;

        [HierarchyPath("Template/Hp1")]
        public Hp1 Hp1Template;

        [HierarchyPath("Template/Chest")]
        public Chest ChestTemplate;

        private LevelModel _levelModel;

        protected void Awake()
        {
            this.BindHierarchyComponent();

            _levelModel = this.GetModel<LevelModel>();

            Enemy.DisableGameObject();
            FinalTemplate.DisableGameObject();
            RoomTemplate.DisableGameObject();
            DoorTemplate.DisableGameObject();
            Hp1Template.DisableGameObject();
            ChestTemplate.DisableGameObject();

            // DontDestroyOnLoad(this); // 防止被销毁
        }

        private void Start()
        {
            var initRoomNode = new RoomNode(RoomType.Init);
            initRoomNode.Connect(RoomType.Normal)
               .Connect(RoomType.Normal)
               .Connect(RoomType.Chest)
               .Connect(RoomType.Normal)
               .Connect(RoomType.Normal)
               .Connect(RoomType.Normal)
               .Connect(RoomType.Final);

            GenerateRoomMapBFS(initRoomNode);
        }

        private void GenerateRoomMapBFS(RoomNode roomNode)
        {
            var queue = new Queue<RoomNode>();
            queue.Enqueue(roomNode);

            var visited = new HashSet<RoomNode>(); // 记录已访问的节点

            while (queue.Count > 0)
            {
                var node = queue.Dequeue();
                visited.Add(node); // 标记为已访问

                GenerateRoomByNode(node);

                if (node.ConnectNodes.Count == 0)
                {
                    continue;
                }

                foreach (var connectNode in node.ConnectNodes.Values.ToList())
                {
                    if (visited.Contains(connectNode)) // 检查是否已访问
                    {
                        continue;
                    }

                    queue.Enqueue(connectNode);
                }
            }
        }

        private void GenerateRoomByNode(RoomNode node)
        {
            var roomGrid = node.RoomType switch
            {
                RoomType.Init   => _levelModel.InitRoom,
                RoomType.Normal => _levelModel.NormalRoom.RandomTakeOne(),
                RoomType.Final  => _levelModel.FinalRoom,
                RoomType.Chest  => _levelModel.ChestRoom,
                _               => throw new ArgumentOutOfRangeException()
            };

            var roomWidth  = roomGrid.Column;
            var roomHeight = roomGrid.Row;

            var pos = new Vector2Int(
                node.Index.x * (roomWidth + 2),
                node.Index.y * (roomHeight + 2) // 每个房间目前间距为 2
            );

            var room = RoomTemplate.Instantiate(this)
               .SetGrid(roomGrid)
               .SetPosition(x: pos.x + 0.5f, y: pos.y + 0.5f) // +0.5f to the center grid
               .EnableGameObject();

            var roomTrigger = room.GetComponent<BoxCollider2D>();
            roomTrigger.size = new Vector2(roomWidth - 4, roomHeight - 4); // 减去 4 是为了防止玩家卡在墙壁上

            // 绘制墙体与背景
            for (int i = -roomHeight / 2; i <= roomHeight / 2; i++)
            {
                for (int j = -roomWidth / 2; j <= roomWidth / 2; j++)
                {
                    var code = roomGrid[i + roomHeight / 2, j + roomWidth / 2];

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
                        var generatePos = new Vector3(x + 0.5f, y + 0.5f, 0);
                        room.AddEnemyGeneratePos(generatePos);
                    }
                    else if (code == '#')
                    {
                        FinalTemplate.Instantiate(keepName: true)
                           .EnableGameObject()
                           .SetPosition(x + 0.5f, y + 0.5f, 0); // +0.5f to the center grid
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
                        ChestTemplate.Instantiate(parent: room)
                           .EnableGameObject()
                           .SetPosition(x + 0.5f, y + 0.5f, 0); // +0.5f to the center grid
                    }
                }
            }

            var openDirections  = node.ConnectNodes.Keys.ToList();
            var closeDirections = Enum.GetValues(typeof(Direction)).Cast<Direction>().Except(openDirections).ToList();

            // 开启的方向绘制连接道路
            foreach (var direction in openDirections)
            {
                var connectNode = node.ConnectNodes[direction]; // 连接的节点
                var connectNodePos = new Vector2Int(
                    connectNode.Index.x * (roomWidth + 2),
                    connectNode.Index.y * (roomHeight + 2) // 每个房间目前间距为 2
                );
                var dirVector    = direction.ToVector2Int();                  // 门朝向的向量
                var normalVector = new Vector2Int(dirVector.y, -dirVector.x); // 门朝向的垂直向量

                // 开始位置与结束位置的门位置
                var startPos = new Vector2Int(
                    direction.ToVector2Int().x * roomWidth / 2 + pos.x,
                    direction.ToVector2Int().y * roomHeight / 2 + pos.y
                );
                var endPos = new Vector2Int(
                    direction.Opposite().ToVector2Int().x * roomWidth / 2 + connectNodePos.x,
                    direction.Opposite().ToVector2Int().y * roomHeight / 2 + connectNodePos.y
                );

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

                // var door = DoorTemplate.Instantiate(parent: room)
                //    .DisableGameObject()
                //    .SetPosition(x + 0.5f, y + 0.5f, 0); // +0.5f to the center grid
                //
                // room.AddDoor(door);
            }

            // 关闭的方向绘制墙壁
            foreach (var direction in closeDirections)
            {
                var doorPosX = direction.ToVector2Int().x * roomWidth / 2 + pos.x;
                var doorPosY = direction.ToVector2Int().y * roomHeight / 2 + pos.y;
                WallTilemap.SetTile(new Vector3Int(doorPosX, doorPosY, 0), WallTile);
            }
        }

        protected override IArchitecture _Architecture { get => Game.Interface; }
    }
}