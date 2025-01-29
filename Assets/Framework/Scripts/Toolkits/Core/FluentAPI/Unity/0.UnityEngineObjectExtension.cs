// ------------------------------------------------------------
// @file       0.UnityEngineObjectExtension.cs
// @brief
// @author     zheliku
// @Modified   2024-10-18 00:10:02
// @Copyright  Copyright (c) 2024, zheliku
// ------------------------------------------------------------

namespace Framework.Toolkits.FluentAPI
{
    using UnityEngine;

    /// <summary>
    /// 针对 <see cref="UnityEngine.Object"/> 提供的链式扩展
    /// </summary>
    /// <example> <code>
    /// <![CDATA[
    /// var gameObject = new GameObject();
    /// --------------------------------------------------------------
    /// gameObject.Instantiate()
    ///           .Name("ExtensionExample")
    ///           .Destroy();
    /// gameObject.Instantiate()
    ///           .DestroyGracefully();
    /// gameObject.Instantiate()
    ///           .Destroy(1.0f);
    /// gameObject.Instantiate()
    ///           .DestroyGracefully(1.0f);
    /// --------------------------------------------------------------
    /// gameObject
    ///    .Self(selfObj => Debug.Log(selfObj.name))
    ///    .Name("TestObj")
    ///    .Self(selfObj => Debug.Log(selfObj.name))
    ///    .Name("ExtensionExample")
    ///    .DontDestroyOnLoad();
    /// ]]>
    /// </code> </example>
    public static class UnityEngineObjectExtension
    {
        /// <summary>
        /// <see cref="Object.Instantiate(Object)"/> 的简单链式封装
        /// </summary>
        /// <example> <code>
        /// <![CDATA[
        /// prefab.Instantiate();
        /// ]]>
        /// </code> </example>
        public static T Instantiate<T>(this T selfObj, bool keepName = false) where T : Object
        {
            var instance = Object.Instantiate(selfObj);
            if (keepName)
            {
                instance.name = selfObj.name;
            }
            return instance;
        }
        
        /// <summary>
        /// <see cref="Object.Instantiate(Object, Vector3, Quaternion)"/> 的简单链式封装
        /// </summary>
        /// <example> <code>
        /// <![CDATA[
        /// prefab.Instantiate(Vector3.zero);
        /// ]]>
        /// </code> </example>
        public static T Instantiate<T>(this T selfObj, Vector3 position)
            where T : Object
        {
            return Object.Instantiate(selfObj, position, Quaternion.identity);
        }

        /// <summary>
        /// <see cref="Object.Instantiate(Object, Vector3, Quaternion)"/> 的简单链式封装
        /// </summary>
        /// <example> <code>
        /// <![CDATA[
        /// prefab.Instantiate(Vector3.zero, Quaternion.identity);
        /// ]]>
        /// </code> </example>
        public static T Instantiate<T>(this T selfObj, Vector3 position, Quaternion rotation)
            where T : Object
        {
            return Object.Instantiate(selfObj, position, rotation);
        }
        
        /// <summary>
        /// <see cref="Object.Instantiate(Object, Vector3, Quaternion, Transform)"/> 的简单链式封装
        /// </summary>
        /// <example> <code>
        /// <![CDATA[
        /// prefab.Instantiate(Vector3.zero, transformRoot);
        /// ]]>
        /// </code> </example>
        public static T Instantiate<T>(
            this T     selfObj,
            Vector3    position,
            Transform  parent)
            where T : Object
        {
            return Object.Instantiate(selfObj, position, Quaternion.identity, parent);
        }

        /// <summary>
        /// <see cref="Object.Instantiate(Object, Vector3, Quaternion, Transform)"/> 的简单链式封装
        /// </summary>
        /// <example> <code>
        /// <![CDATA[
        /// prefab.Instantiate(Vector3.zero, Quaternion.identity, transformRoot);
        /// ]]>
        /// </code> </example>
        public static T Instantiate<T>(
            this T     selfObj,
            Vector3    position,
            Quaternion rotation,
            Transform  parent)
            where T : Object
        {
            return Object.Instantiate(selfObj, position, rotation, parent);
        }

        /// <summary>
        /// <see cref="Object.Instantiate(Object, Transform, bool)"/> 的简单链式封装
        /// </summary>
        /// <example> <code>
        /// <![CDATA[
        /// prefab.Instantiate(transformRoot, true);
        /// ]]>
        /// </code> </example>
        public static T Instantiate<T>(this T selfObj, Transform parent, bool worldPositionStays = false)
            where T : Object
        {
            return (T) Object.Instantiate((Object) selfObj, parent, worldPositionStays);
        }

        /// <example> <code>
        /// <![CDATA[
        /// prefab.Instantiate(componentRoot, true);
        /// ]]>
        /// </code> </example>
        public static T Instantiate<T>(this T selfObj, Component parent, bool worldPositionStays = false)
            where T : Object
        {
            return (T) Object.Instantiate((Object) selfObj, parent.transform, worldPositionStays);
        }
        
        /// <example> <code>
        /// <![CDATA[
        /// prefab.Instantiate(componentRoot, true);
        /// ]]>
        /// </code> </example>
        public static T Instantiate<T>(this T selfObj, GameObject parent, bool worldPositionStays = false)
            where T : Object
        {
            return (T) Object.Instantiate((Object) selfObj, parent.transform, worldPositionStays);
        }

        /// <summary>
        /// 设置名字
        /// </summary>
        /// <example> <code>
        /// <![CDATA[
        /// scriptableObject.Name("LevelData");
        /// Debug.Log(scriptableObject.name); // LevelData
        /// ]]>
        /// </code> </example>
        public static T Name<T>(this T selfObj, string name) where T : Object
        {
            selfObj.name = name;
            return selfObj;
        }

        /// <summary>
        /// <see cref="Object.Destroy(Object)"/> 简单链式封装
        /// </summary>
        /// <example> <code>
        /// <![CDATA[
        /// new GameObject().Destroy()
        /// ]]>
        /// </code> </example>
        public static void Destroy<T>(this T selfObj) where T : Object
        {
            Object.Destroy(selfObj);
        }

        /// <summary>
        /// <see cref="Object.Destroy(Object)"/> 简单链式封装，不会报异常（但不好调试）
        /// </summary>
        /// <example> <code>
        /// <![CDATA[
        /// GameObject gameObj = null;
        /// gameObj.DestroyGracefully(); // not throw null exception
        /// ]]>
        /// </code> </example>
        public static T DestroyGracefully<T>(this T selfObj) where T : Object
        {
            if (selfObj)
            {
                Object.Destroy(selfObj);
            }

            return selfObj;
        }

        /// <summary>
        /// <see cref="Object.Destroy(Object, float)"/> 简单链式封装
        /// </summary>
        /// <example> <code>
        /// <![CDATA[
        /// new GameObject().Destroy(5);
        /// ]]>
        /// </code> </example>
        public static T Destroy<T>(this T selfObj, float delayTime) where T : Object
        {
            Object.Destroy(selfObj, delayTime);
            return selfObj;
        }

        /// <summary>
        /// <see cref="Object.Destroy(Object, float)"/> 简单链式封装，不会报异常（但不好调试）
        /// </summary>
        /// <example> <code>
        /// <![CDATA[
        /// GameObject gameObj = null;
        /// gameObj.DestroyGracefully(5); // not throw null exception
        /// ]]>
        /// </code> </example>
        public static T DestroyGracefully<T>(this T selfObj, float delayTime) where T : Object
        {
            if (selfObj)
            {
                Object.Destroy(selfObj, delayTime);
            }

            return selfObj;
        }

        /// <summary>
        /// <see cref="Object.DontDestroyOnLoad(Object)"/> 简单链式封装
        /// </summary>
        /// <example> <code>
        /// <![CDATA[
        /// new GameObject().DontDestroyOnLoad();
        /// ]]>
        /// </code> </example>
        public static T DontDestroyOnLoad<T>(this T selfObj) where T : Object
        {
            Object.DontDestroyOnLoad(selfObj);
            return selfObj;
        }

        public static T DebugLog<T>(this T selfObj)
        {
            Debug.Log(selfObj);
            return selfObj;
        }

        public static T DebugLogWarning<T>(this T selfObj)
        {
            Debug.LogWarning(selfObj);
            return selfObj;
        }

        public static T DebugLogError<T>(this T selfObj)
        {
            Debug.LogError(selfObj);
            return selfObj;
        }
    }
}