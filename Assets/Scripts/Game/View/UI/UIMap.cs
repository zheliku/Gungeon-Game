// ------------------------------------------------------------
// @file       UIMap.cs
// @brief
// @author     zheliku
// @Modified   2025-03-14 22:06:08
// @Copyright  Copyright (c) 2025, zheliku
// ------------------------------------------------------------

namespace Game
{
    using Framework.Core;
    using Framework.Toolkits.FluentAPI;
    using Framework.Toolkits.UIKit;
    using UnityEngine;

    public class UIMap : UIPanel
    {
        [HierarchyPath("MapItemRoot")]
        public Transform MapItemRoot;

        [HierarchyPath("MapItemTemplate")]
        public MapItem MapItemTemplate;

        protected override void OnShow()
        {
            Time.timeScale = 0;
            MapItemRoot.DestroyChildren();

            var levelModel = this.GetModel<LevelModel>();

            var generatedRooms = levelModel.GeneratedRooms;
            foreach (var pair in generatedRooms)
            {
                var room = pair.Value;
                var x    = pair.Key.x;
                var y    = pair.Key.y;
                if (room.State == RoomState.Unlocked)
                {
                    MapItemTemplate.Instantiate(MapItemRoot)
                       .SetData(room)
                       .SetLocalPosition(x: x * 60, y: y * 60)
                       .EnableGameObject();
                }

                if (room == levelModel.CurrentRoom)
                {
                    MapItemRoot.SetLocalPosition(x: -x * 60, y: -y * 60); // 将当前房间移动到屏幕中心
                }
            }
        }

        protected override void OnHide()
        {
            Time.timeScale = 1;
        }

        protected override IArchitecture _Architecture { get => Game.Interface; }
    }
}