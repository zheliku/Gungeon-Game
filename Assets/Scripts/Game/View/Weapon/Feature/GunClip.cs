// ------------------------------------------------------------
// @file       GunClip.cs
// @brief
// @author     zheliku
// @Modified   2025-02-21 04:02:22
// @Copyright  Copyright (c) 2025, zheliku
// ------------------------------------------------------------

using Sirenix.OdinInspector;

namespace Game
{
    using System;
    using Framework.Core;
    using Framework.Toolkits.ActionKit;
    using Framework.Toolkits.AudioKit;
    using UnityEngine;

    /// <summary>
    /// 枪械弹夹
    /// </summary>
    public class GunClip
    {
        public Gun Gun { get; } // 属于哪个枪

        [ShowInInspector]
        public int RemainBulletCount // 当前子弹数量
        {
            get => Gun.Data.ClipRemainBulletCount;
        }

        [ShowInInspector]
        public int MaxBulletCount
        {
            get => Gun.Data.Config.ClipMaxBulletCount;
        }

        public bool IsReloading { get; private set; }

        public int UsedBulletCount
        {
            get => MaxBulletCount - RemainBulletCount;
        }

        public bool IsFull
        {
            get => RemainBulletCount == MaxBulletCount;
        }

        public bool IsEmpty
        {
            get => RemainBulletCount == 0;
        }

        public GunClip(Gun gun)
        {
            Gun = gun;
        }

        /// <summary>
        /// 使用子弹
        /// </summary>
        public void Use()
        {
            Gun.Data.UseClipBullet(1);

            if (RemainBulletCount == 0)
            {
                Player.DisplayText("子弹用完了", 2f);

                TypeEventSystem.GLOBAL.Send(new GunBulletEmptyEvent(Gun));
            }
        }

        /// <summary>
        /// 重装子弹
        /// </summary>
        /// <param name="reloadSound">音效</param>
        /// <param name="reloadBulletCount">需要重装多少子弹</param>
        public void Reload(AudioClip reloadSound, int reloadBulletCount)
        {
            if (IsReloading)
            {
                return;
            }

            // 重装子弹前首先抬枪
            Gun.ShootUp();
            
            if (reloadBulletCount == 0)
            {
                return;
            }
            
            IsReloading = true;

            var targetCount = RemainBulletCount + reloadBulletCount;

            // 子弹填充动画
            ActionKit.Lerp(RemainBulletCount, targetCount, reloadSound.length, f =>
            {
                var loadBulletCountThisFrame = (int) f - RemainBulletCount;
                Gun.Data.AddClipBullet(loadBulletCountThisFrame);
                Gun.Data.UseBagBullet(loadBulletCountThisFrame);
                TypeEventSystem.GLOBAL.Send(new GunBulletLoadingEvent(Gun));
            }).Start(Gun);

            AudioKit.PlaySound(reloadSound, onPlayFinish: (player) =>
            {
                IsReloading = false;
                TypeEventSystem.GLOBAL.Send(new GunBulletLoadedEvent(Gun)); // 重装完毕，发送事件
            });
        }
    }
}