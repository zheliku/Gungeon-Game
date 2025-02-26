// ------------------------------------------------------------
// @file       Tree.cs
// @brief
// @author     zheliku
// @Modified   2025-02-27 00:02:22
// @Copyright  Copyright (c) 2025, zheliku
// ------------------------------------------------------------

namespace Framework.Toolkits.TreeKit
{
    using System;
    using System.Collections.Generic;

    public class Tree<T>
    {
        // 根节点
        public TreeNode<T> Root { get; set; }

        // 构造函数
        public Tree(T rootData)
        {
            Root = new TreeNode<T>(rootData);
        }

        public Tree(TreeNode<T> node)
        {
            Root = node;
        }

        // 遍历整棵树（深度优先先序遍历）
        public void Traverse(Action<T> action)
        {
            Root.Traverse(action);
        }

        // 查找节点（深度优先搜索）
        public TreeNode<T> Find(Func<T, bool> predicate)
        {
            return FindNode(Root, predicate);
        }

        // 递归查找节点
        private TreeNode<T> FindNode(TreeNode<T> node, Func<T, bool> predicate)
        {
            if (predicate(node.Data))
                return node;

            foreach (var child in node.Children)
            {
                var result = FindNode(child, predicate);
                if (result != null)
                    return result;
            }

            return null;
        }

        // 删除节点及其子树
        public void Remove(TreeNode<T> node)
        {
            if (node == Root)
            {
                Root = null;
                return;
            }

            var parent = FindParent(Root, node);
            if (parent != null)
            {
                parent.Children.Remove(node);
            }
        }

        // 查找父节点
        private TreeNode<T> FindParent(TreeNode<T> current, TreeNode<T> target)
        {
            foreach (var child in current.Children)
            {
                if (child == target)
                    return current;

                var result = FindParent(child, target);
                if (result != null)
                    return result;
            }

            return null;
        }

        // 获取树的高度
        public int Height
        {
            get => Root?.Height ?? 0;
        }

        // 获取树的节点数量
        public int Count
        {
            get => Root?.Count ?? 0;
        }

        // 层级遍历（广度优先遍历）
        public void LevelOrderTraverse(Action<T> action)
        {
            if (Root == null)
                return;

            var queue = new Queue<TreeNode<T>>();
            queue.Enqueue(Root);

            while (queue.Count > 0)
            {
                var node = queue.Dequeue();
                action(node.Data);

                foreach (var child in node.Children)
                {
                    queue.Enqueue(child);
                }
            }
        }

        // 深度优先遍历（前序遍历）
        public void PreOrderTraverse(Action<T> action)
        {
            Root?.Traverse(action);
        }

        // 深度优先遍历（后序遍历）
        public void PostOrderTraverse(Action<T> action)
        {
            PostOrderTraverse(Root, action);
        }

        private void PostOrderTraverse(TreeNode<T> node, Action<T> action)
        {
            if (node == null)
                return;

            foreach (var child in node.Children)
            {
                PostOrderTraverse(child, action);
            }

            action(node.Data);
        }

        // 复制整棵树
        public Tree<T> Clone()
        {
            var newTree = new Tree<T>(Root.Data);
            CloneNode(Root, newTree.Root);
            return newTree;
        }

        private void CloneNode(TreeNode<T> source, TreeNode<T> target)
        {
            foreach (var child in source.Children)
            {
                var newChild = new TreeNode<T>(child.Data);
                target.AddChild(newChild);
                CloneNode(child, newChild);
            }
        }

        // 将树转换为字符串（序列化）
        public string Serialize(Func<T, string> formatter, bool convertToSingleLine = false)
        {
            return Root?.Serialize(formatter, convertToSingleLine) ?? string.Empty;
        }
    }
}