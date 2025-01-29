// ------------------------------------------------------------
// @file       ISingleton.cs
// @brief
// @author     zheliku
// @Modified   2024-10-20 17:10:19
// @Copyright  Copyright (c) 2024, zheliku
// ------------------------------------------------------------

namespace Framework.Toolkits.SingletonKit
{
    /// <summary>
    /// 单例接口
    /// </summary>
    public interface ISingleton
    {
        /// <summary>
        /// 单例初始化（继承当前接口的类都需要实现该方法）
        /// </summary>
        void OnSingletonInit();
    }
}