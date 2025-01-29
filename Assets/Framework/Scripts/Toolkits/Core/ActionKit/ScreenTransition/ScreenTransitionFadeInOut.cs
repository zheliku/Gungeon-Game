// ------------------------------------------------------------
// @file       ScreenTransitionInOut.cs
// @brief
// @author     zheliku
// @Modified   2024-10-31 15:10:17
// @Copyright  Copyright (c) 2024, zheliku
// ------------------------------------------------------------

namespace Framework.Toolkits.ActionKit
{
    using System;
    using UnityEngine;

    public class ScreenTransitionFadeInOut : AbstractAction<ScreenTransitionFadeInOut>
    {
        private Action<ScreenTransitionFade> _onSetIn;
        private Action<ScreenTransitionFade> _onSetOut;

        private float _intervalTime;

        private ScreenTransitionFade _in;
        private ScreenTransitionFade _out;
        private Action               _onInFinish;
        private Action               _onOutFinish;

        public static ScreenTransitionFadeInOut Create(ScreenTransitionFade transitionIn, ScreenTransitionFade transitionOut)
        {
            var transitionInOut = CreateInternal();
            transitionInOut._in  = transitionIn;
            transitionInOut._out = transitionOut;
            return transitionInOut;
        }

        public ScreenTransitionFadeInOut OnInFinish(Action onInFinish)
        {
            _onInFinish = onInFinish;
            return this;
        }

        public ScreenTransitionFadeInOut OnOutFinish(Action onOutFinish)
        {
            _onOutFinish = onOutFinish;
            return this;
        }

        public ScreenTransitionFadeInOut In(Action<ScreenTransitionFade> onSetIn)
        {
            _onSetIn = onSetIn;
            return this;
        }

        public ScreenTransitionFadeInOut Out(Action<ScreenTransitionFade> onSetOut)
        {
            _onSetOut = onSetOut;
            return this;
        }

        public ScreenTransitionFadeInOut Color(Color color)
        {
            _in.Color(color);
            _out.Color(color);
            return this;
        }

        public ScreenTransitionFadeInOut IntervalTime(float time)
        {
            _intervalTime = time;
            return this;
        }

        public ScreenTransitionFadeInOut FromAlpha(float alpha)
        {
            _in.FromAlpha(alpha);
            _out.ToAlpha(alpha);
            return this;
        }

        public ScreenTransitionFadeInOut ToAlpha(float alpha)
        {
            _in.ToAlpha(alpha);
            _out.FromAlpha(alpha);
            return this;
        }
        
        public ScreenTransitionFadeInOut InDuration(float duration)
        {
            _in.Duration(duration);
            return this;
        }
        
        public ScreenTransitionFadeInOut OutDuration(float duration)
        {
            _out.Duration(duration);
            return this;
        }

        public override void OnCreate() { }

        public override void OnStart()
        {
            _onSetIn?.Invoke(_in);
            _onSetOut?.Invoke(_out);
            ActionKit.Sequence()
               .Append(_in)
               .Callback(() => _onInFinish?.Invoke())
               .Delay(_intervalTime)
               .Append(_out)
               .Callback(() => _onOutFinish?.Invoke())
               .StartGlobal();
        }

        public override void OnExecute(float deltaTime) { }

        public override void OnFinish() { }

        protected override void OnReset() { }

        protected override void OnDeinit() { }
    }
}