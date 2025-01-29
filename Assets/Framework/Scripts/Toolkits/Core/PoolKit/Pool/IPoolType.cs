// ------------------------------------------------------------
// @file       IPoolType.cs
// @brief
// @author     zheliku
// @Modified   2024-10-23 21:10:05
// @Copyright  Copyright (c) 2024, zheliku
// ------------------------------------------------------------

namespace Framework.Toolkits.PoolKit
{
    /// <summary>
    /// I cache type.
    /// </summary>
    public interface IPoolType
    {
        void RecycleToCache();
    }
}