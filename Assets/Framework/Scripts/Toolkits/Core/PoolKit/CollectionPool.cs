// ------------------------------------------------------------
// @file       CollectionPool.cs
// @brief
// @author     zheliku
// @Modified   2025-05-15 22:57:53
// @Copyright  Copyright (c) 2025, zheliku
// ------------------------------------------------------------

namespace Framework.Toolkits.PoolKit
{
    using System.Collections.Generic;

    public class CollectionPool<TCollection, TItem> where TCollection : class, ICollection<TItem>, new()
    {
        public static readonly ObjectPool<TCollection> POOL = new(
            () => new TCollection(),
            actionOnGet: null,
            actionOnRelease: l => l.Clear(),
            actionOnDestroy: l => l.Clear(),
            defaultCapacity: 5,
            maxSize: 20
        );

        public static TCollection Get()
        {
            return POOL.Get();
        }

        public static void Release(TCollection toRelease)
        {
            POOL.Release(toRelease);
        }
    }
}