// ------------------------------------------------------------
// @file       AbstractViewExtension.cs
// @brief
// @author     zheliku
// @Modified   2025-01-31 20:01:23
// @Copyright  Copyright (c) 2025, zheliku
// ------------------------------------------------------------

namespace Framework.Core
{
    using System.Linq;
    using System.Reflection;
    using UnityEngine;
    using UnityEngine.SceneManagement;

    public static class AbstractViewExtension
    {
        private static readonly BindingFlags _FIELD_BINDING_FLAGS =
            BindingFlags.Public |
            BindingFlags.NonPublic |
            BindingFlags.Instance;

        public static void BindHierarchyComponent(this AbstractView view)
        {
            if (view == null) return;

            var fields = view.GetType().GetFields(_FIELD_BINDING_FLAGS);

            // 获取所有合格字段
            var targetFields = fields
               .Where(f => f.GetCustomAttribute<HierarchyPathAttribute>() != null)                                // 必须拥有 HierarchyPathAttribute 特性
               .Where(f => f.FieldType == typeof(GameObject) || typeof(Component).IsAssignableFrom(f.FieldType)); // 类型必须是 GameObject 或 Component 的派生类

            foreach (var field in targetFields)
            {
                var attribute = field.GetCustomAttribute<HierarchyPathAttribute>();

                var fieldType = field.FieldType;

                Transform targetTransform = null;

                if (string.IsNullOrEmpty(attribute.HierarchyPath)) // 空路径，表示自己
                {
                    targetTransform = view.transform;
                }
                else if (attribute.HierarchyPath.StartsWith('/')) // 以 "/" 开头，表示从根节点开始查找
                {
                    var objNames = attribute.HierarchyPath[1..].Split('/');
                    var parent   = SceneManager.GetActiveScene().GetRootGameObjects().FirstOrDefault(o => o.name == objNames[0]);

                    if (parent)
                    {
                        var relativePath = string.Join('/', objNames[1..]);
                        targetTransform = parent.transform.Find(relativePath);
                    }
                }
                else // 相对路径
                {
                    targetTransform = view.transform.Find(attribute.HierarchyPath);
                }

                if (targetTransform == null)
                {
                    Debug.LogError($"Path resolve failed: {attribute.HierarchyPath}", view.gameObject);
                    continue;
                }

                // 赋值字段
                if (typeof(Component).IsAssignableFrom(fieldType))
                {
                    var component = targetTransform.GetComponent(fieldType);
                    if (component)
                    {
                        field.SetValue(view, component);
                    }
                    else
                    {
                        Debug.LogError($"Component resolve failed: {attribute.HierarchyPath}", view.gameObject);
                    }
                }
                else
                {
                    field.SetValue(view, targetTransform.gameObject);
                }
            }
        }
    }
}