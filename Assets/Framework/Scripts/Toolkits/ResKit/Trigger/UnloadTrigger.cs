// ------------------------------------------------------------
// @file       UnLoadTrigger.cs
// @brief
// @author     zheliku
// @Modified   2024-12-10 13:12:35
// @Copyright  Copyright (c) 2024, zheliku
// ------------------------------------------------------------

namespace Framework.Toolkits.ResKit
{
    using System.Collections.Generic;
    using Sirenix.OdinInspector;
    using UnityEngine;
    using UnityEngine.ResourceManagement.AsyncOperations;

    public class UnloadTrigger : MonoBehaviour
    {
        [ShowInInspector]
        private readonly HashSet<AsyncOperationHandle> _handles = new HashSet<AsyncOperationHandle>();
        
        public AsyncOperationHandle AddHandle(AsyncOperationHandle handle)
        {
            _handles.Add(handle);
            return handle;
        }
        
        public void RemoveHandle(AsyncOperationHandle handle)
        {
            _handles.Remove(handle);
        }

        public void Unload()
        {
            foreach (var handle in _handles)
            {
                handle.Unload();
            }

            // 清空 HashSet
            _handles.Clear();
        }
    }
}
