// ------------------------------------------------------------
// @file       ErrorArea.cs
// @brief
// @author     zheliku
// @Modified   2024-10-15 16:10:48
// @Copyright  Copyright (c) 2024, zheliku
// ------------------------------------------------------------

namespace Framework.Core.Example._7.PointPointPoint.Scripts.View.Object
{
    using Command;
    using Core.View;

    public class ErrorArea : AbstractView
    {
        protected override IArchitecture Architecture => PointGame.Interface;

        private void OnMouseDown()
        {
            this.SendCommand<MissCommand>();
        }
    }
}