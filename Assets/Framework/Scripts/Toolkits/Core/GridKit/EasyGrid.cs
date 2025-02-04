﻿// ------------------------------------------------------------
// @file       Grid.cs
// @brief
// @author     zheliku
// @Modified   2024-11-01 12:11:43
// @Copyright  Copyright (c) 2024, zheliku
// ------------------------------------------------------------

namespace Framework.Toolkits.GridKit
{
    using System;
    using Framework.Core;
    using Sirenix.OdinInspector;
    using UnityEngine;

    [HideReferenceObjectPicker]
    public class EasyGrid<TValue>
    {
        [ShowInInspector]
        [TableMatrix(Transpose = true)]
        private TValue[,] _grid;

        public EasyGrid(int row, int column)
        {
            _grid = new TValue[row, column];
        }

        public TValue this[int row, int column]
        {
            get
            {
                if (row >= 0 && row < _grid.GetLength(0) && column >= 0 && column < _grid.GetLength(1))
                {
                    return _grid[row, column];
                }

                throw new FrameworkException($"Grid index ({row}, {column}) out of range ({_grid.GetLength(0)}, {_grid.GetLength(1)}");
            }

            set
            {
                if (row >= 0 && row < _grid.GetLength(0) && column >= 0 && column < _grid.GetLength(1))
                {
                    _grid[row, column] = value;
                    return;
                }
                
                throw new FrameworkException($"Grid index ({row}, {column}) out of range ({_grid.GetLength(0)}, {_grid.GetLength(1)}");
            }
        }

        public void Fill(TValue value)
        {
            for (var i = 0; i < _grid.GetLength(0); i++)
            {
                for (var j = 0; j < _grid.GetLength(1); j++)
                {
                    _grid[i, j] = value;
                }
            }
        }
        
        public void Fill(Func<int, int, TValue> onFill)
        {
            for (var i = 0; i < _grid.GetLength(0); i++)
            {
                for (var j = 0; j < _grid.GetLength(1); j++)
                {
                    _grid[i, j] = onFill(i, j);
                }
            }
        }

        public void Resize(int row, int column, Func<int, int, TValue> onAdd)
        {
            var newGrid = new TValue[row, column];

            var minRow    = Mathf.Min(_grid.GetLength(0), row);
            var minColumn = Mathf.Min(_grid.GetLength(1), column);
            
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
            for (var i = 0; i < _grid.GetLength(0); i++)
            {
                for (var j = 0; j < _grid.GetLength(1); j++)
                {
                    each(i, j, _grid[i, j]);
                }
            }
        }

        public void ForEach(Action<TValue> each)
        {
            for (var i = 0; i < _grid.GetLength(0); i++)
            {
                for (var j = 0; j < _grid.GetLength(1); j++)
                {
                    each(_grid[i, j]);
                }
            }
        }

        public void Clear(Action<TValue> cleanUpItem = null)
        {
            for (var i = 0; i < _grid.GetLength(0); i++)
            {
                for (var j = 0; j < _grid.GetLength(1); j++)
                {
                    cleanUpItem?.Invoke(_grid[i, j]);
                    _grid[i, j] = default(TValue);
                }
            }

            _grid = null;
        }
    }
}