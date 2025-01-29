// ------------------------------------------------------------
// @file       UnRegisterOnDestroyTrigger.cs
// @brief
// @author     zheliku
// @Modified   2024-10-04 16:10:03
// @Copyright  Copyright (c) 2024, zheliku
// ------------------------------------------------------------

namespace Framework.Core
{
    /// <summary>
    /// 对象销毁时的注销触发器
    /// </summary>
    public sealed class UnRegisterOnDestroyTrigger : UnRegisterTrigger
    {
        private void OnDestroy()
        { // OnDestroy 时触发注销
            UnRegister();
        }
    }
}