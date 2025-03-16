// ------------------------------------------------------------
// @file       MapItem.cs
// @brief
// @author     zheliku
// @Modified   2025-03-14 22:09:15
// @Copyright  Copyright (c) 2025, zheliku
// ------------------------------------------------------------

namespace Game
{
    using System;
    using Framework.Core;
    using Framework.Toolkits.FluentAPI;
    using Framework.Toolkits.UIKit;
    using TMPro;
    using UnityEngine;
    using UnityEngine.UI;

    public class MapItem : AbstractView
    {
        [HierarchyPath("txtType")]
        public TextMeshProUGUI TxtType;

        [HierarchyPath("btnGo")]
        public Button BtnGo;

        [HierarchyPath("imgLeftDoor")]
        public Image LeftDoor;

        [HierarchyPath("imgRightDoor")]
        public Image RightDoor;

        [HierarchyPath("imgUpDoor")]
        public Image UpDoor;

        [HierarchyPath("imgDownDoor")]
        public Image DownDoor;

        [HierarchyPath("IconGroup")]
        public Transform IconGroup;

        [HierarchyPath("imgIcon")]
        public Image Icon;

        public Room Room;

        private void Awake()
        {
            this.BindHierarchyComponent();

            BtnGo.onClick.AddListener(() =>
            {
                UIKit.HidePanel<UIMap>();
                Player.Instance.SetPosition(Room.GetPosition());
            });

            Init();
        }

        public MapItem SetData(Room room)
        {
            Room = room;

            return this;
        }

        public MapItem Init()
        {
            foreach (var closedDirection in Room.Node.ClosedDirections)
            {
                switch (closedDirection)
                {
                    case Direction.Left:
                        LeftDoor.DisableGameObject();
                        break;
                    case Direction.Up:
                        UpDoor.DisableGameObject();
                        break;
                    case Direction.Right:
                        RightDoor.DisableGameObject();
                        break;
                    case Direction.Down:
                        DownDoor.DisableGameObject();
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }

            IconGroup.DisableGameObject();
            Icon.DisableGameObject();

            switch (Room.RoomType)
            {
                case RoomType.Init:
                    TxtType.text = "起始";
                    break;
                case RoomType.Normal:
                    TxtType.DisableGameObject();
                    break;
                case RoomType.Final:
                    TxtType.text = "终点";
                    break;
                case RoomType.Chest:
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

            foreach (var powerUp in Room.PowerUps)
            {
                Icon.Instantiate(IconGroup)
                   .Self(self =>
                    {
                        self.sprite = powerUp.SpriteRenderer.sprite;
                    })
                   .EnableGameObject();
                IconGroup.EnableGameObject();
            }

            if (this.GetModel<LevelModel>().CurrentRoom == Room)
            {
                TxtType.text = "我";
                TxtType.EnableGameObject();
            }

            return this;
        }

        protected override IArchitecture _Architecture { get => Game.Interface; }
    }
}