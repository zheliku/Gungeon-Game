// ------------------------------------------------------------
// @file       MusicAttacher.cs
// @brief
// @author     zheliku
// @Modified   2024-11-16 03:11:02
// @Copyright  Copyright (c) 2024, zheliku
// ------------------------------------------------------------

namespace Framework.Toolkits.AudioKit
{
    using SingletonKit;

    /// <summary>
    /// 仅用于挂载 Music 的 AudioSource
    /// </summary>
    [MonoSingletonPath("Framework/AudioKit/AudioMgr/MusicAttacher")]
    public class MusicAttacher : MonoSingleton<MusicAttacher>
    { }
}