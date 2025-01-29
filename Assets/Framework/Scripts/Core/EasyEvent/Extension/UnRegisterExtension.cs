// ------------------------------------------------------------
// @file       UnRegisterExtension.cs
// @brief
// @author     zheliku
// @Modified   2024-10-04 16:10:22
// @Copyright  Copyright (c) 2024, zheliku
// ------------------------------------------------------------

namespace Framework.Core
{
    using UnityEngine;

    /// <summary>
    /// IUnRegister 扩展
    /// </summary>
    public static class UnRegisterExtension
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

        /// <summary>
        /// 当 GameObject 销毁时注销
        /// </summary>
        /// <param name="self">注销器</param>
        /// <param name="gameObject">绑定的 GameObject</param>
        /// <returns>注销器</returns>
        public static IUnRegister UnRegisterWhenGameObjectDestroyed(this IUnRegister self, GameObject gameObject)
        {
            return GetOrAddComponent<UnRegisterOnDestroyTrigger>(gameObject).AddUnRegister(self); // 添加到 UnRegisterOnDestroyTrigger 中
        }

        /// <summary>
        /// 当 GameObject 禁用时注销
        /// </summary>
        /// <param name="self">注销器</param>
        /// <param name="gameObject">绑定的 GameObject</param>
        /// <returns>注销器</returns>
        public static IUnRegister UnRegisterWhenGameObjectDisabled(this IUnRegister self, GameObject gameObject)
        {
            return GetOrAddComponent<UnRegisterOnDisableTrigger>(gameObject).AddUnRegister(self); // 添加到 UnRegisterOnDisableTrigger 中
        }

        /// <summary>
        /// 当组件挂载的 GameObject 销毁时注销
        /// </summary>
        /// <param name="self">注销器</param>
        /// <param name="component">绑定的组件</param>
        /// <returns>注销器</returns>
        public static IUnRegister UnRegisterWhenGameObjectDestroyed<TComponent>(this IUnRegister self, TComponent component) where TComponent : Component
        {
            return self.UnRegisterWhenGameObjectDestroyed(component.gameObject);
        }

        /// <summary>
        /// 当组件挂载的 GameObject 禁用时注销
        /// </summary>
        /// <param name="self">注销器</param>
        /// <param name="component">绑定的组件</param>
        /// <returns>注销器</returns>
        public static IUnRegister UnRegisterWhenGameObjectDisabled<TComponent>(this IUnRegister self, TComponent component) where TComponent : Component
        {
            return self.UnRegisterWhenGameObjectDisabled(component.gameObject);
        }


        /// <summary>
        /// 当前场景卸载时注销
        /// </summary>
        /// <param name="self">注销器</param>
        /// <returns>注销器</returns>
        public static IUnRegister UnRegisterWhenCurrentSceneUnloaded(this IUnRegister self)
        {
            return UnRegisterCurrentSceneUnloadedTrigger.Default.AddUnRegister(self); // 添加到 UnRegisterCurrentSceneUnloadedTrigger 中
        }
    }
}