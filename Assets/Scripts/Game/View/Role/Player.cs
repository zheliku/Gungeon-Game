// ------------------------------------------------------------
// @file       Player.cs
// @brief
// @author     zheliku
// @Modified   2025-01-29 16:01:52
// @Copyright  Copyright (c) 2025, zheliku
// ------------------------------------------------------------

namespace Game
{
    using System.Collections.Generic;
    using Framework.Core;
    using Framework.Toolkits.ActionKit;
    using Framework.Toolkits.AudioKit;
    using Framework.Toolkits.FluentAPI;
    using Framework.Toolkits.InputKit;
    using Framework.Toolkits.SingletonKit;
    using Framework.Toolkits.UIKit;
    using TMPro;
    using UnityEngine;
    using UnityEngine.InputSystem;
    using ISingleton = Framework.Toolkits.SingletonKit.ISingleton;

    public class Player : AbstractRole, ISingleton
    {
        public static Player Instance { get => MonoSingletonProperty<Player>.Instance; }

        public static void DisplayText(string text, float duration)
        {
            var textLocalScale = Instance.FloatingText.GetLocalScale();
            var textLocalPos   = Instance.FloatingText.GetLocalPosition();

            var floatingText = Instance.FloatingText.Instantiate(Instance)
               .EnableGameObject();
            
            ActionKit.Sequence()
               .Callback(() =>
                {
                    floatingText.text = text;
                })
               .Lerp01(0.5f, f =>
                {
                    floatingText.SetLocalScale(textLocalScale * f);
                    floatingText.SetLocalPosition(textLocalPos + Vector3.up * f);
                })
               .Delay(duration)
               .Lerp01(0.5f, f =>
                {
                    f = 1 - f;
                    floatingText.SetLocalScale(textLocalScale * f);
                    floatingText.SetLocalPosition(textLocalPos + Vector3.up * f);
                })
               .Callback(() =>
                {
                    floatingText.DestroyGameObject();
                })
               .Start(Instance);
        }

        [HierarchyPath("Weapon")]
        public Transform WeaponTransform;

        [HierarchyPath("FloatingText")]
        public TextMeshPro FloatingText;

        public int CurrentGunIndex;

        private InputAction _moveAction;

        private PlayerModel _playerModel;

        private Property _Property { get => _playerModel.Property; }

        public List<Gun> Guns = new List<Gun>();

        public List<AudioClip> GunTakeOutSounds = new List<AudioClip>();

        public Gun CurrentGun
        {
            get => Guns[CurrentGunIndex];
        }

        protected override void Awake()
        {
            base.Awake();

            FloatingText.DisableGameObject();

            _playerModel = this.GetModel<PlayerModel>();

            _moveAction = InputKit.GetInputAction("Move");

            _Property.Hp.Register((oldValue, value) =>
            {
                if (value <= 0)
                {
                    UIKit.ShowPanelAsync<GameOver>();
                    this.DisableGameObject();
                }
            }).UnRegisterWhenGameObjectDestroyed(gameObject);

            UIKit.ShowPanelAsync<GamePlay>();

            for (int i = 0; i < WeaponTransform.childCount; i++)
            {
                var gun = WeaponTransform.GetChild(i).GetComponent<Gun>();
                if (!gun)
                {
                    continue;
                }

                Guns.Add(gun);

                // 寻找激活的 Gun，设置为初始 Gun
                if (gun.gameObject.IsEnabled())
                {
                    UseGun(i);
                }
            }
        }

        private void OnEnable()
        {
            InputKit.BindPerformed("ChangeGun", context =>
            {
                var currentGun = Guns[CurrentGunIndex];

                // 射击、切枪时不可切换 Gun
                if (currentGun.IsShooting || currentGun.Clip.IsReloading)
                {
                    return;
                }

                var newIndex = CurrentGunIndex + (int) context.ReadValue<float>();
                if (newIndex < 0)
                {
                    newIndex = Guns.Count - 1;
                }
                else if (newIndex >= Guns.Count)
                {
                    newIndex = 0;
                }
                UseGun(newIndex);
            }).UnBindAllWhenGameObjectDisabled(gameObject);
        }

        // Update is called once per frame
        void Update()
        {
            var moveDirection = _moveAction.ReadValue<Vector2>();
            Rigidbody2D.linearVelocity = moveDirection * _Property.MoveSpeed;

            if (CurrentGun)
            {
                if (CurrentGun.ShootDirection.x > 0)
                {
                    SpriteRenderer.flipX = false;
                }
                else if (CurrentGun.ShootDirection.x < 0)
                {
                    SpriteRenderer.flipX = true;
                }
            }
        }

        public override void Hurt(float damage)
        {
            _Property.Hp.Value -= damage;
        }

        public void UseGun(int gunIndex)
        {
            var gunChange = CurrentGunIndex != gunIndex;

            var oldGun = Guns[CurrentGunIndex];
            oldGun.DisableGameObject();

            CurrentGunIndex = gunIndex;
            var newGun = Guns[CurrentGunIndex];
            newGun.EnableGameObject();

            if (gunChange)
            {
                AudioKit.PlaySound(GunTakeOutSounds.RandomTakeOne());
                TypeEventSystem.GLOBAL.Send(new GunChangeEvent(oldGun, newGun));
            }
        }

        protected override IArchitecture _Architecture { get => Game.Interface; }

        public void OnSingletonInit() { }
    }
}