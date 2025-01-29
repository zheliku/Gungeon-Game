// ------------------------------------------------------------
// @file       MonoSingletonPathAttribute.cs
// @brief
// @author     zheliku
// @Modified   2024-10-20 18:10:48
// @Copyright  Copyright (c) 2024, zheliku
// ------------------------------------------------------------

namespace Framework.Toolkits.SingletonKit
{
    using System;

    /// <summary>
    /// 修改 MonoSingleton 或者 MonoSingletonProperty 的 gameObject 名字和路径
    /// </summary>
    [AttributeUsage(AttributeTargets.Class)] // 这个特性只能标记在 Class 上
    public class MonoSingletonPathAttribute : Attribute
    {
        public MonoSingletonPathAttribute(string pathInHierarchy)
        {
            PathInHierarchy = pathInHierarchy;
        }

        public string PathInHierarchy { get; private set; }
    }
}