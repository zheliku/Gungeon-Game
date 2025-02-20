// ------------------------------------------------------------
// @file       GunCooling.cs
// @brief
// @author     zheliku
// @Modified   2025-02-21 04:02:44
// @Copyright  Copyright (c) 2025, zheliku
// ------------------------------------------------------------

namespace Game
{
    using Sirenix.OdinInspector;
    using UnityEngine;

    /// <summary>
    /// 枪械射击间隔
    /// </summary>
    public class GunShootInterval
    {
        [ShowInInspector]
        protected float _shootInterval;

        protected float _lastShootTime = 0;

        public bool CanShoot
        {
            get => _lastShootTime + _shootInterval <= Time.time;
        }

        public GunShootInterval(float shootInterval)
        {
            _shootInterval = shootInterval;
        }

        public void Reset()
        {
            _lastShootTime = Time.time;
        }
    }
}