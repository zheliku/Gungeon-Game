// ------------------------------------------------------------
// @file       UnRegisterTrigger.cs
// @brief
// @author     zheliku
// @Modified   2024-10-04 16:10:29
// @Copyright  Copyright (c) 2024, zheliku
// ------------------------------------------------------------

namespace Framework.Core
{
    using System.Collections.Generic;
    using Sirenix.OdinInspector;
    using UnityEngine;

    /// <summary>
    /// 注销触发器基类
    /// </summary>
    public abstract class UnRegisterTrigger : MonoBehaviour
    {
        [ShowInInspector]
        private readonly HashSet<IUnRegister> _unRegisters = new HashSet<IUnRegister>(); // 存储 IUnRegister 接口的实现类

        /// <summary>
        /// 添加注销器
        /// </summary>
        /// <param name="unRegister">待添加的注销器</param>
        /// <returns>添加后的注销器</returns>
        public IUnRegister AddUnRegister(IUnRegister unRegister)
        {
            _unRegisters.Add(unRegister);
            return unRegister;
        }

        /// <summary>
        /// 移除注销器
        /// </summary>
        /// <param name="unRegister">待移除的注销器</param>
        public void RemoveUnRegister(IUnRegister unRegister)
        {
            _unRegisters.Remove(unRegister);
        }

        /// <summary>
        /// 触发所有注销器
        /// </summary>
        public void UnRegister()
        {
            foreach (var unRegister in _unRegisters)
            {
                unRegister.UnRegister();
            }

            // 清空 HashSet
            _unRegisters.Clear();
        }
    }
}