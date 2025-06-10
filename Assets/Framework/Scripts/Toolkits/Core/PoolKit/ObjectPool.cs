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
        private readonly Action<T> _actionOnGet;

        [ShowInInspector]
        private readonly Action<T> _actionOnRelease;

        [ShowInInspector]
        private readonly Action<T> _actionOnDestroy;

        public ObjectPool(
            Func<T>   createFunc,
            Action<T> actionOnGet     = null,
            Action<T> actionOnRelease = null,
            Action<T> actionOnDestroy = null,
            bool      collectionCheck = false,
            int       defaultCapacity = 10,
            int       maxSize         = 100)
        {
            _factory         = new CustomObjectFactory<T>(createFunc);
            _actionOnGet     = actionOnGet;
            _actionOnRelease = actionOnRelease;
            _actionOnDestroy = actionOnDestroy;
            _collectionCheck = collectionCheck;
            _maxSize         = maxSize;
            _cacheStack      = new Stack<T>(defaultCapacity);

            for (var i = 0; i < defaultCapacity; i++)
            {
                _cacheStack.Push(_factory.Create());
            }
            _countAll = defaultCapacity;
        }

        public override T Get()
        {
            var item = base.Get();
            _actionOnGet?.Invoke(item);
            return item;
        }

        public override bool Release(T obj)
        {
            if (_collectionCheck && _cacheStack.Contains(obj))
            {
                throw new InvalidOperationException("Trying to release an object that has already been released to the pool.");
            }

            _actionOnRelease?.Invoke(obj);

            if (CountInactive < _maxSize)
            {
                _cacheStack.Push(obj);
                return true;
            }
            else
            {
                --_countAll;
                _actionOnDestroy?.Invoke(obj);
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

            if (_actionOnDestroy != null)
            {
                foreach (var t in _cacheStack)
                {
                    _actionOnDestroy.Invoke(t);
                }
            }

            base.Clear(onClear);
        }
    }
}