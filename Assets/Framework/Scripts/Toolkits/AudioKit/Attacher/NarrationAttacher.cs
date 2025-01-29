// ------------------------------------------------------------
// @file       NarrationAttacher.cs
// @brief
// @author     zheliku
// @Modified   2024-11-16 03:11:35
// @Copyright  Copyright (c) 2024, zheliku
// ------------------------------------------------------------

namespace Framework.Toolkits.AudioKit
{
    using SingletonKit;

    /// <summary>
    /// 仅用于挂载 Narration 的 AudioSource
    /// </summary>
    [MonoSingletonPath("Framework/AudioKit/AudioManager/NarrationAttacher")]
    public class NarrationAttacher : MonoSingleton<NarrationAttacher>
    { }
}