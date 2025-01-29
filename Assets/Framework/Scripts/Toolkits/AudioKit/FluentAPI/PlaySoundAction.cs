// ------------------------------------------------------------
// @file       PlaySoundAction.cs
// @brief
// @author     zheliku
// @Modified   2024-11-13 11:11:06
// @Copyright  Copyright (c) 2024, zheliku
// ------------------------------------------------------------

namespace Framework.Toolkits.AudioKit
{
    using System;
    using ActionKit;
    using UnityEngine;

    /// <summary>
    /// 用于支持 ActionKit 的 PlaySoundAction
    /// </summary>
    public class PlaySoundAction : AbstractAction<PlaySoundAction>
    {
        public enum Mode
        {
            ByName,
            ByClip,
        }

        private Mode      _mode;
        private string    _soundPath;
        private AudioClip _audioClip;
        private Action    _onFinish;

        public static PlaySoundAction Spawn(string soundPath, Action onFinish = null)
        {
            var playSoundAction = CreateInternal();
            playSoundAction._mode      = Mode.ByName;
            playSoundAction._soundPath = soundPath;
            playSoundAction._onFinish  = onFinish;
            return playSoundAction;
        }

        public static PlaySoundAction Spawn(AudioClip audioClip, Action onFinish = null)
        {
            var playSoundAction = CreateInternal();
            playSoundAction._mode      = Mode.ByClip;
            playSoundAction._audioClip = audioClip;
            playSoundAction._onFinish  = onFinish;
            return playSoundAction;
        }

        public override void OnCreate() { }

        public override void OnStart()
        {
            if (_mode == Mode.ByName)
            {
                AudioKit.PlaySound(_soundPath, onPlayFinish: player => this.Finish());
            }
            else if (_mode == Mode.ByClip)
            {
                AudioKit.PlaySound(_audioClip, onPlayFinish: player => this.Finish());
            }
        }
        
        public override void OnFinish()
        {
            _onFinish?.Invoke();
        }

        public override void OnExecute(float deltaTime) { }

        protected override void OnReset() { }

        protected override void OnDeinit()
        {
            _soundPath = null;
            _audioClip = null;
            _onFinish  = null;
        }
    }

    public static class PlaySoundActionExtension
    {
        public static ISequence PlaySound(this ISequence self, string soundPath)
        {
            return self.Append(PlaySoundAction.Spawn(soundPath));
        }

        public static ISequence PlaySound(this ISequence self, AudioClip clip)
        {
            return self.Append(PlaySoundAction.Spawn(clip));
        }
    }
}