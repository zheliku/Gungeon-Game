// ------------------------------------------------------------
// @file       BindableDictionary.cs
// @brief
// @author     zheliku
// @Modified   2024-10-16 16:10:08
// @Copyright  Copyright (c) 2024, zheliku
// ------------------------------------------------------------

namespace Framework.Toolkits.BindableKit
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Runtime.Serialization;
    using Framework.Core;

    [Serializable]
    public class BindableDictionary<TKey, TValue> : IDictionary<TKey, TValue>,
                                                    IDictionary,
                                                    ISerializable,
                                                    IDeserializationCallback
    {
    #region 字段

        private readonly Dictionary<TKey, TValue> _inner;

        [NonSerialized] private EasyEvent<int> _onCountChanged;

        [NonSerialized] private EasyEvent _onClear;

        [NonSerialized] private EasyEvent<TKey, TValue> _onAdd;

        [NonSerialized] private EasyEvent<TKey, TValue> _onRemove;

        [NonSerialized] private EasyEvent<TKey, TValue, TValue> _onReplace;

    #endregion

    #region 属性

        public EasyEvent<int> OnCountChanged => _onCountChanged ??= new EasyEvent<int>();

        public EasyEvent OnClear => _onClear ??= new EasyEvent();

        public EasyEvent<TKey, TValue> OnAdd => _onAdd ??= new EasyEvent<TKey, TValue>();

        public EasyEvent<TKey, TValue> OnRemove => _onRemove ??= new EasyEvent<TKey, TValue>();

        /// <summary>
        /// TKey:key
        /// TValue:oldValue
        /// TValue:newValue
        /// </summary>
        public EasyEvent<TKey, TValue, TValue> OnReplace => _onReplace ??= new EasyEvent<TKey, TValue, TValue>();

        public int Count => _inner.Count;

        public TValue this[TKey key]
        {
            get => _inner[key];
            set
            {
                if (TryGetValue(key, out var oldValue))
                {
                    _inner[key] = value;
                    _onReplace?.Trigger(key, oldValue, value);
                }
                else
                {
                    _inner[key] = value;
                    _onAdd?.Trigger(key, value);
                    _onCountChanged?.Trigger(Count);
                }
            }
        }

        public Dictionary<TKey, TValue>.KeyCollection Keys => _inner.Keys;

        public Dictionary<TKey, TValue>.ValueCollection Values => _inner.Values;

    #endregion

    #region 接口

        object IDictionary.this[object key]
        {
            get => this[(TKey) key];
            set => this[(TKey) key] = (TValue) value;
        }

        bool IDictionary.IsFixedSize => ((IDictionary) _inner).IsFixedSize;

        bool IDictionary.IsReadOnly => ((IDictionary) _inner).IsReadOnly;

        bool ICollection.IsSynchronized => ((IDictionary) _inner).IsSynchronized;

        ICollection IDictionary.Keys => ((IDictionary) _inner).Keys;

        object ICollection.SyncRoot => ((IDictionary) _inner).SyncRoot;

        ICollection IDictionary.Values => ((IDictionary) _inner).Values;

        bool ICollection<KeyValuePair<TKey, TValue>>.IsReadOnly => ((ICollection<KeyValuePair<TKey, TValue>>) _inner).IsReadOnly;

        ICollection<TKey> IDictionary<TKey, TValue>.Keys => _inner.Keys;

        ICollection<TValue> IDictionary<TKey, TValue>.Values => _inner.Values;

        void IDictionary.Add(object key, object value)
        {
            Add((TKey) key, (TValue) value);
        }

        bool IDictionary.Contains(object key)
        {
            return ((IDictionary) _inner).Contains(key);
        }

        void ICollection.CopyTo(Array array, int index)
        {
            ((IDictionary) _inner).CopyTo(array, index);
        }

        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            ((ISerializable) _inner).GetObjectData(info, context);
        }

        public void OnDeserialization(object sender)
        {
            ((IDeserializationCallback) _inner).OnDeserialization(sender);
        }

        void IDictionary.Remove(object key)
        {
            Remove((TKey) key);
        }

        void ICollection<KeyValuePair<TKey, TValue>>.Add(KeyValuePair<TKey, TValue> item)
        {
            Add(item.Key, item.Value);
        }

        bool ICollection<KeyValuePair<TKey, TValue>>.Contains(KeyValuePair<TKey, TValue> item)
        {
            return ((ICollection<KeyValuePair<TKey, TValue>>) _inner).Contains(item);
        }

        void ICollection<KeyValuePair<TKey, TValue>>.CopyTo(KeyValuePair<TKey, TValue>[] array, int arrayIndex)
        {
            ((ICollection<KeyValuePair<TKey, TValue>>) _inner).CopyTo(array, arrayIndex);
        }

        IEnumerator<KeyValuePair<TKey, TValue>> IEnumerable<KeyValuePair<TKey, TValue>>.GetEnumerator()
        {
            return ((ICollection<KeyValuePair<TKey, TValue>>) _inner).GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return _inner.GetEnumerator();
        }

        bool ICollection<KeyValuePair<TKey, TValue>>.Remove(KeyValuePair<TKey, TValue> item)
        {
            if (TryGetValue(item.Key, out var v))
            {
                if (EqualityComparer<TValue>.Default.Equals(v, item.Value))
                {
                    Remove(item.Key);
                    return true;
                }
            }

            return false;
        }

        IDictionaryEnumerator IDictionary.GetEnumerator()
        {
            return ((IDictionary) _inner).GetEnumerator();
        }

    #endregion

    #region 方法

        public BindableDictionary()
        {
            _inner = new Dictionary<TKey, TValue>();
        }

        public BindableDictionary(IEqualityComparer<TKey> comparer)
        {
            _inner = new Dictionary<TKey, TValue>(comparer);
        }

        public BindableDictionary(Dictionary<TKey, TValue> dictionary)
        {
            _inner = dictionary;
        }

        public void Add(TKey key, TValue value)
        {
            _inner.Add(key, value);

            _onAdd?.Trigger(key, value);
            _onCountChanged?.Trigger(Count);
        }

        public void Clear()
        {
            var oldCount = Count;
            _inner.Clear();

            _onClear?.Trigger();
            if (oldCount > 0)
            {
                _onCountChanged?.Trigger(Count);
            }
        }

        public bool Remove(TKey key)
        {
            if (_inner.TryGetValue(key, out var oldValue))
            {
                var isSuccessRemove = _inner.Remove(key);
                if (isSuccessRemove)
                {
                    _onRemove?.Trigger(key, oldValue);
                    _onCountChanged?.Trigger(Count);
                }
                return isSuccessRemove;
            }

            return false;
        }

        public bool ContainsKey(TKey key)
        {
            return _inner.ContainsKey(key);
        }

        public bool TryGetValue(TKey key, out TValue value)
        {
            return _inner.TryGetValue(key, out value);
        }

        public Dictionary<TKey, TValue>.Enumerator GetEnumerator()
        {
            return _inner.GetEnumerator();
        }

    #endregion
    }

    public static class BindableDictionaryExtensions
    {
        public static BindableDictionary<TKey, TValue> ToBindableDictionary<TKey, TValue>(this Dictionary<TKey, TValue> self)
        {
            return new BindableDictionary<TKey, TValue>(self);
        }
    }
}