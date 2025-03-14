// ------------------------------------------------------------
// @file       Camera.cs
// @brief
// @author     zheliku
// @Modified   2025-01-29 16:01:35
// @Copyright  Copyright (c) 2025, zheliku
// ------------------------------------------------------------

namespace Game
{
    using System;
    using Framework.Core;
    using Framework.Toolkits.FluentAPI;
    using Framework.Toolkits.SingletonKit;
    using UnityEngine;

    public class CameraController : MonoSingleton<CameraController>
    {
        public EasyEvent<float, int> Shake = new EasyEvent<float, int>();

        public float ShakeA;
        public int   ShakeFrames;
        public bool  Shaking;

        private void Awake()
        {
            Shake.Register((A, frames) =>
            {
                ShakeA      = A;
                ShakeFrames = frames;
                Shaking     = true;
            }).UnRegisterWhenGameObjectDestroyed(this);
        }

        protected override void Update()
        {
            var player = Player.Instance;

            if (player)
            {
                var targetPos  = player.GetPosition();
                var currentPos = transform.position.LerpWithSpeed(targetPos, 5);

                if (Shaking)
                {
                    ShakeFrames--;
                    
                    if (ShakeFrames <= 0)
                    {
                        Shaking = false;
                    }
                    else
                    {
                        var shakeA = (ShakeFrames / 30f).Lerp(ShakeA, 0);
                        currentPos += UnityEngine.Random.insideUnitSphere * shakeA;
                    }
                }

                currentPos = currentPos.Set(z: -10);

                transform.position = currentPos;
            }

            base.Update();
        }

        protected override IArchitecture _Architecture { get => Game.Interface; }
    }
}