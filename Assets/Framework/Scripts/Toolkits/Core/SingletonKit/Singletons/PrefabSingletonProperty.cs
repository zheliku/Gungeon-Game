// ------------------------------------------------------------
// @file       PrefabSingletonProperty.cs
// @brief
// @author     zheliku
// @Modified   2024-10-20 19:10:46
// @Copyright  Copyright (c) 2024, zheliku
// ------------------------------------------------------------

namespace Framework.Toolkits.SingletonKit
{
    using System;
    using Framework.Toolkits.FluentAPI;
    using UnityEngine;
    using Object = UnityEngine.Object;

    public static class PrefabSingletonProperty<TSingleton> where TSingleton : MonoBehaviour
    {
        public static Func<string, GameObject> PrefabLoader = Resources.Load<GameObject>;

        private static TSingleton _Instance;

        public static TSingleton InstanceWithLoader(Func<string, GameObject> loader)
        {
            PrefabLoader = loader;
            return Instance;
        }

        public static TSingleton Instance
        {
            get
            {
                if (_Instance == null)
                {
                    _Instance = Object.FindFirstObjectByType<TSingleton>();
                    if (!_Instance)
                    {
                        var prefab = PrefabLoader?.Invoke(typeof(TSingleton).Name);
                        if (prefab)
                        {
                            _Instance = prefab.Instantiate().GetComponent<TSingleton>();
                            _Instance.DontDestroyOnLoad();
                        }
                    }
                }
                return _Instance;
            }
        }

        public static void Dispose()
        {
            Object.Destroy(_Instance.gameObject);

            _Instance = null;
        }
    }
}