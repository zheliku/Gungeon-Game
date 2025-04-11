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

    public class Level3
    {
        public static readonly LevelData DATA = new LevelData()
           .Self(self =>
            {
                self.Pacing = new List<int>() { 4, 2, 6, 5, 3, 2, 4, 5, 4, 2, 6, 5, 3, 2, 4, 5, 6, 5, 3, 2, 4, 5, 6, 5, 3, 2, 4, 5, };
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