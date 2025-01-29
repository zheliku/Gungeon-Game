// ------------------------------------------------------------
// @file       ScriptableSingletonProperty.cs
// @brief
// @author     zheliku
// @Modified   2024-10-20 19:10:35
// @Copyright  Copyright (c) 2024, zheliku
// ------------------------------------------------------------

namespace Framework.Toolkits.SingletonKit
{
    using System;
    using UnityEngine;

    public static class ScriptableSingletonProperty<T> where T : ScriptableObject
    {
        public static Func<string, T> ScriptableLoader = Resources.Load<T>;

        private static T _Instance;

        public static T InstanceWithLoader(Func<string, T> loader)
        {
            ScriptableLoader = loader;
            return Instance;
        }

        public static T Instance
        {
            get
            {
                if (_Instance == null) _Instance = ScriptableLoader?.Invoke(typeof(T).Name);
                return _Instance;
            }
        }

        public static void Dispose()
        {
            Resources.UnloadAsset(_Instance);
            
            _Instance = null;
        }
    }
}