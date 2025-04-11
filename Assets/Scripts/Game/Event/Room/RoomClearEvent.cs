// ------------------------------------------------------------
// @file       RoomClearEvent.cs
// @brief
// @author     zheliku
// @Modified   2025-04-11 11:04:16
// @Copyright  Copyright (c) 2025, zheliku
// ------------------------------------------------------------

namespace Game
{
    public struct RoomClearEvent
    {
        public Room Room;
        
        public RoomClearEvent(Room room)
        {
            Room = room;
        }
    }
}