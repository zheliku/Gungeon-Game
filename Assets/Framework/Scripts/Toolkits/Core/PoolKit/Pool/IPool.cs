// ------------------------------------------------------------
// @file       IPool.cs
// @brief
// @author     zheliku
// @Modified   2024-10-23 21:10:24
// @Copyright  Copyright (c) 2024, zheliku
// ------------------------------------------------------------

namespace Framework.Toolkits.PoolKit
{
    using System;

    /// <summary>
    /// 对象池接口
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IPool<T>
    {
        public int CountAll { get; }

        public int CountInactive { get; }

        public int CountActive { get; }

        /// <summary>
        /// 分配对象
        /// </summary>
        /// <returns></returns>
        T Get();

        /// <summary>
        /// 回收对象
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        bool Release(T obj);

        void Clear(Action<T> onClear);
    }
}