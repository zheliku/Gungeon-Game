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
        public TileBase Tile;

        public Tilemap Tilemap;

        public Enemy Enemy;

        public Final Final;

        private LevelModel _levelModel;

        private void Awake()
        {
            _levelModel = this.GetModel<LevelModel>();

            Player.Instance = "Template/Player".GetComponentInHierarchy<Player>(transform);
            Enemy           = "Template/Enemy".GetComponentInHierarchy<Enemy>(transform);
            Final           = "Template/Final".GetComponentInHierarchy<Final>(transform);

            Player.Instance.DisableGameObject();
            Enemy.DisableGameObject();
            Final.DisableGameObject();
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

                    if (code == '1')
                    {
                        Tilemap.SetTile(new Vector3Int(x, y, 0), Tile);
                    }
                    else if (code == '@')
                    {
                        var player = Player.Instance.Instantiate(keepName: true)
                           .EnableGameObject()
                           .SetPosition(x + 0.5f, y + 0.5f, 0);
                        Player.Instance = player;
                    }
                    else if (code == 'e')
                    {
                        Enemy.Instantiate(keepName: true)
                           .EnableGameObject()
                           .SetPosition(x + 0.5f, y + 0.5f, 0);
                    }
                    else if (code == '#')
                    {
                        Final.Instantiate(keepName: true)
                           .EnableGameObject()
                           .SetPosition(x + 0.5f, y + 0.5f, 0);
                    }
                }
            }

            currentRoomStartPosX += roomCode[0].Length;
        }

        protected override IArchitecture Architecture { get => Game.Interface; }
    }
}