// ------------------------------------------------------------
// @file       GunData.cs
// @brief
// @author     zheliku
// @Modified   2025-04-12 15:04:20
// @Copyright  Copyright (c) 2025, zheliku
// ------------------------------------------------------------

using Framework.Toolkits.BindableKit;

namespace Game
{
    using System.Collections.Generic;
    using Framework.Toolkits.FluentAPI;

    public partial class GunConfig
    {
        public static List<GunConfig> All => new List<GunConfig>()
        {
            Pistol, MP5, ShotGun, AK, AWP, Bow, LaserGun, RocketGun
        };

        public static GunConfig Pistol => new GunConfig()
        {
            Key = nameof(Pistol),
            ClipMaxBulletCount = BG_GunTable.GetEntity(nameof(Pistol)).ClipBulletCount,
            BagMaxBulletCount = BG_GunTable.GetEntity(nameof(Pistol)).BagBulletCount,
            Description = BG_UnlockTable.GetEntity(nameof(Pistol)).Description,
            Price = BG_UnlockTable.GetEntity(nameof(Pistol)).Price,
            Unlocked = BG_UnlockTable.GetEntity(nameof(Pistol)).Unlocked
        };

        public static GunConfig MP5 => new GunConfig()
        {
            Key = nameof(MP5),
            ClipMaxBulletCount = BG_GunTable.GetEntity(nameof(MP5)).ClipBulletCount,
            BagMaxBulletCount = BG_GunTable.GetEntity(nameof(MP5)).BagBulletCount,
            Description = BG_UnlockTable.GetEntity(nameof(MP5)).Description,
            Price = BG_UnlockTable.GetEntity(nameof(MP5)).Price,
            Unlocked = BG_UnlockTable.GetEntity(nameof(MP5)).Unlocked
        };

        public static GunConfig ShotGun => new GunConfig()
        {
            Key = nameof(ShotGun),
            ClipMaxBulletCount = BG_GunTable.GetEntity(nameof(ShotGun)).ClipBulletCount,
            BagMaxBulletCount = BG_GunTable.GetEntity(nameof(ShotGun)).BagBulletCount,
            Description = BG_UnlockTable.GetEntity(nameof(ShotGun)).Description,
            Price = BG_UnlockTable.GetEntity(nameof(ShotGun)).Price,
            Unlocked = BG_UnlockTable.GetEntity(nameof(ShotGun)).Unlocked
        };

        public static GunConfig AK => new GunConfig()
        {
            Key = nameof(AK),
            ClipMaxBulletCount = BG_GunTable.GetEntity(nameof(AK)).ClipBulletCount,
            BagMaxBulletCount = BG_GunTable.GetEntity(nameof(AK)).BagBulletCount,
            Description = BG_UnlockTable.GetEntity(nameof(AK)).Description,
            Price = BG_UnlockTable.GetEntity(nameof(AK)).Price,
            Unlocked = BG_UnlockTable.GetEntity(nameof(AK)).Unlocked
        };

        public static GunConfig AWP => new GunConfig()
        {
            Key = nameof(AWP),
            ClipMaxBulletCount = BG_GunTable.GetEntity(nameof(AWP)).ClipBulletCount,
            BagMaxBulletCount = BG_GunTable.GetEntity(nameof(AWP)).BagBulletCount,
            Description = BG_UnlockTable.GetEntity(nameof(AWP)).Description,
            Price = BG_UnlockTable.GetEntity(nameof(AWP)).Price,
            Unlocked = BG_UnlockTable.GetEntity(nameof(AWP)).Unlocked
        };

        public static GunConfig Bow => new GunConfig()
        {
            Key = nameof(Bow),
            ClipMaxBulletCount = BG_GunTable.GetEntity(nameof(Bow)).ClipBulletCount,
            BagMaxBulletCount = BG_GunTable.GetEntity(nameof(Bow)).BagBulletCount,
            Description = BG_UnlockTable.GetEntity(nameof(Bow)).Description,
            Price = BG_UnlockTable.GetEntity(nameof(Bow)).Price,
            Unlocked = BG_UnlockTable.GetEntity(nameof(Bow)).Unlocked
        };

        public static GunConfig LaserGun => new GunConfig()
        {
            Key = nameof(LaserGun),
            ClipMaxBulletCount = BG_GunTable.GetEntity(nameof(LaserGun)).ClipBulletCount,
            BagMaxBulletCount = BG_GunTable.GetEntity(nameof(LaserGun)).BagBulletCount,
            Description = BG_UnlockTable.GetEntity(nameof(LaserGun)).Description,
            Price = BG_UnlockTable.GetEntity(nameof(LaserGun)).Price,
            Unlocked = BG_UnlockTable.GetEntity(nameof(LaserGun)).Unlocked
        };

        public static GunConfig RocketGun => new GunConfig()
        {
            Key = nameof(RocketGun),
            ClipMaxBulletCount = BG_GunTable.GetEntity(nameof(RocketGun)).ClipBulletCount,
            BagMaxBulletCount = BG_GunTable.GetEntity(nameof(RocketGun)).BagBulletCount,
            Description = BG_UnlockTable.GetEntity(nameof(RocketGun)).Description,
            Price = BG_UnlockTable.GetEntity(nameof(RocketGun)).Price,
            Unlocked = BG_UnlockTable.GetEntity(nameof(RocketGun)).Unlocked
        };
    }

    public partial class GunConfig
    {
        public string Key;
        public int ClipMaxBulletCount;
        public int BagMaxBulletCount;
        public string Description;
        public int Price;
        public bool Unlocked;

        public GunData CreateData()
        {
            return new GunData()
            {
                Key = Key,
                ClipRemainBulletCount = ClipMaxBulletCount,
                BagRemainBulletCount = BagMaxBulletCount,
                IsUnlocked = new($"{Key}_Unlocked", Unlocked),
                Owned = Unlocked,
                Config = this
            };
        }
    }

    public class GunData
    {
        public string Key;
        public int ClipRemainBulletCount;
        public int BagRemainBulletCount;
        public PlayerPrefsBoolProperty IsUnlocked;
        public bool Owned;
        public GunConfig Config;

        public void AddClipBullet(int bulletCountToAdd)
        {
            ClipRemainBulletCount += bulletCountToAdd;
            ClipRemainBulletCount = ClipRemainBulletCount.MinWith(Config.ClipMaxBulletCount);
        }

        public void UseClipBullet(int bulletCountToUse)
        {
            ClipRemainBulletCount -= bulletCountToUse;
            ClipRemainBulletCount = ClipRemainBulletCount.MaxWith(0);
        }

        public void AddBagBullet(int bulletCountToAdd)
        {
            BagRemainBulletCount += bulletCountToAdd;
            BagRemainBulletCount = BagRemainBulletCount.MinWith(Config.BagMaxBulletCount);
        }

        public void UseBagBullet(int bulletCountToUse)
        {
            BagRemainBulletCount -= bulletCountToUse;
            BagRemainBulletCount = BagRemainBulletCount.MaxWith(0);
        }
    }
}