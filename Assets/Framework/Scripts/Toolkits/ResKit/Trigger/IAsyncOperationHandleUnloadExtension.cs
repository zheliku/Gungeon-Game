// ------------------------------------------------------------
// @file       IUnLoaderExtension.cs
// @brief
// @author     zheliku
// @Modified   2024-12-10 13:12:46
// @Copyright  Copyright (c) 2024, zheliku
// ------------------------------------------------------------

namespace Framework.Toolkits.ResKit
{
    using UnityEngine;
    using UnityEngine.ResourceManagement.AsyncOperations;

    public static class IAsyncOperationHandleUnloadExtension
    {
        /// <summary>
        /// 获取 GameObject 上的组件，不存在则添加
        /// </summary>
        /// <param name="gameObject">GameObject 实例</param>
        /// <typeparam name="TComponent">组件类型</typeparam>
        /// <returns>获取的组件实例</returns>
        private static TComponent GetOrAddComponent<TComponent>(GameObject gameObject) where TComponent : Component
        {
            var trigger = gameObject.GetComponent<TComponent>();

            if (!trigger)
            {
                trigger = gameObject.AddComponent<TComponent>();
            }

            return trigger;
        }
        
        public static AsyncOperationHandle UnLoadWhenGameObjectDestroyed(this AsyncOperationHandle self, GameObject gameObject)
        {
            return GetOrAddComponent<UnloadOnDestroyTrigger>(gameObject).AddHandle(self);
        }
        
        public static AsyncOperationHandle UnLoadWhenGameObjectDisabled(this AsyncOperationHandle self, GameObject gameObject)
        {
            return GetOrAddComponent<UnloadOnDisableTrigger>(gameObject).AddHandle(self);
        }
        
        public static AsyncOperationHandle UnLoadWhenGameObjectDestroyed<TComponent>(this AsyncOperationHandle self, TComponent component) where TComponent : Component
        {
            return GetOrAddComponent<UnloadOnDestroyTrigger>(component.gameObject).AddHandle(self);
        }
        
        public static AsyncOperationHandle UnLoadWhenGameObjectDisabled<TComponent>(this AsyncOperationHandle self, TComponent component) where TComponent : Component
        {
            return GetOrAddComponent<UnloadOnDisableTrigger>(component.gameObject).AddHandle(self);
        }
        
        public static AsyncOperationHandle UnLoadWhenCurrentSceneUnloaded(this AsyncOperationHandle self)
        {
            return UnloadCurrentSceneUnLoadedTrigger.Default.AddHandle(self);
        }
        
        public static AsyncOperationHandle<T> UnLoadWhenGameObjectDestroyed<T>(this AsyncOperationHandle<T> self, GameObject gameObject)
        {
            return GetOrAddComponent<UnloadOnDestroyTrigger>(gameObject).AddHandle(self).Convert<T>();
        }
        
        public static AsyncOperationHandle<T> UnLoadWhenGameObjectDisabled<T>(this AsyncOperationHandle<T> self, GameObject gameObject)
        {
            return GetOrAddComponent<UnloadOnDisableTrigger>(gameObject).AddHandle(self).Convert<T>();
        }
        
        public static AsyncOperationHandle<T> UnLoadWhenGameObjectDestroyed<T, TComponent>(this AsyncOperationHandle<T> self, TComponent component) where TComponent : Component
        {
            return GetOrAddComponent<UnloadOnDestroyTrigger>(component.gameObject).AddHandle(self).Convert<T>();
        }
        
        public static AsyncOperationHandle<T> UnLoadWhenGameObjectDisabled<T, TComponent>(this AsyncOperationHandle<T> self, TComponent component) where TComponent : Component
        {
            return GetOrAddComponent<UnloadOnDisableTrigger>(component.gameObject).AddHandle(self).Convert<T>();
        }
        
        public static AsyncOperationHandle<T> UnLoadWhenCurrentSceneUnloaded<T>(this AsyncOperationHandle<T> self)
        {
            return UnloadCurrentSceneUnLoadedTrigger.Default.AddHandle(self).Convert<T>();
        }
    }
}