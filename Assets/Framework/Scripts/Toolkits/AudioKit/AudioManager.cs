// ------------------------------------------------------------
// @file       AudioManager.cs
// @brief
// @author     zheliku
// @Modified   2024-11-14 17:11:27
// @Copyright  Copyright (c) 2024, zheliku
// ------------------------------------------------------------

namespace Framework.Toolkits.AudioKit
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Core;
    using PoolKit;
    using SingletonKit;
    using Sirenix.OdinInspector;
    using UnityEngine;
    
#if UNITY_EDITOR
    using UnityEditor;
#endif

    [MonoSingletonPath("Framework/AudioKit/AudioManager")]
    public class AudioManager : MonoSingleton<AudioManager>
    {
    #region Static

        [ShowInInspector]
        private static Dictionary<string, List<AudioPlayer>> _SoundPlayerInPlaying = new Dictionary<string, List<AudioPlayer>>(30);

    #endregion

    #region 字段

        [ShowInInspector]
        private AudioListener _audioListener;

    #endregion

    #region 属性

        [ShowInInspector]
        public AudioPlayer MusicPlayer { get; private set; }

        [ShowInInspector]
        public AudioPlayer NarrationPlayer { get; private set; }

        [ShowInInspector]
        public AudioClip CurrentMusic { get; set; }

        [ShowInInspector]
        public AudioClip CurrentNarration { get; set; }

    #endregion

    #region 公共方法

        public override void OnSingletonInit()
        {
            SingletonObjectPool<AudioPlayer>.Instance.Init(2, 20);

            // 与 AudioKit.Setting.MusicVolume 绑定
            MusicPlayer           = AudioPlayer.Create(AudioKit.Setting.MusicVolume);
            MusicPlayer.UsedCache = false;
            MusicPlayer.IsLoop    = true;

            // 与 AudioKit.Setting.NarrationVolume 绑定
            NarrationPlayer           = AudioPlayer.Create(AudioKit.Setting.NarrationVolume);
            NarrationPlayer.UsedCache = false;
            NarrationPlayer.IsLoop    = false;

            CheckAudioListener();

            AudioKit.Setting.IsMusicOn.Register((oldValue, musicOn) =>
            {
                // 更改 IsMusicOn 即可控制 Music 播放
                if (musicOn)
                {
                    if (CurrentMusic != null)
                    {
                        AudioKit.PlayMusic(CurrentMusic);
                    }
                }
                else
                {
                    MusicPlayer.Stop();
                }
            }).UnRegisterWhenGameObjectDestroyed(gameObject);

            AudioKit.Setting.IsNarrationOn.Register((oldValue, narrationOn) =>
            {
                // 更改 IsNarrationOn 即可控制 Narration 播放
                if (narrationOn)
                {
                    if (CurrentNarration != null)
                    {
                        AudioKit.PlayNarration(CurrentNarration);
                    }
                }
                else
                {
                    NarrationPlayer.Stop();
                }
            }).UnRegisterWhenGameObjectDestroyed(gameObject);

            AudioKit.Setting.IsSoundOn.Register((oldValue, soundOn) =>
            {
                // 更改 IsSoundOn 即可控制 Sound 播放，打开时不播放已停止的音效
                if (soundOn)
                { }
                else
                {
                    ForEachSound(soundPlayer => soundPlayer.Stop());
                }
            }).UnRegisterWhenGameObjectDestroyed(gameObject);
        }

        /// <summary>
        /// 确认 AudioListener 存在
        /// </summary>
        public void CheckAudioListener()
        {
            _audioListener ??= FindFirstObjectByType<AudioListener>();

            _audioListener ??= gameObject.AddComponent<AudioListener>();
        }

        /// <summary>
        /// 遍历所有 Sound，执行 operation
        /// </summary>
        /// <param name="operation">每个 Sound 执行的方法</param>
        public void ForEachSound(Action<AudioPlayer> operation)
        {
            foreach (var audioPlayer in _SoundPlayerInPlaying.SelectMany(keyValuePair => keyValuePair.Value))
            {
                operation(audioPlayer);
            }
        }

        public void AddSoundPlayerToPool(AudioPlayer audioPlayer)
        {
            if (_SoundPlayerInPlaying.ContainsKey(audioPlayer.AudioClipName))
            {
                _SoundPlayerInPlaying[audioPlayer.AudioClipName].Add(audioPlayer);
            }
            else
            {
                _SoundPlayerInPlaying.Add(audioPlayer.AudioClipName, new List<AudioPlayer> { audioPlayer });
            }
        }

        public void RemoveSoundPlayerFromPool(AudioPlayer audioPlayer)
        {
            _SoundPlayerInPlaying[audioPlayer.AudioClipName].Remove(audioPlayer);
        }

        public void ClearAllPlayingSound()
        {
            _SoundPlayerInPlaying.Clear();
        }

    #endregion
    }
}