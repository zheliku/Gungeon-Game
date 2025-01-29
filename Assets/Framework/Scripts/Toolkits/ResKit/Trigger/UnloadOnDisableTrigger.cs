// ------------------------------------------------------------
// @file       UnLoadOnDestroyTrigger.cs
// @brief
// @author     zheliku
// @Modified   2024-12-10 13:12:02
// @Copyright  Copyright (c) 2024, zheliku
// ------------------------------------------------------------

namespace Framework.Toolkits.ResKit
{
    public class UnloadOnDisableTrigger : UnloadTrigger
    {
        private void OnDisable()
        {
            Unload();
        }
    }
}
