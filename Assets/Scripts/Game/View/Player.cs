// ------------------------------------------------------------
// @file       Player.cs
// @brief
// @author     zheliku
// @Modified   2025-01-29 16:01:52
// @Copyright  Copyright (c) 2025, zheliku
// ------------------------------------------------------------

namespace Game
{
    using System;
    using Framework.Core;
    using Framework.Core.View;
    using Framework.Toolkits.EventKit;
    using Framework.Toolkits.FluentAPI;
    using Framework.Toolkits.InputKit;
    using Framework.Toolkits.SingletonKit;
    using Framework.Toolkits.UIKit;
    using UnityEngine;
    using UnityEngine.InputSystem;

    public class Player : AbstractView, ISingleton
    {
        public Bullet Bullet;

        private InputAction _moveAction;

        private PlayerModel _playerModel;

        private Property _Property { get => _playerModel.Property; }

        public static Player Instance { get; set; }

        private void Awake()
        {
            _playerModel = this.GetModel<PlayerModel>();
        }

        // Start is called once before the first execution of Update after the MonoBehaviour is created
        void Start()
        {
            InputKit.BindPerformed("Attack", context =>
            {
                var bullet = Bullet.Instantiate(transform.position)
                   .EnableGameObject();
                bullet.Direction = Vector2.right;

                bullet.OnCollisionEnter2DEvent(collider2D =>
                {
                    if (collider2D.gameObject.GetComponent<Enemy>())
                    {
                        collider2D.gameObject.Disable();
                    }
                });
            }).UnBindAllPerformedWhenGameObjectDestroyed(gameObject);
        }

        // Update is called once per frame
        void Update()
        {
            _moveAction = InputKit.GetInputAction("Move");
            transform.Translate(_moveAction.ReadValue<Vector2>() * (_Property.MoveSpeed * Time.deltaTime));
        }

        protected override IArchitecture Architecture { get => Game.Interface; }

        public void OnSingletonInit() { }
    }
}