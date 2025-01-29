// ------------------------------------------------------------
// @file       ActionController.cs
// @brief
// @author     zheliku
// @Modified   2024-10-24 20:10:37
// @Copyright  Copyright (c) 2024, zheliku
// ------------------------------------------------------------

namespace Framework.Toolkits.ActionKit
{
    using PoolKit;

    public class ActionController : IActionController
    {
        private static readonly ObjectPool<IActionController> _POOL = new ObjectPool<IActionController>(
            () => new ActionController(),
            null,
            controller =>
            {
                controller.UpdateMode = ActionUpdateMode.ScaledDeltaTime;
                controller.ActionID   = 0;
                controller.Action     = null;
            },
            null,
            10);
        
        public static IActionController Spawn()
        {
            return _POOL.Create() as ActionController;
        }

        public ulong ActionID { get; set; }

        public IAction Action { get; set; }

        public ActionUpdateMode UpdateMode { get; set; }

        public bool Paused
        {
            get => Action.Paused;
            set => Action.Paused = value;
        }

        public void Reset()
        {
            if (Action.ActionID == ActionID)
            {
                Action.Reset();
            }
        }

        public void Deinit()
        {
            if (Action != null && Action.ActionID == ActionID)
            {
                Action.Deinit();
            }
        }

        public void Recycle()
        {
            _POOL.Recycle(this);
        }
    }
}