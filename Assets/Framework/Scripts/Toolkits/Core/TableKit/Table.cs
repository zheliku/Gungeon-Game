// ------------------------------------------------------------
// @file       Table.cs
// @brief
// @author     zheliku
// @Modified   2024-11-01 13:11:58
// @Copyright  Copyright (c) 2024, zheliku
// ------------------------------------------------------------

namespace Framework.Toolkits.TableKit
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using Sirenix.OdinInspector;

    /// <summary>
    /// 目前仅支持 <c>TData</c> 为引用类型
    /// </summary>
    [HideReferenceObjectPicker]
    public abstract class Table<TData> : IEnumerable<TData>, IDisposable where TData : class
    {
        protected abstract void OnAdd(TData data);

        protected abstract void OnRemove(TData data);

        protected abstract void OnClear();

        protected abstract void OnDispose();

        public abstract IEnumerator<TData> GetEnumerator();

        public void Add(TData data)
        {
            OnAdd(data);
        }

        public void Remove(TData data)
        {
            OnRemove(data);
        }

        public void Clear()
        {
            OnClear();
        }

        public void Dispose()
        {
            OnDispose();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}