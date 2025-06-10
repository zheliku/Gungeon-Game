// ------------------------------------------------------------
// @file       HashSetPool.cs
// @brief
// @author     zheliku
// @Modified   2025-05-15 23:12:39
// @Copyright  Copyright (c) 2025, zheliku
// ------------------------------------------------------------

namespace Framework.Toolkits.PoolKit
{
    using System.Collections.Generic;

    public class HashSetPool<T> : CollectionPool<HashSet<T>, T>
    { }
    
    public static class HashSetPoolExtension
    {
        /// <summary>
        /// 对 HashSet 拓展自身入栈的方法
        /// </summary>
        public static void Release2Pool<T>(this HashSet<T> toRelease)
        {
            HashSetPool<T>.Release(toRelease);
        }
    }
}