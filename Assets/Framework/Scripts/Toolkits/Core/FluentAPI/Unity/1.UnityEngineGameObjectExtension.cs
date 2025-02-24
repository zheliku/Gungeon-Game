// ------------------------------------------------------------
// @file       1.UnityEngineGameObjectExtension.cs
// @brief
// @author     zheliku
// @Modified   2024-10-18 12:10:07
// @Copyright  Copyright (c) 2024, zheliku
// ------------------------------------------------------------

namespace Framework.Toolkits.FluentAPI
{
    using System;
    using EventKit;
    using UnityEngine;

    /// <summary>
    /// 针对 <see cref="UnityEngine.GameObject"/> 提供的链式扩展
    /// </summary>
    /// <example> <code>
    /// <![CDATA[
    /// var gameObject  = new GameObject();
    /// var transform   = gameObject.transform;
    /// var selfScript  = gameObject.AddComponent<MonoBehaviour>();
    /// var boxCollider = gameObject.AddComponent<BoxCollider>();
    ///  
    /// gameObject.Enable();           // gameObject.SetActive(true)
    /// selfScript.Enable();           // this.gameObject.SetActive(true)
    /// boxCollider.Enable();          // boxCollider.gameObject.SetActive(true)
    /// gameObject.transform.Enable(); // transform.gameObject.SetActive(true)
    ///  
    /// gameObject.Disable();  // gameObject.SetActive(false)
    /// selfScript.Disable();  // this.gameObject.SetActive(false)
    /// boxCollider.Disable(); // boxCollider.gameObject.SetActive(false)
    /// transform.Disable();   // transform.gameObject.SetActive(false)
    ///  
    /// selfScript.DestroyGameObject();
    /// boxCollider.DestroyGameObject();
    ///     .transform.DestroyGameObject();
    ///  
    /// selfScript.DestroyGameObjectGracefully();
    /// boxCollider.DestroyGameObjectGracefully();
    /// transform.DestroyGameObjectGracefully();
    ///  
    /// selfScript.DestroyGameObject(1.0f);
    /// boxCollider.DestroyGameObject(1.0f);
    /// transform.DestroyGameObject(1.0f);
    ///  
    /// selfScript.DestroyGameObjectGracefully(1.0f);
    /// boxCollider.DestroyGameObjectGracefully(1.0f);
    /// transform.DestroyGameObjectGracefully(1.0f);
    ///  
    /// gameObject.Layer(0);
    /// selfScript.Layer(0);
    /// boxCollider.Layer(0);
    /// transform.Layer(0);
    ///  
    /// gameObject.Layer("Default");
    /// selfScript.Layer("Default");
    /// boxCollider.Layer("Default");
    /// transform.Layer("Default");
    /// ]]>
    /// </code> </example>
    public static class UnityEngineGameObjectExtension
    {
        /// <summary>
        /// <see cref="GameObject.SetActive(bool)"/> 简单链式封装
        /// </summary>
        /// <example> <code>
        /// <![CDATA[
        /// new GameObject().Enable();
        /// ]]> 
        /// </code> </example>
        public static GameObject Enable(this GameObject selfObj)
        {
            selfObj.SetActive(true);
            return selfObj;
        }
        
        public static bool IsEnabled(this GameObject selfObj)
        {
            return selfObj.activeInHierarchy;
        }

        /// <summary>
        /// <c> <![CDATA[
        /// script.gameObject.SetActive(true);
        /// ]]> </c>
        /// </summary>
        /// <example> <code>
        /// <![CDATA[
        /// GetComponent<MyScript>().EnableGameObject();
        /// ]]>
        /// </code> </example>
        public static T EnableGameObject<T>(this T selfComponent) where T : Component
        {
            selfComponent.gameObject.SetActive(true);
            return selfComponent;
        }
        
        public static bool IsGameObjectEnabled<T>(this T selfComponent) where T : Component
        {
            return selfComponent.gameObject.activeSelf;
        }

        /// <summary>
        /// <see cref="GameObject.SetActive(bool)"/> 简单链式封装
        /// </summary>
        /// <example> <code>
        /// <![CDATA[
        /// new GameObject().Disable();
        /// ]]>
        /// </code> </example>
        public static GameObject Disable(this GameObject selfObj)
        {
            selfObj.SetActive(false);
            return selfObj;
        }
        
        public static bool IsDisabled(this GameObject selfObj)
        {
            return !selfObj.activeInHierarchy;
        }

        /// <summary>
        /// <c> <![CDATA[
        /// script.gameObject.SetActive(false);
        /// ]]> </c>
        /// </summary>
        /// <example> <code>
        /// <![CDATA[
        /// GetComponent<MyScript>().Disable();
        /// ]]>
        /// </code> </example>
        public static T DisableGameObject<T>(this T selfComponent) where T : Component
        {
            selfComponent.gameObject.SetActive(false);
            return selfComponent;
        }
        
        public static bool IsGameObjectDisabled<T>(this T selfComponent) where T : Component
        {
            return !selfComponent.gameObject.activeSelf;
        }

        /// <summary>
        /// <c> <![CDATA[
        /// Destroy(myScript.gameObject);
        /// ]]> </c>
        /// </summary>
        /// <example> <code>
        /// <![CDATA[
        /// myScript.DestroyGameObject();
        /// ]]>
        /// </code> </example>
        public static void DestroyGameObject<T>(this T selfBehaviour) where T : Component
        {
            selfBehaviour.gameObject.Destroy();
        }

        /// <summary>
        /// <c> <![CDATA[
        /// DestroyGracefully(myScript.gameObject);
        /// ]]> </c>
        /// </summary>
        /// <example> <code>
        /// <![CDATA[
        /// myScript.DestroyGameObjectGracefully();
        /// ]]>
        /// </code> </example>
        public static void DestroyGameObjectGracefully<T>(this T selfBehaviour) where T : Component
        {
            if (selfBehaviour && selfBehaviour.gameObject)
            {
                selfBehaviour.gameObject.DestroyGracefully();
            }
        }

        /// <summary>
        /// <c> <![CDATA[
        /// Object.Destroy(myScript.gameObject, delaySeconds);
        /// ]]> </c>
        /// </summary>
        /// <example> <code>
        /// <![CDATA[
        /// myScript.DestroyGameObject(5);
        /// ]]>
        /// </code> </example>
        public static T DestroyGameObject<T>(this T selfBehaviour, float delayTime) where T : Component
        {
            selfBehaviour.gameObject.Destroy(delayTime);
            return selfBehaviour;
        }

        /// <summary>
        /// <c> <![CDATA[
        /// if (myScript && myScript.gameObject) Object.Destroy(myScript.gameObject, delaySeconds); 
        /// ]]> </c>
        /// </summary>
        /// <example> <code>
        /// <![CDATA[
        /// myScript.DestroyGameObjectGracefully(5);
        /// ]]>
        /// </code> </example>
        public static T DestroyGameObjectGracefully<T>(this T selfBehaviour, float delayTime) where T : Component
        {
            if (selfBehaviour && selfBehaviour.gameObject)
            {
                selfBehaviour.gameObject.DestroyGracefully(delayTime);
            }

            return selfBehaviour;
        }

        /// <summary>
        /// <c> <![CDATA[
        /// gameObject.layer = layer;
        /// ]]> </c>
        /// </summary>
        /// <example> <code>
        /// <![CDATA[
        /// new GameObject().Layer(0);
        /// ]]>
        /// </code> </example>
        public static GameObject Layer(this GameObject selfObj, int layer)
        {
            selfObj.layer = layer;
            return selfObj;
        }

        /// <summary>
        /// <c> <![CDATA[
        /// component.gameObject.layer = layer;
        /// ]]> </c>
        /// </summary>
        /// <example> <code>
        /// <![CDATA[
        /// rigidbody2D.Layer(0);
        /// ]]>
        /// </code> </example>
        public static T Layer<T>(this T selfComponent, int layer) where T : Component
        {
            selfComponent.gameObject.layer = layer;
            return selfComponent;
        }

        /// <summary>
        /// <c> <![CDATA[
        /// gameObj.layer = LayerMask.NameToLayer(layerName);
        /// ]]> </c>
        /// </summary>
        /// <example> <code>
        /// <![CDATA[
        /// new GameObject().Layer("Default");
        /// ]]>
        /// </code> </example>
        public static GameObject Layer(this GameObject selfObj, string layerName)
        {
            selfObj.layer = LayerMask.NameToLayer(layerName);
            return selfObj;
        }

        /// <summary>
        /// <c> <![CDATA[
        /// component.gameObject.layer = LayerMask.NameToLayer(layerName);
        /// ]]> </c>
        /// </summary>
        /// <example> <code>
        /// <![CDATA[
        /// spriteRenderer.Layer("Default");
        /// ]]>
        /// </code> </example>
        public static T Layer<T>(this T selfComponent, string layerName) where T : Component
        {
            selfComponent.gameObject.layer = LayerMask.NameToLayer(layerName);
            return selfComponent;
        }

        /// <summary>
        /// layerMask 中的层级是否包含 selfObj 所在的层级
        /// </summary>
        /// <example> <code>
        /// <![CDATA[
        /// gameObj.IsInLayerMask(layerMask);
        /// ]]>
        /// </code> </example>
        public static bool IsInLayerMask(this GameObject selfObj, LayerMask layerMask)
        {
            // 根据 Layer 数值进行移位获得用于运算的 Mask 值
            var objLayerMask = 1 << selfObj.layer;
            return (layerMask.value & objLayerMask) == objLayerMask;
        }

        /// <summary>
        /// layerMask 中的层级是否包含 selfComponent.gameObject 所在的层级
        /// </summary>
        /// <example> <code>
        /// <![CDATA[
        /// spriteRenderer.IsInLayerMask(layerMask);
        /// ]]>
        /// </code> </example>
        public static bool IsInLayerMask<T>(this T selfComponent, LayerMask layerMask) where T : Component
        {
            return selfComponent.gameObject.IsInLayerMask(layerMask);
        }

        /// <summary>
        /// 获取组件，没有则添加再返回
        /// </summary>
        /// <example> <code>
        /// <![CDATA[
        /// gameObj.GetOrAddComponent<SpriteRenderer>();
        /// ]]>
        /// </code> </example>
        public static T GetOrAddComponent<T>(this GameObject self) where T : Component
        {
            var comp = self.gameObject.GetComponent<T>();
            return comp ? comp : self.gameObject.AddComponent<T>();
        }
        
        /// <summary>
        /// 获取组件，没有则添加再返回
        /// </summary>
        /// <example> <code>
        /// <![CDATA[
        /// component.GetOrAddComponent<SpriteRenderer>();
        /// ]]>
        /// </code> </example>
        public static T GetOrAddComponent<T>(this Component component) where T : Component
        {
            return component.gameObject.GetOrAddComponent<T>();
        }
        
        /// <summary>
        /// 获取组件，没有则添加再返回
        /// </summary>
        /// <example> <code>
        /// <![CDATA[
        /// gameObj.GetOrAddComponent(typeof(SpriteRenderer));
        /// ]]>
        /// </code> </example>
        public static Component GetOrAddComponent(this GameObject self, Type type)
        {
            var component = self.gameObject.GetComponent(type);
            return component ? component : self.gameObject.AddComponent(type);
        }
        
        /// <summary>
        /// 获取组件，没有则添加再返回
        /// </summary>
        /// <example> <code>
        /// <![CDATA[
        /// component.GetOrAddComponent(typeof(SpriteRenderer));
        /// ]]>
        /// </code> </example>
        public static Component GetOrAddComponent(this Component component, Type type)
        {
            return component.gameObject.GetOrAddComponent(type);
        }

        public static GameObject DestroyWhenGameObjectDisabled(this GameObject self, GameObject target, int priority = 0)
        {
            target.GetOrAddComponent<OnDisableEventTrigger>().OnDisableEvent.Register(() =>
            {
                self.Destroy();
            }, priority);
            return self;
        }
        
        public static GameObject DestroyWhenGameObjectDestroyed(this GameObject self, GameObject target, int priority = 0)
        {
            target.GetOrAddComponent<OnDestroyEventTrigger>().OnDestroyEvent.Register(() =>
            {
                self.Destroy();
            }, priority);
            return self;
        }
        
        public static GameObject DestroyGracefullyWhenGameObjectDisabled(this GameObject self, GameObject target, int priority = 0)
        {
            target.GetOrAddComponent<OnDisableEventTrigger>().OnDisableEvent.Register(() =>
            {
                self.DestroyGracefully();
            }, priority);
            return self;
        }
        
        public static GameObject DestroyGracefullyWhenGameObjectDestroyed(this GameObject self, GameObject target, int priority = 0)
        {
            target.GetOrAddComponent<OnDestroyEventTrigger>().OnDestroyEvent.Register(() =>
            {
                self.DestroyGracefully();
            }, priority);
            return self;
        }
        
        public static GameObject DisableWhenGameObjectDisabled(this GameObject self, GameObject target, int priority = 0)
        {
            target.GetOrAddComponent<OnDisableEventTrigger>().OnDisableEvent.Register(() =>
            {
                self.Disable();
            }, priority);
            return self;
        }
        
        public static GameObject DisableWhenGameObjectDestroyed(this GameObject self, GameObject target, int priority = 0)
        {
            target.GetOrAddComponent<OnDestroyEventTrigger>().OnDestroyEvent.Register(() =>
            {
                self.Disable();
            }, priority);
            return self;
        }
    }
}