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

    public class Level4
    {
        public static readonly LevelData DATA = new LevelData()
           .Self(self =>
            {
                self.LevelId             = 4;
                self.Pacing              = new List<int>() { 3, 5, 6, 6, 4, 3, 5, 6, 7, 5, 3, 5, 7, 6, 4, 3, 5, 6, 4, 6, 7, 5, 3, 5, 7, 6, 4, 3 };
                self.NormalRoomTemplates = Lv4RoomConfig.NORMAL_ROOMS;
            })
           .Self(self =>
            {
                self.RoomTree.Root
                   .Self(node =>
                    {
                        node.AddChild(RoomType.Normal)
                           .AddChild(RoomType.Normal)
                           .AddChild(RoomType.Normal)
                           .AddChild(RoomType.Chest)
                           .AddChild(RoomType.Final);
                    })
                   .AddChild(RoomType.Normal)
                   .AddChild(RoomType.Normal)
                   .AddChild(RoomType.Normal)
                   .AddChild(RoomType.Shop, child =>
                    {
                        child.AddChild(RoomType.Normal)
                           .AddChild(RoomType.Normal)
                           .AddChild(RoomType.Normal)
                           .AddChild(RoomType.Normal)
                           .AddChild(RoomType.Chest);
                    })
                   .AddChild(RoomType.Normal)
                   .AddChild(RoomType.Normal)
                   .AddChild(RoomType.Normal, child =>
                    {
                        child.AddChild(RoomType.Normal)
                           .AddChild(RoomType.Normal)
                           .AddChild(RoomType.Normal)
                           .AddChild(RoomType.Normal)
                           .AddChild(RoomType.Chest);
                    })
                   .AddChild(RoomType.Normal)
                   .AddChild(RoomType.Normal)
                   .AddChild(RoomType.Normal);
            });
    }
}