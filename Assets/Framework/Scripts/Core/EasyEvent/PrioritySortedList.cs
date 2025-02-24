// ------------------------------------------------------------
// @file       PriorityQueue.cs
// @brief
// @author     zheliku
// @Modified   2025-02-23 06:02:34
// @Copyright  Copyright (c) 2025, zheliku
// ------------------------------------------------------------

namespace Framework.Core
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;
    using Sirenix.OdinInspector;

    /// <summary>
    /// 支持按优先级排序的列表（最小优先级在前），相同优先级后插入的排在后面
    /// </summary>
    [HideReferenceObjectPicker]
    public class PrioritySortedList<TElement, TPriority> : IEnumerable<TElement>
    {
        [ShowInInspector]
        private readonly List<Entry> _elements = new List<Entry>();

        private int _sequenceCounter;

        private readonly IComparer<TPriority> _priorityComparer;

        private readonly EntryComparer _entryComparer;

        public PrioritySortedList() : this(Comparer<TPriority>.Default) { }

        public PrioritySortedList(IComparer<TPriority> priorityComparer)
        {
            _priorityComparer = priorityComparer ?? throw new ArgumentNullException(nameof(priorityComparer));
            _entryComparer    = new EntryComparer(_priorityComparer);
        }

        public void Add(TElement element, TPriority priority)
        {
            var entry            = new Entry(element, priority, ++_sequenceCounter);
            int index            = _elements.BinarySearch(entry, _entryComparer);
            if (index < 0) index = ~index;
            _elements.Insert(index, entry);
        }

        public bool TryDequeue(out TElement element)
        {
            if (_elements.Count == 0)
            {
                element = default;
                return false;
            }

            element = _elements[0].Element;
            _elements.RemoveAt(0);
            return true;
        }

        public TElement Dequeue()
        {
            if (!TryDequeue(out var element))
                throw new InvalidOperationException("Queue is empty");
            return element;
        }

        public bool TryPeek(out TElement element)
        {
            if (_elements.Count == 0)
            {
                element = default;
                return false;
            }

            element = _elements[0].Element;
            return true;
        }

        public TElement Peek()
        {
            if (!TryPeek(out var element))
                throw new InvalidOperationException("Queue is empty");
            return element;
        }

        public bool Remove(TElement element)
        {
            var comparer = EqualityComparer<TElement>.Default;

            // 倒序遍历以避免索引错位
            for (int i = _elements.Count - 1; i >= 0; i--)
            {
                if (comparer.Equals(_elements[i].Element, element))
                {
                    _elements.RemoveAt(i);
                    return true;
                }
            }
            return false;
        }

        public int RemoveAll(Predicate<TElement> match)
        {
            return _elements.RemoveAll(entry => match(entry.Element));
        }

        public void Clear()
        {
            _elements.Clear();
            _sequenceCounter = 0;
        }

        public int Count => _elements.Count;

        public bool IsEmpty => _elements.Count == 0;

        public TElement this[int index]
        {
            get
            {
                if (index < 0 || index >= _elements.Count)
                {
                    throw new ArgumentOutOfRangeException(nameof(index));
                }
                return _elements[index].Element;
            }
        }

        public IEnumerator<TElement> GetEnumerator() =>
            _elements.Select(entry => entry.Element).GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        [ShowInInspector]
        private readonly struct Entry
        {
            [ShowInInspector]
            public TElement Element { get; }

            [ShowInInspector]
            public TPriority Priority { get; }

            [ShowInInspector]
            public int SequenceNumber { get; }

            public Entry(TElement element, TPriority priority, int sequenceNumber)
            {
                Element        = element;
                Priority       = priority;
                SequenceNumber = sequenceNumber;
            }
        }

        private class EntryComparer : IComparer<Entry>
        {
            private readonly IComparer<TPriority> _priorityComparer;

            public EntryComparer(IComparer<TPriority> priorityComparer)
            {
                _priorityComparer = priorityComparer;
            }

            public int Compare(Entry x, Entry y)
            {
                int priorityCompare = _priorityComparer.Compare(x.Priority, y.Priority);
                return priorityCompare != 0
                    ? priorityCompare
                    : x.SequenceNumber.CompareTo(y.SequenceNumber);
            }
        }
    }
}