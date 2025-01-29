// ------------------------------------------------------------
// @file       PersistentMonoSingleton.cs
// @brief
// @author     zheliku
// @Modified   2024-10-20 19:10:19
// @Copyright  Copyright (c) 2024, zheliku
// ------------------------------------------------------------

namespace Framework.Toolkits.SingletonKit
{
    using UnityEngine;

    /// <summary>
    /// 当场景里包含两个 PersistentMonoSingleton，保留先创建的
    /// </summary>
    public abstract class PersistentMonoSingleton<TSingleton> : MonoBehaviour where TSingleton : Component
    {
        protected static TSingleton _Instance;

        public static TSingleton Instance
        {
            get
            {
                if (_Instance != null) 
                    return _Instance;
                
                _Instance = FindFirstObjectByType<TSingleton>();
                if (_Instance != null) 
                    return _Instance;
                
                var obj = new GameObject();
                _Instance = obj.AddComponent<TSingleton>();
                return _Instance;
            }
        }

        // 当场景里包含两个 PersistentMonoSingleton，保留先创建的
        protected virtual void Awake()
        {
            if (!Application.isPlaying)
            {
                return;
            }

            if (_Instance == null)
            {
                _Instance = this as TSingleton;
                DontDestroyOnLoad(transform.gameObject);
            }
            else
            {
                if (this != _Instance)
                {
                    Destroy(gameObject);
                }
            }
        }
    }
}