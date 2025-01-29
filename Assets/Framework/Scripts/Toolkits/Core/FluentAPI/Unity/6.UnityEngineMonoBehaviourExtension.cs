// ------------------------------------------------------------
// @file       6.UnityEngineMonoBehaviourExtension.cs
// @brief
// @author     zheliku
// @Modified   2024-10-18 22:10:37
// @Copyright  Copyright (c) 2024, zheliku
// ------------------------------------------------------------

namespace Framework.Toolkits.FluentAPI
{
    using UnityEngine;

    /// <summary>
    /// 针对 <see cref="UnityEngine.Behaviour"/> 提供的链式扩展
    /// </summary>
    public static class UnityEngineMonoBehaviourExtension
    {
        /// <summary>
        /// <c> <![CDATA[
        /// behaviour.enable = true
        /// ]]> </c>
        /// </summary>
        /// <example> <code>
        /// <![CDATA[
        /// myScript.Enable();
        /// ]]>
        /// </code> </example>
        public static T Enable<T>(this T selfBehaviour, bool enable = true) where T : Behaviour
        {
            selfBehaviour.enabled = enable;
            return selfBehaviour;
        }
        
        /// <summary>
        /// <c> <![CDATA[
        /// behaviour.enable = false
        /// ]]> </c>
        /// </summary>
        /// <example> <code>
        /// <![CDATA[
        /// myScript.Disable();
        /// ]]>
        /// </code> </example>
        public static T Disable<T>(this T selfBehaviour) where T : Behaviour
        {
            selfBehaviour.enabled = false;
            return selfBehaviour;
        }
    }
}