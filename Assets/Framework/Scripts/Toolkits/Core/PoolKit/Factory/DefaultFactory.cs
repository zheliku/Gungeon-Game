// ------------------------------------------------------------
// @file       DefaultFactory.cs
// @brief
// @author     zheliku
// @Modified   2024-10-23 21:10:16
// @Copyright  Copyright (c) 2024, zheliku
// ------------------------------------------------------------

namespace Framework.Toolkits.PoolKit
{
    /// <summary>
    /// 默认对象工厂：相关对象是通过 New 出来的
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class DefaultObjectFactory<T> : IObjectFactory<T> where T : new()
    {
        public T Create()
        {
            return new T();
        }
    }
}