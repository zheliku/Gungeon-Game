// ------------------------------------------------------------
// @file       ReplaceableMonoSingleton.cs
// @brief
// @author     zheliku
// @Modified   2024-10-20 19:10:03
// @Copyright  Copyright (c) 2024, zheliku
// ------------------------------------------------------------

namespace Framework.Toolkits.SingletonKit
{
    using Framework.Core;
    using UnityEngine;

    /// <summary>
    /// 当场景里包含两个 ReplaceableMonoSingleton，保留最后创建的
    /// </summary>
    public class ReplaceableMonoSingleton<TSingleton> : MonoBehaviour where TSingleton : Component
    {
        protected static TSingleton _Instance;
        
        public float InitializationTime;
        
        public static TSingleton Instance
        {
            get
            {
                if (_Instance == null)
                {
                    var singletons = FindObjectsByType<TSingleton>(FindObjectsSortMode.None);

                    if (singletons.Length == 0)
                    {
                        throw new FrameworkException("No instance of " + typeof(TSingleton).Name + " found");
                    }

                    if (singletons.Length > 1)
                    {
                        throw new FrameworkException("More than one instance of " + typeof(TSingleton).Name + " found");
                    }
                    
                    _Instance = singletons[0];
                    if (_Instance == null)
                    {
                        var obj = new GameObject(typeof(TSingleton).Name);
                        _Instance = obj.AddComponent<TSingleton>();
                    }
                }

                return _Instance;
            }
        }
        
        protected virtual void Awake()
        {
            if (!Application.isPlaying)
            {
                return;
            }

            // 记录初始化时间
            InitializationTime = Time.time;

            DontDestroyOnLoad(gameObject);

            var check = FindObjectsByType<TSingleton>(FindObjectsSortMode.None);
            foreach (var searched in check)
            {
                // 如果查找到的对象是当前对象，则跳过
                if (searched == this) continue;
                // 如果查找到的对象的初始化时间小于当前对象的初始化时间，则销毁该对象
                if (searched.GetComponent<ReplaceableMonoSingleton<TSingleton>>().InitializationTime < InitializationTime)
                {
                    Destroy(searched.gameObject);
                }
            }

            // 如果_Instance为空，则将当前对象赋值给_Instance
            if (_Instance == null)
            {
                _Instance = this as TSingleton;
            }
        }

    }
}
