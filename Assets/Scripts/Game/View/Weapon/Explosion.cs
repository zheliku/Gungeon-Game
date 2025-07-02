// ------------------------------------------------------------
// @file       Explosion.cs
// @brief
// @author     zheliku
// @Modified   2025-07-03 00:57:19
// @Copyright  Copyright (c) 2025, zheliku
// ------------------------------------------------------------

using System;
using System.Collections.Generic;
using Framework.Toolkits.ActionKit;
using Framework.Toolkits.FluentAPI;
using UnityEngine;

namespace Game
{
    using Framework.Core;

    public class Explosion : AbstractView
    {
        [HierarchyPath]
        public Collider2D Collider2D;

        public List<Sprite> Frames;

        public string HurtTag = "Enemy";

        public float Damage = 10;

        private void Start()
        {
            this.SetLocalScale(2, 2, 2);
            CameraController.SHAKE.Trigger(0.3f, 30);

            ActionKit.Custom(a =>
            {
                var frameIndex = 0;
                var updateFrameCount = 0;
                var spriterenderer = this.GetComponent<SpriteRenderer>();
                a.OnStart(() =>
                {
                    frameIndex            = 0;
                    spriterenderer.sprite = Frames[frameIndex];
                }).OnExecute(dt =>
                {
                    if (updateFrameCount >= 3)
                    {
                        updateFrameCount = 0;
                        frameIndex++;
                        if (frameIndex >= Frames.Count)
                        {
                            a.Finish();
                        }
                        else
                        {
                            spriterenderer.sprite = Frames[frameIndex];
                        }

                        if (frameIndex == 4)
                        {
                            var filter2D = new ContactFilter2D();
                            var collider2Ds = new Collider2D[10];

                            Collider2D.EnableGameObject();
                            var count = Collider2D.Overlap(filter2D, collider2Ds);

                            if (count >= 0)
                            {
                                foreach (var collider2D in collider2Ds)
                                {
                                    if (HurtTag == "Enemy")
                                    {
                                        if (collider2D && collider2D.attachedRigidbody &&
                                            collider2D.CompareTag("Enemy"))
                                        {
                                            var enemy = collider2D.attachedRigidbody.GetComponent<IEnemy>();
                                            enemy?.Hurt(Damage, new HitInfo()
                                            {
                                                HitPoint  = this.GetPosition(),
                                                HitNormal = enemy.Position - this.GetPosition()
                                            });
                                        }
                                    }
                                }
                            }
                        }
                        else
                        {
                            updateFrameCount++;
                        }
                    }
                    else
                    {
                        updateFrameCount++;
                    }
                });
            }).Start(this, this.DestroyGameObject);
        }

        protected override IArchitecture _Architecture { get => Game.Architecture; }
    }
}