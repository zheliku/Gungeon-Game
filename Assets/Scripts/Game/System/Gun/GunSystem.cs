// ------------------------------------------------------------
// @file       GunSystem.cs
// @brief
// @author     zheliku
// @Modified   2025-04-12 15:04:03
// @Copyright  Copyright (c) 2025, zheliku
// ------------------------------------------------------------

namespace Game
{
    using System.Collections.Generic;
    using Framework.Core;
    using Sirenix.OdinInspector;

    public class GunSystem : AbstractSystem
    {
        [ShowInInspector]
        protected List<BG_GunTable> _bgGunConfigs;

        [ShowInInspector]
        public List<GunData> GunDataList = new List<GunData>();

        protected override void OnInit()
        {
            _bgGunConfigs = BG_GunTable.FindEntities(config => true);
            
            Reset();
        }

        public void Reset()
        {
            GunDataList.Clear();
            GunDataList.Add(GunConfig.Pistol.CreateData());
            GunDataList.Add(GunConfig.AK.CreateData());
            GunDataList.Add(GunConfig.MP5.CreateData());
        }
    }
}