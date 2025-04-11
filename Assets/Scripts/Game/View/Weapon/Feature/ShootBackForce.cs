// ------------------------------------------------------------
// @file       ShootBackForce.cs
// @brief
// @author     zheliku
// @Modified   2025-04-12 03:04:25
// @Copyright  Copyright (c) 2025, zheliku
// ------------------------------------------------------------

namespace Game
{
    using Framework.Toolkits.FluentAPI;
    using UnityEngine;

    public class ShootBackForce
    {
        private SpriteRenderer _spriteRenderer;

        private Vector2 _spriteOriginLocalPos;
        private Vector2 _spriteBackwardPos;

        private int _spriteBackwardFrames;
        private int _spriteBackwardTotalFrames;

        public ShootBackForce(SpriteRenderer spriteRenderer)
        {
            _spriteRenderer       = spriteRenderer;
            _spriteOriginLocalPos = spriteRenderer.GetLocalPosition();
        }

        public void Update()
        {
            if (_spriteBackwardFrames > 0)
            {
                var pos = _spriteOriginLocalPos.Lerp(_spriteBackwardPos, (float) _spriteBackwardFrames / _spriteBackwardTotalFrames);
                _spriteRenderer.SetLocalPosition(pos);
                _spriteBackwardFrames--;
            }
        }

        public void Shoot(float A, int frames)
        {
            _spriteBackwardPos         = _spriteOriginLocalPos + Vector2.left * A * 2;
            _spriteBackwardFrames      = frames * 2;
            _spriteBackwardTotalFrames = frames * 2;
        }
    }
}