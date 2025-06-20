// ------------------------------------------------------------
// @file       ResKit.cs
// @brief
// @author     zheliku
// @Modified   2024-12-27 15:12:37
// @Copyright  Copyright (c) 2024, zheliku
// ------------------------------------------------------------

namespace Framework.Toolkits.ResKit
{
    using System;
    using System.Collections.Generic;
    using FluentAPI;
    using UnityEngine;
    using UnityEngine.AddressableAssets;
    using UnityEngine.ResourceManagement.AsyncOperations;
    using UnityEngine.ResourceManagement.ResourceProviders;
    using UnityEngine.SceneManagement;
    using Object = UnityEngine.Object;

    public class ResKit
    {
        /// <summary>
        /// 使用 Resources 同步加载对象
        /// </summary>
        /// <param name="path">资源路径（Resources 文件夹下）</param>
        /// <typeparam name="T">资源类型</typeparam>
        /// <returns>资源</returns>
        public static T LoadFromResources<T>(string path) where T : Object
        {
            var res = Resources.Load<T>(path);

            // 更新 Mono 显示
            ResMgr.ResourceAssetPathMap.TryAdd(res, path);
            ResMgr.Instance.GetResourcesMono(res).BindRes(res);

            return res;
        }

        /// <summary>
        /// 使用 Addressables 异步加载 GameObject 并实例化
        /// </summary>
        /// <param name="assetNameOrLabel">资源名称或标签</param>
        /// <param name="position">实例化的位置</param>
        /// <param name="rotation">实例化的角度</param>
        /// <param name="callback">加载成功时的回调</param>
        /// <param name="parent">实例化的父物体</param>
        /// <param name="instantiateInWorldSpace">是否在世界空间下实例化</param>
        /// <returns>异步加载句柄</returns>
        public static AsyncOperationHandle<GameObject> InstantiateAsync(
            string             assetNameOrLabel,
            Vector3            position,
            Quaternion         rotation,
            Action<GameObject> callback                = null,
            Transform          parent                  = null,
            bool               instantiateInWorldSpace = false)
        {
            var handle = Addressables.InstantiateAsync(assetNameOrLabel, position, rotation, parent, instantiateInWorldSpace);

            // 更新 Mono 显示
            ResMgr.HandleAssetNameMap.TryAdd(handle, assetNameOrLabel);
            ResMgr.HandleAssetTypeMap.TryAdd(handle, typeof(GameObject));
            ResMgr.Instance.GetAddressableMono(handle).BindHandle(handle);

            if (callback != null)
            {
                handle.OnCompleted(callback);
            }

            return handle;
        }

        /// <summary>
        /// 使用 Addressables 异步加载 GameObject 并实例化
        /// </summary>
        /// <param name="assetNameOrLabel">资源名称或标签</param>
        /// <param name="callback">加载成功时的回调</param>
        /// <param name="parent">实例化的父物体</param>
        /// <param name="instantiateInWorldSpace">是否在世界空间下实例化</param>
        /// <returns>异步加载句柄</returns>
        public static AsyncOperationHandle<GameObject> InstantiateAsync(
            string             assetNameOrLabel,
            Action<GameObject> callback                = null,
            Transform          parent                  = null,
            bool               instantiateInWorldSpace = false)
        {
            var handle = Addressables.InstantiateAsync(assetNameOrLabel, parent, instantiateInWorldSpace);

            // 更新 Mono 显示
            ResMgr.HandleAssetNameMap.TryAdd(handle, assetNameOrLabel);
            ResMgr.HandleAssetTypeMap.TryAdd(handle, typeof(GameObject));
            ResMgr.Instance.GetAddressableMono(handle).BindHandle(handle);

            if (callback != null)
            {
                handle.OnCompleted(callback);
            }

            return handle;
        }

        /// <summary>
        /// 使用 Addressables 同步加载 GameObject 并实例化
        /// </summary>
        /// <param name="assetNameOrLabel">资源名称或标签</param>
        /// <param name="position">实例化的位置</param>
        /// <param name="rotation">实例化的角度</param>
        /// <param name="parent">实例化的父物体</param>
        /// <param name="instantiateInWorldSpace">是否在世界空间下实例化</param>
        /// <returns>异步加载句柄</returns>
        public static AsyncOperationHandle<GameObject> Instantiate(
            string     assetNameOrLabel,
            Vector3    position                = default,
            Quaternion rotation                = default,
            Transform  parent                  = null,
            bool       instantiateInWorldSpace = false)
        {
            var handle = InstantiateAsync(assetNameOrLabel, position, rotation, null, parent, instantiateInWorldSpace);
            handle.WaitForCompletion();
            return handle;
        }

        /// <summary>
        /// 使用 Addressables 异步加载单个资源
        /// </summary>
        /// <param name="assetNameOrLabel">资源名称或标签</param>
        /// <param name="callback">加载成功时的回调</param>
        /// <returns>异步加载句柄</returns>
        public static AsyncOperationHandle<T> LoadAssetAsync<T>(string assetNameOrLabel, Action<T> callback = null)
        {
            var handle = Addressables.LoadAssetAsync<T>(assetNameOrLabel);

            ResMgr.HandleAssetNameMap.TryAdd(handle, assetNameOrLabel);
            ResMgr.HandleAssetTypeMap.TryAdd(handle, typeof(T));
            ResMgr.Instance.GetAddressableMono(handle).BindHandle(handle);

            if (callback != null)
            {
                handle.OnCompleted(callback);
            }

            return handle;
        }

        /// <summary>
        /// 使用 Addressables 同步加载单个资源
        /// </summary>
        /// <param name="assetNameOrLabel">资源名称或标签</param>
        /// <returns>异步加载句柄</returns>
        public static AsyncOperationHandle<T> LoadAsset<T>(string assetNameOrLabel)
        {
            var handle = LoadAssetAsync<T>(assetNameOrLabel);
            handle.WaitForCompletion();
            return handle;
        }

        /// <summary>
        /// 使用 Addressables 异步加载多个资源
        /// </summary>
        /// <param name="names">所有资源名称或标签</param>
        /// <param name="callback">加载成功时的回调</param>
        /// <param name="mode">搜索资源模式，默认为并集</param>
        /// <param name="releaseDependenciesOnFailure">加载失败时是否自动释放资源</param>
        /// <returns>异步加载句柄</returns>
        public static AsyncOperationHandle<IList<T>> LoadAssetsAsync<T>(
            IList<string>          names,
            Action<IList<T>>       callback                     = null,
            Addressables.MergeMode mode                         = Addressables.MergeMode.Union,
            bool                   releaseDependenciesOnFailure = false)
        {
            var handle = Addressables.LoadAssetsAsync<T>(names, null, mode, releaseDependenciesOnFailure);

            ResMgr.HandleAssetNameMap.TryAdd(handle, names.Join(", "));
            ResMgr.HandleAssetTypeMap.TryAdd(handle, typeof(T));
            ResMgr.Instance.GetAddressableMono(handle).BindHandle(handle);

            if (callback != null)
            {
                handle.OnCompleted(callback);
            }

            return handle;
        }

        /// <summary>
        /// 使用 Addressables 同步加载多个资源
        /// </summary>
        /// <param name="names">所有资源名称或标签</param>
        /// <param name="mode">搜索资源模式，默认为并集</param>
        /// <returns>异步加载句柄</returns>
        public static AsyncOperationHandle<IList<T>> LoadAssets<T>(IList<string> names, Addressables.MergeMode mode = Addressables.MergeMode.Union)
        {
            var handle = LoadAssetsAsync<T>(names, callback: null, mode: mode);
            handle.WaitForCompletion();
            return handle;
        }

        /// <summary>
        /// 使用 Addressables 同步加载多个资源
        /// </summary>
        /// <param name="names">所有资源名称或标签，搜索资源模式为并集</param>
        /// <returns>异步加载句柄</returns>
        public static AsyncOperationHandle<IList<T>> LoadAssets<T>(params string[] names)
        {
            var handle = LoadAssetsAsync<T>(names);
            handle.WaitForCompletion();
            return handle;
        }

        /// <summary>
        /// 使用 Addressables 异步加载场景
        /// </summary>
        /// <param name="name">场景名称</param>
        /// <param name="callback">加载成功时的回调</param>
        /// <param name="activateOnLoad">加载成功时，是否激活场景</param>
        /// <param name="loadSceneMode">加载模式</param>
        /// <param name="priority">加载优先级</param>
        /// <param name="releaseMode">释放模式</param>
        /// <returns>异步加载句柄</returns>
        public static AsyncOperationHandle<SceneInstance> LoadSceneAsync(
            string           name,
            Action           callback       = null,
            bool             activateOnLoad = true,
            LoadSceneMode    loadSceneMode  = LoadSceneMode.Single,
            int              priority       = 100,
            SceneReleaseMode releaseMode    = SceneReleaseMode.ReleaseSceneWhenSceneUnloaded)
        {
            var asyncOperationHandle = Addressables.LoadSceneAsync(name, loadSceneMode, activateOnLoad, priority, releaseMode);

            asyncOperationHandle.Completed += handle =>
            {
                if (handle.Status == AsyncOperationStatus.Succeeded)
                {
                    callback?.Invoke();
                }
                else
                {
                    Debug.Log($"LoadSceneAsync<{name}>: " + handle.OperationException.Message);
                }
            };

            return asyncOperationHandle;
        }

        /// <summary>
        /// 封装方法：卸载 Resources 并更新 Mono 显示
        /// </summary>
        public static void Unload(Object res)
        {
            var mono = ResMgr.Instance.GetResourcesMono(res);

            if (mono)
            {
                mono.SubRef(res);
                if (res is GameObject) // GameObject 特殊处理
                {
                    res = null;
                    Resources.UnloadUnusedAssets();
                    GC.Collect();
                }
                else
                {
                    Resources.UnloadAsset(res);
                }
            }
        }

        /// <summary>
        /// 封装方法：卸载 AsyncOperationHandle 并更新 Mono 显示
        /// </summary>
        public static void Unload(AsyncOperationHandle handle)
        {
            ResMgr.Instance.GetAddressableMono(handle).UnbindHandle(handle);
            handle.Release();
        }

        /// <summary>
        /// 封装方法：卸载 AsyncOperationHandle 并更新 Mono 显示
        /// </summary>
        public static void Unload<T>(AsyncOperationHandle<T> handle)
        {
            ResMgr.Instance.GetAddressableMono(handle).UnbindHandle(handle);
            handle.Release();
        }

        /// <summary>
        /// 封装方法：卸载 AsyncOperationHandle 并更新 Mono 显示
        /// </summary>
        public static void Unload<T>(AsyncOperationHandle<IList<T>> handle)
        {
            ResMgr.Instance.GetAddressableMono(handle).UnbindHandle(handle);
            handle.Release();
        }
    }
}