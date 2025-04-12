// ------------------------------------------------------------
// @file       BulletFactory.cs
// @brief
// @author     zheliku
// @Modified   2025-03-12 22:28:54
// @Copyright  Copyright (c) 2025, zheliku
// ------------------------------------------------------------

namespace Game
{
    using System.Collections.Generic;
    using Framework.Core;
    using Framework.Toolkits.ActionKit;
    using Framework.Toolkits.AudioKit;
    using Framework.Toolkits.FluentAPI;
    using Framework.Toolkits.SingletonKit;
    using UnityEngine;

    public class BulletFactory : MonoSingleton<BulletFactory>
    {
        public List<AudioClip> HitWallSounds  = new List<AudioClip>();
        public List<AudioClip> HitEnemySounds = new List<AudioClip>();

        [HierarchyPath("GunBullet")]
        public PlayerBullet GunBullet;

        [HierarchyPath("RocketBullet")]
        public PlayerBullet RocketBullet;

        [HierarchyPath("BowArrow")]
        public PlayerBullet BowArrow;

        [HierarchyPath("PistolShell")]
        public Rigidbody2D PistolShell;

        [HierarchyPath("AWPShell")]
        public Rigidbody2D AWPShell;

        [HierarchyPath("AKShell")]
        public Rigidbody2D AKShell;

        [HierarchyPath("ShotGunShell")]
        public Rigidbody2D ShotGunShell;

        [HierarchyPath("RocketShell")]
        public Rigidbody2D RocketShell;

        private void Awake()
        {
            this.BindHierarchyComponent();
        }

        public static void GenBulletShell(Vector2 direction, Rigidbody2D shellPrefab)
        {
            // 子弹壳动画
            shellPrefab.Instantiate()
               .SetPosition(Player.Instance.GetPosition() + (Vector3) direction * 0.5f)
               .EnableGameObject()
               .Self(self =>
                {
                    self.linearVelocity = -direction * (2f, 4f).RandomSelect()
                                        + Vector2.up * (3f, 5f).RandomSelect();
                    self.angularVelocity = (-720f, 720f).RandomSelect();
                    self.gravityScale    = 1f;

                    ActionKit.Sequence()
                       .Delay((0.5f, 1f).RandomSelect(), () =>
                        {
                            self.linearVelocity = -direction * (0.5f, 2f).RandomSelect()
                                                + Vector2.up * (0.5f, 1f).RandomSelect();
                            self.angularVelocity = RandomUtility.Range((-720, -180), (180, 720));
                            self.gravityScale    = 0.1f;
                        })
                       .Callback(() =>
                        {
                            AudioKit.PlaySound(AssetConfig.Sound.BulletSound, 0.6f);
                        })
                       .Delay((0.3f, 0.5f).RandomSelect(), () =>
                        {
                            self.linearVelocity  = Vector2.zero;
                            self.angularVelocity = 0;
                            self.gravityScale    = 0f;
                        }).Start(Instance);
                });
        }
    }
}