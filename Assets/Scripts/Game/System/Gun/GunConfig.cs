// ------------------------------------------------------------
// @file       GunData.cs
// @brief
// @author     zheliku
// @Modified   2025-04-12 15:04:20
// @Copyright  Copyright (c) 2025, zheliku
// ------------------------------------------------------------

namespace Game
{
    using System.Collections.Generic;
    using Framework.Toolkits.FluentAPI;

    public partial class GunConfig
    {
        public static GunConfig Pistol = new GunConfig()
        {
            Key               = nameof(Pistol),
            ClipMaxBulletCount   = BG_GunConfig.GetEntity(nameof(Pistol)).ClipBulletCount,
            BagMaxBulletCount = BG_GunConfig.GetEntity(nameof(Pistol)).BagBulletCount,
        };

        public static GunConfig MP5 = new GunConfig()
        {
            Key               = nameof(MP5),
            ClipMaxBulletCount   = BG_GunConfig.GetEntity(nameof(MP5)).ClipBulletCount,
            BagMaxBulletCount = BG_GunConfig.GetEntity(nameof(MP5)).BagBulletCount
        };

        public static GunConfig ShotGun = new GunConfig()
        {
            Key               = nameof(ShotGun),
            ClipMaxBulletCount   = BG_GunConfig.GetEntity(nameof(ShotGun)).ClipBulletCount,
            BagMaxBulletCount = BG_GunConfig.GetEntity(nameof(ShotGun)).BagBulletCount
        };
        
        public static GunConfig AK = new GunConfig()
        {
            Key               = nameof(AK),
            ClipMaxBulletCount   = BG_GunConfig.GetEntity(nameof(AK)).ClipBulletCount,
            BagMaxBulletCount = BG_GunConfig.GetEntity(nameof(AK)).BagBulletCount
        };
        
        public static GunConfig AWP = new GunConfig()
        {
            Key               = nameof(AWP),
            ClipMaxBulletCount   = BG_GunConfig.GetEntity(nameof(AWP)).ClipBulletCount,
            BagMaxBulletCount = BG_GunConfig.GetEntity(nameof(AWP)).BagBulletCount
        };
        
        public static GunConfig Bow = new GunConfig()
        {
            Key               = nameof(Bow),
            ClipMaxBulletCount   = BG_GunConfig.GetEntity(nameof(Bow)).ClipBulletCount,
            BagMaxBulletCount = BG_GunConfig.GetEntity(nameof(Bow)).BagBulletCount
        };
        
        public static GunConfig LaserGun = new GunConfig()
        {
            Key               = nameof(LaserGun),
            ClipMaxBulletCount   = BG_GunConfig.GetEntity(nameof(LaserGun)).ClipBulletCount,
            BagMaxBulletCount = BG_GunConfig.GetEntity(nameof(LaserGun)).BagBulletCount
        };
        
        public static GunConfig RocketGun = new GunConfig()
        {
            Key               = nameof(RocketGun),
            ClipMaxBulletCount   = BG_GunConfig.GetEntity(nameof(RocketGun)).ClipBulletCount,
            BagMaxBulletCount = BG_GunConfig.GetEntity(nameof(RocketGun)).BagBulletCount
        };
        
        public static List<GunConfig> AllConfigs = new List<GunConfig>()
        {
            Pistol, MP5, ShotGun, AK, AWP, Bow, LaserGun, RocketGun
        };
    }

    public partial class GunConfig
    {
        public string Key;
        public int    ClipMaxBulletCount;
        public int    BagMaxBulletCount;

        public GunData CreateData()
        {
            return new GunData()
            {
                Key                     = Key,
                ClipRemainBulletCount      = ClipMaxBulletCount,
                BagRemainBulletCount = BagMaxBulletCount,
                Config                  = this
            };
        }
    }

    public class GunData
    {
        public string    Key;
        public int       ClipRemainBulletCount;
        public int       BagRemainBulletCount;
        public GunConfig Config;
        
        public void AddClipBullet(int bulletCountToAdd)
        {
            ClipRemainBulletCount += bulletCountToAdd;
            ClipRemainBulletCount =  ClipRemainBulletCount.MinWith(Config.ClipMaxBulletCount);
        }

        public void UseClipBullet(int bulletCountToUse)
        {
            ClipRemainBulletCount -= bulletCountToUse;
            ClipRemainBulletCount =  ClipRemainBulletCount.MaxWith(0);
        }
        
        public void AddBagBullet(int bulletCountToAdd)
        {
            BagRemainBulletCount += bulletCountToAdd;
            BagRemainBulletCount =  BagRemainBulletCount.MinWith(Config.BagMaxBulletCount);
        }

        public void UseBagBullet(int bulletCountToUse)
        {
            BagRemainBulletCount -= bulletCountToUse;
            BagRemainBulletCount =  BagRemainBulletCount.MaxWith(0);
        }
    }
}