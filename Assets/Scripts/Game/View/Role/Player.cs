// ------------------------------------------------------------
// @file       Player.cs
// @brief
// @author     zheliku
// @Modified   2025-01-29 16:01:52
// @Copyright  Copyright (c) 2025, zheliku
// ------------------------------------------------------------

namespace Game
{
    using Framework.Core;
    using Framework.Toolkits.FluentAPI;
    using Framework.Toolkits.InputKit;
    using Framework.Toolkits.SingletonKit;
    using Framework.Toolkits.UIKit;
    using UnityEngine;
    using UnityEngine.InputSystem;
    using UnityEngine.Serialization;

    public class Player : AbstractRole, ISingleton
    {
        [HierarchyPath("Weapon")]
        public Transform WeaponTransform;

        [HierarchyPath("Weapon/MP5")]
        public Gun CurrentGun;

        private InputAction _moveAction;
        
        private PlayerModel _playerModel;
        
        private Property _Property { get => _playerModel.Property; }
        
        public static Player Instance { get; set; }

        protected override void Awake()
        {
            base.Awake();

            _playerModel = this.GetModel<PlayerModel>();

            _moveAction  = InputKit.GetInputAction("Move");

            _Property.Hp.Register((oldValue, value) =>
            {
                if (value <= 0)
                {
                    UIKit.ShowPanelAsync<GameOver>();
                    this.DisableGameObject();
                }
            }).UnRegisterWhenGameObjectDestroyed(gameObject);

            UIKit.ShowPanelAsync<GamePlay>();
        }

        // Start is called once before the first execution of Update after the MonoBehaviour is created
        void Start()
        {
            
        }

        // Update is called once per frame
        void Update()
        {
            var moveDirection = _moveAction.ReadValue<Vector2>();
            Rigidbody2D.linearVelocity = moveDirection * _Property.MoveSpeed;

            if (moveDirection.x < 0)
            {
                SpriteRenderer.flipX = true;
            }
            else if (moveDirection.x > 0)
            {
                SpriteRenderer.flipX = false;
            }
        }

        public override void Hurt(float damage)
        {
            _Property.Hp.Value -= damage;
        }

        protected override IArchitecture _Architecture { get => Game.Interface; }

        public void OnSingletonInit() { }
    }
}