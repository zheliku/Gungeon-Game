// ------------------------------------------------------------
// @file       BindableList.cs
// @brief
// @author     zheliku
// @Modified   2024-10-16 16:10:26
// @Copyright  Copyright (c) 2024, zheliku
// ------------------------------------------------------------

namespace Framework.Toolkits.BindableKit
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using Framework.Core;

    [Serializable]
    public class BindableList<TElem> : Collection<TElem>
    {
    #region 字段

        [NonSerialized] private EasyEvent<int, int>          _onCountChanged = null;
        [NonSerialized] private EasyEvent                    _onClear        = null;
        [NonSerialized] private EasyEvent<int, TElem>        _collectionAdd  = null;
        [NonSerialized] private EasyEvent<int, int, TElem>   _onMove         = null;
        [NonSerialized] private EasyEvent<int, TElem>        _onRemove       = null;
        [NonSerialized] private EasyEvent<int, TElem, TElem> _onReplace      = null;

    #endregion

    #region 属性

        public EasyEvent<int, int> OnCountChanged
        {
            get => _onCountChanged ??= new EasyEvent<int, int>();
        }

        public EasyEvent OnClear
        {
            get => _onClear ??= new EasyEvent();
        }

        public EasyEvent<int, TElem> OnAdd
        {
            get => _collectionAdd ??= new EasyEvent<int, TElem>();
        }

        /// <summary>
        /// int:oldIndex
        /// int:newIndex
        /// T:item
        /// </summary>
        public EasyEvent<int, int, TElem> OnMove
        {
            get => _onMove ??= new EasyEvent<int, int, TElem>();
        }

        public EasyEvent<int, TElem> OnRemove
        {
            get => _onRemove ??= new EasyEvent<int, TElem>();
        }

        /// <summary>
        /// int:index
        /// T:oldItem
        /// T:newItem
        /// </summary>
        public EasyEvent<int, TElem, TElem> OnReplace
        {
            get => _onReplace ??= new EasyEvent<int, TElem, TElem>();
        }

    #endregion

    #region 方法

        public BindableList()
        { }

        public BindableList(IEnumerable<TElem> collection)
        {
            if (collection == null)
                throw new FrameworkException("collection is null");

            foreach (var item in collection)
            {
                Add(item);
            }
        }

        public BindableList(List<TElem> list)
            : base(list)
        { }

        public void Move(int oldIndex, int newIndex)
        {
            MoveItem(oldIndex, newIndex);
        }

        protected override void ClearItems()
        {
            var oldCount = Count;
            base.ClearItems();

            _onClear?.Trigger();
            if (oldCount > 0)
            {
                _onCountChanged?.Trigger(oldCount, 0);
            }
        }

        protected override void InsertItem(int index, TElem item)
        {
            var oldCount = Count;
            base.InsertItem(index, item);

            _collectionAdd?.Trigger(index, item);
            _onCountChanged?.Trigger(oldCount, Count);
        }

        protected virtual void MoveItem(int oldIndex, int newIndex)
        {
            var item = this[oldIndex];
            base.RemoveItem(oldIndex);
            base.InsertItem(newIndex, item);

            _onMove?.Trigger(oldIndex, newIndex, item);
        }

        protected override void RemoveItem(int index)
        {
            var item     = this[index];
            var oldCount = Count;

            base.RemoveItem(index);

            _onRemove?.Trigger(index, item);
            _onCountChanged?.Trigger(oldCount, Count);
        }

        protected override void SetItem(int index, TElem item)
        {
            var oldItem = this[index];
            base.SetItem(index, item);

            _onReplace?.Trigger(index, oldItem, item);
        }

    #endregion
    }

    public static class BindableListExtension
    {
        public static BindableList<T> ToBindableList<T>(this IEnumerable<T> self) => new BindableList<T>(self);
    }
}