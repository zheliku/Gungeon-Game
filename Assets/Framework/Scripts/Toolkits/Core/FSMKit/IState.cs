// ------------------------------------------------------------
// @file       IState.cs
// @brief
// @author     zheliku
// @Modified   2024-10-20 20:10:20
// @Copyright  Copyright (c) 2024, zheliku
// ------------------------------------------------------------

namespace Framework.Toolkits.FSMKit
{
    using Sirenix.OdinInspector;

    [HideReferenceObjectPicker]
    public interface IState
    {
        bool Condition();

        void Enter();

        void Update();

        void FixedUpdate();

        void OnGUI();

        void Exit();
    }
}