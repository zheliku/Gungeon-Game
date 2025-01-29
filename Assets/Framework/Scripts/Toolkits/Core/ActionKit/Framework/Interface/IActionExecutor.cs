// ------------------------------------------------------------
// @file       IActionExcutor.cs
// @brief
// @author     zheliku
// @Modified   2024-10-24 22:10:27
// @Copyright  Copyright (c) 2024, zheliku
// ------------------------------------------------------------

namespace Framework.Toolkits.ActionKit
{
    public interface IActionExecutor
    {
        void Execute(IActionController controller, System.Action<IActionController> onFinish = null);
    }
}