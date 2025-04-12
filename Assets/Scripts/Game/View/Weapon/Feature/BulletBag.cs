// ------------------------------------------------------------
// @file       BulletBag.cs
// @brief
// @author     zheliku
// @Modified   2025-02-21 06:02:51
// @Copyright  Copyright (c) 2025, zheliku
// ------------------------------------------------------------

namespace Game
{
    using System;
    using Framework.Toolkits.FluentAPI;
    using UnityEngine;

    public class BulletBag
    {
        public Gun Gun { get; }

        public int MaxBulletCount
        {
            get => Gun.Data.Config.BagMaxBulletCount;
        }

        public int RemainBulletCount // 剩余子弹数量
        {
            get => Gun.Data.BagRemainBulletCount;
        }

        public bool IsFull
        {
            get => RemainBulletCount == MaxBulletCount;
        }

        public bool IsEmpty
        {
            get => RemainBulletCount == 0;
        }

        public BulletBag(Gun gun)
        {
            Gun = gun;
        }

        public void Reload(GunClip clip, AudioClip reloadSound)
        {
            if (clip.IsFull)
            {
                return; // 弹夹已满，不重装
            }

            if (MaxBulletCount < 0) // 无限子弹
            {
                clip.Reload(reloadSound, clip.UsedBulletCount);
            }
            else // 有限子弹
            {
                var reloadCount = RemainBulletCount.MinWith(clip.UsedBulletCount);
                clip.Reload(reloadSound, reloadCount);
            }
        }
    }
}