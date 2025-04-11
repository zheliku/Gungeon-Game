// ------------------------------------------------------------
// @file       IOCContainer.cs
// @brief
// @author     zheliku
// @Modified   2024-10-05 10:10:21
// @Copyright  Copyright (c) 2024, zheliku
// ------------------------------------------------------------

namespace Framework.Core
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Sirenix.OdinInspector;
    using UnityEngine;

    /// <summary>
    /// IOC 管理器
    /// </summary>
    public sealed class IOCContainer
    {
        [ShowInInspector]
        private Dictionary<Type, object> _instances = new Dictionary<Type, object>();

        /// <summary>
        /// 注册实例
        /// </summary>
        /// <param name="instance">实例</param>
        /// <typeparam name="T">实例类型</typeparam>
        public void Register<T>(T instance)
        {
            var key = typeof(T);

            _instances[key] = instance; // 存在，则覆盖；否则，添加
        }

        /// <summary>
        /// 获取实例
        /// </summary>
        /// <typeparam name="T">类型</typeparam>
        /// <returns>实例</returns>
        public T Get<T>() where T : class
        {
            var key = typeof(T);

            if (_instances.TryGetValue(key, out var retInstance))
            {
                return retInstance as T;
            }

            Debug.Log("Can't find " + key.Name + " in IOCContainer");

            return null;
        }

        /// <summary>
        /// 依据类型获取所有实例
        /// </summary>
        /// <typeparam name="T">类型</typeparam>
        /// <returns>可枚举的所有实例</returns>
        public IEnumerable<T> GetInstancesByType<T>()
        {
            var type = typeof(T);
            return _instances.Values.Where(instance => type.IsInstanceOfType(instance)).Cast<T>(); // 派生类也会被筛选到
        }

        /// <summary>
        /// 清空容器
        /// </summary>
        public void Clear()
        {
            _instances.Clear();
        }
    }
}