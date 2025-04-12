// ------------------------------------------------------------
// @file       GunData.cs
// @brief
// @author     zheliku
// @Modified   2025-04-12 15:04:20
// @Copyright  Copyright (c) 2025, zheliku
// ------------------------------------------------------------

namespace Game
{
    public partial class GunConfig
    {
        public static GunConfig Pistol = new GunConfig()
        {
            Key               = nameof(Pistol),
            ClipBulletCount   = BG_GunConfig.GetEntity(nameof(Pistol)).ClipBulletCount,
            BagMaxBulletCount = BG_GunConfig.GetEntity(nameof(Pistol)).BagBulletCount,
        };

        public static GunConfig MP5 = new GunConfig()
        {
            Key               = nameof(MP5),
            ClipBulletCount   = BG_GunConfig.GetEntity(nameof(MP5)).ClipBulletCount,
            BagMaxBulletCount = BG_GunConfig.GetEntity(nameof(MP5)).BagBulletCount
        };

        public static GunConfig ShotGun = new GunConfig()
        {
            Key               = nameof(ShotGun),
            ClipBulletCount   = BG_GunConfig.GetEntity(nameof(ShotGun)).ClipBulletCount,
            BagMaxBulletCount = BG_GunConfig.GetEntity(nameof(ShotGun)).BagBulletCount
        };
        
        public static GunConfig AK = new GunConfig()
        {
            Key               = nameof(AK),
            ClipBulletCount   = BG_GunConfig.GetEntity(nameof(AK)).ClipBulletCount,
            BagMaxBulletCount = BG_GunConfig.GetEntity(nameof(AK)).BagBulletCount
        };
        
        public static GunConfig AWP = new GunConfig()
        {
            Key               = nameof(AWP),
            ClipBulletCount   = BG_GunConfig.GetEntity(nameof(AWP)).ClipBulletCount,
            BagMaxBulletCount = BG_GunConfig.GetEntity(nameof(AWP)).BagBulletCount
        };
        
        public static GunConfig Bow = new GunConfig()
        {
            Key               = nameof(Bow),
            ClipBulletCount   = BG_GunConfig.GetEntity(nameof(Bow)).ClipBulletCount,
            BagMaxBulletCount = BG_GunConfig.GetEntity(nameof(Bow)).BagBulletCount
        };
        
        public static GunConfig LaserGun = new GunConfig()
        {
            Key               = nameof(LaserGun),
            ClipBulletCount   = BG_GunConfig.GetEntity(nameof(LaserGun)).ClipBulletCount,
            BagMaxBulletCount = BG_GunConfig.GetEntity(nameof(LaserGun)).BagBulletCount
        };
        
        public static GunConfig RocketGun = new GunConfig()
        {
            Key               = nameof(RocketGun),
            ClipBulletCount   = BG_GunConfig.GetEntity(nameof(RocketGun)).ClipBulletCount,
            BagMaxBulletCount = BG_GunConfig.GetEntity(nameof(RocketGun)).BagBulletCount
        };
    }

    public partial class GunConfig
    {
        public string Key;
        public int    ClipBulletCount;
        public int    BagMaxBulletCount;

        public GunData CreateData()
        {
            return new GunData()
            {
                Key                     = Key,
                CurrentBulletCount      = ClipBulletCount,
                GunBagRemainBulletCount = BagMaxBulletCount,
                Config                  = this
            };
        }
    }

    public class GunData
    {
        public string    Key;
        public int       CurrentBulletCount;
        public int       GunBagRemainBulletCount;
        public GunConfig Config;
    }
}