// ------------------------------------------------------------
// @file       InputKit.cs
// @brief
// @author     zheliku
// @Modified   2024-12-03 14:12:30
// @Copyright  Copyright (c) 2024, zheliku
// ------------------------------------------------------------

namespace Framework.Toolkits.InputKit
{
    using System;
    using UnityEngine.InputSystem;

    public partial class InputKit
    {
        public static InputAction GetInputAction(string actionName, string actionMapName = "Player")
        {
            return InputMgr.Instance.ActionMaps[actionMapName][actionName];
        }

        public static InputActionMap GetInputActionMap(string actionMapName)
        {
            return InputMgr.Instance.ActionMaps[actionMapName];
        }

        public static InputAction BindPerformed(string actionName, Action<InputAction.CallbackContext> callback, string actionMapName = "Player")
        {
            return InputMgr.Instance.ActionMaps[actionMapName][actionName].BindPerformed(callback);
        }

        public static InputAction BindCanceled(string actionName, Action<InputAction.CallbackContext> callback, string actionMapName = "Player")
        {
            return InputMgr.Instance.ActionMaps[actionMapName][actionName].BindCanceled(callback);
        }

        public static InputAction BindStarted(string actionName, Action<InputAction.CallbackContext> callback, string actionMapName = "Player")
        {
            return InputMgr.Instance.ActionMaps[actionMapName][actionName].BindStarted(callback);
        }

        public static InputAction UnBindPerformed(string actionName, Action<InputAction.CallbackContext> callback, string actionMapName = "Player")
        {
            return InputMgr.Instance.ActionMaps[actionMapName][actionName].UnBindPerformed(callback);
        }

        public static InputAction UnBindCanceled(string actionName, Action<InputAction.CallbackContext> callback, string actionMapName = "Player")
        {
            return InputMgr.Instance.ActionMaps[actionMapName][actionName].UnBindCanceled(callback);
        }

        public static InputAction UnBindStarted(string actionName, Action<InputAction.CallbackContext> callback, string actionMapName = "Player")
        {
            return InputMgr.Instance.ActionMaps[actionMapName][actionName].UnBindStarted(callback);
        }
        
        public static InputAction UnBindAllPerformed(string actionName, string actionMapName = "Player")
        {
            return InputMgr.Instance.ActionMaps[actionMapName][actionName].UnBindAllPerformed();
        }

        public static InputAction UnBindAllCanceled(string actionName, string actionMapName = "Player")
        {
            return InputMgr.Instance.ActionMaps[actionMapName][actionName].UnBindAllCanceled();
        }

        public static InputAction UnBindAllStarted(string actionName, string actionMapName = "Player")
        {
            return InputMgr.Instance.ActionMaps[actionMapName][actionName].UnBindAllStarted();
        }
        
        public static InputAction UnBindAll(string actionName, string actionMapName = "Player")
        {
            return InputMgr.Instance.ActionMaps[actionMapName][actionName].UnBindAll();
        }

        public static InputAction ActivateInputAction(string actionName, string actionMapName = "Player")
        {
            return InputMgr.Instance.ActionMaps[actionMapName][actionName].Activate();
        }

        public static InputAction DeactivateInputAction(string actionName, string actionMapName = "Player")
        {
            return InputMgr.Instance.ActionMaps[actionMapName][actionName].Deactivate();
        }
        
        public static InputActionMap ActivateInputActionMap(string actionMapName)
        {
            return InputMgr.Instance.ActionMaps[actionMapName].Activate();
        }

        public static InputActionMap DeactivateInputActionMap(string actionMapName)
        {
            return InputMgr.Instance.ActionMaps[actionMapName].Deactivate();
        }

        public static TValue ReadValue<TValue>(string actionName, string actionMapName = "Player") where TValue : struct
        {
            return InputMgr.Instance.ActionMaps[actionMapName][actionName].ReadValue<TValue>();
        }

        public static bool WasCompletedThisFrame(string actionName, string actionMapName = "Player")
        {
            return InputMgr.Instance.ActionMaps[actionMapName][actionName].WasCompletedThisFrame();
        }

        public static bool WasPerformedThisFrame(string actionName, string actionMapName = "Player")
        {
            return InputMgr.Instance.ActionMaps[actionMapName][actionName].WasPerformedThisFrame();
        }
        
        public static bool WasPressedThisFrame(string actionName, string actionMapName = "Player")
        {
            return InputMgr.Instance.ActionMaps[actionMapName][actionName].WasPressedThisFrame();
        }
        
        public static bool WasReleasedThisFrame(string actionName, string actionMapName = "Player")
        {
            return InputMgr.Instance.ActionMaps[actionMapName][actionName].WasReleasedThisFrame();
        }
    }
}