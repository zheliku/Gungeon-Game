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

    public class Player : AbstractRole, ISingleton
    {
        public static Player Instance { get => MonoSingletonProperty<Player>.Instance; }

        public static void DisplayText(string text, float duration)
        {
            var textLocalScale = Instance.FloatingText.GetLocalScale();
            var textLocalPos   = Instance.FloatingText.GetLocalPosition();

            var floatingText = Instance.FloatingText.Instantiate(Instance)
               .EnableGameObject();

            // 播放显示动画
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

        private float _playerSpriteOriginLocalPosY;
        private float _weaponTransformOriginLocalPosY;

        public int CurrentGunIndex;

        private InputAction _moveAction;

        private PlayerModel _playerModel;

        private PlayerProperty _Property { get => _playerModel.Property; }

        public List<Gun> Guns = new List<Gun>();

        public List<AudioClip> GunTakeOutSounds = new List<AudioClip>();

        public Gun CurrentGun
        {
            get => Guns[CurrentGunIndex];
        }

        protected override void Awake()
        {
            base.Awake();

            _playerSpriteOriginLocalPosY    = SpriteRenderer.GetLocalPositionY();
            _weaponTransformOriginLocalPosY = WeaponTransform.GetLocalPositionY();

            FloatingText.DisableGameObject();

            _playerModel = this.GetModel<PlayerModel>();

            _moveAction = InputKit.GetInputAction(AssetConfig.Action.MOVE);

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

            UseGun(1);
        }

        private void OnEnable()
        {
            InputKit.BindPerformed(AssetConfig.Action.CHANGE_GUN, context =>
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

            if (moveDirection != Vector2.zero)
            {
                AnimationHelper.UpDownAnimation(SpriteRenderer, 0.16f, _playerSpriteOriginLocalPosY, 0.02f);
                AnimationHelper.UpDownAnimation(WeaponTransform, 0.16f, _weaponTransformOriginLocalPosY, 0.02f);
            }

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

        public override void Hurt(float damage, HitInfo info)
        {
            Debug.Log("Player Hurt");

            if (_Property.Armor.Value > 0)
            {
                damage                -= _Property.Armor;
                _Property.Armor.Value -= ((int) damage).MaxWith(1);

                if (_Property.Armor.Value < 0)
                {
                    _Property.Armor.Value = 0; // 防止 Armor 为负数
                }

                // 播放使用 Armor 音效
                AudioKit.PlaySound(AssetConfig.Sound.USE_ARMOR);
            }

            if (damage <= 0)
            {
                return;
            }

            _Property.Hp.Value -= damage;

            FxFactory.PlayHurtFx(this.GetPosition(), Color.green);
            FxFactory.PlayPlayerBlood(this.GetPosition());

            AudioKit.PlaySound(AssetConfig.Sound.PLAYER_HURT);
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

        protected override IArchitecture _Architecture { get => Game.Architecture; }

        public void OnSingletonInit()
        {
            this.BindHierarchyComponent();
        }
    }
}