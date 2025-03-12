// ------------------------------------------------------------
// @file       Laser.cs
// @brief
// @author     zheliku
// @Modified   2025-01-31 22:01:16
// @Copyright  Copyright (c) 2025, zheliku
// ------------------------------------------------------------

namespace Game
{
    using Framework.Core;
    using UnityEngine;
    using Framework.Toolkits.AudioKit;
    using Framework.Toolkits.FluentAPI;

    public class LaserGun : Gun
    {
        public float Distance = 10;

        [HierarchyPath("Laser")]
        public LineRenderer LineRenderer;

        private AudioPlayer _audioPlayer;
        
        protected override void Awake()
        {
            base.Awake();

            // 子弹空时，停止播放音效
            TypeEventSystem.GLOBAL.Register<GunBulletEnmptyEvent>(e =>
            {
                if (e.Gun == this)
                {
                    _audioPlayer.Stop();
                    LineRenderer.DisableGameObject();
                }
            }).UnRegisterWhenGameObjectDestroyed(this);
            
            // 子弹装填完成时，若仍按下鼠标，则直接开始发射
            TypeEventSystem.GLOBAL.Register<GunBulletLoadedEvent>(e =>
            {
                if (IsMouseLeftButtonDown && e.Gun == this)
                {
                    _audioPlayer = AudioKit.PlaySound(ShootSounds.RandomTakeOne(), volume: 0.6f, loop: true);
                    LineRenderer.EnableGameObject();
                    IsShooting = true;
                }
            }).UnRegisterWhenGameObjectDestroyed(this);
        }

        public override void ShootDown(Vector2 direction)
        {
            if (_CanShoot)
            {
                _audioPlayer = AudioKit.PlaySound(ShootSounds.RandomTakeOne(), volume: 0.6f, loop: true);
                LineRenderer.EnableGameObject();
                IsShooting = true;
            }
            else if (Clip.IsEmpty)
            {
                TryAutoReload();
            }
        }

        public override void Shooting(Vector2 direction)
        {
            if (Clip.IsEmpty)
            {
                PlayBulletEmptySound();
                return;
            }

            // 有子弹，才更新激光位置
            var layers   = LayerMask.GetMask("Wall", "Enemy");
            var hit      = Physics2D.Raycast(ShootPos.GetPosition(), direction, Distance, layers);
            var hitPoint = hit ? hit.point : ShootPos.GetPosition().ToVector2() + direction * Distance;
            LineRenderer.SetPositions(new Vector3[] { ShootPos.GetPosition(), hitPoint });

            if (_CanShoot)
            {
                Clip.Use();             // 弹夹使用子弹
                _ShootInterval.Reset(); // 射击间隔重置

                if (hit)
                {
                    var enemy = hit.collider.gameObject.GetComponent<EnemyA>();
                    if (enemy)
                    {
                        var damage = _gunData.DamageRange.RandomSelect();
                        enemy.Hurt(damage);
                    }
                }

                TypeEventSystem.GLOBAL.Send(new GunShootEvent(this));
            }
        }

        public override void ShootUp(Vector2 direction)
        {
            _audioPlayer?.Stop();
            LineRenderer.DisableGameObject();
            IsShooting = false;
        }
    }
}