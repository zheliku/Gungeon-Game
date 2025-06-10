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
        void OnGet();

        void OnRelease();

        void OnDestroy();

        bool IsInPool { get; set; }
    }

    public static class IPooableExtension
    {
        public static void Release2Pool<T>(this T poolable) where T : IPoolable, new()
        {
            SingletonObjectPool<T>.Instance.Release(poolable);
        }
    }
}