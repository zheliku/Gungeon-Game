// ------------------------------------------------------------
// @file       DictionaryPool.cs
// @brief
// @author     zheliku
// @Modified   2025-05-15 23:11:21
// @Copyright  Copyright (c) 2025, zheliku
// ------------------------------------------------------------

namespace Framework.Toolkits.PoolKit
{
    using System.Collections.Generic;

    public class DictionaryPool<TKey, TValue> :
        CollectionPool<Dictionary<TKey, TValue>, KeyValuePair<TKey, TValue>>
    { }
    
    public static class DictionaryPoolExtension
    {
        /// <summary>
        /// 对 Dictionary 拓展自身入栈的方法
        /// </summary>
        public static void Release2Pool<TKey, TValue>(this Dictionary<TKey, TValue> toRelease)
        {
            DictionaryPool<TKey, TValue>.Release(toRelease);
        }
    }
}