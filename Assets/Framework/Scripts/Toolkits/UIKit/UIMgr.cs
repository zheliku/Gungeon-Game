// ------------------------------------------------------------
// @file       UIMgr.cs
// @brief
// @author     zheliku
// @Modified   2024-12-12 19:12:30
// @Copyright  Copyright (c) 2024, zheliku
// ------------------------------------------------------------

using UnityEngine;

namespace Framework.Toolkits.UIKit
{
    using System;
    using System.Collections.Generic;
    using ResKit;
    using SingletonKit;
    using Sirenix.OdinInspector;
    using UnityEngine.ResourceManagement.AsyncOperations;

    [HideReferenceObjectPicker]
    public class PanelInfo
    {
        [LabelWidth(75)]
        public IPanel Panel;

        [HideInInspector]
        public AsyncOperationHandle Handle;

        public PanelInfo(IPanel panel, AsyncOperationHandle handle)
        {
            Panel  = panel;
            Handle = handle;
        }
    }

    [MonoSingletonPath("UIRoot/UIMgr")]
    public class UIMgr : MonoBehaviour, ISingleton
    {
    #region 常量

    #endregion

    #region Static

        private static UIMgr _Instance;

        public static UIMgr Instance
        {
            get => UIRoot.Instance.GetComponentInChildren<UIMgr>();
        }

    #endregion

    #region 字段

        [ShowInInspector]
        private Dictionary<string, PanelInfo> _panels = new Dictionary<string, PanelInfo>();

    #endregion

    #region 属性

    #endregion

    #region 公共方法

        public AsyncOperationHandle<GameObject> LoadPanelAsync<T>(string panelName, Action<T> callback = null, UILevel level = UILevel.Common) where T : IPanel
        {
            if (!_panels.TryGetValue(panelName, out var value))
            {
                var handle = ResKit.InstantiateAsync(panelName);

                value = new PanelInfo(null, handle);
                _panels.Add(panelName, value);

                handle.OnCompleted(obj =>
                {
                    obj.name = panelName;
                    var panel = obj.GetComponent<T>();
                    panel.Level = level;
                    panel.Load();
                    value.Panel = panel;
                    callback?.Invoke(panel);
                });
            }

            return value.Handle.Convert<GameObject>();
        }

        public T GetPanel<T>(string panelName) where T : IPanel
        {
            if (_panels.TryGetValue(panelName, out var value))
            {
                if (value.Handle.IsDone)
                {
                    return (T) value.Panel;
                }
            }
            return default;
        }

        public void UnloadPanel(string panelName, Action callback = null)
        {
            if (_panels.TryGetValue(panelName, out var value))
            {
                value.Panel.Unload();
                value.Handle.Release();
                callback?.Invoke();
                _panels.Remove(panelName);
                Destroy(value.Panel.Transform.gameObject);
            }
        }

        public AsyncOperationHandle<GameObject> ShowPanelAsync<T>(string panelName, Action<T> callback = null, UILevel level = UILevel.Common) where T : IPanel
        {
            var handle = LoadPanelAsync<T>(panelName);
            
            if (!handle.IsDone)
            {
                handle.OnCompleted(obj =>
                {
                    var panel = obj.GetComponent<T>();
                    panel.Level = level;
                    panel.Show();
                    callback?.Invoke(panel);
                });
            }
            else if (handle.IsValid())
            {
                var panel = handle.Result.GetComponent<T>();
                panel.Level = level;
                panel.Show();
                callback?.Invoke(panel);
            }
            else
            {
                Debug.LogError("Panel " + panelName + " is not loaded");
            }

            return handle;
        }

        public void HidePanel<T>(string panelName, Action<T> callback = null) where T : IPanel
        {
            if (_panels.TryGetValue(panelName, out var value))
            {
                value.Panel.Hide();
                callback?.Invoke((T) value.Panel);
            }
        }

        public void HideAllPanel(Action<IPanel> callback = null)
        {
            foreach (var info in _panels.Values)
            {
                info.Panel.Hide();
                callback?.Invoke(info.Panel);
            }
        }

        public void UnloadAllPanel(Action callback = null)
        {
            foreach (var panelName in _panels.Keys)
            {
                UnloadPanel(panelName, callback);
            }
        }

        public void OnSingletonInit() { }

    #endregion

    #region 其他方法

    #endregion

    #region Unity 事件

        private void Update()
        {
#if UNITY_EDITOR

            // 强制刷新 Inspector GUI
            UnityEditor.EditorUtility.SetDirty(this);
#endif
        }

    #endregion
    }
}