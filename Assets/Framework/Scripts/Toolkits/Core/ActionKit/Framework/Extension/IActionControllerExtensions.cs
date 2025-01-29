// ------------------------------------------------------------
// @file       IActionControllerExtensions.cs
// @brief
// @author     zheliku
// @Modified   2024-10-24 20:10:45
// @Copyright  Copyright (c) 2024, zheliku
// ------------------------------------------------------------

namespace Framework.Toolkits.ActionKit
{
    public static class IActionControllerExtensions
    {
        public static IActionController IgnoreTimeScale(this IActionController self)
        {
            self.UpdateMode = ActionUpdateMode.UnscaledDeltaTime;
            return self;
        }
    }
}