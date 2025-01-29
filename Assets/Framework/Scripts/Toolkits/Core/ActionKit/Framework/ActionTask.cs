// ------------------------------------------------------------
// @file       ActionTask.cs
// @brief
// @author     zheliku
// @Modified   2024-10-24 22:10:39
// @Copyright  Copyright (c) 2024, zheliku
// ------------------------------------------------------------

namespace Framework.Toolkits.ActionKit
{
    /// <summary>
    /// 包括 Action、Controller 和 OnFinish 回调
    /// </summary>
    public class ActionTask
    {
        public IAction Action;

        public IActionController Controller;

        public System.Action<IActionController> OnFinish;

        public void Recycle()
        {
            Action     = null;
            Controller = null;
            OnFinish   = null;
        }
    }
}