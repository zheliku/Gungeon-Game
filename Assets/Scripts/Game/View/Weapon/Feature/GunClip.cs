// ------------------------------------------------------------
// @file       GunClip.cs
// @brief
// @author     zheliku
// @Modified   2025-02-21 04:02:22
// @Copyright  Copyright (c) 2025, zheliku
// ------------------------------------------------------------

namespace Game
{
    using Framework.Core;
    using Framework.Toolkits.ActionKit;
    using Framework.Toolkits.AudioKit;
    using UnityEngine;

    /// <summary>
    /// 枪械弹夹
    /// </summary>
    public class GunClip
    {
        public int ClipBulletCount; // 弹夹容量

        public int CurrentBulletCount; // 当前子弹数量

        public Gun Gun { get; } // 属于哪个枪

        public bool IsReloading { get; private set; }

        public int UsedBulletCount
        {
            get => ClipBulletCount - CurrentBulletCount;
        }

        public bool IsFull
        {
            get => CurrentBulletCount == ClipBulletCount;
        }

        public bool IsEmpty
        {
            get => CurrentBulletCount == 0;
        }

        public GunClip(Gun gun, int clipBulletCount)
        {
            Gun                = gun;
            ClipBulletCount    = clipBulletCount;
            CurrentBulletCount = clipBulletCount;
        }

        /// <summary>
        /// 使用子弹
        /// </summary>
        public void Use()
        {
            CurrentBulletCount--;
        }

        public void Reload(AudioClip reloadSound, int reloadBulletCount)
        {
            // 重装子弹前首先抬枪
            Gun.ShootUp(Gun.ShootDirection);

            IsReloading = true;

            var targetCount = CurrentBulletCount + reloadBulletCount;

            // 子弹填充动画
            ActionKit.Lerp(CurrentBulletCount, targetCount, reloadSound.length, f =>
            {
                CurrentBulletCount = (int) f;
                TypeEventSystem.GLOBAL.Send(new GunLoadBulletEvent(Gun));
            }).StartCurrentScene();

            AudioKit.PlaySound(reloadSound, onPlayFinish: (player) =>
            {
                IsReloading        =  false;
                CurrentBulletCount = targetCount;
                TypeEventSystem.GLOBAL.Send(new GunLoadBulletEvent(Gun));
            });
        }
    }
}