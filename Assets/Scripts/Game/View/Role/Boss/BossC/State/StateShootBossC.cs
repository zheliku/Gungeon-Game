// ------------------------------------------------------------
// @file       StateFollow.cs
// @brief
// @author     zheliku
// @Modified   2025-04-16 00:04:13
// @Copyright  Copyright (c) 2025, zheliku
// ------------------------------------------------------------

namespace Game
{
    using Framework.Toolkits.AudioKit;
    using Framework.Toolkits.EventKit;
    using Framework.Toolkits.FluentAPI;
    using Framework.Toolkits.FSMKit;
    using Framework.Toolkits.TimerKit;
    using UnityEngine;

    public class StateShootBossC : AbstractState<BossC.State, BossC>
    {
        public StateShootBossC(FSM<BossC.State> fsm, BossC owner) : base(fsm, owner)
        { }

        protected override void OnEnter()
        {
            _owner.Rigidbody2D.linearVelocity = Vector2.zero;
        }

        protected override void OnUpdate()
        {
            switch (_owner.HpRatio)
            {
                case > 0.7f: Stage1Update(); break;
                case > 0.3f: Stage2Update(); break;
                case >= 0f:  Stage3Update(); break;
            }
        }

        protected override void OnExit()
        {
            var randomSpeed = _owner.HpRatio switch // 根据血量随机选择移动速度
            {
                > 0.7f => (3, 6).RandomSelect(),
                > 0.3f => (4, 7).RandomSelect(),
                >= 0f  => (5, 8).RandomSelect(),
                _      => 3f
            };
            _owner.Property.MoveSpeed = randomSpeed;
        }

        private void Stage1Update() // 阶段一，只攻击一次
        {
            BulletHelper.Shoot(
                _owner.GetPosition(),
                _owner.Direction2DTo(Player.Instance),
                _owner.Bullet,
                1,
                _owner.BulletSpeed);

            AudioKit.PlaySound(_owner.ShootSounds.RandomTakeOne());

            _fsm.ChangeState(BossC.State.Follow);
        }

        private void Stage2Update() // 阶段二，持续攻击 1 s
        {
            if (TimerKit.HasPassedInterval(this, 0.5f))
            {
                var bullets = BulletHelper.SpreadShoot(
                    3,
                    _owner.GetPosition(),
                    1.5f,
                    _owner.Direction2DTo(Player.Instance),
                    15,
                    _owner.Bullet,
                    1,
                    _owner.BulletSpeed);

                foreach (var bullet in bullets)
                {
                    var fullBounceBullet = bullet as FullBounceEnemyBullet;

                    if (fullBounceBullet != null)
                    {
                        fullBounceBullet.ReflectCount = 5; // 弹 5 次消失
                        fullBounceBullet.SpriteColor  = Color.red;
                    }
                }

                AudioKit.PlaySound(_owner.ShootSounds.RandomTakeOne());
            }

            if (_fsm.SecondsOfCurrentState >= 1f)
            {
                _fsm.ChangeState(BossC.State.Follow);
            }
        }

        private void Stage3Update() // 阶段二，持续攻击 1 s
        {
            if (TimerKit.HasPassedInterval(this, 1f))
            {
                var bullets = BulletHelper.CircleShoot(
                    8,
                    _owner.GetPosition(),
                    1.5f,
                    (0f, 360f).RandomSelect(),
                    _owner.Bullet,
                    1,
                    _owner.BulletSpeed);

                foreach (var bullet in bullets)
                {
                    var fullBounceBullet = bullet as FullBounceEnemyBullet;

                    if (fullBounceBullet != null)
                    {
                        fullBounceBullet.ReflectCount = 3; // 弹 3 次消失
                        fullBounceBullet.SpriteColor  = Color.yellow;
                    }
                }

                AudioKit.PlaySound(_owner.ShootSounds.RandomTakeOne());

                if (_fsm.SecondsOfCurrentState >= 5f)
                {
                    _fsm.ChangeState(BossC.State.Follow);
                }
            }
        }
    }
}