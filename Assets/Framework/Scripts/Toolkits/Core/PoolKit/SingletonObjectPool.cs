// ------------------------------------------------------------
// @file       SafeObjectPool.cs
// @brief
// @author     zheliku
// @Modified   2024-10-23 22:10:42
// @Copyright  Copyright (c) 2024, zheliku
// ------------------------------------------------------------

namespace Framework.Toolkits.PoolKit
{
    using System;
    using System.Collections.Generic;
    using Core;
    using SingletonKit;

    /// <summary>
    /// 对象必须继承 IPoolable
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class SingletonObjectPool<T> : Pool<T>, ISingleton where T : IPoolable, new()
    {
        protected SingletonObjectPool()
        {
            _factory = new DefaultObjectFactory<T>();
        }

        /// <summary>
        /// Init the specified maxCount and initCount.
        /// </summary>
        /// <param name="initCount">Init Cache count.</param>
        /// <param name="maxCount">Max Cache count.</param>
        public void Init(int initCount, int maxCount)
        {
            if (maxCount < 0)
            {
                throw new FrameworkException("maxCount must not be less than 0");
            }

            if (initCount < 0)
            {
                throw new FrameworkException("initCount must not be less than 0");
            }

            if (initCount > maxCount)
            {
                throw new FrameworkException("initCount must not be greater than maxCount");
            }

            _maxSize   = maxCount;
            _cacheStack = new Stack<T>(_maxSize);

            for (var i = 0; i < initCount; ++i)
            {
                _cacheStack.Push(_factory.Create());
            }
            _countAll = initCount;
        }

        /// <summary>
        /// Allocate T instance.
        /// </summary>
        public override T Get()
        {
            var result = base.Get();
            result.IsInPool = false;
            result.OnGet();
            return result;
        }

        /// <summary>
        /// Recycle the T instance
        /// </summary>
        /// <param name="t">T.</param>
        public override bool Release(T t)
        {
            if (_collectionCheck && _cacheStack.Contains(t))
            {
                throw new InvalidOperationException("Trying to release an object that has already been released to the pool.");
            }
            
            if (t == null || t.IsInPool)
            {
                return false;
            }

            t.OnRelease();
            t.IsInPool = true;

            if (_maxSize > 0 && _cacheStack.Count >= _maxSize)
            {
                --_countAll;
                t.OnDestroy();
                return false;
            }

            _cacheStack.Push(t);

            return true;
        }
        
        public override void Clear(Action<T> onClear = null)
        {
            if (onClear != null)
            {
                foreach (var t in _cacheStack)
                {
                    onClear.Invoke(t);
                    t.OnDestroy();
                }
            }
            
            base.Clear(onClear);
        }

        void ISingleton.OnSingletonInit()
        { }

        public static SingletonObjectPool<T> Instance { get => SingletonProperty<SingletonObjectPool<T>>.Instance; }

        public void Dispose()
        {
            SingletonProperty<SingletonObjectPool<T>>.Dispose();
        }
    }
}