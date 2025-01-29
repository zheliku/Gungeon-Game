// ------------------------------------------------------------
// @file       MonoSingleton.cs
// @brief
// @author     zheliku
// @Modified   2024-10-20 18:10:48
// @Copyright  Copyright (c) 2024, zheliku
// ------------------------------------------------------------

namespace Framework.Toolkits.SingletonKit
{
    using Core.View;
    using UnityEngine;

    public abstract class MonoSingleton<TMonoSingleton> : MonoBehaviour, ISingleton where TMonoSingleton : MonoSingleton<TMonoSingleton>
    {
        /// <summary>
        /// 静态实例
        /// </summary>
        protected static TMonoSingleton _Instance;

        /// <summary>
        /// 静态属性：封装相关实例对象
        /// </summary>
        public static TMonoSingleton Instance
        {
            get
            {
                if (_Instance == null)
                {
                    _Instance = SingletonCreator.CreateMonoSingleton<TMonoSingleton>();
                }

                return _Instance;
            }
        }

        /// <summary>
        /// 实现接口的单例初始化
        /// </summary>
        public virtual void OnSingletonInit()
        { }

        /// <summary>
        /// 资源释放
        /// </summary>
        public virtual void Dispose()
        {
            Destroy(gameObject);
        }

        protected virtual void Update()
        {
#if UNITY_EDITOR

            // 强制刷新 Inspector GUI
            UnityEditor.EditorUtility.SetDirty(this);
#endif
        }

        /// <summary>
        /// 应用程序退出：释放当前对象并销毁相关 GameObject
        /// </summary>
        protected virtual void OnApplicationQuit()
        {
            if (_Instance == null)
                return;

            Destroy(_Instance.gameObject);
            _Instance = null;
        }

        /// <summary>
        /// 释放当前对象
        /// </summary>
        protected virtual void OnDestroy()
        {
            _Instance = null;
        }
    }
}