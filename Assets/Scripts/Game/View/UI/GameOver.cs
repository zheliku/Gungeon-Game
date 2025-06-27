// ------------------------------------------------------------
// @file       GamePass.cs
// @brief
// @author     zheliku
// @Modified   2025-01-29 17:01:07
// @Copyright  Copyright (c) 2025, zheliku
// ------------------------------------------------------------

namespace Game
{
    using Framework.Core;
    using Framework.Toolkits.UIKit;
    using UnityEngine;
    using UnityEngine.UI;

    public class GameOver : UIPanel
    {
        [HierarchyPath("btnRestart")]
        private Button _btnRestart;
        
        protected override void OnLoad()
        {
            // _btnRestart = "btnRestart".GetComponentInHierarchy<Button>(transform);
            
            _btnRestart.onClick.AddListener(() =>
            {
                this.SendCommand<StartGameCommand>();
                Hide();
            });
        }

        protected override void OnShow()
        {
            Time.timeScale = 0;
        }
        
        protected override IArchitecture _Architecture { get => Game.Architecture; }
    }
}