// ------------------------------------------------------------
// @file       GunSystem.cs
// @brief
// @author     zheliku
// @Modified   2025-04-12 15:04:03
// @Copyright  Copyright (c) 2025, zheliku
// ------------------------------------------------------------

using System.Linq;
using UnityEngine;

namespace Game
{
    using System.Collections.Generic;
    using Framework.Core;
    using Sirenix.OdinInspector;

    public class GunSystem : AbstractSystem
    {
        [ShowInInspector]
        protected List<BG_GunTable> _bgGunConfigs;

        public GunData CurrentGunData;

        [ShowInInspector]
        public List<GunData> AllGuns = new();

        [ShowInInspector]
        public List<GunData> UnlockedGuns
        {
            get { return AllGuns.FindAll(gun => gun.IsUnlocked); }
        }

        [ShowInInspector]
        public List<GunData> OwnedGuns // All unlocked guns that are owned
        {
            get { return AllGuns.FindAll(gun => gun.Owned); }
        }
        
        [ShowInInspector]
        public List<GunData> UnOwnedGuns // All unlocked guns that are not yet owned
        {
            get { return AllGuns.FindAll(gun => gun.IsUnlocked && !gun.Owned); }
        }

        protected override void OnInit()
        {
            _bgGunConfigs = BG_GunTable.FindEntities(config => true);

            foreach (var config in GunConfig.All)
            {
                AllGuns.Add(config.CreateData());
            }
            
            CurrentGunData = OwnedGuns[0];
            
            Debug.Log(CurrentGunData);

            Reset();
        }

        public void Reset()
        {
            // AvailableGuns.Clear();
            // AvailableGuns.Add(GunConfig.Pistol.CreateData());
            // AvailableGuns.Add(GunConfig.AK.CreateData());
            // AvailableGuns.Add(GunConfig.MP5.CreateData());
        }
    }
}