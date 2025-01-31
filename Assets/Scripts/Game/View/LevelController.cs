// ------------------------------------------------------------
// @file       LevelController.cs
// @brief
// @author     zheliku
// @Modified   2025-01-30 20:01:15
// @Copyright  Copyright (c) 2025, zheliku
// ------------------------------------------------------------

namespace Game
{
    using System.Collections.Generic;
    using Framework.Core;
    using Framework.Toolkits.FluentAPI;
    using UnityEngine;
    using UnityEngine.Tilemaps;

    public class LevelController : AbstractView
    {
        public TileBase WallTile;
        public TileBase FloorTile;

        [HierarchyPath("Grid/Wall")]
        public Tilemap WallTilemap;

        [HierarchyPath("Grid/Floor")]
        public Tilemap FloorTilemap;

        [HierarchyPath("/Template/Enemy")]
        public Enemy Enemy;

        [HierarchyPath("/Template/Final")]
        public Final Final;

        private LevelModel _levelModel;

        protected void Awake()
        {
            this.BindHierarchyComponent();
            
            _levelModel = this.GetModel<LevelModel>();

            Enemy.DisableGameObject();
            Final.DisableGameObject();

            Player.Instance = "Template/Player".GetComponentInHierarchy<Player>();
            Player.Instance.DisableGameObject();
        }

        private void Start()
        {
            var currentRoomStartPosX = 0;
            GenerateRoom(_levelModel.InitRoom, ref currentRoomStartPosX);
            currentRoomStartPosX += 2;
            GenerateRoom(_levelModel.NormalRoom, ref currentRoomStartPosX);
            currentRoomStartPosX += 2;
            GenerateRoom(_levelModel.FinalRoom, ref currentRoomStartPosX);
        }

        private void GenerateRoom(List<string> roomCode, ref int currentRoomStartPosX)
        {
            for (int i = 0; i < roomCode.Count; i++)
            {
                var rowCode = roomCode[i];
                for (int j = 0; j < rowCode.Length; j++)
                {
                    var code = rowCode[j];

                    var x = j + currentRoomStartPosX;
                    var y = roomCode.Count - i;

                    FloorTilemap.SetTile(new Vector3Int(x, y, 0), FloorTile);

                    if (code == '1')
                    {
                        WallTilemap.SetTile(new Vector3Int(x, y, 0), WallTile);
                    }
                    else if (code == '@')
                    {
                        var player = Player.Instance.Instantiate(keepName: true)
                           .EnableGameObject()
                           .SetPosition(x + 0.5f, y + 0.5f, 0); // +0.5f to the center grid
                        Player.Instance = player;
                    }
                    else if (code == 'e')
                    {
                        Enemy.Instantiate(keepName: true)
                           .EnableGameObject()
                           .SetPosition(x + 0.5f, y + 0.5f, 0); // +0.5f to the center grid
                    }
                    else if (code == '#')
                    {
                        Final.Instantiate(keepName: true)
                           .EnableGameObject()
                           .SetPosition(x + 0.5f, y + 0.5f, 0); // +0.5f to the center grid
                    }
                }
            }

            currentRoomStartPosX += roomCode[0].Length;
        }

        protected override IArchitecture _Architecture { get => Game.Interface; }
    }
}