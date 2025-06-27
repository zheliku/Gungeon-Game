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

        public LevelData CurrentLevel;

        public Queue<int> PacingQueue = new Queue<int>();

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
                    // CurrentRoom = null;
                }
            });
            
            Reset();
        }

        public void Reset()
        {
            CurrentLevel = Level1.DATA;
            PacingQueue  = new Queue<int>(CurrentLevel.Pacing);
        }

        /// <summary>
        /// 下一关
        /// </summary>
        /// <returns>是否还有下一关</returns>
        public bool NextLevel()
        {
            var levelIndex = LevelConfig.LEVELS.FindIndex(level => level == CurrentLevel);
            levelIndex++;
            if (levelIndex >= LevelConfig.LEVELS.Count)
            {
                return false; // 游戏通关
            }
            
            CurrentLevel = LevelConfig.LEVELS[levelIndex];
            return true;
        }
    }
}