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
    using Framework.Toolkits.EventKit;
    using Framework.Toolkits.FluentAPI;
    using Framework.Toolkits.InputKit;
    using Framework.Toolkits.SingletonKit;
    using Framework.Toolkits.UIKit;
    using UnityEngine;
    using UnityEngine.InputSystem;

    public class Player : Role, ISingleton
    {
        public GameObject Bullet;

        private InputAction _moveAction;

        private PlayerModel _playerModel;

        private Rigidbody2D _rigidbody2D;

        private Property _Property { get => _playerModel.Property; }

        public static Player Instance { get; set; }

        protected override void Awake()
        {
            base.Awake();
            
            _playerModel = this.GetModel<PlayerModel>();

            Bullet         = "Bullet".GetGameObjectInHierarchy(transform);
            SpriteRenderer = "Sprite".GetComponentInHierarchy<SpriteRenderer>(transform);

            _moveAction  = InputKit.GetInputAction("Move");
            _rigidbody2D = GetComponent<Rigidbody2D>();

            _Property.Hp.Register((oldValue, value) =>
            {
                if (value <= 0)
                {
                    UIKit.ShowPanelAsync<GameOver>();
                    this.DisableGameObject();
                }
            }).UnRegisterWhenGameObjectDestroyed(gameObject);

            UIKit.ShowPanelAsync<GamePlay>();

            Bullet.Disable();
        }

        // Start is called once before the first execution of Update after the MonoBehaviour is created
        void Start()
        {
            InputKit.BindPerformed("Attack", context =>
            {
                Fire();
            }).UnBindAllPerformedWhenGameObjectDestroyed(gameObject);
        }

        // Update is called once per frame
        void Update()
        {
            var moveDirection = _moveAction.ReadValue<Vector2>();
            _rigidbody2D.linearVelocity = moveDirection * _Property.MoveSpeed;

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

        public void Fire()
        {
            var bullet = Bullet.Instantiate(transform.position)
               .Enable();

            var mouse          = Mouse.current.position;
            var mousePosition  = Camera.main.ScreenToWorldPoint(mouse.ReadValue());
            var shootDirection = (mousePosition - transform.position).ToVector2().normalized;

            bullet.OnUpdateEvent(() =>
            {
                bullet.transform.Translate(shootDirection * (Time.deltaTime * 5));
            });

            bullet.OnCollisionEnter2DEvent(collider2D =>
            {
                if (collider2D.gameObject.GetComponent<Enemy>())
                {
                    collider2D.gameObject.Destroy();
                }
                
                bullet.Destroy();
            });
        }

        protected override IArchitecture Architecture { get => Game.Interface; }

        public void OnSingletonInit() { }
    }
}