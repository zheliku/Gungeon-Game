// ------------------------------------------------------------
// @file       PoolableObject.cs
// @brief
// @author     zheliku
// @Modified   2024-10-23 21:10:53
// @Copyright  Copyright (c) 2024, zheliku
// ------------------------------------------------------------

namespace Framework.Toolkits.PoolKit
{
    using System.Collections.Generic;

    public abstract class PoolableObject<T> where T : PoolableObject<T>, new()
    {
        private static Stack<T> _pool = new Stack<T>(10);

        protected bool _inPool;

        public static T Spawn()
        {
            var node = _pool.Count == 0 ? new T() : _pool.Pop();
            node._inPool = false;
            return node;
        }
        
        public void Recycle2Cache()
        {
            OnRecycle();
            _inPool = true;
            _pool.Push(this as T);
        }

        protected abstract void OnRecycle();
    }
}