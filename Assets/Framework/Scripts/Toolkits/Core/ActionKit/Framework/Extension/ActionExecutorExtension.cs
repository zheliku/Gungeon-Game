// ------------------------------------------------------------
// @file       IActionExecutorExtension.cs
// @brief
// @author     zheliku
// @Modified   2024-10-24 22:10:10
// @Copyright  Copyright (c) 2024, zheliku
// ------------------------------------------------------------

namespace Framework.Toolkits.ActionKit
{
    using FluentAPI;
    using UnityEngine;

    public static class ActionExecutorExtension
    {
        public static bool UpdateAction(
            this IActionExecutor             self,
            IActionController                controller,
            float                            deltaTime,
            System.Action<IActionController> onFinish = null)
        {
            // 如果控制器中的动作未执行完成，并且执行动作成功
            if (!controller.Action.Deinited && controller.Action.Execute(deltaTime))
            {
                onFinish?.Invoke(controller); // 执行回调
                controller.Deinit();          // 执行完成后的反初始化
                return true;
            }

            return controller.Action.Deinited;
        }

        public static IAction ExecuteByUpdate<T>(
            this T                           self,
            IAction                          action,
            IActionController                controller,
            System.Action<IActionController> onFinish = null)
            where T : Component
        {
            if (action.Status == ActionStatus.Finished) { action.Reset(); }
            self.gameObject.GetOrAddComponent<ActionExecutor>().Execute(controller, onFinish); // 挂载 ActionExecutor 帧更新执行 Action
            return action;
        }

        public static IAction ExecuteByUpdate(
            this GameObject                  self,
            IAction                          action,
            IActionController                controller,
            System.Action<IActionController> onFinish = null)
        {
            if (action.Status == ActionStatus.Finished) { action.Reset(); }
            self.GetOrAddComponent<ActionExecutor>().Execute(controller, onFinish); // 挂载 ActionExecutor 帧更新执行 Action
            return action;
        }
    }
}