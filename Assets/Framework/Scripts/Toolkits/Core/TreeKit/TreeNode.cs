// ------------------------------------------------------------
// @file       TreeNode.cs
// @brief
// @author     zheliku
// @Modified   2025-02-26 23:02:16
// @Copyright  Copyright (c) 2025, zheliku
// ------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;

namespace Framework.Toolkits.TreeKit
{
    /// <summary>
    /// 树节点实现类
    /// </summary>
    /// <typeparam name="T">数据类型</typeparam>
    public class TreeNode<T>
    {
        // 当前节点的数据
        public T Data { get; }

        // 父节点
        public TreeNode<T> Parent { get; set; }

        // 子节点集合
        private readonly List<TreeNode<T>> _children = new List<TreeNode<T>>();

        /// <summary>
        /// 根节点
        /// </summary>
        public TreeNode<T> Root
        {
            get
            {
                var current = this;
                while (current.Parent != null)
                {
                    current = current.Parent;
                }
                return current;
            }
        }

        /// <summary>
        /// 获取以当前节点为根的子树中的节点总数
        /// </summary>
        public int Count
        {
            get
            {
                int count = 1; // 当前节点
                foreach (var child in Children)
                {
                    count += child.Count; // 递归计算子节点的节点数
                }
                return count;
            }
        }

        /// <summary>
        /// 祖先节点集合（按路径距离升序）
        /// </summary>
        public IEnumerable<TreeNode<T>> Ancestors
        {
            get
            {
                var current = this;
                while (current.Parent != null)
                {
                    yield return current.Parent;
                    current = current.Parent;
                }
            }
        }

        /// <summary>
        /// 子节点集合
        /// </summary>
        public List<TreeNode<T>> Children
        {
            get => _children;
        }

        /// <summary>
        /// 后代节点集合（深度优先先序遍历）
        /// </summary>
        public IEnumerable<TreeNode<T>> Descendants
        {
            get
            {
                yield return this;
                foreach (var child in _children)
                {
                    foreach (var descendant in child.Descendants)
                    {
                        yield return descendant;
                    }
                }
            }
        }

        /// <summary>
        /// 兄弟节点集合（不包括自身节点）
        /// </summary>
        public IEnumerable<TreeNode<T>> Siblings
        {
            get
            {
                if (Parent == null)
                    return Enumerable.Empty<TreeNode<T>>();

                return Parent.Children.Where(child => child != this);
            }
        }

        /// <summary>
        /// 在兄弟节点中的排行
        /// </summary>
        public int IndexOfSiblings
        {
            get
            {
                if (Parent == null)
                    return 0;

                var siblings = Parent.Children.ToList();
                return siblings.IndexOf(this);
            }
        }

        /// <summary>
        /// 节点的层
        /// </summary>
        public int Level
        {
            get
            {
                int level   = 0;
                var current = this;
                while (current.Parent != null)
                {
                    level++;
                    current = (TreeNode<T>) current.Parent;
                }
                return level;
            }
        }

        /// <summary>
        /// 节点（以当前节点为根的子树）的高度
        /// </summary>
        public int Height
        {
            get
            {
                if (!_children.Any())
                    return 0;

                return _children.Max(child => child.Height) + 1;
            }
        }

        /// <summary>
        /// 节点的度
        /// </summary>
        public int Degree
        {
            get => _children.Count;
        }

        /// <summary>
        /// 树（以当前节点为根的子树）的所有节点中度最大的节点的度
        /// </summary>
        public int MaxDegreeOfTree
        {
            get
            {
                int maxDegree = Degree;
                foreach (var child in _children)
                {
                    maxDegree = Math.Max(maxDegree, child.MaxDegreeOfTree);
                }
                return maxDegree;
            }
        }

        /// <summary>
        /// 当前节点是否是根节点
        /// </summary>
        public bool IsRoot
        {
            get => Parent == null;
        }

        /// <summary>
        /// 当前节点是否是叶子节点
        /// </summary>
        public bool IsLeaf
        {
            get => !_children.Any();
        }

        /// <summary>
        /// 当前节点是否有子节点
        /// </summary>
        public bool HasChild
        {
            get => _children.Any();
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="data">节点数据</param>
        public TreeNode(T data)
        {
            Data = data;
        }

        /// <summary>
        /// 添加子节点
        /// </summary>
        /// <param name="child">子节点</param>
        public void AddChild(TreeNode<T> child)
        {
            if (child.Parent != null)
                throw new InvalidOperationException("节点已有一个父节点");

            child.Parent = this;
            _children.Add(child);
        }

        /// <summary>
        /// 添加子节点
        /// </summary>
        /// <param name="childData">子节点数据</param>
        /// <param name="childAction">对子节点执行的方法</param>
        public TreeNode<T> AddChild(T childData, Action<TreeNode<T>> childAction = null)
        {
            var child = new TreeNode<T>(childData)
            {
                Parent = this
            };

            _children.Add(child);
            childAction?.Invoke(child);
            return child;
        }

        // 递归遍历树
        public void Traverse(Action<T> action)
        {
            action(Data);
            foreach (var child in Children)
            {
                child.Traverse(action);
            }
        }

        /// <summary>
        /// 以当前节点为根返回树形排版的结构字符串
        /// </summary>
        /// <param name="formatter">数据对象格式化器</param>
        /// <param name="convertToSingleLine">处理掉换行符变成单行文本</param>
        /// <returns></returns>
        public string Serialize(Func<T, string> formatter, bool convertToSingleLine = false)
        {
            var result = new List<string>();
            BuildTreeString(this, "", true, result, formatter);
            var treeString = string.Join(Environment.NewLine, result);

            if (convertToSingleLine)
            {
                treeString = treeString.Replace(Environment.NewLine, " ");
            }

            return treeString;
        }

        public static TreeNode<T> Deserialize(string data, Func<string, T> parser, bool isSingleLine = false)
        {
            if (string.IsNullOrEmpty(data))
                return null;

            // 如果是单行文本，先还原为多行文本
            if (isSingleLine)
            {
                data = data.Replace("+- ", Environment.NewLine + "+- ");
            }

            // 按行分割字符串
            var lines = data.Split(new[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);
            if (lines.Length == 0)
                return null;

            // 解析根节点
            var rootLine = lines[0];
            var rootData = rootLine.Substring(rootLine.IndexOf("+- ", StringComparison.Ordinal) + 3); // 提取根节点数据
            var root     = new TreeNode<T>(parser(rootData));

            // 使用栈来维护当前节点的层级关系
            var stack = new Stack<(TreeNode<T> Node, int Level)>();
            stack.Push((root, 0));

            for (int i = 1; i < lines.Length; i++)
            {
                var line     = lines[i];
                var indent   = line.TakeWhile(c => c == ' ' || c == '|').Count();                 // 计算缩进长度
                var nodeData = line.Substring(line.IndexOf("+- ", StringComparison.Ordinal) + 3); // 提取节点数据

                // 创建新节点
                var newNode = new TreeNode<T>(parser(nodeData));

                // 找到当前节点的父节点
                while (stack.Count > 0 && stack.Peek().Level >= indent)
                {
                    stack.Pop();
                }

                if (stack.Count > 0)
                {
                    var parentNode = stack.Peek().Node;
                    parentNode.AddChild(newNode);
                }

                // 将新节点压入栈
                stack.Push((newNode, indent));
            }

            return root;
        }

        /// <summary>
        /// 递归构建树形结构的字符串表示
        /// </summary>
        /// <param name="node">当前节点</param>
        /// <param name="indent">缩进</param>
        /// <param name="isLast">是否是最后一个子节点</param>
        /// <param name="result">结果列表</param>
        /// <param name="formatter">格式化器</param>
        private void BuildTreeString(TreeNode<T> node, string indent, bool isLast, List<string> result, Func<T, string> formatter)
        {
            result.Add($"{indent}+- {formatter(node.Data)}");

            indent += isLast ? "   " : "|  ";

            var children = node.Children.ToList();
            for (int i = 0; i < children.Count; i++)
            {
                BuildTreeString(children[i], indent, i == children.Count - 1, result, formatter);
            }
        }
    }
}