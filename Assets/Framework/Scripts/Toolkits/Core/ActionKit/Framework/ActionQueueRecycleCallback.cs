// ------------------------------------------------------------
// @file       ActionQueueRecycleCallback.cs
// @brief
// @author     zheliku
// @Modified   2024-10-24 20:10:41
// @Copyright  Copyright (c) 2024, zheliku
// ------------------------------------------------------------

namespace Framework.Toolkits.ActionKit
{
    using PoolKit;

    public struct ActionQueueRecycleCallback<T> : IActionQueueCallback
    {
        /// <summary>
        /// 需要回收倒哪个 Pool 中
        /// </summary>
        public ObjectPool<T> Pool;

        /// <summary>
        /// 哪个 Action 需要回收
        /// </summary>
        public T Action;

        public ActionQueueRecycleCallback(ObjectPool<T> pool, T action)
        {
            Pool = pool;
            Action = action;
        }
        
        /// <summary>
        /// 回收方法
        /// </summary>
        public void Call()
        {
            Pool.Recycle(Action);
            Pool   = null;
            Action = default;
        }
    }
}