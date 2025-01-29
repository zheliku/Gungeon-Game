// ------------------------------------------------------------
// @file       CustomObjectFactory.cs
// @brief
// @author     zheliku
// @Modified   2024-10-23 21:10:01
// @Copyright  Copyright (c) 2024, zheliku
// ------------------------------------------------------------

namespace Framework.Toolkits.PoolKit
{
    using System;
    using Sirenix.OdinInspector;

    /// <summary>
    /// 自定义对象工厂：相关对象是 自己定义 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class CustomObjectFactory<T> : IObjectFactory<T>
    {
        [ShowInInspector]
        protected readonly Func<T> _factoryMethod;

        public CustomObjectFactory(Func<T> factoryMethod)
        {
            _factoryMethod = factoryMethod;
        }


        public T Create()
        {
            return _factoryMethod();
        }
    }
}