// ------------------------------------------------------------
// @file       0.SystemObjectExtension.cs
// @brief
// @author     zheliku
// @Modified   2024-10-16 17:10:39
// @Copyright  Copyright (c) 2024, zheliku
// ------------------------------------------------------------

namespace Framework.Toolkits.FluentAPI
{
    using System;

    /// <summary>
    /// 针对 System.Object 提供的链式扩展，理论上任何对象都可以使用
    /// </summary>
    public static class SystemObjectExtension
    {
        /// <summary>
        /// 将自己传到 Action 委托中
        /// </summary>
        /// <example> <code>
        /// <![CDATA[
        /// new GameObject()
        ///     .Self(gameObj => gameObj.name = "Enemy")
        ///     .Self(gameObj =>
        ///     {
        ///         Debug.Log(gameObj.name);
        ///     });
        /// ]]>
        /// </code> </example>
        public static T Self<T>(this T self, Action<T> onDo)
        {
            onDo?.Invoke(self);
            return self;
        }

        /// <summary>
        /// 将自己传到 <see cref="Func{T,T}" /> 委托中，然后返回自己。
        /// </summary>
        /// <example> <code>
        /// <![CDATA[
        /// new GameObject()
        ///     .Self(gameObj => gameObj.name = "Enemy")
        ///     .Self(gameObj =>
        ///     {
        ///         Debug.Log(gameObj.name);
        ///     });"
        /// ]]>
        /// </code> </example>
        public static T Self<T>(this T self, Func<T, T> onDo)
        {
            return onDo.Invoke(self);
        }

        /// <summary>
        /// 判断是否为空
        /// </summary>
        /// <example> <code>
        /// <![CDATA[
        /// var simpleObject = new object();
        /// if (simpleObject.IsNull()) // simpleObject == null
        /// {
        ///     // do sth
        /// }
        /// ]]>
        /// </code> </example>
        public static bool IsNull<T>(this T selfObj) where T : class
        {
            return null == selfObj;
        }
        
        /// <summary>
        /// 判断是否不为空
        /// </summary>
        /// <example> <code>
        /// <![CDATA[
        /// var simpleObject = new object();
        /// if (simpleObject.IsNotNull()) // simpleObject == null
        /// {
        ///     // do sth
        /// }
        /// ]]>
        /// </code> </example>
        public static bool IsNotNull<T>(this T selfObj) where T : class
        {
            return null != selfObj;
        }
        
        /// <summary>
        /// 转型
        /// </summary>
        /// <example> <code>
        /// <![CDATA[
        /// int a = 10;
        /// Debug.Log(a.As<float>()) // 10
        /// ]]>
        /// </code> </example>
        public static T As<T>(this object selfObj) where T : class
        {
            return selfObj as T;
        }
    }
}