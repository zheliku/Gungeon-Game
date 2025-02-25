// ------------------------------------------------------------
// @file       SingletonCreator.cs
// @brief
// @author     zheliku
// @Modified   2024-10-20 17:10:35
// @Copyright  Copyright (c) 2024, zheliku
// ------------------------------------------------------------

namespace Framework.Toolkits.SingletonKit
{
    using System;
    using System.Reflection;
    using Framework.Core;
    using FluentAPI;
    using UnityEngine;
    using Object = UnityEngine.Object;

    /// <summary>
    /// 普通单例创建类
    /// </summary>
    internal static class SingletonCreator
    {
        public static TSingleton CreateSingleton<TSingleton>() where TSingleton : class, ISingleton
        {
            var type = typeof(TSingleton);

            // 获取全部私有构造函数
            var constructorInfos = type.GetConstructors(BindingFlags.Instance | BindingFlags.NonPublic);

            // 获取无参构造函数
            var ctor = Array.Find(constructorInfos, c => c.GetParameters().Length == 0);

            if (ctor == null)
            {
                throw new FrameworkException("Non-Public Constructor() not found! in " + type);
            }

            var instance = ctor.Invoke(null) as TSingleton;

            if (instance == null)
            {
                throw new FrameworkException("Create Singleton Failed! in " + type);
            }

            instance.OnSingletonInit();
            return instance;
        }

        /// <summary>
        /// 泛型方法：创建MonoBehaviour单例
        /// </summary>
        /// <typeparam name="TMonoSingleton"></typeparam>
        /// <returns></returns>
        public static TMonoSingleton CreateMonoSingleton<TMonoSingleton>() where TMonoSingleton : MonoBehaviour, ISingleton
        {
            // 判断 TMonoSingleton 实例存在的条件是否满足
            if (!Application.isPlaying)
                return null;

            var type = typeof(TMonoSingleton);

            // 判断当前场景中是否存在T实例
            var instance = Object.FindFirstObjectByType(type) as TMonoSingleton;
            if (instance != null)
            {
                instance.OnSingletonInit();
                // Object.DontDestroyOnLoad(instance.gameObject); 场景中的物体手动 DontDestroyOnLoad，因为必须满足为 root GameObject
                return instance;
            }

            // MemberInfo：获取有关成员属性的信息并提供对成员元数据的访问
            var memberInfo = type.As<MemberInfo>();

            // 获取 TMonoSingleton 类型自定义属性，并找到相关路径属性，利用该属性创建T实例
            var attributes = memberInfo.GetCustomAttributes(true);
            foreach (var attribute in attributes)
            {
                var defineAttribute = attribute as MonoSingletonPathAttribute;
                if (defineAttribute == null)
                {
                    continue;
                }

                instance = CreateComponentOnGameObject<TMonoSingleton>(defineAttribute.PathInHierarchy, true);
                
                // Object.DontDestroyOnLoad(instance.gameObject); 场景中的物体手动 DontDestroyOnLoad，因为必须满足为 root GameObject
                
                break;
            }

            // 如果还是无法找到 instance，则主动去创建同名 Obj，并挂载相关脚本组件
            if (instance == null)
            {
                var obj = new GameObject(typeof(TMonoSingleton).Name);
                Object.DontDestroyOnLoad(obj);
                instance = obj.AddComponent(typeof(TMonoSingleton)).As<TMonoSingleton>();
            }

            instance.OnSingletonInit();
            return instance;
        }

        /// <summary>
        /// 在 GameObject 上创建 T 组件（脚本）
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="path">路径（应该就是 Hierarchy 下的树结构路径）</param>
        /// <param name="dontDestroy">不要销毁 标签</param>
        /// <returns></returns>
        private static T CreateComponentOnGameObject<T>(string path, bool dontDestroy) where T : class
        {
            var obj = FindGameObject(path, true, dontDestroy);
            if (obj == null)
            {
                obj = new GameObject("Singleton of " + typeof(T).Name);
                if (dontDestroy)
                {
                    Object.DontDestroyOnLoad(obj);
                }
            }

            return obj.AddComponent(typeof(T)) as T;
        }

        /// <summary>
        /// 查找Obj（对于路径 进行拆分）
        /// </summary>
        /// <param name="path">路径</param>
        /// <param name="build">true</param>
        /// <param name="dontDestroy">不要销毁 标签</param>
        /// <returns></returns>
        private static GameObject FindGameObject(string path, bool build, bool dontDestroy)
        {
            if (string.IsNullOrEmpty(path))
            {
                return null;
            }

            var subPath = path.Split('/');
            if (subPath == null || subPath.Length == 0)
            {
                return null;
            }

            return FindGameObject(null, subPath, 0, build, dontDestroy);
        }

        /// <summary>
        /// 查找 Obj（一个嵌套查找 Obj 的过程）
        /// </summary>
        /// <param name="root">父节点</param>
        /// <param name="subPath">拆分后的路径节点</param>
        /// <param name="index">下标</param>
        /// <param name="build">true</param>
        /// <param name="dontDestroy">不要销毁 标签</param>
        /// <returns></returns>
        private static GameObject FindGameObject(
            GameObject root,
            string[]   subPath,
            int        index,
            bool       build,
            bool       dontDestroy)
        {
            GameObject client = null;

            if (root == null)
            {
                client = GameObject.Find(subPath[index]);
            }
            else
            {
                var child = root.transform.Find(subPath[index]);
                if (child != null)
                {
                    client = child.gameObject;
                }
            }

            if (client == null)
            {
                if (build)
                {
                    client = new GameObject(subPath[index]);
                    if (root != null)
                    {
                        client.transform.SetParent(root.transform);
                    }

                    if (dontDestroy && index == 0)
                    {
                        Object.DontDestroyOnLoad(client);
                    }
                }
            }

            if (client == null)
            {
                return null;
            }

            return ++index == subPath.Length ? client : FindGameObject(client, subPath, index, build, dontDestroy);
        }
    }
}