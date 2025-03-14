// ------------------------------------------------------------
// @file       EnterRoomEvent.cs
// @brief
// @author     zheliku
// @Modified   2025-03-09 02:03:39
// @Copyright  Copyright (c) 2025, zheliku
// ------------------------------------------------------------

namespace Game
{
    public struct EnterRoomEvent
    {
        public Room Room;

        public EnterRoomEvent(Room room)
        {
            Room = room;
        }
    }
}