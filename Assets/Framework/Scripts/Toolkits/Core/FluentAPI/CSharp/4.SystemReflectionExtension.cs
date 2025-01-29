// ------------------------------------------------------------
// @file       4.SystemReflectionExtension.cs
// @brief
// @author     zheliku
// @Modified   2024-10-18 00:10:15
// @Copyright  Copyright (c) 2024, zheliku
// ------------------------------------------------------------

namespace Framework.Toolkits.FluentAPI
{
    using System;
    using System.Linq;
    using System.Reflection;

    /// <summary>
    /// 针对 System.Reflection 提供的链式扩展
    /// </summary>
    public static class SystemReflectionExtension
    {
        /// <summary>
        /// 通过 Type 创建 Instance
        /// </summary>
        /// <example> <code>
        /// <![CDATA[
        /// interface IA { }
        /// class A { }
        /// IA a = typeof(A).CreateInstance<IA>();
        /// ]]>
        /// </code> </example>
        public static T CreateInstance<T>(this Type self) where T : class
        {
            // 获取构造函数
            var ctors = self.GetConstructors(BindingFlags.Instance | BindingFlags.Public);

            // 获取无参构造函数
            var ctor = Array.Find(ctors, c => c.GetParameters().Length == 0);

            return ctor.Invoke(null) as T;
        }

        /// <summary>
        /// 通过反射调用私有方法
        /// </summary>
        /// <example> <code>
        /// <![CDATA[
        /// class A
        /// {
        ///     private void Say() { Debug.Log("I'm A!"); }
        /// }
        /// new A().ReflectionCallPrivateMethod(""Say""); // I'm A!
        /// ]]>
        /// </code> </example>
        public static object ReflectionCallPrivateMethod<T>(this T self, string methodName, params object[] args)
        {
            var methodInfo = typeof(T).GetMethod(methodName, BindingFlags.Instance | BindingFlags.NonPublic);

            return methodInfo?.Invoke(self, args);
        }

        /// <summary>
        /// 通过反射调用私有方法，有返回值
        /// </summary>
        /// <example> <code>
        /// <![CDATA[
        /// class A
        /// {
        ///     private bool Add(int a, int b) { return a + b; }
        /// }
        /// Debug.Log(new A().ReflectionCallPrivateMethod("Add", 1, 2)); // 3
        /// ]]>
        /// </code> </example>
        public static TReturnType ReflectionCallPrivateMethod<T, TReturnType>(this T self, string methodName, params object[] args)
        {
            return (TReturnType) self.ReflectionCallPrivateMethod(methodName, args);
        }

        /// <summary>
        /// 检查是否有指定的 Attribute，同时也支持 MethodInfo、PropertyInfo、FieldInfo
        /// </summary>
        /// <example> <code>
        /// <![CDATA[
        /// [DisplayName(""A Class"")]
        /// class A
        /// {
        ///     [DisplayName("A Number")]
        ///     public int Number;
        ///
        ///     [DisplayName("Is Complete?")]
        ///     private bool Complete => Number > 100;
        ///
        ///     [DisplayName("Say complete result?")]
        ///     public void SayComplete()
        ///     {
        ///         Debug.Log(Complete);
        ///     }
        /// }
        /// var aType = typeof(A);
        /// Debug.Log(aType.HasAttribute(typeof(DisplayNameAttribute)); // true
        /// Debug.Log(aType.HasAttribute<DisplayNameAttribute>()); // true
        /// ]]>
        /// </code> </example>
        public static bool HasAttribute<T>(this Type type, bool inherit = false) where T : Attribute
        {
            return type.GetCustomAttributes(typeof(T), inherit).Any();
        }

        /// <summary>
        /// 检查是否有指定的 Attribute
        /// </summary>
        public static bool HasAttribute(this Type type, Type attributeType, bool inherit = false)
        {
            return type.GetCustomAttributes(attributeType, inherit).Any();
        }

        /// <summary>
        /// 检查是否有指定的 Attribute
        /// </summary>
        public static bool HasAttribute<T>(this PropertyInfo prop, bool inherit = false) where T : Attribute
        {
            return prop.GetCustomAttributes(typeof(T), inherit).Any();
        }

        /// <summary>
        /// 检查是否有指定的 Attribute
        /// </summary>
        public static bool HasAttribute(this PropertyInfo prop, Type attributeType, bool inherit = false)
        {
            return prop.GetCustomAttributes(attributeType, inherit).Any();
        }

        /// <summary>
        /// 检查是否有指定的 Attribute
        /// </summary>
        public static bool HasAttribute<T>(this FieldInfo field, bool inherit = false) where T : Attribute
        {
            return field.GetCustomAttributes(typeof(T), inherit).Any();
        }

        /// <summary>
        /// 检查是否有指定的 Attribute
        /// </summary>
        public static bool HasAttribute(this FieldInfo field, Type attributeType, bool inherit)
        {
            return field.GetCustomAttributes(attributeType, inherit).Any();
        }

        /// <summary>
        /// 检查是否有指定的 Attribute
        /// </summary>
        public static bool HasAttribute<T>(this MethodInfo method, bool inherit = false) where T : Attribute
        {
            return method.GetCustomAttributes(typeof(T), inherit).Any();
        }

        /// <summary>
        /// 检查是否有指定的 Attribute
        /// </summary>
        public static bool HasAttribute(this MethodInfo method, Type attributeType, bool inherit = false)
        {
            return method.GetCustomAttributes(attributeType, inherit).Any();
        }

        /// <summary>
        /// 获取指定的 Attribute，同时也支持 MethodInfo、PropertyInfo、FieldInfo
        /// </summary>
        /// <example> <code>
        /// <![CDATA[
        /// [DisplayName(""A Class"")]
        /// class A
        /// {
        ///     [DisplayName("A Number")]
        ///     public int Number;
        ///
        ///     [DisplayName("Is Complete?")]
        ///     private bool Complete => Number > 100;
        ///
        ///     [DisplayName("Say complete result?")]
        ///     public void SayComplete()
        ///     {
        ///         Debug.Log(Complete);
        ///     }
        /// }
        /// var aType = typeof(A);
        /// Debug.Log(aType.GetAttribute(typeof(DisplayNameAttribute)); // DisplayNameAttribute
        /// Debug.Log(aType.GetAttribute<DisplayNameAttribute>()); // DisplayNameAttribute
        /// ]]>
        /// </code> </example>
        public static T GetAttribute<T>(this Type type, bool inherit = false) where T : Attribute
        {
            return type.GetCustomAttributes<T>(inherit).FirstOrDefault();
        }

        /// <summary>
        /// 获取指定的 Attribute
        /// </summary>
        public static object GetAttribute(this Type type, Type attributeType, bool inherit = false)
        {
            return type.GetCustomAttributes(attributeType, inherit).FirstOrDefault();
        }

        /// <summary>
        /// 获取指定的 Attribute
        /// </summary>
        public static T GetAttribute<T>(this MethodInfo method, bool inherit = false) where T : Attribute
        {
            return method.GetCustomAttributes<T>(inherit).FirstOrDefault();
        }

        /// <summary>
        /// 获取指定的 Attribute
        /// </summary>
        public static object GetAttribute(this MethodInfo method, Type attributeType, bool inherit = false)
        {
            return method.GetCustomAttributes(attributeType, inherit).FirstOrDefault();
        }

        /// <summary>
        /// 获取指定的 Attribute
        /// </summary>
        public static T GetAttribute<T>(this FieldInfo field, bool inherit = false) where T : Attribute
        {
            return field.GetCustomAttributes<T>(inherit).FirstOrDefault();
        }

        /// <summary>
        /// 获取指定的 Attribute
        /// </summary>
        public static object GetAttribute(this FieldInfo field, Type attributeType, bool inherit = false)
        {
            return field.GetCustomAttributes(attributeType, inherit).FirstOrDefault();
        }

        /// <summary>
        /// 获取指定的 Attribute
        /// </summary>
        public static T GetAttribute<T>(this PropertyInfo prop, bool inherit = false) where T : Attribute
        {
            return prop.GetCustomAttributes<T>(inherit).FirstOrDefault();
        }

        /// <summary>
        /// 获取指定的 Attribute
        /// </summary>
        public static object GetAttribute(this PropertyInfo prop, Type attributeType, bool inherit = false)
        {
            return prop.GetCustomAttributes(attributeType, inherit).FirstOrDefault();
        }
        
        /// <summary>
        /// 依据类型名称获取类型
        /// </summary>
        public static Type GetTypeByName(this string name) {
            var type = Type.GetType(name);
            if (type != null)
                return type;

            Assembly[] assembly = AppDomain.CurrentDomain.GetAssemblies();
            foreach (Assembly ass in assembly) {
                type = ass.GetType(name);
                if (type != null)
                    return type;
            }

            return null;
        }
    }
}