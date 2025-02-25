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
    using Framework.Toolkits.FSMKit;
    using UnityEngine;

    public enum DoorState
    {
        Open,
        Close
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
                    Collider.isTrigger    = true;
                    SpriteRenderer.sprite = OpenSprite;
                });
            State.State(DoorState.Close)
               .OnEnter(() =>
                {
                    Collider.isTrigger    = false;
                    SpriteRenderer.sprite = CloseSprite;
                })
               .OnExit(() =>
                {
                    AudioKit.PlaySound(Config.Sound.DOOR_OPEN);
                });
            
            State.StartState(DoorState.Open);
        }

        protected override IArchitecture _Architecture { get => Game.Interface; }
    }
}