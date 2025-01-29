// ------------------------------------------------------------
// @file       9.UnityEngineHirechyPathExtension.cs
// @brief
// @author     zheliku
// @Modified   2024-12-13 13:12:23
// @Copyright  Copyright (c) 2024, zheliku
// ------------------------------------------------------------

namespace Framework.Toolkits.FluentAPI
{
    using System.Linq;
    using Core;
    using UnityEngine;
    using UnityEngine.SceneManagement;

    public static class UnityEngineHierarchyPathExtension
    {
        public static GameObject GetGameObjectInHierarchy(
            this string hierarchyPath,
            bool        includeInactive          = true,
            bool        throwExceptionIfNotFound = true)
        {
            GameObject obj;
            if (includeInactive)
            {
                var objNames = hierarchyPath.Split('/');
                var parent   = SceneManager.GetActiveScene().GetRootGameObjects().FirstOrDefault(o => o.name == objNames[0]);

                if (parent == null)
                {
                    obj = null;
                }
                else
                {
                    obj = objNames[1..].Join("/").GetGameObjectInHierarchy(parent?.transform, true, false);
                }
            }
            else
            {
                obj = GameObject.Find(hierarchyPath);
            }

            if (throwExceptionIfNotFound && obj == null)
            {
                throw new FrameworkException($"Can not find GameObject in hierarchy path: {hierarchyPath}");
            }

            return obj;
        }

        public static GameObject AddGameObjectInHierarchy(this string hierarchyPath, Transform parent = null)
        {
            var        objNames   = hierarchyPath.Split('/');
            GameObject obj        = null;
            var        parentCopy = parent;
            foreach (var name in objNames)
            {
                obj = new GameObject(name);
                obj.transform.SetParent(parentCopy);
                parentCopy = obj.transform;
            }
            return obj;
        }

        public static GameObject GetOrAddGameObjectInHierarchy(
            this string hierarchyPath,
            bool        includeInactive = true)
        {
            var objNames = hierarchyPath.Split('/');
            var rootObjs = SceneManager.GetActiveScene().GetRootGameObjects();
            var parent = includeInactive
                ? rootObjs.FirstOrDefault(o => o.name == objNames[0])
                : rootObjs.FirstOrDefault(o => o.name == objNames[0] && o.activeInHierarchy);
            if (parent == null)
            {
                return hierarchyPath.AddGameObjectInHierarchy();
            }
            else
            {
                return objNames[1..].Join("/").GetOrAddGameObjectInHierarchy(parent.transform, includeInactive);
            }
        }

        public static T GetComponentInHierarchy<T>(
            this string hierarchyPath,
            bool        includeInactive          = true,
            bool        throwExceptionIfNotFound = true) where T : Component
        {
            var obj       = hierarchyPath.GetGameObjectInHierarchy(includeInactive, false);
            var component = obj?.GetComponent<T>();

            if (throwExceptionIfNotFound && component == null)
            {
                throw new FrameworkException($"Can not find component {typeof(T)} in hierarchy path: {hierarchyPath}");
            }

            return component;
        }

        public static GameObject GetGameObjectInHierarchy(
            this string hierarchyPath,
            Transform   parent,
            bool        includeInactive          = true,
            bool        throwExceptionIfNotFound = true)
        {
            GameObject obj;
            if (includeInactive)
            {
                obj = parent.Find(hierarchyPath).gameObject; // Transform.Find 只能找到所有物体
            }
            else
            {
                var objNames = hierarchyPath.Split('/');
                obj = parent.gameObject;

                // 层次遍历，广度优先
                foreach (var name in objNames)
                {
                    // 记录父物体
                    var parentTrans = obj.transform;

                    for (int i = 0; i < parentTrans.childCount; i++)
                    {
                        var child = obj.transform.GetChild(i);
                        if (child.gameObject.activeInHierarchy && child.name == name)
                        {
                            // 找到，指向子物体
                            obj = child.gameObject;
                            break;
                        }
                    }

                    // 如果找完所在层所有子物体后，obj 没有变化，则说明没找到
                    if (obj == parentTrans.gameObject)
                    {
                        obj = null;
                        break;
                    }
                }
            }

            if (throwExceptionIfNotFound && obj == null)
            {
                throw new FrameworkException($"Can not find GameObject from {parent} in hierarchy path: {hierarchyPath}");
            }

            return obj;
        }

        public static GameObject GetGameObjectInHierarchy(
            this string hierarchyPath,
            Component   parent,
            bool        includeInactive          = true,
            bool        throwExceptionIfNotFound = true)
        {
            return hierarchyPath.GetGameObjectInHierarchy(parent.transform, includeInactive, throwExceptionIfNotFound);
        }

        public static GameObject GetGameObjectInHierarchy(
            this string hierarchyPath,
            GameObject  parent,
            bool        includeInactive          = true,
            bool        throwExceptionIfNotFound = true)
        {
            return hierarchyPath.GetGameObjectInHierarchy(parent.transform, includeInactive, throwExceptionIfNotFound);
        }

        public static T GetComponentInHierarchy<T>(
            this string hierarchyPath,
            Transform   parent,
            bool        includeInactive          = true,
            bool        throwExceptionIfNotFound = true)
            where T : Component
        {
            var obj       = hierarchyPath.GetGameObjectInHierarchy(parent.transform, includeInactive, false);
            var component = obj?.GetComponent<T>();

            if (throwExceptionIfNotFound && component == null)
            {
                throw new FrameworkException($"Can not find component {typeof(T)} from {parent} in hierarchy path: {hierarchyPath}");
            }

            return component;
        }

        public static T GetComponentInHierarchy<T>(
            this string hierarchyPath,
            Component   parent,
            bool        includeInactive          = true,
            bool        throwExceptionIfNotFound = true)
            where T : Component
        {
            return hierarchyPath.GetComponentInHierarchy<T>(parent.transform, includeInactive, throwExceptionIfNotFound);
        }

        public static T GetComponentInHierarchy<T>(
            this string hierarchyPath,
            GameObject  parent,
            bool        includeInactive          = true,
            bool        throwExceptionIfNotFound = true)
            where T : Component
        {
            return hierarchyPath.GetComponentInHierarchy<T>(parent.transform, includeInactive, throwExceptionIfNotFound);
        }

        public static GameObject GetOrAddGameObjectInHierarchy(
            this string hierarchyPath,
            Transform   parent,
            bool        includeInactive = true)
        {
            var objNames = hierarchyPath.Split('/');
            var obj      = parent.gameObject;

            // 层次遍历，广度优先
            for (int i = 0; i < objNames.Length; i++)
            {
                string name = objNames[i];

                // 记录父物体
                var parentTrans = obj.transform;

                for (int j = 0; j < parentTrans.childCount; j++)
                {
                    var child = obj.transform.GetChild(j);
                    if (child.name == name)
                    {
                        if (!includeInactive && !child.gameObject.activeInHierarchy)
                        {
                            continue;
                        }

                        // 找到，指向子物体
                        obj = child.gameObject;
                        break;
                    }
                }

                // 如果找完所在层所有子物体后，obj 没有变化，则说明没找到
                if (obj == parentTrans.gameObject)
                {
                    obj = objNames[i..].Join("/").AddGameObjectInHierarchy(parentTrans);
                    break;
                }
            }

            return obj;
        }

        public static GameObject GetOrAddGameObjectInHierarchy(
            this string hierarchyPath,
            Component   parent,
            bool        includeInactive = true)
        {
            return hierarchyPath.GetOrAddGameObjectInHierarchy(parent.transform, includeInactive);
        }

        public static GameObject GetOrAddGameObjectInHierarchy(
            this string hierarchyPath,
            GameObject  parent,
            bool        includeInactive = true)
        {
            return hierarchyPath.GetOrAddGameObjectInHierarchy(parent.transform, includeInactive);
        }

        public static T GetOrAddComponentInHierarchy<T>(
            this string hierarchyPath,
            Transform   parent,
            bool        includeInactive = true)
            where T : Component
        {
            var obj       = hierarchyPath.GetOrAddGameObjectInHierarchy(parent.transform, includeInactive);
            var component = obj.GetOrAddComponent<T>();
            return component;
        }

        public static T GetOrAddComponentInHierarchy<T>(
            this string hierarchyPath,
            Component   parent,
            bool        includeInactive = true)
            where T : Component
        {
            return hierarchyPath.GetOrAddComponentInHierarchy<T>(parent.transform, includeInactive);
        }

        public static T GetOrAddComponentInHierarchy<T>(
            this string hierarchyPath,
            GameObject  parent,
            bool        includeInactive = true)
            where T : Component
        {
            return hierarchyPath.GetOrAddComponentInHierarchy<T>(parent.transform, includeInactive);
        }
    }
}