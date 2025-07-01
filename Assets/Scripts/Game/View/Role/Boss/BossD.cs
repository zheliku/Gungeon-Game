// ------------------------------------------------------------
// @file       Enemy.cs
// @brief
// @author     zheliku
// @Modified   2025-01-29 16:01:14
// @Copyright  Copyright (c) 2025, zheliku
// ------------------------------------------------------------

namespace Game
{
    using System.Collections.Generic;
    using System.Linq;
    using Framework.Core;
    using Framework.Toolkits.AudioKit;
    using Framework.Toolkits.EventKit;
    using Framework.Toolkits.FluentAPI;
    using Framework.Toolkits.FSMKit;
    using UnityEngine;

    public class BossD : Boss
    {
        public enum State
        {
            Follow,
            PrepareToShoot,
            Shoot
        }

        public float HpRatio
        {
            get => Property.Hp.Value * 1f / Property.MaxHp.Value;
        }

        public FSM<State> FSM = new FSM<State>();

        [HierarchyPath("ArmorLeft")]
        public Collider2D ArmorLeft;

        [HierarchyPath("ArmorRight")]
        public Collider2D ArmorRight;

        protected override void Awake()
        {
            base.Awake();

            FSM.AddState(State.Follow, new StateFollowBossD(FSM, this));
            FSM.AddState(State.PrepareToShoot, new StatePrepareToShootBossD(FSM, this));
            FSM.AddState(State.Shoot, new StateShootBossD(FSM, this));

            FSM.StartState(State.Follow);

            ArmorLeft.OnCollisionEnter2DEvent(ReflectBullet);
            ArmorRight.OnCollisionEnter2DEvent(ReflectBullet);

            ArmorLeft.OnUpdateEvent(() =>
            {
                ArmorLeft.SetLocalEulerAngles(z: Time.time * Time.time.Log(3));
            });
            ArmorRight.OnUpdateEvent(() =>
            {
                ArmorRight.SetLocalEulerAngles(z: Time.time * Time.time.Log(3));
            });
        }

        private void ReflectBullet(Collision2D collision2D)
        {
            var playerBullet = collision2D.gameObject.GetComponent<PlayerBullet>();

            if (playerBullet)
            {
                // 计算反弹方向（法线反射）
                Vector2 reflectedVelocity = Vector2.Reflect(collision2D.relativeVelocity, collision2D.contacts[0].normal);

                if (reflectedVelocity == Vector2.zero)
                {
                    reflectedVelocity = this.Direction2DTo(Player.Instance);
                }

                var newBullet = Bullet.Instantiate()
                   .SetPosition(collision2D.contacts[0].point)
                   .Enable()
                   .GetComponent<Bullet>();

                // 应用新的速度
                newBullet.Velocity = reflectedVelocity.normalized * BulletSpeed;

                newBullet.Damage = Property.Damage;
                
                Debug.Log($"relativeVelocity: {collision2D.relativeVelocity}, normal: {collision2D.contacts[0].normal}, reflectedVelocity: {reflectedVelocity}");

                // 可选：旋转子弹使其朝向运动方向
                // float angle = Mathf.Atan2(reflectedVelocity.y, reflectedVelocity.x) * Mathf.Rad2Deg;
                // playerBullet.transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
            }
        }

        protected override void Update()
        {
            base.Update();

            FSM.Update();
        }

        private void FixedUpdate()
        {
            FSM.FixedUpdate();
        }

        protected override IArchitecture _Architecture { get => Game.Architecture; }
    }
}