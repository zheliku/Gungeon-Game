// ------------------------------------------------------------
// @file       Door.cs
// @brief
// @author     zheliku
// @Modified   2025-02-23 00:02:52
// @Copyright  Copyright (c) 2025, zheliku
// ------------------------------------------------------------

namespace Game
{
    using System;
    using Framework.Core;
    using Framework.Toolkits.AudioKit;
    using Framework.Toolkits.FluentAPI;
    using Framework.Toolkits.FSMKit;
    using UnityEngine;

    public enum DoorState
    {
        Open,
        IdleClose,   // 房间默认关闭
        BattleClose, // 战斗中关闭
    }

    public class Door : AbstractView
    {
        public Sprite OpenSprite;
        public Sprite CloseSprite;

        [HierarchyPath]
        public SpriteRenderer SpriteRenderer;

        [HierarchyPath]
        public Collider2D Collider;

        public FSM<DoorState> State { get; private set; } = new FSM<DoorState>();

        private void Awake()
        {
            this.BindHierarchyComponent();

            State.State(DoorState.Open)
               .OnEnter(() =>
                {
                    Collider.Disable();
                    SpriteRenderer.sprite = OpenSprite;
                    AudioKit.PlaySound(Config.Sound.DOOR_OPEN);
                });
            State.State(DoorState.IdleClose)
               .OnEnter(() =>
                {
                    Collider.isTrigger    = true;
                    SpriteRenderer.sprite = CloseSprite;
                });
            State.State(DoorState.BattleClose)
               .OnEnter(() =>
                {
                    Collider.isTrigger    = false;
                    Collider.Enable();
                    SpriteRenderer.sprite = CloseSprite;
                });

            State.StartState(DoorState.IdleClose);
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag("Player") && State.CurrentStateId == DoorState.IdleClose)
            {
                State.ChangeState(DoorState.Open);
            }
        }

        protected override IArchitecture _Architecture { get => Game.Interface; }
    }
}