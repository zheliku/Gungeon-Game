// ------------------------------------------------------------
// @file       IAudioLoader.cs
// @brief
// @author     zheliku
// @Modified   2024-11-14 10:11:44
// @Copyright  Copyright (c) 2024, zheliku
// ------------------------------------------------------------

namespace Framework.Toolkits.AudioKit
{
    using System;
    using UnityEngine;

    public interface IAudioLoader
    {
        AudioClip Clip { get; }

        AudioClip LoadClip(string audioName);

        void LoadClipAsync(string audioName, Action<bool, AudioClip> onLoad = null);

        void Unload();
    }
}