// ------------------------------------------------------------
// @file       NonPublicObjectFactory.cs
// @brief
// @author     zheliku
// @Modified   2024-10-23 21:10:11
// @Copyright  Copyright (c) 2024, zheliku
// ------------------------------------------------------------

namespace Framework.Toolkits.PoolKit
{
    using System;
    using System.Reflection;

    /// <summary>
    /// 没有公共构造函数的对象工厂：相关对象只能通过反射获得
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class NonPublicObjectFactory<T> : IObjectFactory<T> where T : class
    {
        public T Create()
        {
            var ctors = typeof(T).GetConstructors(BindingFlags.Instance | BindingFlags.NonPublic);
            var ctor  = Array.Find(ctors, c => c.GetParameters().Length == 0);

            if (ctor == null)
            {
                throw new Exception("Non-Public Constructor() not found! in " + typeof(T) + "\n 在没有找到非 public 的构造方法");
            }

            return ctor.Invoke(null) as T;
        }
    }
}