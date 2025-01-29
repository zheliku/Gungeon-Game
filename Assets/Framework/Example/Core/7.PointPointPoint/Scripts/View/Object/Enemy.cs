﻿// ------------------------------------------------------------
// @file       Enemy.cs
// @brief
// @author     zheliku
// @Modified   2024-10-15 16:10:54
// @Copyright  Copyright (c) 2024, zheliku
// ------------------------------------------------------------

namespace Framework.Core.Example._7.PointPointPoint.Scripts.View.Object
{
    using Command;
    using Core.View;

    public class Enemy : AbstractView
    {
        protected override IArchitecture Architecture => PointGame.Interface;

        private void OnMouseDown()
        {
            gameObject.SetActive(false);

            this.SendCommand<KillEnemyCommand>();
        }
    }
}