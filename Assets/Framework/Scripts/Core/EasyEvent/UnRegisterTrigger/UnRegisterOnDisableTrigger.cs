// ------------------------------------------------------------
// @file       UnRegisterOnDisableTrigger.cs
// @brief
// @author     zheliku
// @Modified   2024-10-04 16:10:35
// @Copyright  Copyright (c) 2024, zheliku
// ------------------------------------------------------------

namespace Framework.Core
{
    /// <summary>
    /// 对象禁用时的注销触发器
    /// </summary>
    public sealed class UnRegisterOnDisableTrigger : UnRegisterTrigger
    {
        private void OnDisable()
        { // OnDisable 时触发注销
            UnRegister();
        }
    }
}