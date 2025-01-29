// ------------------------------------------------------------
// @file       Singleton.cs
// @brief
// @author     zheliku
// @Modified   2024-10-20 17:10:13
// @Copyright  Copyright (c) 2024, zheliku
// ------------------------------------------------------------

namespace Framework.Toolkits.SingletonKit
{
    using System;

    public abstract class Singleton<TSingleton> : ISingleton where TSingleton : Singleton<TSingleton>
    {
        /// <summary>
        /// 静态 Lazy
        /// </summary>
        protected static readonly Lazy<TSingleton> _LAZY_HOLDER = new Lazy<TSingleton>(SingletonCreator.CreateSingleton<TSingleton>);

        /// <summary>
        /// 静态属性
        /// </summary>
        public static TSingleton Instance
        {
            get => _LAZY_HOLDER.Value;
        }

        /// <summary>
        /// 单例初始化方法
        /// </summary>
        public virtual void OnSingletonInit()
        { }

        /// <summary>
        /// 资源释放
        /// </summary>
        public virtual void Dispose()
        { }
    }
}