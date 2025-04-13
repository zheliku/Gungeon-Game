// ------------------------------------------------------------
// @file       Level1.cs
// @brief
// @author     zheliku
// @Modified   2025-04-11 15:04:33
// @Copyright  Copyright (c) 2025, zheliku
// ------------------------------------------------------------

namespace Game
{
    using System.Collections.Generic;
    using Framework.Toolkits.FluentAPI;

    public class Level1
    {
        public static readonly LevelData DATA = new LevelData()
           .Self(self =>
            {
                self.Pacing = new List<int>() { 2, 1, 3, 3, 2, 1, 2, 3, 3, 2, 3, 1, 3 };
            })
           .Self(self =>
            {
                self.RoomTree.Root
                   .AddChild(RoomType.Chest)
                   .AddChild(RoomType.Shop)
                   .AddChild(RoomType.Final)
                   .AddChild(RoomType.Chest)
                   .AddChild(RoomType.Chest)
                   .AddChild(RoomType.Chest)
                   .AddChild(RoomType.Chest)
                   .AddChild(RoomType.Chest)
                   .AddChild(RoomType.Chest)
                   .AddChild(RoomType.Chest)
                   .AddChild(RoomType.Chest);
                
                return;
                
                var randomIndex = new[] { 0, 1, 2 }.RandomTakeOne();

                switch (randomIndex)
                {
                    case 0:
                        self.RoomTree.Root
                           .AddChild(RoomType.Normal)
                           .AddChild(RoomType.Shop, child =>
                            {
                                child.AddChild(RoomType.Normal)
                                   .AddChild(RoomType.Normal)
                                   .AddChild(RoomType.Chest);
                            })
                           .AddChild(RoomType.Normal)
                           .AddChild(RoomType.Normal)
                           .AddChild(RoomType.Final);
                        break;
                    case 1:
                        self.RoomTree.Root
                           .AddChild(RoomType.Normal)
                           .AddChild(RoomType.Chest, child =>
                            {
                                child.AddChild(RoomType.Normal)
                                   .AddChild(RoomType.Normal)
                                   .AddChild(RoomType.Shop);
                            })
                           .AddChild(RoomType.Normal)
                           .AddChild(RoomType.Normal)
                           .AddChild(RoomType.Final);
                        break;
                    case 2:
                        self.RoomTree.Root
                           .AddChild(RoomType.Normal)
                           .AddChild(RoomType.Normal, child =>
                            {
                                child.AddChild(RoomType.Chest)
                                   .AddChild(RoomType.Normal)
                                   .AddChild(RoomType.Normal)
                                   .AddChild(RoomType.Shop);
                            })
                           .AddChild(RoomType.Normal)
                           .AddChild(RoomType.Normal)
                           .AddChild(RoomType.Final);
                        break;
                }
            });
    }
}