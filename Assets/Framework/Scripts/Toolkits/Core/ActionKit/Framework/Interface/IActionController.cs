// ------------------------------------------------------------
// @file       IActionController.cs
// @brief
// @author     zheliku
// @Modified   2024-10-24 19:10:06
// @Copyright  Copyright (c) 2024, zheliku
// ------------------------------------------------------------

namespace Framework.Toolkits.ActionKit
{
    public interface IActionController
    {
        ulong ActionID { get; set; }

        IAction Action { get; set; }

        ActionUpdateMode UpdateMode { get; set; }

        // Controller 控制暂停
        bool Paused { get; set; }

        void Reset();

        void Deinit();

        // Controller 控制回收
        void Recycle();
    }
}