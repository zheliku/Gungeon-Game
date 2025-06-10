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
    /// 针对 <see cref="UnityEngine.Transform"/> 中有关 Position 提供的链式扩展
    /// </summary>
    public static class UnityEngineTransformPositionExtension
    {
        /// <summary>
        /// <c> <![CDATA[
        /// component.transform.localPosition = localPosition;
        /// ]]> </c>
        /// </summary>
        /// <example> <code>
        /// <![CDATA[
        /// spriteRenderer.SetLocalPosition(new Vector3(0, 100, 0));
        /// ]]>
        /// </code> </example>
        public static T SetLocalPosition<T>(this T selfComponent, Vector3 localPos) where T : Component
        {
            selfComponent.transform.localPosition = localPos;
            return selfComponent;
        }

        /// <summary>
        /// <c> <![CDATA[
        /// gameObject.transform.localPosition = localPosition;
        /// ]]> </c>
        /// </summary>
        /// <example> <code>
        /// <![CDATA[
        /// new GameObject().SetLocalPosition(new Vector3(0, 100, 0));
        /// ]]>
        /// </code> </example>
        public static GameObject SetLocalPosition(this GameObject self, Vector3 localPos)
        {
            self.transform.localPosition = localPos;
            return self;
        }

        /// <summary>
        /// <c> <![CDATA[
        /// component.transform.localPosition = new Vector3(x, y, z);
        /// ]]> </c>
        /// </summary>
        /// <example> <code>
        /// <![CDATA[
        /// myScript.SetLocalPosition(x:0, y:0, z:-10);
        /// myScript.SetLocalPosition(x:0, z:-10);
        /// ]]>
        /// </code> </example>
        public static T SetLocalPosition<T>(
            this T selfComponent,
            float? x = null,
            float? y = null,
            float? z = null)
            where T : Component
        {
            var localPosition = selfComponent.transform.localPosition;
            selfComponent.transform.localPosition = new Vector3(x ?? localPosition.x, y ?? localPosition.y, z ?? localPosition.z);
            return selfComponent;
        }

        /// <summary>
        /// <c> <![CDATA[
        /// gameObject.transform.localPosition = new Vector3(x, y, z);
        /// ]]> </c>
        /// </summary>
        /// <example> <code>
        /// <![CDATA[
        /// new GameObject().SetLocalPosition(x:0, y:0, z:-10);
        /// new GameObject().SetLocalPosition(x:0, z:-10);
        /// ]]>
        /// </code> </example>
        public static GameObject SetLocalPosition(
            this GameObject self,
            float?          x = null,
            float?          y = null,
            float?          z = null)
        {
            var localPosition = self.transform.localPosition;
            self.transform.localPosition = new Vector3(x ?? localPosition.x, y ?? localPosition.y, z ?? localPosition.z);
            return self;
        }

        /// <summary>
        /// <c> <![CDATA[
        /// component.transform.localPosition = Vector3.zero;
        /// ]]> </c>
        /// </summary>
        /// <example> <code>
        /// <![CDATA[
        /// component.SetLocalPositionIdentity();
        /// ]]>
        /// </code> </example>
        public static T SetLocalPositionIdentity<T>(this T selfComponent) where T : Component
        {
            selfComponent.transform.localPosition = Vector3.zero;
            return selfComponent;
        }

        /// <summary>
        /// <c> <![CDATA[
        /// gameObject.transform.localPosition = Vector3.zero;
        /// ]]> </c>
        /// </summary>
        /// <example> <code>
        /// <![CDATA[
        /// gameObject.SetLocalPositionIdentity();
        /// ]]>
        /// </code> </example>
        public static GameObject SetLocalPositionIdentity(this GameObject self)
        {
            self.transform.localPosition = Vector3.zero;
            return self;
        }

        /// <summary>
        /// <c> <![CDATA[
        /// return component.transform.localPosition;
        /// ]]> </c>
        /// </summary>
        /// <example> <code>
        /// <![CDATA[
        /// var localPosition = spriteRenderer.GetLocalPosition();
        /// ]]>
        /// </code> </example>
        public static Vector3 GetLocalPosition<T>(this T selfComponent) where T : Component
        {
            return selfComponent.transform.localPosition;
        }

        /// <summary>
        /// <c> <![CDATA[
        /// return gameObject.transform.localPosition;
        /// ]]> </c>
        /// </summary>
        /// <example> <code>
        /// <![CDATA[
        /// Debug.Log(new GameObject().GetLocalPosition());
        /// ]]>
        /// </code> </example>
        public static Vector3 GetLocalPosition(this GameObject self)
        {
            return self.transform.localPosition;
        }

        /// <summary>
        /// <c> <![CDATA[
        /// return component.transform.localPosition.x;
        /// ]]> </c>
        /// </summary>
        /// <example> <code>
        /// <![CDATA[
        /// var posX = component.GetLocalPositionX();
        /// ]]>
        /// </code> </example>
        public static float GetLocalPositionX<T>(this T selfComponent) where T : Component
        {
            return selfComponent.transform.localPosition.x;
        }

        /// <summary>
        /// <c> <![CDATA[
        /// return gameObject.transform.localPosition.x;
        /// ]]> </c>
        /// </summary>
        /// <example> <code>
        /// <![CDATA[
        /// var posX = gameObject.GetLocalPositionX();
        /// ]]>
        /// </code> </example>
        public static float GetLocalPositionX(this GameObject self)
        {
            return self.transform.localPosition.x;
        }

        /// <summary>
        /// <c> <![CDATA[
        /// return component.transform.localPosition.y;
        /// ]]> </c>
        /// </summary>
        /// <example> <code>
        /// <![CDATA[
        /// var posY = component.GetLocalPositionY();
        /// ]]>
        /// </code> </example>
        public static float GetLocalPositionY<T>(this T selfComponent) where T : Component
        {
            return selfComponent.transform.localPosition.y;
        }

        /// <summary>
        /// <c> <![CDATA[
        /// return gameObject.transform.localPosition.y;
        /// ]]> </c>
        /// </summary>
        /// <example> <code>
        /// <![CDATA[
        /// var posY = gameObject.GetLocalPositionY();
        /// ]]>
        /// </code> </example>
        public static float GetLocalPositionY(this GameObject self)
        {
            return self.transform.localPosition.y;
        }

        /// <summary>
        /// <c> <![CDATA[
        /// return component.transform.localPosition.z;
        /// ]]> </c>
        /// </summary>
        /// <example> <code>
        /// <![CDATA[
        /// var posZ = component.GetLocalPositionZ();
        /// ]]>
        /// </code> </example>
        public static float GetLocalPositionZ<T>(this T selfComponent) where T : Component
        {
            return selfComponent.transform.localPosition.z;
        }

        /// <summary>
        /// <c> <![CDATA[
        /// return gameObject.transform.localPosition.z;
        /// ]]> </c>
        /// </summary>
        /// <example> <code>
        /// <![CDATA[
        /// var posZ = gameObject.GetLocalPositionZ();
        /// ]]>
        /// </code> </example>
        public static float GetLocalPositionZ(this GameObject self)
        {
            return self.transform.localPosition.z;
        }

        /// <summary>
        /// <c> <![CDATA[
        /// component.transform.position = position;
        /// ]]> </c>
        /// </summary>
        /// <example> <code>
        /// <![CDATA[
        /// spriteRenderer.SetPosition(new Vector3(0, 100, 0));
        /// ]]>
        /// </code> </example>
        public static T SetPosition<T>(this T selfComponent, Vector3 position) where T : Component
        {
            selfComponent.transform.position = position;
            return selfComponent;
        }

        /// <summary>
        /// <c> <![CDATA[
        /// gameObject.transform.position = position;
        /// ]]> </c>
        /// </summary>
        /// <example> <code>
        /// <![CDATA[
        /// new GameObject().SetPosition(new Vector3(0, 100, 0));
        /// ]]>
        /// </code> </example>
        public static GameObject SetPosition(this GameObject self, Vector3 position)
        {
            self.transform.position = position;
            return self;
        }

        /// <summary>
        /// <c> <![CDATA[
        /// component.transform.position = new Vector3(x, y, z);
        /// ]]> </c>
        /// </summary>
        /// <example> <code>
        /// <![CDATA[
        /// myScript.SetPosition(x:0, y:0, z:-10);
        /// myScript.SetPosition(x:0, z:-10);
        /// ]]>
        /// </code> </example>
        public static T SetPosition<T>(
            this T selfComponent,
            float? x = null,
            float? y = null,
            float? z = null
        ) where T : Component
        {
            var position = selfComponent.transform.position;
            selfComponent.transform.position = new Vector3(x ?? position.x, y ?? position.y, z ?? position.z);
            return selfComponent;
        }

        /// <summary>
        /// <c> <![CDATA[
        /// gameObject.transform.position = new Vector3(x, y, z);
        /// ]]> </c>
        /// </summary>
        /// <example> <code>
        /// <![CDATA[
        /// new GameObject().SetPosition(x:0, y:0, z:-10);
        /// new GameObject().SetPosition(x:0, z:-10);
        /// ]]>
        /// </code> </example>
        public static GameObject SetPosition(
            this GameObject self,
            float?          x = null,
            float?          y = null,
            float?          z = null)
        {
            var position = self.transform.position;
            self.transform.position = new Vector3(x ?? position.x, y ?? position.y, z ?? position.z);
            return self;
        }

        /// <summary>
        /// <c> <![CDATA[
        /// component.transform.position = Vector3.zero;
        /// ]]> </c>
        /// </summary>
        /// <example> <code>
        /// <![CDATA[
        /// component.SetPositionIdentity();
        /// ]]>
        /// </code> </example>
        public static T SetPositionIdentity<T>(this T selfComponent) where T : Component
        {
            selfComponent.transform.position = Vector3.zero;
            return selfComponent;
        }

        /// <summary>
        /// <c> <![CDATA[
        /// gameObject.transform.Position = Vector3.zero;
        /// ]]> </c>
        /// </summary>
        /// <example> <code>
        /// <![CDATA[
        /// gameObject.SetPositionIdentity();
        /// ]]>
        /// </code> </example>
        public static GameObject SetPositionIdentity(this GameObject self)
        {
            self.transform.position = Vector3.zero;
            return self;
        }

        /// <summary>
        /// <c> <![CDATA[
        /// return component.transform.position;
        /// ]]> </c>
        /// </summary>
        /// <example> <code>
        /// <![CDATA[
        /// var localPosition = spriteRenderer.GetPosition();
        /// ]]>
        /// </code> </example>
        public static Vector3 GetPosition<T>(this T selfComponent) where T : Component
        {
            return selfComponent.transform.position;
        }
        
        /// <summary>
        /// <c> <![CDATA[
        /// return component.transform.position;
        /// ]]> </c>
        /// </summary>
        /// <example> <code>
        /// <![CDATA[
        /// var localPosition = spriteRenderer.GetPosition2D();
        /// ]]>
        /// </code> </example>
        public static Vector2 GetPosition2D<T>(this T selfComponent) where T : Component
        {
            return selfComponent.transform.position;
        }

        /// <summary>
        /// <c> <![CDATA[
        /// return gameObject.transform.position;
        /// ]]> </c>
        /// </summary>
        /// <example> <code>
        /// <![CDATA[
        /// Debug.Log(new GameObject().GetPosition());
        /// ]]>
        /// </code> </example>
        public static Vector3 GetPosition(this GameObject self)
        {
            return self.transform.position;
        }
        
        /// <summary>
        /// <c> <![CDATA[
        /// return gameObject.transform.position;
        /// ]]> </c>
        /// </summary>
        /// <example> <code>
        /// <![CDATA[
        /// Debug.Log(new GameObject().GetPosition2D());
        /// ]]>
        /// </code> </example>
        public static Vector2 GetPosition2D(this GameObject self)
        {
            return self.transform.position;
        }

        /// <summary>
        /// <c> <![CDATA[
        /// return component.transform.position.x;
        /// ]]> </c>
        /// </summary>
        /// <example> <code>
        /// <![CDATA[
        /// var posX = component.GetPositionX();
        /// ]]>
        /// </code> </example>
        public static float GetPositionX<T>(this T selfComponent) where T : Component
        {
            return selfComponent.transform.position.x;
        }

        /// <summary>
        /// <c> <![CDATA[
        /// return gameObject.transform.position.x;
        /// ]]> </c>
        /// </summary>
        /// <example> <code>
        /// <![CDATA[
        /// var posX = gameObject.GetPositionX();
        /// ]]>
        /// </code> </example>
        public static float GetPositionX(this GameObject self)
        {
            return self.transform.position.x;
        }

        /// <summary>
        /// <c> <![CDATA[
        /// return component.transform.position.y;
        /// ]]> </c>
        /// </summary>
        /// <example> <code>
        /// <![CDATA[
        /// var posY = component.GetPositionY();
        /// ]]>
        /// </code> </example>
        public static float GetPositionY<T>(this T selfComponent) where T : Component
        {
            return selfComponent.transform.position.y;
        }

        /// <summary>
        /// <c> <![CDATA[
        /// return gameObject.transform.position.y;
        /// ]]> </c>
        /// </summary>
        /// <example> <code>
        /// <![CDATA[
        /// var posY = gameObject.GetPositionY();
        /// ]]>
        /// </code> </example>
        public static float GetPositionY(this GameObject self)
        {
            return self.transform.position.y;
        }

        /// <summary>
        /// <c> <![CDATA[
        /// return component.transform.position.z;
        /// ]]> </c>
        /// </summary>
        /// <example> <code>
        /// <![CDATA[
        /// var posZ = component.GetPositionZ();
        /// ]]>
        /// </code> </example>
        public static float GetPositionZ<T>(this T selfComponent) where T : Component
        {
            return selfComponent.transform.position.z;
        }

        /// <summary>
        /// <c> <![CDATA[
        /// return gameObject.transform.position.z;
        /// ]]> </c>
        /// </summary>
        /// <example> <code>
        /// <![CDATA[
        /// var posZ = gameObject.GetPositionZ();
        /// ]]>
        /// </code> </example>
        public static float GetPositionZ(this GameObject self)
        {
            return self.transform.position.z;
        }

        /// <summary>
        /// <c> <![CDATA[
        /// to.transform.position = self.transform.position;
        /// ]]> </c>
        /// </summary>
        /// <example> <code>
        /// <![CDATA[
        /// component1.CopyPositionTo(component2);
        /// ]]>
        /// </code> </example>
        public static T CopyPositionTo<T>(this T self, Component to) where T : Component
        {
            to.transform.position = self.transform.position;
            return self;
        }

        /// <summary>
        /// <c> <![CDATA[
        /// to.transform.position = self.transform.position;
        /// ]]> </c>
        /// </summary>
        /// <example> <code>
        /// <![CDATA[
        /// component1.CopyPositionTo(gameObject);
        /// ]]>
        /// </code> </example>
        public static T CopyPositionTo<T>(this T self, GameObject to) where T : Component
        {
            to.transform.position = self.transform.position;
            return self;
        }

        /// <summary>
        /// <c> <![CDATA[
        /// to.transform.position = self.transform.position;
        /// ]]> </c>
        /// </summary>
        /// <example> <code>
        /// <![CDATA[
        /// gameObject.CopyPositionTo(component);
        /// ]]>
        /// </code> </example>
        public static GameObject CopyPositionTo(this GameObject self, Component to)
        {
            to.transform.position = self.transform.position;
            return self;
        }

        /// <summary>
        /// <c> <![CDATA[
        /// to.transform.position = self.transform.position;
        /// ]]> </c>
        /// </summary>
        /// <example> <code>
        /// <![CDATA[
        /// gameObject1.CopyPositionTo(gameObject2);
        /// ]]>
        /// </code> </example>
        public static GameObject CopyPositionTo(this GameObject self, GameObject to)
        {
            to.transform.position = self.transform.position;
            return self;
        }

        /// <summary>
        /// <c> <![CDATA[
        /// self.transform.position = from.transform.position;
        /// ]]> </c>
        /// </summary>
        /// <example> <code>
        /// <![CDATA[
        /// component1.CopyPositionFrom(component2);
        /// ]]>
        /// </code> </example>
        public static T CopyPositionFrom<T>(this T self, Component from) where T : Component
        {
            self.transform.position = from.transform.position;
            return self;
        }

        /// <summary>
        /// <c> <![CDATA[
        /// self.transform.position = from.transform.position;
        /// ]]> </c>
        /// </summary>
        /// <example> <code>
        /// <![CDATA[
        /// component1.CopyPositionFrom(gameObject);
        /// ]]>
        /// </code> </example>
        public static T CopyPositionFrom<T>(this T self, GameObject from) where T : Component
        {
            self.transform.position = from.transform.position;
            return self;
        }

        /// <summary>
        /// <c> <![CDATA[
        /// self.transform.position = from.transform.position;
        /// ]]> </c>
        /// </summary>
        /// <example> <code>
        /// <![CDATA[
        /// gameObject.CopyPositionFrom(component);
        /// ]]>
        /// </code> </example>
        public static GameObject CopyPositionFrom(this GameObject self, Component from)
        {
            self.transform.position = from.transform.position;
            return self;
        }

        /// <summary>
        /// <c> <![CDATA[
        /// self.transform.position = from.transform.position;
        /// ]]> </c>
        /// </summary>
        /// <example> <code>
        /// <![CDATA[
        /// component1.CopyPositionFrom(gameObject);
        /// ]]>
        /// </code> </example>
        public static GameObject CopyPositionFrom(this GameObject self, GameObject from)
        {
            self.transform.position = from.transform.position;
            return self;
        }
    }
}