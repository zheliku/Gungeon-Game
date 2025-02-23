// ------------------------------------------------------------
// @file       GamePlay.cs
// @brief
// @author     zheliku
// @Modified   2025-01-31 14:01:37
// @Copyright  Copyright (c) 2025, zheliku
// ------------------------------------------------------------

namespace Game
{
    using Framework.Core;
    using Framework.Toolkits.UIKit;
    using TMPro;

    public class GamePlay : UIPanel
    {
        [HierarchyPath("txtHp")]
        private TextMeshProUGUI _txtHp;

        [HierarchyPath("txtGun")]
        private TextMeshProUGUI _txtGun;

        protected override void OnShow()
        {
            this.GetModel<PlayerModel>().Property.Hp.RegisterWithInitValue((oldValue, value) =>
            {
                _txtHp.text = $"HP:{value}";
            }).UnRegisterWhenGameObjectDisabled(gameObject);

            TypeEventSystem.GLOBAL.Register<GunShootEvent>(e =>
            {
                UpdateGunInfo(e.Gun);
            }).UnRegisterWhenGameObjectDisabled(this);

            TypeEventSystem.GLOBAL.Register<GunChangeEvent>(e =>
            {
                UpdateGunInfo(e.NewGun);
            }).UnRegisterWhenGameObjectDisabled(this);

            TypeEventSystem.GLOBAL.Register<GunBulletLoadingEvent>(e =>
            {
                UpdateGunInfo(e.Gun);
            }).UnRegisterWhenGameObjectDisabled(this);
        }

        private void UpdateGunInfo(Gun gun)
        {
            var clipInfo = $"{gun.Clip.CurrentBulletCount}/{gun.Clip.ClipBulletCount}";
            var bagInfo  = gun.Bag.RemainBulletCount < 0 ? "(\u221e)" : $"({gun.Bag.RemainBulletCount}/{gun.Bag.MaxBulletCount})";
            _txtGun.text = $"Bullet: {clipInfo} {bagInfo} R Reload!";
        }

        protected override IArchitecture _Architecture { get => Game.Interface; }
    }
}