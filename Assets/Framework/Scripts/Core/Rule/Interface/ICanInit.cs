// ------------------------------------------------------------
// @file       ICanInit.cs
// @brief
// @author     zheliku
// @Modified   2024-10-04 11:10:53
// @Copyright  Copyright (c) 2024, zheliku
// ------------------------------------------------------------

namespace Framework.Core
{
    /// <summary>
    /// 继承该接口，即可初始化
    /// </summary>
    public interface ICanInit
    {
        /// <summary>
        /// 是否已经初始化
        /// </summary>
        bool Initialized { get; set; }

        /// <summary>
        /// 初始化方法
        /// </summary>
        void Init();

        /// <summary>
        /// 反初始化方法
        /// </summary>
        void Deinit();
    }
}