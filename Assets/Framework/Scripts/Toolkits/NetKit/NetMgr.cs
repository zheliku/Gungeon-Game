// ------------------------------------------------------------
// @file       NetMgr.cs
// @brief
// @author     zheliku
// @Modified   2025-05-14 11:27:43
// @Copyright  Copyright (c) 2025, zheliku
// ------------------------------------------------------------

namespace Framework.Toolkits.NetKit
{
    using SingletonKit;
    using Sirenix.OdinInspector;

    [MonoSingletonPath("Framework/NetKit")]
    public class NetMgr : MonoSingleton<NetMgr>
    {
        [ShowInInspector]
        public string HttpServerPath
        {
            get => NetKit.HttpServerUrl;
            set => NetKit.HttpServerUrl = value;
        }
    }
}