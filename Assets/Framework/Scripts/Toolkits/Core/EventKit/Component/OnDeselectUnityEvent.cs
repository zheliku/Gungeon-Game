/****************************************************************************
 * Copyright (c) 2015 - 2024 liangxiegame UNDER MIT License
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

    public class OnDeselectUnityEvent: MonoBehaviour, IDeselectHandler
    {
        public UnityEvent<BaseEventData> OnDeselectEvent;

        public void OnDeselect(BaseEventData eventData)
        {
            OnDeselectEvent?.Invoke(eventData);
        }
    }
}