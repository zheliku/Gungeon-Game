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
    using UnityEngine;

    public class CameraController : MonoSingleton<CameraController>
    {
        protected override void Update()
        {
            var player = Player.Instance;
            
            if (player)
            {
                var targetPos = player.GetPosition().Set(z: -10);
                transform.position = transform.position.LerpWithSpeed(targetPos, 5);
            }
            
            base.Update();
        }
    }
}