// ------------------------------------------------------------
// @file       Enemy.cs
// @brief
// @author     zheliku
// @Modified   2025-01-29 16:01:14
// @Copyright  Copyright (c) 2025, zheliku
// ------------------------------------------------------------

namespace Game
{
    using Framework.Core;
    using Framework.Toolkits.EventKit;
    using Framework.Toolkits.FluentAPI;
    using UnityEngine;
    using Random = UnityEngine.Random;

    public class Enemy : Role
    {
        public enum States
        {
            Follow,
            Shoot
        }

        public GameObject Bullet;

        public States State;

        public float FollowSeconds = 3;

        public float CurrentSeconds = 0;

        private EnemyModel _enemyModel;

        private Property _Property { get => _enemyModel.Property; }

        protected override void Awake()
        {
            base.Awake();

            Bullet      = "Bullet".GetGameObjectInHierarchy(transform);
            _enemyModel = this.GetModel<EnemyModel>();
        }

        public override void Hurt(float damage)
        { }

        private void Update()
        {
            var player = Player.Instance;

            if (State == States.Follow)
            {
                if (CurrentSeconds >= FollowSeconds)
                {
                    State          = States.Shoot;
                    CurrentSeconds = 0;
                }

                if (player)
                {
                    transform.Translate(player.Direction2DFrom(transform) * (Time.deltaTime * _Property.MoveSpeed));
                }

                CurrentSeconds += Time.deltaTime;
            }
            else if (State == States.Shoot)
            {
                CurrentSeconds += Time.deltaTime;

                if (CurrentSeconds >= 1)
                {
                    State          = States.Follow;
                    FollowSeconds  = Random.Range(2f, 4f);
                    CurrentSeconds = 0;
                }

                if (Time.frameCount % 20 == 0 && player)
                {
                    var bullet = Bullet.Instantiate(transform.position)
                       .Enable();
                    var direction = player.Direction2DFrom(bullet);

                    bullet.OnUpdateEvent(() =>
                    {
                        bullet.transform.Translate(direction * (Time.deltaTime * 5));
                    });

                    bullet.OnCollisionEnter2DEvent(collider2D =>
                    {
                        var player = collider2D.gameObject.GetComponent<Player>();
                        if (player)
                        {
                            player.Hurt(1);
                            bullet.Destroy();
                        }
                    });
                }
            }
        }

        protected override IArchitecture Architecture { get => Game.Interface; }
    }
}