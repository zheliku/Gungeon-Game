// ------------------------------------------------------------
// @file       ObjectPool.cs
// @brief
// @author     zheliku
// @Modified   2024-10-23 21:10:44
// @Copyright  Copyright (c) 2024, zheliku
// ------------------------------------------------------------

namespace Framework.Toolkits.PoolKit
{
    using System;
    using System.Collections.Generic;
    using Sirenix.OdinInspector;

    public sealed class ObjectPool<T> : Pool<T>
    {
        [ShowInInspector]
        private readonly Action<T> _onCreate;

        [ShowInInspector]
        private readonly Action<T> _onRecycle;

        [ShowInInspector]
        private readonly Action<T> _onDestroy;

        public ObjectPool(
            Func<T>   createMethod,
            Action<T> onCreate  = null,
            Action<T> onRecycle = null,
            Action<T> onDestroy = null,
            int       initCount = 0,
            int       maxCount  = 50)
        {
            _factory    = new CustomObjectFactory<T>(createMethod);
            _onCreate   = onCreate;
            _onRecycle  = onRecycle;
            _onDestroy  = onDestroy;
            _maxCount   = maxCount;
            _cacheStack = new Stack<T>(maxCount);

            for (var i = 0; i < initCount; i++)
            {
                _cacheStack.Push(_factory.Create());
            }
            _allCount = initCount;
        }

        public override T Create()
        {
            var item = base.Create();
            _onCreate?.Invoke(item);
            return item;
        }

        public override bool Recycle(T obj)
        {
            _onRecycle?.Invoke(obj);

            if (InactiveCount < _maxCount)
            {
                _cacheStack.Push(obj);
                return true;
            }
            else
            {
                --_allCount;
                _onDestroy?.Invoke(obj);
                return false;
            }
        }

        public override void Clear(Action<T> onClear = null)
        {
            if (onClear != null)
            {
                foreach (var t in _cacheStack)
                {
                    onClear.Invoke(t);
                }
            }

            if (_onDestroy != null)
            {
                foreach (var t in _cacheStack)
                {
                    _onDestroy.Invoke(t);
                }
            }

            base.Clear(onClear);
        }
    }
}