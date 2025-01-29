// ------------------------------------------------------------
// @file       OrEventExtensions.cs
// @brief
// @author     zheliku
// @Modified   2024-10-06 04:10:29
// @Copyright  Copyright (c) 2024, zheliku
// ------------------------------------------------------------

namespace Framework.Core
{
    /// <summary>
    /// OrEvent 扩展
    /// </summary>
    public static class OrEventExtension
    {
        /// <summary>
        /// 级联 IEasyEvent
        /// </summary>
        /// <param name="self">IEasyEvent 实例 1</param>
        /// <param name="e">IEasyEvent 实例 2</param>
        /// <returns>OrEvent 实例</returns>
        public static OrEvent Or(this IEasyEvent self, IEasyEvent e) => new OrEvent().Or(self).Or(e);
    }
}