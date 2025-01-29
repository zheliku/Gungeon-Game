// ------------------------------------------------------------
// @file       Enemy.cs
// @brief
// @author     zheliku
// @Modified   2025-01-29 16:01:14
// @Copyright  Copyright (c) 2025, zheliku
// ------------------------------------------------------------

namespace Game.View
{
    using Framework.Core;
    using Framework.Core.View;
    using Framework.Toolkits.EventKit;
    using Framework.Toolkits.FluentAPI;
    using Framework.Toolkits.UIKit;
    using UnityEngine;

    public class Enemy : AbstractView
    {
        public Player Player;

        public float MoveSpeed = 3;

        public Bullet Bullet;

        public enum States
        {
            Follow,
            Shoot
        }

        public States State;

        public float FollowSeconds = 3;

        public float CurrentSeconds = 0;

        private void Update()
        {
            if (State == States.Follow)
            {
                if (CurrentSeconds >= FollowSeconds)
                {
                    State          = States.Shoot;
                    CurrentSeconds = 0;
                }

                transform.Translate(Player.Direction2DFrom(transform) * (Time.deltaTime * MoveSpeed));

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

                if (Time.frameCount % 20 == 0)
                {
                    var bullet = Bullet.Instantiate(transform.position)
                       .EnableGameObject();
                    bullet.Direction = Player.Direction2DFrom(bullet);
                    
                    bullet.OnCollisionEnter2DEvent(collider2D =>
                    {
                        if (collider2D.gameObject.name == "Player")
                        {
                            UIKit.ShowPanelAsync<GameOver>();
                            collider2D.gameObject.Disable();
                        }
                    });
                }
            }
        }

        protected override IArchitecture Architecture { get => Game.Interface; }
    }
}