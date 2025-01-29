// ------------------------------------------------------------
// @file       GamePass.cs
// @brief
// @author     zheliku
// @Modified   2025-01-29 17:01:07
// @Copyright  Copyright (c) 2025, zheliku
// ------------------------------------------------------------

namespace Game.View
{
    using Framework.Core;
    using Framework.Toolkits.FluentAPI;
    using Framework.Toolkits.ResKit;
    using Framework.Toolkits.UIKit;
    using UnityEngine;
    using UnityEngine.UI;

    public class GamePass : UIPanel
    {
        private Button _btnRestart;
        
        protected override void OnLoad()
        {
            _btnRestart = "btnRestart".GetComponentInHierarchy<Button>(transform);
            
            _btnRestart.onClick.AddListener(() =>
            {
                ResKit.LoadSceneAsync("Game");
                Time.timeScale = 1;
                Hide();
            });
        }

        
        protected override void OnShow()
        {
            Time.timeScale = 0;
        }

        protected override IArchitecture Architecture { get => Game.Interface; }
    }
}