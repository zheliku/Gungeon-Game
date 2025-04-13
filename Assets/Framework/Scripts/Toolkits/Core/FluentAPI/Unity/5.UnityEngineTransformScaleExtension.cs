// ------------------------------------------------------------
// @file       3.UnityEngineTGransformPositionExtension.cs
// @brief
// @author     zheliku
// @Modified   2024-10-18 14:10:29
// @Copyright  Copyright (c) 2024, zheliku
// ------------------------------------------------------------

namespace Framework.Toolkits.FluentAPI
{
    using UnityEngine;

    /// <summary>
    /// 针对 <see cref="UnityEngine.Transform"/> 中有关 Scale 提供的链式扩展
    /// </summary>
    public static class UnityEngineTransformScaleExtension
    {
        /// <summary>
        /// <c> <![CDATA[
        /// component.transform.localScale = scale;
        /// ]]> </c>
        /// </summary>
        /// <example> <code>
        /// <![CDATA[
        /// component.SetLocalScale(Vector3.one);
        /// ]]>
        /// </code> </example>
        public static T SetLocalScale<T>(this T selfComponent, Vector3 scale) where T : Component
        {
            selfComponent.transform.localScale = scale;
            return selfComponent;
        }

        /// <summary>
        /// <c> <![CDATA[
        /// gameObject.transform.localScale = scale;
        /// ]]> </c>
        /// </summary>
        /// <example> <code>
        /// <![CDATA[
        /// gameObject.SetLocalScale(Vector3.one);
        /// ]]>
        /// </code> </example>
        public static GameObject SetLocalScale(this GameObject self, Vector3 scale)
        {
            self.transform.localScale = scale;
            return self;
        }

        /// <summary>
        /// <c> <![CDATA[
        /// component.transform.localScale = new Vector3(x, y, z);
        /// ]]> </c>
        /// </summary>
        /// <example> <code>
        /// <![CDATA[
        /// myScript.SetLocalScale(x:2, y:2, z:2);
        /// ]]>
        /// </code> </example>
        public static T SetLocalScale<T>(
            this T selfComponent,
            float? x = null,
            float? y = null,
            float? z = null
        ) where T : Component
        {
            var localScale     = selfComponent.transform.localScale;
            selfComponent.transform.localScale = new Vector3(x ?? localScale.x, y ?? localScale.y, z ?? localScale.z);
            return selfComponent;
        }

        /// <summary>
        /// <c> <![CDATA[
        /// gameObject.transform.localScale = new Vector3(x, y, z);
        /// ]]> </c>
        /// </summary>
        /// <example> <code>
        /// <![CDATA[
        /// gameObject.SetLocalScale(x:2, y:2, z:2);
        /// ]]>
        /// </code> </example>
        public static GameObject SetLocalScale(
            this GameObject selfComponent,
            float?          x = null,
            float?          y = null,
            float?          z = null)
        {
            var localScale = selfComponent.transform.localScale;
            selfComponent.transform.localScale = new Vector3(x ?? localScale.x, y ?? localScale.y, z ?? localScale.z);
            return selfComponent;
        }
        
        /// <summary>
        /// <c> <![CDATA[
        /// component.transform.localScale = Vector3.one;
        /// ]]> </c>
        /// </summary>
        /// <example> <code>
        /// <![CDATA[
        /// component.SetLocalScaleIdentity();
        /// ]]>
        /// </code> </example>
        public static T SetLocalScaleIdentity<T>(this T selfComponent) where T : Component
        {
            selfComponent.transform.localScale = Vector3.one;
            return selfComponent;
        }

        /// <summary>
        /// <c> <![CDATA[
        /// gameObject.transform.localScale = Vector3.one;
        /// ]]> </c>
        /// </summary>
        /// <example> <code>
        /// <![CDATA[
        /// gameObject.SetLocalScaleIdentity();
        /// ]]>
        /// </code> </example>
        public static GameObject SetLocalScaleIdentity(this GameObject selfComponent)
        {
            selfComponent.transform.localScale = Vector3.one;
            return selfComponent;
        }
        
        /// <summary>
        /// <c> <![CDATA[
        /// return component.transform.localScale;
        /// ]]> </c>
        /// </summary>
        /// <example> <code>
        /// <![CDATA[
        /// var localScale = myScript.GetLocalScale();
        /// ]]>
        /// </code> </example>
        public static Vector3 GetLocalScale<T>(this T selfComponent) where T : Component
        {
            return selfComponent.transform.localScale;
        }

        /// <summary>
        /// <c> <![CDATA[
        /// return gameObject.transform.localScale;
        /// ]]> </c>
        /// </summary>
        /// <example> <code>
        /// <![CDATA[
        /// var localScale = gameObject.GetLocalScale();
        /// ]]>
        /// </code> </example>
        public static Vector3 GetLocalScale(this GameObject self)
        {
            return self.transform.localScale;
        }

        /// <summary>
        /// <c> <![CDATA[
        /// return component.transform.localScale.x;
        /// ]]> </c>
        /// </summary>
        /// <example> <code>
        /// <![CDATA[
        /// var scaleX = component.GetLocalScaleX();
        /// ]]>
        /// </code> </example>
        public static float GetLocalScaleX<T>(this T self) where T : Component
        {
            return self.transform.localScale.x;
        }

        /// <summary>
        /// <c> <![CDATA[
        /// return gameObject.transform.localScale.x;
        /// ]]> </c>
        /// </summary>
        /// <example> <code>
        /// <![CDATA[
        /// var scaleX = gameObject.GetLocalScaleX();
        /// ]]>
        /// </code> </example>
        public static float GetLocalScaleX(this GameObject self)
        {
            return self.transform.localScale.x;
        }

        /// <summary>
        /// <c> <![CDATA[
        /// return component.transform.localScale.y;
        /// ]]> </c>
        /// </summary>
        /// <example> <code>
        /// <![CDATA[
        /// var scaleX = component.GetLocalScaleY();
        /// ]]>
        /// </code> </example>
        public static float GetLocalScaleY<T>(this T self) where T : Component
        {
            return self.transform.localScale.y;
        }

        /// <summary>
        /// <c> <![CDATA[
        /// return gameObject.transform.localScale.y;
        /// ]]> </c>
        /// </summary>
        /// <example> <code>
        /// <![CDATA[
        /// var scaleX = gameObject.GetLocalScaleY();
        /// ]]>
        /// </code> </example>
        public static float GetLocalScaleY(this GameObject self)
        {
            return self.transform.localScale.y;
        }

        /// <summary>
        /// <c> <![CDATA[
        /// return component.transform.localScale.x;
        /// ]]> </c>
        /// </summary>
        /// <example> <code>
        /// <![CDATA[
        /// var scaleX = component.GetLocalScaleZ();
        /// ]]>
        /// </code> </example>
        public static float GetLocalScaleZ<T>(this T self) where T : Component
        {
            return self.transform.localScale.z;
        }

        /// <summary>
        /// <c> <![CDATA[
        /// return gameObject.transform.localScale.x;
        /// ]]> </c>
        /// </summary>
        /// <example> <code>
        /// <![CDATA[
        /// var scaleX = gameObject.GetLocalScaleZ();
        /// ]]>
        /// </code> </example>
        public static float GetLocalScaleZ(this GameObject self)
        {
            return self.transform.localScale.z;
        }

        /// <summary>
        /// <c> <![CDATA[
        /// return component.transform.lossyScale;
        /// ]]> </c>
        /// </summary>
        /// <example> <code>
        /// <![CDATA[
        /// var scale = component.GetScale();
        /// ]]>
        /// </code> </example>
        public static Vector3 GetScale<T>(this T selfComponent) where T : Component
        {
            return selfComponent.transform.lossyScale;
        }

        /// <summary>
        /// <c> <![CDATA[
        /// return gameObject.transform.lossyScale;
        /// ]]> </c>
        /// </summary>
        /// <example> <code>
        /// <![CDATA[
        /// var scale = gameObject.GetScale();
        /// ]]>
        /// </code> </example>
        public static Vector3 GetScale(this GameObject selfComponent)
        {
            return selfComponent.transform.lossyScale;
        }
        
        /// <summary>
        /// <c> <![CDATA[
        /// return component.transform.lossyScale.x;
        /// ]]> </c>
        /// </summary>
        /// <example> <code>
        /// <![CDATA[
        /// var scaleX = component.GetScaleX();
        /// ]]>
        /// </code> </example>
        public static float GetScaleX<T>(this T selfComponent) where T : Component
        {
            return selfComponent.transform.lossyScale.x;
        }
        
        /// <summary>
        /// <c> <![CDATA[
        /// return gameObject.transform.lossyScale.x;
        /// ]]> </c>
        /// </summary>
        /// <example> <code>
        /// <![CDATA[
        /// var scaleX = gameObject.GetScaleX();
        /// ]]>
        /// </code> </example>
        public static float GetScaleX(this GameObject selfComponent)
        {
            return selfComponent.transform.lossyScale.x;
        }
        
        /// <summary>
        /// <c> <![CDATA[
        /// return component.transform.lossyScale.y;
        /// ]]> </c>
        /// </summary>
        /// <example> <code>
        /// <![CDATA[
        /// var scaleY = component.GetScaleY();
        /// ]]>
        /// </code> </example>
        public static float GetScaleY<T>(this T selfComponent) where T : Component
        {
            return selfComponent.transform.lossyScale.y;
        }
        
        /// <summary>
        /// <c> <![CDATA[
        /// return gameObject.transform.lossyScale.y;
        /// ]]> </c>
        /// </summary>
        /// <example> <code>
        /// <![CDATA[
        /// var scaleY = gameObject.GetScaleY();
        /// ]]>
        /// </code> </example>
        public static float GetScaleY(this GameObject selfComponent)
        {
            return selfComponent.transform.lossyScale.y;
        }
        
        /// <summary>
        /// <c> <![CDATA[
        /// return component.transform.lossyScale.z;
        /// ]]> </c>
        /// </summary>
        /// <example> <code>
        /// <![CDATA[
        /// var scaleZ = component.GetScaleZ();
        /// ]]>
        /// </code> </example>
        public static float GetScaleZ<T>(this T selfComponent) where T : Component
        {
            return selfComponent.transform.lossyScale.z;
        }
        
        /// <summary>
        /// <c> <![CDATA[
        /// return gameObject.transform.lossyScale.z;
        /// ]]> </c>
        /// </summary>
        /// <example> <code>
        /// <![CDATA[
        /// var scaleZ = gameObject.GetScaleZ();
        /// ]]>
        /// </code> </example>
        public static float GetScaleZ(this GameObject selfComponent)
        {
            return selfComponent.transform.lossyScale.z;
        }
    }
}