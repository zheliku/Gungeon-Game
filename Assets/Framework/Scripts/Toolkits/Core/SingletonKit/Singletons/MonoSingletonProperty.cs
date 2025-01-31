// ------------------------------------------------------------
// @file       MonoSingletonProperty.cs
// @brief
// @author     zheliku
// @Modified   2024-10-20 19:10:25
// @Copyright  Copyright (c) 2024, zheliku
// ------------------------------------------------------------

namespace Framework.Toolkits.SingletonKit
{
    using UnityEngine;

    /// <summary>
    /// 通过属性实现的 MonoSingleton，不占用父类的位置
    /// </summary>
    public static class MonoSingletonProperty<TSingleton> where TSingleton : MonoBehaviour, ISingleton
    {
        private static TSingleton _Instance;

        public static TSingleton Instance
        {
            get
            {
                if (null == _Instance)
                {
                    _Instance = SingletonCreator.CreateSingleton<TSingleton>();
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