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

    public class Player : AbstractRole, ISingleton
    {
        public Transform Weapon;

        public Pistol Pistol;

        private InputAction _moveAction;

        private PlayerModel _playerModel;
        
        private Property _Property { get => _playerModel.Property; }

        private Vector3 _MousePosition { get => Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue()).Set(z: 0); }

        public static Player Instance { get; set; }

        protected override void Awake()
        {
            base.Awake();

            _playerModel = this.GetModel<PlayerModel>();

            Weapon         = "Weapon".GetComponentInHierarchy<Transform>(transform);
            Pistol         = "Weapon/Pistol".GetComponentInHierarchy<Pistol>(transform);

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
            InputKit.BindPerformed("Attack", context =>
            {
                var shootDirection = (_MousePosition - transform.position).normalized;

                Pistol.ShootDown(shootDirection);
            }).UnBindAllPerformedWhenGameObjectDestroyed(gameObject);
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

            var shootDirection = (_MousePosition - transform.position).ToVector2().normalized;
            var angle          = Mathf.Atan2(shootDirection.y, shootDirection.x).Rad2Deg();
            Weapon.SetLocalEulerAngles(z: angle);             // 使 Weapon 方向跟手
            Weapon.SetLocalScale(y: shootDirection.x.Sign()); // 使 Weapon 随鼠标左右翻转
        }

        public override void Hurt(float damage)
        {
            _Property.Hp.Value -= damage;
        }

        protected override IArchitecture Architecture { get => Game.Interface; }

        public void OnSingletonInit() { }
    }
}