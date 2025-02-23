/****************************************************************************
 * Copyright (c) 2016 - 2022 liangxiegame UNDER MIT License
 *
 * https://qframework.cn
 * https://github.com/liangxiegame/QFramework
 * https://gitee.com/liangxiegame/QFramework
 ****************************************************************************/

namespace Framework.Toolkits.EventKit
{
    using System;
    using FluentAPI;
    using Framework.Core;
    using UnityEngine;

    public class OnCollisionExit2DEventTrigger : MonoBehaviour
    {
        public readonly EasyEvent<Collision2D> OnCollisionExit2DEvent = new EasyEvent<Collision2D>();

        private void OnCollisionExit2D(Collision2D col)
        {
            OnCollisionExit2DEvent.Trigger(col);
        }
    }

    public static class OnCollisionExit2DEventTriggerExtension
    {
        public static IUnRegister OnCollisionExit2DEvent<T>(this T self, Action<Collision2D> onCollisionExit2D, int priority = 0)
            where T : Component
        {
            return self.GetOrAddComponent<OnCollisionExit2DEventTrigger>().OnCollisionExit2DEvent
                       .Register(onCollisionExit2D, priority);
        }

        public static IUnRegister OnCollisionExit2DEvent(this GameObject self, Action<Collision2D> onCollisionExit2D, int priority = 0)
        {
            return self.GetOrAddComponent<OnCollisionExit2DEventTrigger>().OnCollisionExit2DEvent
                       .Register(onCollisionExit2D, priority);
        }
    }
}