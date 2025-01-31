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
                var gun = e.Gun;
                _txtGun.text = $"Bullet: {gun.CurrentBulletCount}/{gun.BulletCount} R Reload!";
            });
            
            TypeEventSystem.GLOBAL.Register<GunChangeEvent>(e =>
            {
                var gun = e.NewGun;
                _txtGun.text = $"Bullet: {gun.CurrentBulletCount}/{gun.BulletCount} R Reload!";
            });
            
            TypeEventSystem.GLOBAL.Register<GunLoadBulletEvent>(e =>
            {
                var gun = e.Gun;
                _txtGun.text = $"Bullet: {gun.CurrentBulletCount}/{gun.BulletCount} R Reload!";
            });
        }

        protected override IArchitecture _Architecture { get => Game.Interface; }
    }
}