// ------------------------------------------------------------
// @file       AudioPlayer.cs
// @brief
// @author     zheliku
// @Modified   2024-11-14 11:11:21
// @Copyright  Copyright (c) 2024, zheliku
// ------------------------------------------------------------

namespace Framework.Toolkits.AudioKit
{
    using System;
    using Core;
    using PoolKit;
    using Sirenix.OdinInspector;
    using TimerKit;
    using UnityEngine;
    using Object = UnityEngine.Object;

    [HideReferenceObjectPicker]
    public class AudioPlayer : IPoolable, IPoolType
    {
    #region 常量

    #endregion

    #region Static

        public static AudioPlayer Create(BindableProperty<float> volume)
        {
            var player = SingletonObjectPool<AudioPlayer>.Instance.Create();
            player.Volume         = volume; // 指向 AudioKitSetting 中的 Volume
            player._onStart       = null;
            player._onFinish      = null;
            player._settingVolume = 1;
            player._volumeScale   = 1;

            // 创建时，为 AudioKitSetting 中的 Volume 绑定事件：更改值时，同步更改 AudioSource 的音量
            player.Volume.RegisterWithInitValue(player.OnAudioSettingVolumeChanged);

            return player;
        }

    #endregion

    #region 字段

        private IAudioLoader _loader;

        [ShowInInspector]
        private Timer _timer;

        [ShowInInspector]
        private Action<AudioPlayer> _onStart; // 开始播放的回调

        [ShowInInspector]
        private Action<AudioPlayer> _onFinish; // 播放结束的回调

        private bool  _isPaused;
        private float _volumeScale = 1;
        private float _settingVolume; // AudioKitSetting 中的 Volume，实际音量通过 _volumeScale 和 _settingVolume 共同控制

    #endregion

    #region 属性

        public BindableProperty<float> Volume { get; set; } // 指向 AudioKitSetting 中的 Volume

        [ShowInInspector]
        public string AudioClipName { get; private set; }

        [ShowInInspector]
        public AudioSource AudioSource { get; private set; }

        public bool IsRecycled { get; set; }

        public bool UsedCache { get; set; } = true;

        public bool IsLoop { get; set; }

        [ShowInInspector]
        public AudioClip AudioClip { get; private set; }

        public float PlayedCount { get; private set; } // 已播放次数

    #endregion

    #region 公共方法

        public AudioPlayer OnStart(Action<AudioPlayer> onStart)
        {
            if (onStart == null)
            {
                return this;
            }

            if (_onStart == null)
            {
                _onStart = onStart;
            }
            else
            {
                _onStart += onStart;
            }

            return this;
        }

        public AudioPlayer OnFinish(Action<AudioPlayer> onFinish)
        {
            if (onFinish == null)
            {
                return this;
            }

            if (_onFinish == null)
            {
                _onFinish = onFinish;
            }
            else
            {
                _onFinish += onFinish;
            }

            return this;
        }

        public AudioPlayer VolumeScale(float volumeScale)
        {
            _volumeScale = volumeScale;
            UpdateAudioSourceVolume();
            return this;
        }

        /// <summary>
        /// 加载 Clip 并播放
        /// </summary>
        /// <param name="clipName">clip 名称</param>
        /// <param name="loop">是否循环播放</param>
        /// <param name="attachedObject">将 AudioSource 挂载到哪个 GameObject 上</param>
        /// <returns>自己</returns>
        public AudioPlayer PlayAsync(string clipName, bool loop, GameObject attachedObject = null)
        {
            // 加载同名 Clip，则跳过
            if (string.IsNullOrEmpty(clipName) || AudioClipName == clipName)
            {
                return this;
            }

            if (AudioSource == null)
            {
                if (attachedObject != null)
                {
                    AudioSource = attachedObject.AddComponent<AudioSource>();
                }
                else // 不指定 attachedObject，则默认挂载到 AudioManager.Instance.gameObject 上
                {
                    AudioSource = AudioManager.Instance.gameObject.AddComponent<AudioSource>();
                }
            }

            // 防止卸载后立马加载的情况
            var preLoader = _loader;
            _loader = null;
            ClearResources();

            _loader = AudioKit.AudioLoaderPool.Create();

            // 记录设置
            IsLoop        = loop;
            AudioClipName = clipName;

            _loader.LoadClipAsync(AudioClipName, (success, clip) =>
            {
                if (!success)
                {
                    Release();
                    return;
                }

                // 记录 clip
                AudioClip = clip;

                if (AudioClip == null)
                {
                    Debug.LogError("Asset Is Invalid AudioClip: " + AudioClipName);
                    Release();
                    return;
                }

                PlayInternal(); // 依据记录信息进行播放
            });

            if (preLoader != null)
            {
                preLoader.Unload();
                AudioKit.AudioLoaderPool.Recycle(preLoader);
            }

            return this;
        }

        /// <summary>
        /// 播放已有 AudioClip
        /// </summary>
        /// <param name="clip">音频</param>
        /// <param name="loop">是否循环</param>
        /// <param name="attachedObject">将 AudioSource 挂载到哪个 GameObject 上</param>
        public void PlayClip(AudioClip clip, bool loop, GameObject attachedObject = null)
        {
            if (clip == null)
            {
                return;
            }

            if (AudioSource == null)
            {
                if (attachedObject != null)
                {
                    AudioSource = attachedObject.AddComponent<AudioSource>();
                }
                else
                {
                    AudioSource = AudioManager.Instance.gameObject.AddComponent<AudioSource>();
                }
            }

            ClearResources();

            IsLoop    = loop;
            AudioClip = clip;

            PlayInternal();
        }

        public void Stop()
        {
            Release();
        }

        [ShowInInspector]
        public void Pause()
        {
            if (_isPaused)
            {
                return;
            }

            if (_timer != null)
            {
                _timer.Paused = true;
            }

            _isPaused = true;

            AudioSource.Pause();
        }

        [ShowInInspector]
        public void UnPause()
        {
            if (!_isPaused)
            {
                return;
            }

            _timer.Paused = false;
            _isPaused     = false;

            AudioSource.Play(); // 与 UnPause 方法相同
        }


        public void OnSpawn() { }

        public void OnRecycle()
        {
            Volume?.UnRegister(OnAudioSettingVolumeChanged);
            Volume = null;

            _onStart    = null;
            _onFinish   = null;
            PlayedCount = 0;

            ClearResources();
        }

        public void OnDestroy() { }

        public void RecycleToCache()
        {
            if (!SingletonObjectPool<AudioPlayer>.Instance.Recycle(this))
            {
                // 如果无法回收，则直接销毁 AudioSource
                if (AudioSource != null)
                {
                    Object.Destroy(AudioSource);
                    AudioSource = null;
                }
            }
        }

    #endregion

    #region 其他方法

        private void ClearResources()
        {
            AudioClipName = null;
            _isPaused     = false;

            if (_timer != null) // 回收 _timer
            {
                _timer.Cancel();
                _timer.RecycleToCache();
                _timer = null;
            }

            if (AudioSource != null) // 不回收 _audioSource，重复利用
            {
                if (AudioSource.clip == AudioClip)
                {
                    AudioSource.Stop();
                    AudioSource.clip = null;
                }
            }

            AudioClip = null;

            if (_loader != null)
            {
                _loader.Unload();
                AudioKit.AudioLoaderPool.Recycle(_loader);
                _loader = null;
            }
        }

        private void Release()
        {
            ClearResources();

            if (UsedCache)
            {
                RecycleToCache();
            }
        }

        /// <summary>
        /// 事件：更改值时，同步更改 AudioSource 的音量
        /// </summary>
        /// <param name="oldValue">更改前的音量</param>
        /// <param name="volume">更改后的音量</param>
        private void OnAudioSettingVolumeChanged(float oldValue, float volume)
        {
            _settingVolume = volume;

            UpdateAudioSourceVolume();
        }

        /// <summary>
        /// 同步更改 AudioSource 的音量
        /// </summary>
        private void UpdateAudioSourceVolume()
        {
            if (AudioSource)
            {
                AudioSource.volume = _volumeScale * _settingVolume;
            }
        }

        /// <summary>
        /// 依据记录信息进行播放
        /// </summary>
        private void PlayInternal()
        {
            if (AudioSource == null || AudioClip == null)
            {
                Release();
                return;
            }

            AudioSource.clip = AudioClip;
            AudioSource.loop = IsLoop;
            UpdateAudioSourceVolume();

            _timer = TimerKit.CreateScaled(OnAudioClipPlayFinish, AudioClip.length, IsLoop ? -1 : 1);

            _onStart?.Invoke(this);

            AudioSource.Play();
        }

        /// <summary>
        /// 事件：播放结束时执行的方法
        /// </summary>
        /// <param name="timer">依附的计时器</param>
        private void OnAudioClipPlayFinish(Timer timer)
        {
            PlayedCount++;

            _onFinish?.Invoke(this);

            if (!IsLoop)
            {
                Release();
            }
        }

    #endregion
    }
}