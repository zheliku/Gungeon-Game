// ------------------------------------------------------------
// @file       CanGetModelExtension.cs
// @brief
// @author     zheliku
// @Modified   2024-10-04 16:10:39
// @Copyright  Copyright (c) 2024, zheliku
// ------------------------------------------------------------

namespace Framework.Core
{
    using Model;

    /// <summary>
    /// ICanGetModel 的扩展实现
    /// </summary>
    public static class CanGetModelExtension
    {
        /// <summary>
        /// 获取指定类型的 Model
        /// </summary>
        /// <typeparam name="TModel">要获取的 Model 类型</typeparam>
        /// <param name="self">ICanGetModel 实例</param>
        /// <returns>指定类型的 Model</returns>
        public static TModel GetModel<TModel>(this ICanGetModel self) where TModel : class, IModel
        {
            return self.Architecture.GetModel<TModel>(); // 调用 Architecture 类的 GetModel 方法
        }
    }
}