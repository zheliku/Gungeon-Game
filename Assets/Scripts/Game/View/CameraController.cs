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
    using System.Collections.Generic;
    using Framework.Core;
    using Framework.Toolkits.ActionKit;
    using Framework.Toolkits.FluentAPI;
    using Framework.Toolkits.SingletonKit;
    using UnityEngine;

    public class CameraController : MonoSingleton<CameraController>
    {
        public static readonly EasyEvent<float, int> SHAKE = new EasyEvent<float, int>();

        public static float AdditionalOrthographicSize { get; set; }
        
        public static Vector3 CameraPosOffset { get; set; } // 相机偏移值

        [HierarchyPath]
        public Camera Camera;

        private float _originOrthographicSize;

        public  List<Color> BackgroundColors;
        private Color       _targetBackgroundColor;

        public float ShakeA;
        public int   ShakeFrames;

        public bool Shaking;

        private void Awake()
        {
            this.BindHierarchyComponent();

            SHAKE.Register((A, frames) =>
            {
                ShakeA      = A;
                ShakeFrames = frames;
                Shaking     = true;
            }).UnRegisterWhenGameObjectDestroyed(this);

            // 进入房间，随机更换背景颜色
            TypeEventSystem.GLOBAL.Register<EnterRoomEvent>(e =>
            {
                var currentBackgroundColor = Camera.backgroundColor;
                _targetBackgroundColor = BackgroundColors.RandomTakeOne();
                ActionKit.Lerp(0, 1, 0.5f, f =>
                {
                    Camera.backgroundColor = Color.Lerp(currentBackgroundColor, _targetBackgroundColor, f);
                }).Start(this);
            }).UnRegisterWhenGameObjectDestroyed(gameObject);
        }

        private void Start()
        {
            _originOrthographicSize = Camera.orthographicSize;
        }

        protected override void Update()
        {
            // 相机视野变化
            Camera.orthographicSize = (Camera.orthographicSize, _originOrthographicSize + AdditionalOrthographicSize)
               .LerpWithSpeed(Time.deltaTime * 5);

            var player = Player.Instance;

            if (player)
            {
                var targetPos  = player.GetPosition() + CameraPosOffset;
                var currentPos = transform.position.LerpWithSpeed(targetPos, Time.deltaTime * 2.5f);

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

            // 相机随角色所处位置倾斜
            var currentRoom = this.GetModel<LevelModel>().CurrentRoom;
            if (currentRoom)
            {
                var playerToRoomCenter = player.Direction2DFrom(currentRoom);

                var width = currentRoom.Grid.Column;

                var originAngleZ = this.GetLocalEulerAnglesZ();
                var targetAngleZ = playerToRoomCenter.x * 10f / width;
                var angleZ       = (originAngleZ, targetAngleZ).LerpAngleWithSpeed(Time.deltaTime);
                this.SetLocalEulerAngles(z: angleZ);
            }

            base.Update();
        }

        protected override IArchitecture _Architecture { get => Game.Architecture; }
    }
}