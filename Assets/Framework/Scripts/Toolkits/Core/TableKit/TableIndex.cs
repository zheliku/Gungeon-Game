// ------------------------------------------------------------
// @file       TableIndex.cs
// @brief
// @author     zheliku
// @Modified   2024-11-01 13:11:26
// @Copyright  Copyright (c) 2024, zheliku
// ------------------------------------------------------------

namespace Framework.Toolkits.TableKit
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Sirenix.OdinInspector;

    [HideReferenceObjectPicker]
    public class TableIndex<TKey, TValue> : IDisposable
    {
        [ShowInInspector]
        private readonly Dictionary<TKey, List<TValue>> _index = new Dictionary<TKey, List<TValue>>();

        private readonly Func<TValue, TKey> _getKeyByValue;

        public TableIndex(Func<TValue, TKey> keyGetter)
        {
            _getKeyByValue = keyGetter;
        }
        
        public IDictionary<TKey, List<TValue>> Dictionary { get => _index; }

        public void Add(TValue value)
        {
            var key = _getKeyByValue(value);

            if (_index.TryGetValue(key, out var valueList))
            {
                valueList.Add(value);
            }
            else
            {
                var list = new List<TValue> { value };
                _index.Add(key, list);
            }
        }

        public void Remove(TValue value)
        {
            var key = _getKeyByValue(value);

            _index[key].Remove(value);
        }

        public IEnumerable<TValue> Get(TKey key)
        {
            if (_index.TryGetValue(key, out var valueList))
            {
                return valueList;
            }

            return Enumerable.Empty<TValue>();
        }

        public void Clear()
        {
            foreach (var list in _index.Values)
            {
                list.Clear();
            }
            
            _index.Clear();
        }

        public void Dispose()
        {
            Clear();
        }
    }
}