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
    using Framework.Core.View;
    using Framework.Toolkits.FluentAPI;
    using UnityEngine;
    using UnityEngine.Tilemaps;

    public class LevelController : AbstractView
    {
        public TileBase Tile;

        public Tilemap Tilemap;

        public Enemy Enemy;

        private LevelModel _levelModel;

        private void Awake()
        {
            _levelModel = this.GetModel<LevelModel>();

            Player.Instance = "Template/Player".GetComponentInHierarchy<Player>(transform);
            Enemy = "Template/Enemy".GetComponentInHierarchy<Enemy>(transform);
            
            Player.Instance.DisableGameObject();
            Enemy.DisableGameObject();
        }

        private void Start()
        {
            Tilemap.SetTile(new Vector3Int(0, 0, 0), Tile);
            Tilemap.SetTile(new Vector3Int(1, 0, 0), Tile);
            Tilemap.SetTile(new Vector3Int(2, 0, 0), Tile);

            var initRoom = _levelModel.InitRoom;
            for (int i = 0; i < initRoom.Count; i++)
            {
                var rowCode = initRoom[i];
                for (int j = 0; j < rowCode.Length; j++)
                {
                    var code = rowCode[j];

                    var x = j;
                    var y = initRoom.Count - i;

                    if (code == '1')
                    {
                        Tilemap.SetTile(new Vector3Int(x, y, 0), Tile);
                    }
                    else if (code == '@')
                    {
                        var player = Player.Instance.Instantiate(keepName: true)
                           .EnableGameObject();
                        player.SetPosition(x + 0.5f, y + 0.5f, 0);
                        Player.Instance = player;
                    }
                    else if (code == 'e')
                    {
                        var enemy = Enemy.Instantiate(keepName: true)
                           .EnableGameObject();
                        enemy.SetPosition(x + 0.5f, y + 0.5f, 0);
                    }
                }
            }
        }

        protected override IArchitecture Architecture { get => Game.Interface; }
    }
}