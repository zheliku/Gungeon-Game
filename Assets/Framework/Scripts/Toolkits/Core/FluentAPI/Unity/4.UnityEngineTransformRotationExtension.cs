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
    /// 针对 <see cref="UnityEngine.Transform"/> 中有关 Rotation 提供的链式扩展
    /// </summary>
    public static class UnityEngineTransformRotationExtension
    {
        /// <summary>
        /// <c> <![CDATA[
        /// component.transform.localRotation = localRotation;
        /// ]]> </c>
        /// </summary>
        /// <example> <code>
        /// <![CDATA[
        /// myScript.SetLocalRotation(Quaternion.identity);
        /// ]]>
        /// </code> </example>
        public static T SetLocalRotation<T>(this T selfComponent, Quaternion localRotation) where T : Component
        {
            selfComponent.transform.localRotation = localRotation;
            return selfComponent;
        }

        /// <summary>
        /// <c> <![CDATA[
        /// gameObject.transform.localRotation = localRotation;
        /// ]]> </c>
        /// </summary>
        /// <example> <code>
        /// <![CDATA[
        /// gameObject.SetLocalRotation(Quaternion.identity);
        /// ]]>
        /// </code> </example>
        public static GameObject SetLocalRotation(this GameObject selfComponent, Quaternion localRotation)
        {
            selfComponent.transform.localRotation = localRotation;
            return selfComponent;
        }

        /// <summary>
        /// <c> <![CDATA[
        /// component.transform.localRotation = Quaternion.identity;
        /// ]]> </c>
        /// </summary>
        /// <example> <code>
        /// <![CDATA[
        /// component.SetLocalRotationIdentity();
        /// ]]>
        /// </code> </example>
        public static T SetLocalRotationIdentity<T>(this T selfComponent) where T : Component
        {
            selfComponent.transform.localRotation = Quaternion.identity;
            return selfComponent;
        }

        /// <summary>
        /// <c> <![CDATA[
        /// gameObject.transform.localRotation = Quaternion.identity;
        /// ]]> </c>
        /// </summary>
        /// <example> <code>
        /// <![CDATA[
        /// gameObject.SetLocalRotationIdentity();
        /// ]]>
        /// </code> </example>
        public static GameObject SetLocalRotationIdentity(this GameObject selfComponent)
        {
            selfComponent.transform.localRotation = Quaternion.identity;
            return selfComponent;
        }

        /// <summary>
        /// <c> <![CDATA[
        /// component.transform.localRotation = Quaternion.Euler(x, y, z);
        /// ]]> </c>
        /// </summary>
        /// <example> <code>
        /// <![CDATA[
        /// myScript.SetLocalRotation(new Vector3(0, 90, 0));
        /// ]]>
        /// </code> </example>
        public static T SetLocalEulerAngles<T>(this T selfComponent, Vector3 eulerAngles) where T : Component
        {
            selfComponent.transform.localRotation = Quaternion.Euler(eulerAngles);
            return selfComponent;
        }

        /// <summary>
        /// <c> <![CDATA[
        /// gameObject.transform.localRotation = Quaternion.Euler(x, y, z);
        /// ]]> </c>
        /// </summary>
        /// <example> <code>
        /// <![CDATA[
        /// new GameObject().SetLocalRotation(new Vector3(0, 90, 0));
        /// ]]>
        /// </code> </example>
        public static GameObject SetLocalEulerAngles(this GameObject self, Vector3 eulerAngles)
        {
            self.transform.localRotation = Quaternion.Euler(eulerAngles);
            return self;
        }

        /// <summary>
        /// <c> <![CDATA[
        /// component.transform.localRotation = Quaternion.Euler(x, y, z);
        /// ]]> </c>
        /// </summary>
        /// <example> <code>
        /// <![CDATA[
        /// myScript.SetLocalEulerAngles(x:0, y:90, z:0);
        /// myScript.SetLocalEulerAngles(x:0, z:0);
        /// ]]>
        /// </code> </example>
        public static T SetLocalEulerAngles<T>(
            this T selfComponent,
            float? x = null,
            float? y = null,
            float? z = null
        ) where T : Component
        {
            var localEulerAngles    = selfComponent.transform.localEulerAngles;
            var newLocalEulerAngles = new Vector3(x ?? localEulerAngles.x, y ?? localEulerAngles.y, z ?? localEulerAngles.z);
            selfComponent.transform.localRotation = Quaternion.Euler(newLocalEulerAngles);
            return selfComponent;
        }

        /// <summary>
        /// <c> <![CDATA[
        /// gameObject.transform.localRotation = Quaternion.Euler(x, y, z);
        /// ]]> </c>
        /// </summary>
        /// <example> <code>
        /// <![CDATA[
        /// new GameObject().SetLocalRotation(x:0, y:90, z:0);
        /// new GameObject().SetLocalRotation(x:0, z:0);
        /// ]]>
        /// </code> </example>
        public static GameObject SetLocalEulerAngles(
            this GameObject self,
            float?          x = null,
            float?          y = null,
            float?          z = null)
        {
            var localEulerAngles = self.transform.localEulerAngles;
            var newLocalEulerAngles = new Vector3(x ?? localEulerAngles.x, y ?? localEulerAngles.y, z ?? localEulerAngles.z);
            self.transform.localRotation = Quaternion.Euler(newLocalEulerAngles);
            return self;
        }

        /// <summary>
        /// <c> <![CDATA[
        /// return component.transform.localRotation;
        /// ]]> </c>
        /// </summary>
        /// <example> <code>
        /// <![CDATA[
        /// var localRotation = myScript.GetLocalRotation();
        /// ]]>
        /// </code> </example>
        public static Quaternion GetLocalRotation<T>(this T selfComponent) where T : Component
        {
            return selfComponent.transform.localRotation;
        }

        /// <summary>
        /// <c> <![CDATA[
        /// return gameObject.transform.localRotation;
        /// ]]> </c>
        /// </summary>
        /// <example> <code>
        /// <![CDATA[
        /// var localRotation = gameObject.GetLocalRotation();
        /// ]]>
        /// </code> </example>
        public static Quaternion GetLocalRotation(this GameObject self)
        {
            return self.transform.localRotation;
        }

        /// <summary>
        /// <c> <![CDATA[
        /// return component.transform.localEulerAngles;
        /// ]]> </c>
        /// </summary>
        /// <example> <code>
        /// <![CDATA[
        /// myScript.GetLocalEulerAngles();
        /// ]]>
        /// </code> </example>
        public static Vector3 GetLocalEulerAngles<T>(this T selfComponent) where T : Component
        {
            return selfComponent.transform.localEulerAngles;
        }

        /// <summary>
        /// <c> <![CDATA[
        /// return gameObject.transform.localEulerAngles;
        /// ]]> </c>
        /// </summary>
        /// <example> <code>
        /// <![CDATA[
        /// gameObject.GetLocalEulerAngles();
        /// ]]>
        /// </code> </example>
        public static Vector3 GetLocalEulerAngles(this GameObject self)
        {
            return self.transform.localEulerAngles;
        }

        /// <summary>
        /// <c> <![CDATA[
        /// return component.transform.localEulerAngles.x;
        /// ]]> </c>
        /// </summary>
        /// <example> <code>
        /// <![CDATA[
        /// var rotX = component.GetLocalEulerAnglesX();
        /// ]]>
        /// </code> </example>
        public static float GetLocalEulerAnglesX<T>(this T selfComponent) where T : Component
        {
            return selfComponent.transform.localEulerAngles.x;
        }

        /// <summary>
        /// <c> <![CDATA[
        /// return gameObject.transform.localEulerAngles.x;
        /// ]]> </c>
        /// </summary>
        /// <example> <code>
        /// <![CDATA[
        /// var rotX = gameObject.GetLocalEulerAnglesX();
        /// ]]>
        /// </code> </example>
        public static float GetLocalEulerAnglesX(this GameObject self)
        {
            return self.transform.localEulerAngles.x;
        }

        /// <summary>
        /// <c> <![CDATA[
        /// return component.transform.localEulerAngles.y;
        /// ]]> </c>
        /// </summary>
        /// <example> <code>
        /// <![CDATA[
        /// var rotY = component.GetLocalEulerAnglesY();
        /// ]]>
        /// </code> </example>
        public static float GetLocalEulerAnglesY<T>(this T selfComponent) where T : Component
        {
            return selfComponent.transform.localEulerAngles.y;
        }

        /// <summary>
        /// <c> <![CDATA[
        /// return gameObject.transform.localEulerAngles.y;
        /// ]]> </c>
        /// </summary>
        /// <example> <code>
        /// <![CDATA[
        /// var rotY = gameObject.GetLocalEulerAnglesY();
        /// ]]>
        /// </code> </example>
        public static float GetLocalEulerAnglesY(this GameObject self)
        {
            return self.transform.localEulerAngles.y;
        }

        /// <summary>
        /// <c> <![CDATA[
        /// return component.transform.localEulerAngles.z;
        /// ]]> </c>
        /// </summary>
        /// <example> <code>
        /// <![CDATA[
        /// var rotZ = component.GetLocalEulerAnglesZ();
        /// ]]>
        /// </code> </example>
        public static float GetLocalEulerAnglesZ<T>(this T selfComponent) where T : Component
        {
            return selfComponent.transform.localEulerAngles.z;
        }

        /// <summary>
        /// <c> <![CDATA[
        /// return gameObject.transform.localEulerAngles.z;
        /// ]]> </c>
        /// </summary>
        /// <example> <code>
        /// <![CDATA[
        /// var rotZ = gameObject.GetLocalEulerAnglesZ();
        /// ]]>
        /// </code> </example>
        public static float GetLocalEulerAnglesZ(this GameObject self)
        {
            return self.transform.localEulerAngles.z;
        }

        /// <summary>
        /// <c> <![CDATA[
        /// component.transform.rotation = rotation;
        /// ]]> </c>
        /// </summary>
        /// <example> <code>
        /// <![CDATA[
        /// myScript.SetRotation(Quaternion.identity);
        /// ]]>
        /// </code> </example>
        public static T SetRotation<T>(this T selfComponent, Quaternion rotation) where T : Component
        {
            selfComponent.transform.rotation = rotation;
            return selfComponent;
        }

        /// <summary>
        /// <c> <![CDATA[
        /// gameObject.transform.rotation = rotation;
        /// ]]> </c>
        /// </summary>
        /// <example> <code>
        /// <![CDATA[
        /// gameObject.SetRotation(Quaternion.identity);
        /// ]]>
        /// </code> </example>
        public static GameObject SetRotation(this GameObject self, Quaternion rotation)
        {
            self.transform.rotation = rotation;
            return self;
        }

        /// <summary>
        /// <c> <![CDATA[
        /// component.transform.rotation = Quaternion.identity;
        /// ]]> </c>
        /// </summary>
        /// <example> <code>
        /// <![CDATA[
        /// component.SetRotationIdentity();
        /// ]]>
        /// </code> </example>
        public static T SetRotationIdentity<T>(this T selfComponent) where T : Component
        {
            selfComponent.transform.rotation = Quaternion.identity;
            return selfComponent;
        }

        /// <summary>
        /// <c> <![CDATA[
        /// gameObject.transform.rotation = Quaternion.identity;
        /// ]]> </c>
        /// </summary>
        /// <example> <code>
        /// <![CDATA[
        /// gameObject.SetRotationIdentity();
        /// ]]>
        /// </code> </example>
        public static GameObject SetRotationIdentity(this GameObject selfComponent)
        {
            selfComponent.transform.rotation = Quaternion.identity;
            return selfComponent;
        }

        /// <summary>
        /// <c> <![CDATA[
        /// component.transform.rotation = Quaternion.Euler(x, y, z);
        /// ]]> </c>
        /// </summary>
        /// <example> <code>
        /// <![CDATA[
        /// myScript.SetRotation(new Vector3(0, 90, 0));
        /// ]]>
        /// </code> </example>
        public static T SetEulerAngles<T>(this T selfComponent, Vector3 eulerAngles) where T : Component
        {
            selfComponent.transform.rotation = Quaternion.Euler(eulerAngles);
            return selfComponent;
        }

        /// <summary>
        /// <c> <![CDATA[
        /// gameObject.transform.rotation = Quaternion.Euler(x, y, z);
        /// ]]> </c>
        /// </summary>
        /// <example> <code>
        /// <![CDATA[
        /// new GameObject().SetRotation(new Vector3(0, 90, 0));
        /// ]]>
        /// </code> </example>
        public static GameObject SetEulerAngles(this GameObject self, Vector3 eulerAngles)
        {
            self.transform.rotation = Quaternion.Euler(eulerAngles);
            return self;
        }

        /// <summary>
        /// <c> <![CDATA[
        /// component.transform.rotation = Quaternion.Euler(x, y, z);
        /// ]]> </c>
        /// </summary>
        /// <example> <code>
        /// <![CDATA[
        /// myScript.Rotation(0, 90, 0);
        /// ]]>
        /// </code> </example>
        public static T SetEulerAngles<T>(
            this T selfComponent,
            float? x = null,
            float? y = null,
            float? z = null
        ) where T : Component
        {
            var eulerAngles = selfComponent.transform.eulerAngles;
            var newEulerAngles = new Vector3(x ?? eulerAngles.x, y ?? eulerAngles.y, z ?? eulerAngles.z);
            selfComponent.transform.localRotation = Quaternion.Euler(newEulerAngles);
            return selfComponent;
        }

        /// <summary>
        /// <c> <![CDATA[
        /// gameObject.transform.rotation = Quaternion.Euler(x, y, z);
        /// ]]> </c>
        /// </summary>
        /// <example> <code>
        /// <![CDATA[
        /// gameObject.Rotation(0, 90, 0);
        /// ]]>
        /// </code> </example>
        public static GameObject SetEulerAngles(
            this GameObject self,
            float?          x = null,
            float?          y = null,
            float?          z = null)
        {
            var eulerAngles    = self.transform.eulerAngles;
            var newEulerAngles = new Vector3(x ?? eulerAngles.x, y ?? eulerAngles.y, z ?? eulerAngles.z);
            self.transform.localRotation = Quaternion.Euler(newEulerAngles);
            return self;
        }

        /// <summary>
        /// <c> <![CDATA[
        /// return component.transform.rotation;
        /// ]]> </c>
        /// </summary>
        /// <example> <code>
        /// <![CDATA[
        /// var rotation = myScript.GetRotation();
        /// ]]>
        /// </code> </example>
        public static Quaternion GetRotation<T>(this T selfComponent) where T : Component
        {
            return selfComponent.transform.rotation;
        }

        /// <summary>
        /// <c> <![CDATA[
        /// return gameObject.transform.rotation;
        /// ]]> </c>
        /// </summary>
        /// <example> <code>
        /// <![CDATA[
        /// var rotation = gameObject.GetRotation();
        /// ]]>
        /// </code> </example>
        public static Quaternion GetRotation(this GameObject self)
        {
            return self.transform.rotation;
        }

        /// <summary>
        /// <c> <![CDATA[
        /// return component.transform.eulerAngles;
        /// ]]> </c>
        /// </summary>
        /// <example> <code>
        /// <![CDATA[
        /// myScript.GetEulerAngles();
        /// ]]>
        /// </code> </example>
        public static Vector3 GetEulerAngles<T>(this T selfComponent) where T : Component
        {
            return selfComponent.transform.eulerAngles;
        }

        /// <summary>
        /// <c> <![CDATA[
        /// return gameObject.transform.eulerAngles;
        /// ]]> </c>
        /// </summary>
        /// <example> <code>
        /// <![CDATA[
        /// gameObject.GetEulerAngles();
        /// ]]>
        /// </code> </example>
        public static Vector3 GetEulerAngles(this GameObject self)
        {
            return self.transform.eulerAngles;
        }

        /// <summary>
        /// <c> <![CDATA[
        /// return component.transform.eulerAngles.x;
        /// ]]> </c>
        /// </summary>
        /// <example> <code>
        /// <![CDATA[
        /// var rotX = component.GetEulerAnglesX();
        /// ]]>
        /// </code> </example>
        public static float GetEulerAnglesX<T>(this T selfComponent) where T : Component
        {
            return selfComponent.transform.eulerAngles.x;
        }

        /// <summary>
        /// <c> <![CDATA[
        /// return gameObject.transform.eulerAngles.x;
        /// ]]> </c>
        /// </summary>
        /// <example> <code>
        /// <![CDATA[
        /// var rotX = gameObject.GetEulerAnglesX();
        /// ]]>
        /// </code> </example>
        public static float GetEulerAnglesX(this GameObject self)
        {
            return self.transform.eulerAngles.x;
        }

        /// <summary>
        /// <c> <![CDATA[
        /// return component.transform.eulerAngles.y;
        /// ]]> </c>
        /// </summary>
        /// <example> <code>
        /// <![CDATA[
        /// var rotY = component.GetEulerAnglesY();
        /// ]]>
        /// </code> </example>
        public static float GetEulerAnglesY<T>(this T selfComponent) where T : Component
        {
            return selfComponent.transform.eulerAngles.y;
        }

        /// <summary>
        /// <c> <![CDATA[
        /// return gameObject.transform.eulerAngles.y;
        /// ]]> </c>
        /// </summary>
        /// <example> <code>
        /// <![CDATA[
        /// var rotY = gameObject.GetEulerAnglesY();
        /// ]]>
        /// </code> </example>
        public static float GetEulerAnglesY(this GameObject self)
        {
            return self.transform.eulerAngles.y;
        }

        /// <summary>
        /// <c> <![CDATA[
        /// return component.transform.eulerAngles.z;
        /// ]]> </c>
        /// </summary>
        /// <example> <code>
        /// <![CDATA[
        /// var rotZ = component.GetEulerAnglesZ();
        /// ]]>
        /// </code> </example>
        public static float GetEulerAnglesZ<T>(this T selfComponent) where T : Component
        {
            return selfComponent.transform.eulerAngles.z;
        }

        /// <summary>
        /// <c> <![CDATA[
        /// return gameObject.transform.eulerAngles.z;
        /// ]]> </c>
        /// </summary>
        /// <example> <code>
        /// <![CDATA[
        /// var rotZ = gameObject.GetEulerAnglesZ();
        /// ]]>
        /// </code> </example>
        public static float GetEulerAnglesZ(this GameObject self)
        {
            return self.transform.eulerAngles.z;
        }
    }
}