// ------------------------------------------------------------
// @file       IPoolable.cs
// @brief
// @author     zheliku
// @Modified   2024-10-23 21:10:45
// @Copyright  Copyright (c) 2024, zheliku
// ------------------------------------------------------------

namespace Framework.Toolkits.PoolKit
{
    /// <summary>
    /// 可放进 Pool 的对象接口.
    /// </summary>
    public interface IPoolable
    {
        void OnSpawn();
        
        void OnRecycle();

        void OnDestroy();

        bool IsRecycled { get; set; }
    }
}