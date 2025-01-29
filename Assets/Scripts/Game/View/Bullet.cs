// ------------------------------------------------------------
// @file       Bullet.cs
// @brief
// @author     zheliku
// @Modified   2025-01-29 16:01:04
// @Copyright  Copyright (c) 2025, zheliku
// ------------------------------------------------------------

namespace Game.View
{
    using System;
    using Framework.Core;
    using Framework.Core.View;
    using UnityEngine;

    public class Bullet : AbstractView
    {
        public Vector2 Direction;
        
        public float Speed = 5f;

        private void Update()
        {
            transform.Translate(Direction * (Time.deltaTime * Speed));
        }

        protected override IArchitecture Architecture { get => Game.Interface; }
    }
}