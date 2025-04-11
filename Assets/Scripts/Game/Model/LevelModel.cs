// ------------------------------------------------------------
// @file       LevelModel.cs
// @brief
// @author     zheliku
// @Modified   2025-01-30 20:01:32
// @Copyright  Copyright (c) 2025, zheliku
// ------------------------------------------------------------

namespace Game
{
    using System.Collections.Generic;
    using Framework.Core;
    using Framework.Core.Model;
    using Framework.Toolkits.GridKit;

    public class LevelModel : AbstractModel
    {
        public DynamicGrid<Room> GeneratedRooms = new DynamicGrid<Room>();

        public Room CurrentRoom { get; private set; }

        protected override void OnInit()
        {
            TypeEventSystem.GLOBAL.Register<EnterRoomEvent>(e =>
            {
                CurrentRoom = e.Room;
            });
            
            TypeEventSystem.GLOBAL.Register<ExitRoomEvent>(e =>
            {
                if (CurrentRoom == e.Room)
                {
                    CurrentRoom = null;
                }
            });
        }
    }
}