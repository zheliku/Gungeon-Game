// ------------------------------------------------------------
// @file       GunItem.cs
// @brief
// @author     zheliku
// @Modified   2025-06-11 01:26:03
// @Copyright  Copyright (c) 2025, zheliku
// ------------------------------------------------------------

namespace Game
{
    using System;
    using Framework.Core;
    using TMPro;
    using UnityEngine.UI;

    public class GunItem : AbstractView
    {
        [HierarchyPath("txtName")]
        public TextMeshProUGUI TxtName;
        
        [HierarchyPath("Icon/imgIcon")]
        public Image ImgIcon;
        
        [HierarchyPath("Palette/imgPalette")]
        public Image ImgPalette;
        
        [HierarchyPath("Palette/txtPrice")]
        public TextMeshProUGUI TxtPrice;
        
        [HierarchyPath("btnUnlock")]
        public Button BtnUnlock;

        private void Awake()
        {
            this.BindHierarchyComponent();
        }

        protected override IArchitecture _Architecture { get => Game.Architecture; }
    }
}