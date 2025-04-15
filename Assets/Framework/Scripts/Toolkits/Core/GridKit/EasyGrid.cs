// ------------------------------------------------------------
// @file       Grid.cs
// @brief
// @author     zheliku
// @Modified   2024-11-01 12:11:43
// @Copyright  Copyright (c) 2024, zheliku
// ------------------------------------------------------------

namespace Framework.Toolkits.GridKit
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using Framework.Core;
    using Sirenix.OdinInspector;
    using UnityEngine;

    [HideReferenceObjectPicker]
    public class EasyGrid<TValue> : IEnumerable<KeyValuePair<Vector2Int, TValue>>
    {
        [ShowInInspector]
        [TableMatrix(Transpose = true)]
        private TValue[,] _grid;

        public EasyGrid(int row, int column)
        {
            _grid = new TValue[row, column];
        }

        public int Row { get => _grid.GetLength(0); }

        public int Column { get => _grid.GetLength(1); }

        public TValue this[int row, int column]
        {
            get
            {
                if (row >= 0 && row < Row && column >= 0 && column < Column)
                {
                    return _grid[row, column];
                }

                throw new FrameworkException($"Grid index ({row}, {column}) out of range ({Row}, {Column})");
            }

            set
            {
                if (row >= 0 && row < Row && column >= 0 && column < Column)
                {
                    _grid[row, column] = value;
                    return;
                }

                throw new FrameworkException($"Grid index ({row}, {column}) out of range ({Row}, {Column})");
            }
        }

        public void Fill(TValue value)
        {
            for (var i = 0; i < Row; i++)
            {
                for (var j = 0; j < Column; j++)
                {
                    _grid[i, j] = value;
                }
            }
        }

        public void Fill(Func<int, int, TValue> onFill)
        {
            for (var i = 0; i < Row; i++)
            {
                for (var j = 0; j < Column; j++)
                {
                    _grid[i, j] = onFill(i, j);
                }
            }
        }

        public void Resize(int row, int column, Func<int, int, TValue> onAdd)
        {
            var newGrid = new TValue[row, column];

            var minRow    = Mathf.Min(Row, row);
            var minColumn = Mathf.Min(Column, column);

            for (var i = 0; i < minRow; i++)
            {
                for (var j = 0; j < minColumn; j++)
                {
                    newGrid[i, j] = _grid[i, j];
                }

                // column addition
                for (int j = minColumn; j < column; j++)
                {
                    newGrid[i, j] = onAdd(i, j);
                }
            }

            // row addition
            for (var i = minRow; i < row; i++)
            {
                for (int j = 0; j < column; j++)
                {
                    newGrid[i, j] = onAdd(i, j);
                }
            }

            // 清空之前的 grid
            Fill(default(TValue));

            _grid = newGrid;
        }

        public void ForEach(Action<int, int, TValue> each)
        {
            for (var i = 0; i < Row; i++)
            {
                for (var j = 0; j < Column; j++)
                {
                    each(i, j, _grid[i, j]);
                }
            }
        }

        public void ForEach(Action<TValue> each)
        {
            for (var i = 0; i < Row; i++)
            {
                for (var j = 0; j < Column; j++)
                {
                    each(_grid[i, j]);
                }
            }
        }

        public void Clear(Action<TValue> cleanUpItem = null)
        {
            for (var i = 0; i < Row; i++)
            {
                for (var j = 0; j < Column; j++)
                {
                    cleanUpItem?.Invoke(_grid[i, j]);
                    _grid[i, j] = default(TValue);
                }
            }

            _grid = null;
        }

        // 实现 IEnumerable<TValue> 接口
        public IEnumerator<KeyValuePair<Vector2Int, TValue>> GetEnumerator()
        {
            for (var i = 0; i < Row; i++)
            {
                for (var j = 0; j < Column; j++)
                {
                    yield return new KeyValuePair<Vector2Int, TValue>(
                        new Vector2Int(i, j),
                        _grid[i, j]
                    );
                }
            }
        }

        // 实现 IEnumerable 接口
        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}