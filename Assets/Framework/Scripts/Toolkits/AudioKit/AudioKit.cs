// ------------------------------------------------------------
// @file       AudioKit.cs
// @brief
// @author     zheliku
// @Modified   2024-11-14 10:11:32
// @Copyright  Copyright (c) 2024, zheliku
// ------------------------------------------------------------

namespace Framework.Toolkits.AudioKit
{
    using System;
    using System.Collections.Generic;
    using PoolKit;
    using UnityEngine;

    /// <summary>
    /// 播放模式
    /// </summary>
    public enum SoundMode
    {
        EveryOne,                      // 随时可播放
        IgnoreSameSoundInGlobalFrames, // 每相邻 GlobalFrameCountForIgnoreSameSound 帧内进行检查，不播放相同音效
        IgnoreSameSoundInSoundFrames,  // 相同音效在各自记录的 SoundFrameCountForIgnoreSameSound 内，不连续播放
        IgnoreSameSoundInGlobalTimes,  // 每相邻 GlobalTimesForIgnoreSameSound 时间内进行检查，不播放相同音效
        IgnoreSameSoundInSoundTimes    // 相同音效在各自记录的 SoundTimesForIgnoreSameSound 内，不连续播放
    }

    public class AudioKit
    {
        public static AudioKitSetting Setting { get => AudioKitSetting.Instance; }

        public static ObjectPool<IAudioLoader> AudioLoaderPool { get; set; } = new ObjectPool<IAudioLoader>(
            () => new DefaultAudioLoader(),
            initCount: 10
        );

        public static AudioPlayer MusicPlayer { get => AudioManager.Instance.MusicPlayer; }

        public static AudioPlayer NarrationPlayer { get => AudioManager.Instance.NarrationPlayer; }

        public static SoundMode SoundMode { get; set; } = SoundMode.IgnoreSameSoundInGlobalTimes;

        public static int GlobalFrameCountForIgnoreSameSound = 10; // 每相邻 GlobalFrameCountForIgnoreSameSound 帧内进行检查，不播放相同音效
        public static int SoundFrameCountForIgnoreSameSound  = 10; // 相同音效在各自记录的 SoundFrameCountForIgnoreSameSound 内，不连续播放

        public static float GlobalTimeForIgnoreSameSound = 0.16f; // 每相邻 GlobalTimesForIgnoreSameSound 时间内进行检查，不播放相同音效
        public static float SoundTimeForIgnoreSameSound  = 0.16f; // 相同音效在各自记录的 SoundTimesForIgnoreSameSound 内，不连续播放

        private static Dictionary<string, int>   _SoundFrameCountDict = new Dictionary<string, int>();
        private static Dictionary<string, float> _SoundTimeDict       = new Dictionary<string, float>();

        private static int   _GlobalFrameCount = 0; // 当前帧数
        private static float _GlobalTime       = 0; // 当前时间

        /// <summary>
        /// 播放背景音乐（异步加载音频）
        /// </summary>
        /// <param name="musicName">背景音乐名</param>
        /// <param name="volume">音量</param>
        /// <param name="loop">是否循环</param>
        /// <param name="onPlayStart">开始播放回调</param>
        /// <param name="onPlayFinish">播放结束回调</param>
        public static void PlayMusic(
            string              musicName,
            float               volume       = 1,
            bool                loop         = true,
            Action<AudioPlayer> onPlayStart  = null,
            Action<AudioPlayer> onPlayFinish = null)
        {
            var mgr = AudioManager.Instance;
            mgr.CheckAudioListener();

            if (!Setting.IsMusicOn.Value)
            {
                return;
            }

            MusicPlayer.VolumeScale(volume)
               .OnStart((player) =>
                {
                    AudioManager.Instance.CurrentMusic = player.AudioClip;
                    onPlayStart?.Invoke(player);
                })
               .OnFinish((player) =>
                {
                    if (!player.IsLoop)
                    {
                        AudioManager.Instance.CurrentMusic = null;
                    }
                    onPlayFinish?.Invoke(player);
                })
               .PlayAsync(musicName, loop, MusicAttacher.Instance.gameObject);
        }

        /// <summary>
        /// 播放背景音乐
        /// </summary>
        /// <param name="clip">背景音乐音频</param>
        /// <param name="volume">音量</param>
        /// <param name="loop">是否循环</param>
        /// <param name="onPlayStart">开始播放回调</param>
        /// <param name="onPlayFinish">播放结束回调</param>
        public static void PlayMusic(
            AudioClip           clip,
            float               volume       = 1,
            bool                loop         = true,
            Action<AudioPlayer> onPlayStart  = null,
            Action<AudioPlayer> onPlayFinish = null)
        {
            var mgr = AudioManager.Instance;
            mgr.CheckAudioListener();

            if (!Setting.IsMusicOn.Value)
            {
                return;
            }

            MusicPlayer.VolumeScale(volume)
               .OnStart((player) =>
                {
                    AudioManager.Instance.CurrentMusic = player.AudioClip;
                    onPlayStart?.Invoke(player);
                })
               .OnFinish((player) =>
                {
                    if (!player.IsLoop)
                    {
                        AudioManager.Instance.CurrentMusic = null;
                    }
                    onPlayFinish?.Invoke(player);
                })
               .PlayClip(clip, loop, MusicAttacher.Instance.gameObject);
        }

        /// <summary>
        /// 停止播放背景音乐
        /// </summary>
        public static void StopMusic()
        {
            AudioManager.Instance.MusicPlayer.Stop();
        }

        /// <summary>
        /// 暂停播放背景音乐
        /// </summary>
        public static void PauseMusic()
        {
            AudioManager.Instance.MusicPlayer.Pause();
        }

        /// <summary>
        /// 恢复播放背景音乐
        /// </summary>
        public static void UnPauseMusic()
        {
            AudioManager.Instance.MusicPlayer.UnPause();
        }

        /// <summary>
        /// 播放背景人声（异步加载音频）
        /// </summary>
        /// <param name="narrationName">背景人声名</param>
        /// <param name="volume">音量</param>
        /// <param name="loop">是否循环</param>
        /// <param name="onPlayStart">开始播放回调</param>
        /// <param name="onPlayFinish">播放结束回调</param>
        public static void PlayNarration(
            string              narrationName,
            float               volume       = 1,
            bool                loop         = true,
            Action<AudioPlayer> onPlayStart  = null,
            Action<AudioPlayer> onPlayFinish = null)
        {
            var mgr = AudioManager.Instance;
            mgr.CheckAudioListener();

            if (!Setting.IsNarrationOn.Value)
            {
                return;
            }

            NarrationPlayer.VolumeScale(volume)
               .OnStart((player) =>
                {
                    AudioManager.Instance.CurrentNarration = player.AudioClip;
                    onPlayStart?.Invoke(player);
                })
               .OnFinish((player) =>
                {
                    if (!player.IsLoop)
                    {
                        AudioManager.Instance.CurrentNarration = null;
                    }
                    onPlayFinish?.Invoke(player);
                })
               .PlayAsync(narrationName, loop, NarrationAttacher.Instance.gameObject);
        }

        /// <summary>
        /// 播放背景人声
        /// </summary>
        /// <param name="clip">背景人声音频</param>
        /// <param name="volume">音量</param>
        /// <param name="loop">是否循环</param>
        /// <param name="onPlayStart">开始播放回调</param>
        /// <param name="onPlayFinish">播放结束回调</param>
        public static void PlayNarration(
            AudioClip           clip,
            float               volume       = 1,
            bool                loop         = true,
            Action<AudioPlayer> onPlayStart  = null,
            Action<AudioPlayer> onPlayFinish = null)
        {
            var mgr = AudioManager.Instance;
            mgr.CheckAudioListener();

            if (!Setting.IsNarrationOn.Value)
            {
                return;
            }

            NarrationPlayer.VolumeScale(volume)
               .OnStart((player) =>
                {
                    AudioManager.Instance.CurrentNarration = player.AudioClip;
                    onPlayStart?.Invoke(player);
                })
               .OnFinish((player) =>
                {
                    if (!player.IsLoop)
                    {
                        AudioManager.Instance.CurrentNarration = null;
                    }
                    onPlayFinish?.Invoke(player);
                })
               .PlayClip(clip, loop, NarrationAttacher.Instance.gameObject);
        }

        /// <summary>
        /// 停止播放背景人声
        /// </summary>
        public static void StopNarration()
        {
            AudioManager.Instance.NarrationPlayer.Stop();
        }

        /// <summary>
        /// 暂停播放背景人声
        /// </summary>
        public static void PauseNarration()
        {
            AudioManager.Instance.NarrationPlayer.Pause();
        }

        /// <summary>
        /// 恢复播放背景人声
        /// </summary>
        public static void UnPauseNarration()
        {
            AudioManager.Instance.NarrationPlayer.UnPause();
        }

        /// <summary>
        /// 播放音效（异步加载音频）
        /// </summary>
        /// <param name="soundName">音效名</param>
        /// <param name="volume">音量</param>
        /// <param name="loop">是否循环</param>
        /// <param name="onPlayStart">开始播放回调</param>
        /// <param name="onPlayFinish">播放结束回调</param>
        public static AudioPlayer PlaySound(
            string              soundName,
            float               volume       = 1,
            bool                loop         = false,
            Action<AudioPlayer> onPlayStart  = null,
            Action<AudioPlayer> onPlayFinish = null
        )
        {
            var mgr = AudioManager.Instance;
            mgr.CheckAudioListener();

            if (!Setting.IsSoundOn.Value)
            {
                return null;
            }

            if (!CanPlaySound(soundName))
            {
                return null;
            }

            var player = AudioPlayer.Create(Setting.SoundVolume);
            player.VolumeScale(volume)
               .OnStart(onPlayStart)
               .PlayAsync(soundName, loop, SoundAttacher.Instance.gameObject)
               .OnFinish((audioPlayer) =>
                {
                    onPlayFinish?.Invoke(audioPlayer);
                    mgr.RemoveSoundPlayerFromPool(audioPlayer);
                    if (SoundMode == SoundMode.IgnoreSameSoundInSoundFrames)
                    {
                        _SoundFrameCountDict.Remove(soundName);
                    }
                });

            mgr.AddSoundPlayerToPool(player);
            return player;
        }

        /// <summary>
        /// 播放音效
        /// </summary>
        /// <param name="clip">音效音频</param>
        /// <param name="volume">音量</param>
        /// <param name="loop">是否循环</param>
        /// <param name="onPlayStart">开始播放回调</param>
        /// <param name="onPlayFinish">播放结束回调</param>
        public static AudioPlayer PlaySound(
            AudioClip           clip,
            float               volume       = 1,
            bool                loop         = false,
            Action<AudioPlayer> onPlayStart  = null,
            Action<AudioPlayer> onPlayFinish = null
        )
        {
            var mgr = AudioManager.Instance;
            mgr.CheckAudioListener();

            if (!Setting.IsSoundOn.Value)
            {
                return null;
            }

            if (!CanPlaySound(clip.name))
            {
                return null;
            }

            var player = AudioPlayer.Create(Setting.SoundVolume);
            player.VolumeScale(volume)
               .OnStart(onPlayStart)
               .OnFinish((audioPlayer) =>
                {
                    onPlayFinish?.Invoke(audioPlayer);
                    mgr.RemoveSoundPlayerFromPool(audioPlayer);
                    if (SoundMode == SoundMode.IgnoreSameSoundInSoundFrames)
                    {
                        _SoundFrameCountDict.Remove(clip.name);
                    }
                })
               .PlayClip(clip, loop, SoundAttacher.Instance.gameObject);

            mgr.AddSoundPlayerToPool(player);
            return player;
        }

        /// <summary>
        /// 停止播放所有音效
        /// </summary>
        public static void StopAllSound()
        {
            AudioManager.Instance.ForEachSound(player => player.Stop());

            AudioManager.Instance.ClearAllPlayingSound();
        }

        /// <summary>
        /// 依据当前播放模式，判断是否可播放音效
        /// </summary>
        /// <param name="soundName">音效名</param>
        /// <returns>是否可播放</returns>
        private static bool CanPlaySound(string soundName)
        {
            // 如果声音模式为 EveryOne，则返回 true
            if (SoundMode == SoundMode.EveryOne)
            {
                return true;
            }

            if (SoundMode == SoundMode.IgnoreSameSoundInGlobalFrames)
            {
                return CanPlaySoundInIgnoreSameSoundInGlobalFramesMode(soundName);
            }

            if (SoundMode == SoundMode.IgnoreSameSoundInSoundFrames)
            {
                return CanPlaySoundInIgnoreSameSoundInSoundFramesMode(soundName);
            }
            
            if (SoundMode == SoundMode.IgnoreSameSoundInGlobalTimes)
            {
                return CanPlaySoundInIgnoreSameSoundInGlobalTimesMode(soundName);
            }

            if (SoundMode == SoundMode.IgnoreSameSoundInSoundTimes)
            {
                return CanPlaySoundInIgnoreSameSoundInSoundTimesMode(soundName);
            }

            return false;
        }
        
        private static bool CanPlaySoundInIgnoreSameSoundInGlobalFramesMode(string soundName)
        {
            // 判断当前帧数与全局帧数的差值是否小于等于忽略相同声音的全局帧数
            if (Time.frameCount - _GlobalFrameCount <= GlobalFrameCountForIgnoreSameSound)
            {
                // 如果字典中已经存在该声音路径，则返回 false
                if (!_SoundFrameCountDict.TryAdd(soundName, 0))
                {
                    return false;
                }
            }
            else
            {
                // 如果当前帧数与全局帧数的差值大于忽略相同声音的全局帧数，则更新全局帧数，清空字典，并添加当前声音路径
                _GlobalFrameCount = Time.frameCount;
                _SoundFrameCountDict.Clear();
                _SoundFrameCountDict.Add(soundName, 0);
            }

            return true;
        }
        
        private static bool CanPlaySoundInIgnoreSameSoundInSoundFramesMode(string soundName)
        {
            // 判断当前帧数与声音帧数的差值是否小于等于忽略相同声音的声音帧数
            if (_SoundFrameCountDict.TryGetValue(soundName, out var frameCount))
            {
                // 如果字典中已经存在该声音路径，则判断当前帧数与声音帧数的差值是否小于等于忽略相同声音的声音帧数
                if (Time.frameCount - frameCount <= SoundFrameCountForIgnoreSameSound)
                {
                    return false;
                }

                // 更新声音帧数
                _SoundFrameCountDict[soundName] = Time.frameCount;
            }
            else
            {
                // 字典中不存在该声音路径，则添加当前声音路径
                _SoundFrameCountDict.Add(soundName, Time.frameCount);
            }

            return true;
        }
        
        private static bool CanPlaySoundInIgnoreSameSoundInGlobalTimesMode(string soundName)
        {
            // 判断当前时间与全局时间的差值是否小于等于忽略相同声音的全局时间
            if (Time.unscaledTime - _GlobalTime <= GlobalTimeForIgnoreSameSound)
            {
                // 如果字典中已经存在该声音路径，则返回 false
                if (!_SoundTimeDict.TryAdd(soundName, 0))
                {
                    return false;
                }
            }
            else
            {
                // 如果当前时间与全局时间的差值大于忽略相同声音的全局时间，则更新全局时间，清空字典，并添加当前声音路径
                _GlobalTime = Time.unscaledTime;
                _SoundTimeDict.Clear();
                _SoundTimeDict.Add(soundName, 0);
            }

            return true;
        }

        private static bool CanPlaySoundInIgnoreSameSoundInSoundTimesMode(string soundName)
        {
            // 判断当前时间与声音时间的差值是否小于等于忽略相同声音的声音时间
            if (_SoundTimeDict.TryGetValue(soundName, out var time))
            {
                // 如果字典中已经存在该声音路径，则判断当前时间与声音时间的差值是否小于等于忽略相同声音的声音时间
                if (Time.unscaledTime - time <= SoundTimeForIgnoreSameSound)
                {
                    return false;
                }

                // 更新声音时间
                _SoundTimeDict[soundName] = Time.unscaledTime;
            }
            else
            {
                // 字典中不存在该声音路径，则添加当前声音路径
                _SoundTimeDict.Add(soundName, Time.unscaledTime);
            }

            return true;
        }
    }
}