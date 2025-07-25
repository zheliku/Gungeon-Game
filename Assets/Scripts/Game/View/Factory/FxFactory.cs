// ------------------------------------------------------------
// @file       FxFactory.cs
// @brief
// @author     zheliku
// @Modified   2025-03-13 17:27:08
// @Copyright  Copyright (c) 2025, zheliku
// ------------------------------------------------------------

namespace Game
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Framework.Core;
    using Framework.Toolkits.ActionKit;
    using Framework.Toolkits.SingletonKit;
    using Framework.Toolkits.FluentAPI;
    using UnityEngine;

    public class FxFactory : MonoSingleton<FxFactory>
    {
        [HierarchyPath("HurtFx")]
        public ParticleSystem HurtFx;

        [HierarchyPath("EnemyBlood")]
        public SpriteRenderer EnemyBlood;

        [HierarchyPath("PlayerBlood")]
        public SpriteRenderer PlayerBlood;

        [HierarchyPath("Explosion")]
        public Explosion Explosion;

        public List<SpriteRenderer> DieBodies = new List<SpriteRenderer>();

        private void Awake()
        {
            this.BindHierarchyComponent();
        }

        public static void PlayHurtFx(Vector3 position, Color color)
        {
            Instance.HurtFx
               .Instantiate(position)
               .EnableGameObject()
               .Self(self =>
                {
                    var main = self.main;
                    main.startColor = color;
                    ActionKit.Delay(main.duration, self.DestroyGameObjectGracefully)
                       .Start(Instance);
                });
        }

        public static void PlayEnemyBlood(Vector3 position)
        {
            var blood = Instance.EnemyBlood
               .Instantiate(position)
               .SetEulerAngles(z: (0f, 360f).RandomSelect())
               .SetLocalScale(0.1f * Vector3.one)
               .EnableGameObject();

            var originPos    = position;
            var randomAngle  = (0f, 360f).RandomSelect();
            var randomRadius = (0.2f, 1.5f).RandomSelect();
            var moveVec      = randomAngle.Deg2Direction2D() * randomRadius;
            var randomScale  = (0.2f, 3f).RandomSelect();

            ActionKit.Lerp01((0.1f, 0.3f).RandomSelect(), f =>
            {
                f = EasyTween.InCubic(0, 1, f);
                blood.SetPosition(originPos + (Vector3) moveVec * f);
                blood.SetLocalScale(Vector3.one * (randomScale * f));
            }).Start(Instance);
        }

        public static void PlayPlayerBlood(Vector3 position)
        {
            var blood = Instance.PlayerBlood
               .Instantiate(position)
               .SetEulerAngles(z: (0f, 360f).RandomSelect())
               .SetLocalScale(0.1f * Vector3.one)
               .EnableGameObject();

            var originPos    = position;
            var randomAngle  = (0f, 360f).RandomSelect();
            var randomRadius = (0.2f, 1.5f).RandomSelect();
            var moveVec      = randomAngle.Deg2Direction2D() * randomRadius;
            var randomScale  = (0.2f, 3f).RandomSelect();

            ActionKit.Lerp01((0.1f, 0.3f).RandomSelect(), f =>
            {
                f = EasyTween.InCubic(0, 1, f);
                blood.SetPosition(originPos + (Vector3) moveVec * f);
                blood.SetLocalScale(Vector3.one * (randomScale * f));
            }).Start(Instance);
        }

        public static SpriteRenderer PlayDieBody(Vector3 position, string dieBodyName, HitInfo hitInfo, float scale)
        {
            var dieSprite = Instance.DieBodies.FirstOrDefault(b => b.name == dieBodyName);
            if (dieSprite == null)
            {
                return null;
            }

            var dieBody = dieSprite.Instantiate(position)
               .SetLocalScale(Vector3.one * scale)
               .SetLocalEulerAngles(z: (-45f, 45f).RandomSelect())
               .Self(self =>
                {
                    self.flipX = new[] { true, false }.RandomTakeOne();
                    self.flipY = new[] { true, false }.RandomTakeOne();
                })
               .EnableGameObject();

            var originPos      = position;
            var moveToDistance = (0.5f, 1.5f).RandomSelect();

            ActionKit.Lerp01(0.3f, f =>
            {
                var targetPos = originPos.Lerp(originPos - (Vector3) (moveToDistance * hitInfo.HitNormal), f);
                dieBody.SetPosition(targetPos);
            }).Start(Instance);

            return dieBody;
        }

        protected override IArchitecture _Architecture { get => Game.Architecture; }
    }
}