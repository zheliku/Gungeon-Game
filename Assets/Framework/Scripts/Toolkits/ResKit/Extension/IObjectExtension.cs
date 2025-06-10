// ------------------------------------------------------------
// @file       IObjectExtension.cs
// @brief
// @author     zheliku
// @Modified   2024-12-27 20:12:01
// @Copyright  Copyright (c) 2024, zheliku
// ------------------------------------------------------------

namespace Framework.Toolkits.ResKit
{
    using Core;
    using Object = UnityEngine.Object;

    /// <summary>
    /// 获取 Resources 对应的资源信息
    /// </summary>
    public static class IObjectExtension
    {
        /// <summary>
        /// 获取 Resources 对应的资源加载路径
        /// </summary>
        public static string AssetPath(this Object obj)
        {
            if (ResMgr.ResourceAssetPathMap.TryGetValue(obj, out var name))
            {
                return name;
            }

            throw new FrameworkException("Can't find asset path of " + obj.name);
        }

        /// <summary>
        /// 获取 Resources 对应的资源加载路径
        /// </summary>
        public static string AssetPath<T>(this T obj) where T : Object
        {
            if (ResMgr.ResourceAssetPathMap.TryGetValue(obj, out var name))
            {
                return name;
            }

            throw new FrameworkException("Can't find asset path of " + obj.name);
        }

        /// <summary>
        /// 封装方法：卸载 Resources 并更新 Mono 显示
        /// </summary>
        public static void Unload(this Object res)
        {
            ResKit.Unload(res);
        }
    }
}