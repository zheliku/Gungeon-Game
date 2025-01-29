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
    using Framework.Core;
    using SingletonKit;

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

            _maxCount   = maxCount;
            _cacheStack = new Stack<T>(_maxCount);

            for (var i = 0; i < initCount; ++i)
            {
                _cacheStack.Push(_factory.Create());
            }
            _allCount = initCount;
        }

        /// <summary>
        /// Allocate T instance.
        /// </summary>
        public override T Create()
        {
            var result = base.Create();
            result.IsRecycled = false;
            result.OnSpawn();
            return result;
        }

        /// <summary>
        /// Recycle the T instance
        /// </summary>
        /// <param name="t">T.</param>
        public override bool Recycle(T t)
        {
            if (t == null || t.IsRecycled)
            {
                return false;
            }

            t.OnRecycle();
            t.IsRecycled = true;

            if (_maxCount > 0 && _cacheStack.Count >= _maxCount)
            {
                --_allCount;
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