// ------------------------------------------------------------
// @file       Bullet.cs
// @brief
// @author     zheliku
// @Modified   2024-10-23 23:10:35
// @Copyright  Copyright (c) 2024, zheliku
// ------------------------------------------------------------

namespace Framework.Toolkits.PoolKit.Example._1.SingletonObjectPoolExample
{
    using UnityEngine;

    public class Bullet : IPoolable
    {
        public void OnSpawn()
        {
            Debug.Log("Bullet OnSpawn");
        }

        public void OnRecycle()
        {
            Debug.Log("Bullet OnRecycle");
        }
        
        public void OnDestroy()
        {
            Debug.Log("Bullet OnDestroy");
        }

        public bool IsRecycled { get; set; }
    }
}