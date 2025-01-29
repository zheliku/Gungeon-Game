// ------------------------------------------------------------
// @file       Player.cs
// @brief
// @author     zheliku
// @Modified   2025-01-29 16:01:52
// @Copyright  Copyright (c) 2025, zheliku
// ------------------------------------------------------------

namespace Game.View
{
    using Framework.Core;
    using Framework.Core.View;
    using Framework.Toolkits.EventKit;
    using Framework.Toolkits.FluentAPI;
    using Framework.Toolkits.InputKit;
    using Framework.Toolkits.UIKit;
    using UnityEngine;
    using UnityEngine.InputSystem;

    public class Player : AbstractView
    {
        public float MoveSpeed = 5;

        public Bullet Bullet;

        private InputAction _moveAction;

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
                    if (collider2D.gameObject.name == "Enemy")
                    {
                        UIKit.ShowPanelAsync<GamePass>();
                        collider2D.gameObject.Disable();
                    }
                });
            }).UnBindAllPerformedWhenGameObjectDestroyed(gameObject);
        }

        // Update is called once per frame
        void Update()
        {
            _moveAction = InputKit.GetInputAction("Move");
            transform.Translate(_moveAction.ReadValue<Vector2>() * (MoveSpeed * Time.deltaTime));
        }

        protected override IArchitecture Architecture { get => Game.Interface; }
    }
}