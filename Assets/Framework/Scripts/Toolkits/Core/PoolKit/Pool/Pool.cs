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
        protected Stack<T> _cacheStack = new Stack<T>(50);

        [ShowInInspector]
        protected IObjectFactory<T> _factory;

        /// <summary>
        /// default is 50
        /// </summary>
        [ShowInInspector]
        protected int _maxCount = 50;

        protected int _allCount;

        [ShowInInspector]
        public int AllCount { get => _allCount; }

        /// <summary>
        /// Gets the current count.
        /// </summary>
        /// <value>The current count.</value>
        [ShowInInspector]
        public int InactiveCount { get => _cacheStack.Count; }

        [ShowInInspector]
        public int ActiveCount { get => _allCount - _cacheStack.Count; }

        public void SetObjectFactory(IObjectFactory<T> factory)
        {
            _factory = factory;
        }

        public virtual T Create()
        {
            if (_cacheStack.Count > 0)
            {
                return _cacheStack.Pop();
            }

            ++_allCount;
            return _factory.Create();
        }

        public abstract bool Recycle(T obj);

        public virtual void Clear(Action<T> onClear = null)
        {
            _cacheStack.Clear();
            _allCount = 0;
        }
    }
}