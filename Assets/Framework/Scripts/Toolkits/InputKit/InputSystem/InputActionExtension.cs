// ------------------------------------------------------------
// @file       InputActionExtension.cs
// @brief
// @author     zheliku
// @Modified   2024-12-26 23:12:59
// @Copyright  Copyright (c) 2024, zheliku
// ------------------------------------------------------------

namespace Framework.Toolkits.InputKit
{
    using System;
    using EventKit;
    using FluentAPI;
    using UnityEngine;
    using UnityEngine.InputSystem;

    /// <summary>
    /// 请使用 InputAction 封装方法，用于更新 Inspector 面板中显示的 Mono
    /// </summary>
    public static class InputActionExtension
    {
        public static InputAction BindPerformed(this InputAction self, Action<InputAction.CallbackContext> action)
        {
            self.performed += action;
            InputManager.Instance.GetInputActionMapMono(self).Init(self.actionMap);
            InputManager.Instance.GetInputActionMono(self).PerformedActions.Add(action);
            return self;
        }

        public static InputAction UnBindPerformed(this InputAction self, Action<InputAction.CallbackContext> action)
        {
            self.performed -= action;
            InputManager.Instance.GetInputActionMono(self).PerformedActions.Remove(action);
            return self;
        }

        public static InputAction UnBindAllPerformed(this InputAction self)
        {
            var actions = InputManager.Instance.GetInputActionMono(self).PerformedActions;
            foreach (var action in actions)
            {
                self.performed -= action;
            }
            actions.Clear();
            return self;
        }

        public static InputAction BindStarted(this InputAction self, Action<InputAction.CallbackContext> action)
        {
            self.started += action;
            InputManager.Instance.GetInputActionMapMono(self).Init(self.actionMap);
            InputManager.Instance.GetInputActionMono(self).StartedActions.Add(action);
            return self;
        }

        public static InputAction UnBindStarted(this InputAction self, Action<InputAction.CallbackContext> action)
        {
            self.started -= action;
            InputManager.Instance.GetInputActionMono(self).StartedActions.Remove(action);
            return self;
        }

        public static InputAction UnBindAllStarted(this InputAction self)
        {
            var actions = InputManager.Instance.GetInputActionMono(self).StartedActions;
            foreach (var action in actions)
            {
                self.started -= action;
            }
            actions.Clear();
            return self;
        }

        public static InputAction BindCanceled(this InputAction self, Action<InputAction.CallbackContext> action)
        {
            self.canceled += action;
            InputManager.Instance.GetInputActionMapMono(self).Init(self.actionMap);
            InputManager.Instance.GetInputActionMono(self).CanceledActions.Add(action);
            return self;
        }

        public static InputAction UnBindCanceled(this InputAction self, Action<InputAction.CallbackContext> action)
        {
            self.canceled -= action;
            InputManager.Instance.GetInputActionMono(self).CanceledActions.Remove(action);
            return self;
        }

        public static InputAction UnBindAllCanceled(this InputAction self)
        {
            var actions = InputManager.Instance.GetInputActionMono(self).CanceledActions;
            foreach (var action in actions)
            {
                self.canceled -= action;
            }
            actions.Clear();
            return self;
        }

        public static InputAction UnBindAll(this InputAction self)
        {
            self.UnBindAllPerformed();
            self.UnBindAllStarted();
            self.UnBindAllCanceled();
            return self;
        }

        public static InputAction Activate(this InputAction self)
        {
            self.Enable();
            InputManager.Instance.GetInputActionMono(self).EnableGameObject();
            return self;
        }

        public static InputAction Deactivate(this InputAction self)
        {
            self.Disable();
            InputManager.Instance.GetInputActionMono(self).DisableGameObject();
            return self;
        }
        
        public static InputActionMap Activate(this InputActionMap self)
        {
            self.Enable();
            InputManager.Instance.GetInputActionMapMono(self).EnableGameObject();
            return self;
        }

        public static InputActionMap Deactivate(this InputActionMap self)
        {
            self.Disable();
            InputManager.Instance.GetInputActionMapMono(self).DisableGameObject();
            return self;
        }
        
        public static InputAction UnBindPerformedWhenGameObjectDisabled(this InputAction self, GameObject target, Action<InputAction.CallbackContext>
                                                                            action)
        {
            target.GetOrAddComponent<OnDisableEventTrigger>().OnDisableEvent.Register(() =>
            {
                self.UnBindPerformed(action);
            });
            return self;
        }

        public static InputAction UnBindPerformedWhenGameObjectDestroyed(this InputAction self, GameObject target, Action<InputAction.CallbackContext> action)
        {
            target.GetOrAddComponent<OnDestroyEventTrigger>().OnDestroyEvent.Register(() =>
            {
                self.UnBindPerformed(action);
            });
            return self;
        }
        
        public static InputAction UnBindAllPerformedWhenGameObjectDisabled(this InputAction self, GameObject target)
        {
            target.GetOrAddComponent<OnDisableEventTrigger>().OnDisableEvent.Register(() =>
            {
                self.UnBindAllPerformed();
            });
            return self;
        }

        public static InputAction UnBindAllPerformedWhenGameObjectDestroyed(this InputAction self, GameObject target)
        {
            target.GetOrAddComponent<OnDestroyEventTrigger>().OnDestroyEvent.Register(() =>
            {
                self.UnBindAllPerformed();
            });
            return self;
        }
        
        public static InputAction UnBindStartedWhenGameObjectDisabled(this InputAction self, GameObject target, Action<InputAction.CallbackContext> action)
        {
            target.GetOrAddComponent<OnDisableEventTrigger>().OnDisableEvent.Register(() =>
            {
                self.UnBindStarted(action);
            });
            return self;
        }

        public static InputAction UnBindStartedWhenGameObjectDestroyed(this InputAction self, GameObject target, Action<InputAction.CallbackContext> action)
        {
            target.GetOrAddComponent<OnDestroyEventTrigger>().OnDestroyEvent.Register(() =>
            {
                self.UnBindStarted(action);
            });
            return self;
        }
        
        public static InputAction UnBindAllStartedWhenGameObjectDisabled(this InputAction self, GameObject target)
        {
            target.GetOrAddComponent<OnDisableEventTrigger>().OnDisableEvent.Register(() =>
            {
                self.UnBindAllStarted();
            });
            return self;
        }

        public static InputAction UnBindAllStartedWhenGameObjectDestroyed(this InputAction self, GameObject target)
        {
            target.GetOrAddComponent<OnDestroyEventTrigger>().OnDestroyEvent.Register(() =>
            {
                self.UnBindAllStarted();
            });
            return self;
        }
        
        public static InputAction UnBindCanceledWhenGameObjectDisabled(this InputAction self, GameObject target, Action<InputAction.CallbackContext> action)
        {
            target.GetOrAddComponent<OnDisableEventTrigger>().OnDisableEvent.Register(() =>
            {
                self.UnBindCanceled(action);
            });
            return self;
        }
        

        public static InputAction UnBindCanceledWhenGameObjectDestroyed(this InputAction self, GameObject target, Action<InputAction.CallbackContext> action)
        {
            target.GetOrAddComponent<OnDestroyEventTrigger>().OnDestroyEvent.Register(() =>
            {
                self.UnBindCanceled(action);
            });
            return self;
        }
        
        public static InputAction UnBindAllCanceledWhenGameObjectDisabled(this InputAction self, GameObject target)
        {
            target.GetOrAddComponent<OnDisableEventTrigger>().OnDisableEvent.Register(() =>
            {
                self.UnBindAllCanceled();
            });
            return self;
        }

        public static InputAction UnBindAllCanceledWhenGameObjectDestroyed(this InputAction self, GameObject target)
        {
            target.GetOrAddComponent<OnDestroyEventTrigger>().OnDestroyEvent.Register(() =>
            {
                self.UnBindAllCanceled();
            });
            return self;
        }
        
        public static InputAction UnBindAllWhenGameObjectDisabled(this InputAction self, GameObject target)
        {
            target.GetOrAddComponent<OnDisableEventTrigger>().OnDisableEvent.Register(() =>
            {
                self.UnBindAll();
            });
            return self;
        }

        public static InputAction UnBindAllWhenGameObjectDestroyed(this InputAction self, GameObject target)
        {
            target.GetOrAddComponent<OnDestroyEventTrigger>().OnDestroyEvent.Register(() =>
            {
                self.UnBindAll();
            });
            return self;
        }
    }
}