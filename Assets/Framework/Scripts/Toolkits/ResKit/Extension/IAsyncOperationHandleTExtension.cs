// ------------------------------------------------------------
// @file       IAsyncOperationHandleTExtesion.cs
// @brief
// @author     zheliku
// @Modified   2024-12-27 16:12:41
// @Copyright  Copyright (c) 2024, zheliku
// ------------------------------------------------------------

namespace Framework.Toolkits.ResKit
{
    using System;
    using System.Collections.Generic;
    using Core;
    using UnityEngine;
    using UnityEngine.ResourceManagement.AsyncOperations;

    /// <summary>
    /// 获取 AsyncOperationHandle 对应的资源信息
    /// </summary>
    public static class IAsyncOperationHandleTExtension
    {
        /// <summary>
        /// 获取 AsyncOperationHandle 对应的资源名称
        /// </summary>
        public static string AssetName(this AsyncOperationHandle handle)
        {
            if (ResMgr.HandleAssetNameMap.TryGetValue(handle, out var name))
            {
                return name;
            }

            throw new FrameworkException("Can not find asset name for handle:\n" + handle.DebugName);
        }
        
        /// <summary>
        /// 获取 AsyncOperationHandle 对应的资源名称
        /// </summary>
        public static string AssetName<T>(this AsyncOperationHandle<T> handle)
        {
            return ((AsyncOperationHandle) handle).AssetName();
        }
        
        /// <summary>
        /// 获取 AsyncOperationHandle 对应的资源类型
        /// </summary>
        public static Type AssetType(this AsyncOperationHandle handle)
        {
            if (ResMgr.HandleAssetTypeMap.TryGetValue(handle, out var type))
            {
                return type;
            }

            throw new FrameworkException("Can not find asset type for handle:\n" + handle.DebugName);
        }
        
        /// <summary>
        /// 封装方法：为 AsyncOperationHandle 添加加载完成回调，并更新 Mono 显示
        /// </summary>
        public static AsyncOperationHandle<T> OnCompleted<T>(this AsyncOperationHandle<T> handle, Action<T> onSuccessfulCompleted)
        {
            handle.Completed += op => OnCompletedAction(op, onSuccessfulCompleted);
            
            var mono = ResMgr.Instance.GetAddressableMono(handle);
            mono.AddOnCompletedAction(handle, onSuccessfulCompleted);
            
            return handle;
        }
        
        /// <summary>
        /// 封装方法：为 AsyncOperationHandle 添加加载完成回调，并更新 Mono 显示
        /// </summary>
        public static AsyncOperationHandle<IList<T>> OnCompleted<T>(this AsyncOperationHandle<IList<T>> handle, Action<IList<T>> onSuccessfulCompleted)
        {
            handle.Completed += op => OnCompletedAction(op, onSuccessfulCompleted);
            
            var mono = ResMgr.Instance.GetAddressableMono(handle);
            mono.AddOnCompletedAction(handle, onSuccessfulCompleted);
            
            return handle;
        }
        
        /// <summary>
        /// 封装方法：卸载 AsyncOperationHandle 并更新 Mono 显示
        /// </summary>
        public static void Unload(this AsyncOperationHandle handle)
        {
            ResKit.Unload(handle);
        }
        
        /// <summary>
        /// 封装方法：卸载 AsyncOperationHandle 并更新 Mono 显示
        /// </summary>
        public static void Unload<T>(this AsyncOperationHandle<T> handle)
        {
            ResKit.Unload(handle);
        }
        
        /// <summary>
        /// 封装方法：卸载 AsyncOperationHandle 并更新 Mono 显示
        /// </summary>
        public static void Unload<T>(this AsyncOperationHandle<IList<T>> handle)
        {
            ResKit.Unload(handle);
        }
        
        /// <summary>
        /// 封装回调事件，自动处理加载失败情况
        /// </summary>
        private static void OnCompletedAction<T>(AsyncOperationHandle<T> handle, Action<T> onSuccessfulCompleted)
        {
            if (handle.Status == AsyncOperationStatus.Succeeded)
            {
                onSuccessfulCompleted.Invoke(handle.Result);
            }
            else // 加载失败，报错
            {
                Debug.LogError($"Load asset failed: {handle.OperationException}");
            }
        }
    }
}