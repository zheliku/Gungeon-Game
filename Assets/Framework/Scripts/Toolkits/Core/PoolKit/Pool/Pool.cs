// ------------------------------------------------------------
// @file       Pool.cs
// @brief
// @author     zheliku
// @Modified   2024-10-23 21:10:03
// @Copyright  Copyright (c) 2024, zheliku
// ------------------------------------------------------------

namespace Framework.Toolkits.PoolKit
{
    using System;
    using System.Collections.Generic;
    using Sirenix.OdinInspector;

    [HideReferenceObjectPicker]
    public abstract class Pool<T> : IPool<T>
    {
        /// <summary>
        /// 存储相关数据的栈
        /// </summary>
        [ShowInInspector]
        protected Stack<T> _cacheStack = new(50);

        [ShowInInspector]
        protected IObjectFactory<T> _factory;

        /// <summary>
        /// default is 50
        /// </summary>
        [ShowInInspector]
        protected int _maxSize = 100;

        protected int _countAll;

        protected bool _collectionCheck = false;

        [ShowInInspector]
        public int CountAll { get => _countAll; }

        /// <summary>
        /// Gets the current count.
        /// </summary>
        /// <value>The current count.</value>
        [ShowInInspector]
        public int CountInactive { get => _cacheStack.Count; }

        [ShowInInspector]
        public int CountActive { get => _countAll - _cacheStack.Count; }

        public void SetObjectFactory(IObjectFactory<T> factory)
        {
            _factory = factory;
        }

        public virtual T Get()
        {
            if (_cacheStack.Count > 0)
            {
                return _cacheStack.Pop();
            }

            ++_countAll;
            return _factory.Create();
        }

        public abstract bool Release(T obj);

        public virtual void Clear(Action<T> onClear = null)
        {
            _cacheStack.Clear();
            _countAll = 0;
        }
    }
}