// ------------------------------------------------------------
// @file       2.UnityEngineTransformExtension.cs
// @brief
// @author     zheliku
// @Modified   2024-10-18 13:10:33
// @Copyright  Copyright (c) 2024, zheliku
// ------------------------------------------------------------

namespace Framework.Toolkits.FluentAPI
{
    using System;
    using UnityEngine;

    /// <summary>
    /// 针对 <see cref="UnityEngine.Transform"/> 提供的链式扩展
    /// </summary>
    public static class UnityEngineTransformExtension
    {
        /// <summary>
        /// <c> <![CDATA[
        /// component.transform.SetParent(parent);
        /// ]]> </c>
        /// </summary>
        /// <example> <code>
        /// <![CDATA[
        /// myScript.Parent(rootGameObj);
        /// ]]>
        /// </code> </example>
        public static T SetParent<T>(this T self, Component parent) where T : Component
        {
            self.transform.SetParent(parent?.transform);
            return self;
        }

        /// <summary>
        /// <c> <![CDATA[
        /// gameObject.transform.SetParent(parent);
        /// ]]> </c>
        /// </summary>
        /// <example> <code>
        /// <![CDATA[
        /// gameObj.Parent(rootGameObj);
        /// ]]>
        /// </code> </example>
        public static GameObject SetParent(this GameObject self, Component parent)
        {
            self.transform.SetParent(parent?.transform);
            return self;
        }
        
        public static T SetParent<T>(this T self, GameObject parent) where T : Component
        {
            self.transform.SetParent(parent?.transform);
            return self;
        }
        
        public static GameObject SetParent(this GameObject self, GameObject parent)
        {
            self.transform.SetParent(parent?.transform);
            return self;
        }
        
        public static Transform GetParent(this GameObject self)
        {
            return self.transform.parent;
        }
        
        public static Transform GetParent(this Component self)
        {
            return self.transform.parent;
        }

        /// <summary>
        /// <c> <![CDATA[
        /// component.transform.SetParent(null);
        /// ]]> </c>
        /// </summary>
        /// <example> <code>
        /// <![CDATA[
        /// component.AsRootTransform();
        /// ]]>
        /// </code> </example>
        public static T AsRoot<T>(this T self) where T : Component
        {
            self.transform.SetParent(null);
            return self;
        }

        /// <summary>
        /// <c> <![CDATA[
        /// gameObject.transform.SetParent(null);
        /// ]]> </c>
        /// </summary>
        /// <example> <code>
        /// <![CDATA[
        /// gameObject.AsRootTransform();
        /// ]]>
        /// </code> </example>
        public static GameObject AsRoot(this GameObject self)
        {
            self.transform.SetParent(null);
            return self;
        }

        /// <summary>
        /// 设置本地位置为 0、本地角度为 0、本地缩放为 1
        /// </summary>
        /// <example> <code>
        /// <![CDATA[
        /// myScript.LocalIdentity();
        /// ]]>
        /// </code> </example>
        public static T LocalIdentity<T>(this T self) where T : Component
        {
            self.transform.localPosition = Vector3.zero;
            self.transform.localRotation = Quaternion.identity;
            self.transform.localScale    = Vector3.one;
            return self;
        }

        /// <summary>
        /// 设置本地位置为 0、本地角度为 0、本地缩放为 1
        /// </summary>
        /// <example> <code>
        /// <![CDATA[
        /// gameObject.LocalIdentity();
        /// ]]>
        /// </code> </example>
        public static GameObject LocalIdentity(this GameObject self)
        {
            self.transform.localPosition = Vector3.zero;
            self.transform.localRotation = Quaternion.identity;
            self.transform.localScale    = Vector3.one;
            return self;
        }

        /// <summary>
        /// 设置世界位置:0 角度:0 缩放:1
        /// </summary>
        /// <example> <code>
        /// <![CDATA[
        /// component.Identity();
        /// ]]>
        /// </code> </example>
        public static T Identity<T>(this T selfComponent) where T : Component
        {
            selfComponent.transform.position   = Vector3.zero;
            selfComponent.transform.rotation   = Quaternion.identity;
            selfComponent.transform.localScale = Vector3.one;
            return selfComponent;
        }

        /// <summary>
        /// 设置世界位置:0 角度:0 缩放:1
        /// </summary>
        /// <example> <code>
        /// <![CDATA[
        /// gameObject.Identity();
        /// ]]>
        /// </code> </example>
        public static GameObject Identity(this GameObject self)
        {
            self.transform.position   = Vector3.zero;
            self.transform.rotation   = Quaternion.identity;
            self.transform.localScale = Vector3.one;
            return self;
        }
        
        /// <summary>
        /// Destroy 所有子 GameObject
        /// </summary>
        /// <example> <code>
        /// <![CDATA[
        /// rootTransform.DestroyChildren();
        /// ]]>
        /// </code> </example>
        public static T DestroyChildren<T>(this T selfComponent) where T : Component
        {
            var childCount = selfComponent.transform.childCount;

            for (var i = childCount - 1; i >= 0; i--)
            {
                selfComponent.transform.GetChild(i).DestroyGameObjectGracefully();
            }

            return selfComponent;
        }
        
        /// <summary>
        /// 根据条件 Destroy 所有子 GameObject
        /// </summary>
        /// <example> <code>
        /// <![CDATA[
        /// rootTransform.DestroyChildrenWithCondition(child => child != other);
        /// ]]>
        /// </code> </example>
        public static T DestroyChildren<T>(this T selfComponent, Func<Transform, bool> condition) where T : Component
        {
            var childCount = selfComponent.transform.childCount;

            for (var i = childCount - 1; i >= 0; i--)
            {
                var child = selfComponent.transform.GetChild(i);
                if (condition(child))
                {
                    child.DestroyGameObjectGracefully();
                }
            }

            return selfComponent;
        }
        
        /// <summary>
        /// Destroy 所有子 GameObject
        /// </summary>
        /// <example> <code>
        /// <![CDATA[
        /// rootGameObject.DestroyChildren();
        /// ]]>
        /// </code> </example>
        public static GameObject DestroyChildren(this GameObject selfGameObj)
        {
            var childCount = selfGameObj.transform.childCount;

            for (var i = childCount - 1; i >= 0; i--)
            {
                selfGameObj.transform.GetChild(i).DestroyGameObjectGracefully();
            }

            return selfGameObj;
        }
        
        /// <summary>
        /// 根据条件 Destroy 所有子 GameObject
        /// </summary>
        /// <example> <code>
        /// <![CDATA[
        /// rootGameObject.DestroyChildrenWithCondition(child => child != other);
        /// ]]>
        /// </code> </example>
        public static GameObject DestroyChildren(this GameObject selfGameObject, Func<Transform, bool> condition)
        {
            var childCount = selfGameObject.transform.childCount;

            for (var i = childCount - 1; i >= 0; i--)
            {
                var child = selfGameObject.transform.GetChild(i);
                if (condition(child))
                {
                    child.DestroyGameObjectGracefully();
                }
            }

            return selfGameObject;
        }
        
        /// <summary>
        /// <c> <![CDATA[
        /// component.transform.SetAsLastSibling();
        /// ]]> </c>
        /// </summary>
        /// <example> <code>
        /// <![CDATA[
        /// myScript.AsLastSibling();
        /// ]]>
        /// </code> </example>
        public static T AsLastSibling<T>(this T selfComponent) where T : Component
        {
            selfComponent.transform.SetAsLastSibling();
            return selfComponent;
        }
        
        /// <summary>
        /// <c> <![CDATA[
        /// gameObject.transform.SetAsLastSibling();
        /// ]]> </c>
        /// </summary>
        /// <example> <code>
        /// <![CDATA[
        /// gameObject.AsLastSibling();
        /// ]]>
        /// </code> </example>
        public static GameObject AsLastSibling(this GameObject self)
        {
            self.transform.SetAsLastSibling();
            return self;
        }
        
        /// <summary>
        /// <c> <![CDATA[
        /// component.transform.SetAsFirstSibling();
        /// ]]> </c>
        /// </summary>
        /// <example> <code>
        /// <![CDATA[
        /// myScript.AsFirstSibling();
        /// ]]>
        /// </code> </example>
        public static T AsFirstSibling<T>(this T selfComponent) where T : Component
        {
            selfComponent.transform.SetAsFirstSibling();
            return selfComponent;
        }
        
        /// <summary>
        /// <c> <![CDATA[
        /// gameObject.transform.SetAsFirstSibling();
        /// ]]> </c>
        /// </summary>
        /// <example> <code>
        /// <![CDATA[
        /// gameObject.AsFirstSibling();
        /// ]]>
        /// </code> </example>
        public static GameObject AsFirstSibling(this GameObject self)
        {
            self.transform.SetAsFirstSibling();
            return self;
        }
        
        /// <summary>
        /// <c> <![CDATA[
        /// component.transform.SetSiblingIndex(index);
        /// ]]> </c>
        /// </summary>
        /// <example> <code>
        /// <![CDATA[
        /// myScript.SiblingIndex(10);
        /// ]]>
        /// </code> </example>
        public static T SiblingIndex<T>(this T selfComponent, int index) where T : Component
        {
            selfComponent.transform.SetSiblingIndex(index);
            return selfComponent;
        }
        
        /// <summary>
        /// <c> <![CDATA[
        /// gameObject.transform.SetSiblingIndex(index);
        /// ]]> </c>
        /// </summary>
        /// <example> <code>
        /// <![CDATA[
        /// gameObject.SiblingIndex(10);
        /// ]]>
        /// </code> </example>
        public static GameObject SiblingIndex(this GameObject selfComponent, int index)
        {
            selfComponent.transform.SetSiblingIndex(index);
            return selfComponent;
        }

        public static GameObject SetTransformRight(this GameObject self, Vector3 right)
        {
            self.transform.right = right;
            return self;
        }
        
        public static T SetTransformRight<T>(this T self, Vector3 right) where T : Component
        {
            self.transform.right = right;
            return self;
        }
        
        public static GameObject SetTransformUp(this GameObject self, Vector3 up)
        {
            self.transform.up = up;
            return self;
        }
        
        public static T SetTransformUp<T>(this T self, Vector3 up) where T : Component
        {
            self.transform.up = up;
            return self;
        }
        
        public static GameObject SetTransformForward(this GameObject self, Vector3 forward)
        {
            self.transform.forward = forward;
            return self;
        }
        
        public static T SetTransformForward<T>(this T self, Vector3 forward) where T : Component
        {
            self.transform.forward = forward;
            return self;
        }
    }
}