// ------------------------------------------------------------
// @file       ListPool.cs
// @brief
// @author     zheliku
// @Modified   2025-05-15 23:05:20
// @Copyright  Copyright (c) 2025, zheliku
// ------------------------------------------------------------

namespace Framework.Toolkits.PoolKit
{
    using System.Collections.Generic;

    public class ListPool<T> : CollectionPool<List<T>, T>
    { }
    
    public static class ListPoolExtension
    {
        /// <summary>
        /// 对 List 拓展自身入栈的方法
        /// </summary>
        public static void Release2Pool<T>(this List<T> toRelease)
        {
            ListPool<T>.Release(toRelease);
        }
    }
}