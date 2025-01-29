// ------------------------------------------------------------
// @file       CanGetSystemExtension.cs
// @brief      ICanGetSystem 的扩展实现，用于获取指定类型的系统
// @author     zheliku
// @Modified   2024-10-04 16:10:54
// @Copyright  Copyright (c) 2024, zheliku
// ------------------------------------------------------------

namespace Framework.Core
{
    /// <summary>
    /// ICanGetSystem 的扩展实现
    /// </summary>
    public static class CanGetSystemExtension
    {
        /// <summary>
        /// 获取指定类型的 System
        /// </summary>
        /// <typeparam name="TSystem">要获取的 System 类型</typeparam>
        /// <param name="self">ICanGetSystem 实例</param>
        /// <returns>指定类型的 System</returns>
        public static TSystem GetSystem<TSystem>(this ICanGetSystem self) where TSystem : class, ISystem
        {
            return self.Architecture.GetSystem<TSystem>(); // 调用 Architecture 类的 GetSystem 方法
        }
    }
}