/****************************************************************************
 * Copyright (c) 2016 - 2024 liangxiegame UNDER MIT License
 *
 * https://qframework.cn
 * https://github.com/liangxiegame/QFramework
 * https://gitee.com/liangxiegame/QFramework
 ****************************************************************************/

namespace Framework.Toolkits.EventKit
{
    using UnityEngine;
    using UnityEngine.Events;
    using UnityEngine.EventSystems;

    public class OnSelectUnityEvent : MonoBehaviour, ISelectHandler
    {
        public UnityEvent<BaseEventData> OnSelectEvent;

        public void OnSelect(BaseEventData eventData)
        {
            OnSelectEvent?.Invoke(eventData);
        }
    }
}