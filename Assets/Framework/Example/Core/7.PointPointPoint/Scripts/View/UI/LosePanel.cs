﻿// ------------------------------------------------------------
// @file       LosePanel.cs
// @brief
// @author     zheliku
// @Modified   2024-10-15 15:10:08
// @Copyright  Copyright (c) 2024, zheliku
// ------------------------------------------------------------

namespace Framework.Core.Example._7.PointPointPoint.Scripts.View.UI
{
    using Command;
    using Core.View;
    using UnityEngine.UI;

    public class LosePanel : AbstractView
    {
        protected override IArchitecture Architecture => PointGame.Interface;

        private Button _btnBack;

        private void Awake()
        {
            _btnBack = transform.Find("btnBack").GetComponent<Button>();

            _btnBack.onClick.AddListener(OnBtnBackClick);
        }

        private void OnBtnBackClick()
        {
            gameObject.SetActive(false);
            this.SendCommand<ReturnMenuCommand>();
        }
    }
}