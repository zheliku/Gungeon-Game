// ------------------------------------------------------------
// @file       UnRegisterListExtension.cs
// @brief
// @author     zheliku
// @Modified   2024-10-05 11:10:29
// @Copyright  Copyright (c) 2024, zheliku
// ------------------------------------------------------------

namespace Framework.Core
{
    /// <summary>
    /// IUnRegisterList 扩展
    /// </summary>
    public static class UnRegisterListExtension
    {
        /// <summary>
        /// 将注销器添加至 List
        /// </summary>
        /// <param name="self">注销器</param>
        /// <param name="unRegisterList">待添加的 List</param>
        public static void AddToUnregisterList(this IUnRegister self, IUnRegisterList unRegisterList)
        {
            unRegisterList.UnregisterList.Add(self);
        }

        /// <summary>
        /// 注销 List 中的所有事件
        /// </summary>
        /// <param name="self">待注销的 List</param>
        public static void UnRegisterAll(this IUnRegisterList self)
        {
            foreach (var unRegister in self.UnregisterList)
            {
                // 遍历未注册列表中的所有未注册对象，并调用其 UnRegister 方法
                unRegister.UnRegister();
            }

            // 清空未注册列表
            self.UnregisterList.Clear();
        }
    }
}