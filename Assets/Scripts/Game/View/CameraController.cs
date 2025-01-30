// ------------------------------------------------------------
// @file       Camera.cs
// @brief
// @author     zheliku
// @Modified   2025-01-29 16:01:35
// @Copyright  Copyright (c) 2025, zheliku
// ------------------------------------------------------------

namespace Game
{
    using Framework.Toolkits.FluentAPI;
    using Framework.Toolkits.SingletonKit;

    public class CameraController : MonoSingleton<CameraController>
    {
        protected override void Update()
        {
            if (Player.Instance != null)
            {
                transform.position = Player.Instance.GetPosition().Set(z: -1);
            }
            
            base.Update();
        }
    }
}