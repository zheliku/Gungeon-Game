// ------------------------------------------------------------
// @file       LevelController.cs
// @brief
// @author     zheliku
// @Modified   2025-01-30 20:01:15
// @Copyright  Copyright (c) 2025, zheliku
// ------------------------------------------------------------

namespace Game
{
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

        [HierarchyPath("/Template/Enemy")]
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
        }

        private void Start()
        {
            var layout = new RoomNode(RoomType.Init);
            layout.Next(RoomType.Normal)
               .Next(RoomType.Normal)
               .Next(RoomType.Chest)
               .Next(RoomType.Normal)
               .Next(RoomType.Normal)
               .Next(RoomType.Normal)
               .Next(RoomType.Final);

            var currentRoomStartPosX = 0;

            GenerateRoomByNode(layout, ref currentRoomStartPosX);

            var grid       = _levelModel.InitRoom.Grid;
            var roomWidth  = grid.Column;
            var roomHeight = grid.Row;

            for (int count = 1; count <= 7; count++)
            {
                var posX = count * (roomWidth + 3) - 2;
                var posY = roomHeight / 2;

                foreach (var (i, j) in (-1, -1).StepTo((1, 1)))
                {
                    FloorTilemap.SetTile(new Vector3Int(posX + i, posY + j, 0), FloorTile);
                }

                foreach (var i in (-1).StepTo(1))
                {
                    WallTilemap.SetTile(new Vector3Int(posX + i, posY + 2, 0), WallTile);
                    WallTilemap.SetTile(new Vector3Int(posX + i, posY - 2, 0), WallTile);
                }
            }
        }

        private void GenerateRoomByNode(RoomNode node, ref int currentRoomStartPosX)
        {
            if (node.RoomType == RoomType.Init)
            {
                GenerateRoom(_levelModel.InitRoom, ref currentRoomStartPosX);
            }
            else if (node.RoomType == RoomType.Normal)
            {
                GenerateRoom(_levelModel.NormalRoom.RandomTakeOne(), ref currentRoomStartPosX);
            }
            else if (node.RoomType == RoomType.Final)
            {
                GenerateRoom(_levelModel.FinalRoom, ref currentRoomStartPosX);
            }
            else if (node.RoomType == RoomType.Chest)
            {
                GenerateRoom(_levelModel.ChestRoom, ref currentRoomStartPosX);
            }

            currentRoomStartPosX += 3;

            foreach (var child in node.Children)
            {
                GenerateRoomByNode(child, ref currentRoomStartPosX);
            }
        }

        private void GenerateRoom(RoomConfig roomConfig, ref int currentRoomStartPosX)
        {
            var grid       = roomConfig.Grid;
            var roomWidth  = grid.Column;
            var roomHeight = grid.Row;

            var room = RoomTemplate.Instantiate(this)
               .SetConfig(roomConfig)
               .SetPosition(x: currentRoomStartPosX + roomWidth / 2f, y: roomHeight / 2f)
               .EnableGameObject();

            var roomTrigger = room.GetComponent<BoxCollider2D>();
            roomTrigger.size = new Vector2(roomWidth - 4, roomHeight - 4); // 减去 4 是为了防止玩家卡在墙壁上

            for (int i = 0; i < roomHeight; i++)
            {
                for (int j = 0; j < roomWidth; j++)
                {
                    var code = grid[i, j];

                    var x = j + currentRoomStartPosX;
                    var y = roomHeight - i - 1;

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
                        var door = DoorTemplate.Instantiate(parent: room)
                           .DisableGameObject()
                           .SetPosition(x + 0.5f, y + 0.5f, 0); // +0.5f to the center grid

                        room.AddDoor(door);
                    }
                    else if (code == 'c')
                    {
                        ChestTemplate.Instantiate(parent: room)
                           .EnableGameObject()
                           .SetPosition(x + 0.5f, y + 0.5f, 0); // +0.5f to the center grid
                    }
                }
            }

            currentRoomStartPosX += roomWidth;
        }

        protected override IArchitecture _Architecture { get => Game.Interface; }
    }
}