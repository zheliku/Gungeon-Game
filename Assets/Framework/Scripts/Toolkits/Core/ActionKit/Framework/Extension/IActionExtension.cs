// ------------------------------------------------------------
// @file       IActionExtension.cs
// @brief
// @author     zheliku
// @Modified   2024-10-24 20:10:02
// @Copyright  Copyright (c) 2024, zheliku
// ------------------------------------------------------------

namespace Framework.Toolkits.ActionKit
{
    using UnityEngine;

    public static class IActionExtension
    {
        public static IActionController Start(this IAction self, Component component, System.Action<IActionController> onFinish = null)
        {
            var controller = ActionController.Spawn();
            controller.ActionID   = self.ActionID;
            controller.Action     = self;
            controller.UpdateMode = ActionUpdateMode.ScaledDeltaTime;
            component.ExecuteByUpdate(self, controller, onFinish);
            return controller;
        }

        public static IActionController Start(this IAction self, Component component, System.Action onFinish)
        {
            var controller = ActionController.Spawn();
            controller.ActionID   = self.ActionID;
            controller.Action     = self;
            controller.UpdateMode = ActionUpdateMode.ScaledDeltaTime;
            component.ExecuteByUpdate(self, controller, _ => onFinish());
            return controller;
        }
        
        public static IActionController Start(this IAction self, GameObject gameObject, System.Action<IActionController> onFinish = null)
        {
            var controller = ActionController.Spawn();
            controller.ActionID   = self.ActionID;
            controller.Action     = self;
            controller.UpdateMode = ActionUpdateMode.ScaledDeltaTime;
            gameObject.ExecuteByUpdate(self, controller, onFinish);
            return controller;
        }

        public static IActionController Start(this IAction self, GameObject gameObject, System.Action onFinish)
        {
            var controller = ActionController.Spawn();
            controller.ActionID   = self.ActionID;
            controller.Action     = self;
            controller.UpdateMode = ActionUpdateMode.ScaledDeltaTime;
            gameObject.ExecuteByUpdate(self, controller, _ => onFinish());
            return controller;
        }

        public static IActionController StartCurrentScene(this IAction self, System.Action<IActionController> onFinish = null)
        {
            return self.Start(ActionKitCurrentScene.SceneComponent, onFinish);
        }

        public static IActionController StartCurrentScene(this IAction self, System.Action onFinish)
        {
            return self.Start(ActionKitCurrentScene.SceneComponent, onFinish);
        }

        public static IActionController StartGlobal(this IAction self, System.Action<IActionController> onFinish = null)
        {
            return self.Start(ActionKitMonoBehaviourEvent.Instance, onFinish);
        }

        public static IActionController StartGlobal(this IAction self, System.Action onFinish)
        {
            return self.Start(ActionKitMonoBehaviourEvent.Instance, onFinish);
        }

        public static void Pause(this IActionController self)
        {
            if (self.ActionID == self.Action.ActionID)
            {
                self.Action.Paused = true;
            }
        }

        public static void Resume(this IActionController self)
        {
            if (self.ActionID == self.Action.ActionID)
            {
                self.Action.Paused = false;
            }
        }

        public static void Finish(this IAction self)
        {
            self.Status = ActionStatus.Finished;
        }

        /// <summary>
        /// 执行 Action 方法
        /// </summary>
        /// <param name="self">Action 实例</param>
        /// <param name="deltaTime">当前帧间隔时间</param>
        /// <returns>是否执行完成</returns>
        public static bool Execute(this IAction self, float deltaTime)
        {
            if (self.Status == ActionStatus.NotStart)
            {
                self.OnStart();

                if (self.Status == ActionStatus.Finished)
                {
                    self.OnFinish();
                    return true; // Finish 后才会 return true
                }

                self.Status = ActionStatus.Started;
            }
            else if (self.Status == ActionStatus.Started)
            {
                if (self.Paused) return false;

                self.OnExecute(deltaTime);

                if (self.Status == ActionStatus.Finished)
                {
                    self.OnFinish();
                    return true; // Finish 后才会 return true
                }
            }
            else if (self.Status == ActionStatus.Finished)
            {
                self.OnFinish();
                return true; // Finish 后才会 return true
            }

            return false;
        }
    }
}