// ------------------------------------------------------------
// @file       ScreenTransitionFade.cs
// @brief
// @author     zheliku
// @Modified   2024-10-31 15:10:48
// @Copyright  Copyright (c) 2024, zheliku
// ------------------------------------------------------------

namespace Framework.Toolkits.ActionKit
{
    using FluentAPI;
    using UnityEngine;

    public class ScreenTransitionFade : AbstractAction<ScreenTransitionFade>
    {
        private Color _color          = UnityEngine.Color.black;
        private float _fromAlpha      = 0;
        private float _toAlpha        = 0;
        private float _duration       = 1.0f;
        private float _currentSeconds = 0;
        
        public static ScreenTransitionFade Create()
        {
            return CreateInternal();
        }

        public ScreenTransitionFade Color(Color color)
        {
            _color = color;
            return this;
        }

        public ScreenTransitionFade FromAlpha(float alpha)
        {
            _fromAlpha = alpha;
            return this;
        }

        public ScreenTransitionFade ToAlpha(float alpha)
        {
            _toAlpha = alpha;
            return this;
        }

        public ScreenTransitionFade Duration(float duration)
        {
            _duration = duration;
            return this;
        }

        public override void OnCreate() { }

        public override void OnStart()
        {
            ScreenTransitionCanvas.Instance.ColorImage.color = _color;
            ScreenTransitionCanvas.Instance.ColorImage.SetColor(a: _fromAlpha);
            _currentSeconds = 0;
        }

        public override void OnExecute(float dt)
        {
            _currentSeconds += dt;

            var alpha = Mathf.Lerp(_fromAlpha, _toAlpha, _currentSeconds / _duration);
            ScreenTransitionCanvas.Instance.ColorImage.SetColor(a: alpha);

            if (_currentSeconds >= _duration)
            {
                this.Finish();
            }
        }

        public override void OnFinish()
        {
            ScreenTransitionCanvas.Instance.ColorImage.SetColor(a: _toAlpha);
        }

        protected override void OnReset() { }

        protected override void OnDeinit() { }
    }
}