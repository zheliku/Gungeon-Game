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

        protected override IArchitecture _Architecture { get => Game.Interface; }
    }
}