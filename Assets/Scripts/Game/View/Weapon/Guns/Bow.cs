// ------------------------------------------------------------
// @file       Bow.cs
// @brief
// @author     zheliku
// @Modified   2025-01-31 22:01:33
// @Copyright  Copyright (c) 2025, zheliku
// ------------------------------------------------------------

namespace Game
{
    using Framework.Core;
    using Framework.Toolkits.AudioKit;
    using Framework.Toolkits.FluentAPI;
    using UnityEngine;

    public class Bow : Gun
    {
        [HierarchyPath("ArrowPrepare")]
        private GameObject _arrowPrepare;

        protected override void Awake()
        {
            base.Awake();

            _arrowPrepare.Disable();
        }

        public override void ShootDown(Vector2 direction)
        {
            if (!Clip.IsEmpty)
            {
                _ShootInterval.Reset();
                IsShooting = true;
            }
            else if (Clip.IsEmpty)
            {
                TryAutoReload();
            }
        }

        public override void Shooting(Vector2 direction)
        {
            // 满足射击条件，则显示箭头
            if (_CanShoot && _arrowPrepare.IsDisabled())
            {
                _arrowPrepare.Enable();
            }
        }

        public override void ShootUp(Vector2 direction)
        {
            if (_CanShoot && _arrowPrepare.IsEnabled())
            {
                _arrowPrepare.Disable();

                ShootOnce(direction);

                AudioKit.PlaySound(ShootSounds.RandomTakeOne(), volume: 0.8f);
            }

            IsShooting = false;
        }
    }
}