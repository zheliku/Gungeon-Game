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
    using System.Linq;
    using Framework.Core;
    using Framework.Toolkits.ActionKit;
    using Framework.Toolkits.AudioKit;
    using Framework.Toolkits.EventKit;
    using Framework.Toolkits.FluentAPI;
    using Framework.Toolkits.FSMKit;
    using Framework.Toolkits.InputKit;
    using Framework.Toolkits.SingletonKit;
    using Framework.Toolkits.UIKit;
    using TMPro;
    using UnityEngine;
    using UnityEngine.InputSystem;

    public class Player : AbstractRole, ISingleton, IRole
    {
        public enum State
        {
            Idle,
            Rolling,
        }

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

        private InputAction _moveAction;

        private Vector2 _rollDirection;

        public FSM<State> Fsm { get; } = new FSM<State>();

        private PlayerProperty _Property { get => this.GetModel<PlayerModel>().Property; }

        public List<AudioClip> GunTakeOutSounds = new List<AudioClip>();

        public Gun CurrentGun
        {
            get;
            private set;
        }

        protected override void Awake()
        {
            base.Awake();

            _playerSpriteOriginLocalPosY    = SpriteRenderer.GetLocalPositionY();
            _weaponTransformOriginLocalPosY = WeaponTransform.GetLocalPositionY();

            FloatingText.DisableGameObject();

            _moveAction = InputKit.GetInputAction(AssetConfig.Action.MOVE);

            _Property.Hp.Register((_, value) =>
            {
                if (value <= 0)
                {
                    UIKit.ShowPanelAsync<GameOver>();
                    this.DisableGameObject();
                }
            }).UnRegisterWhenGameObjectDestroyed(gameObject);

            UIKit.ShowPanelAsync<GamePlay>(play =>
            {
                play.UpdateGunView(CurrentGun);
            });

            UseGun(0);
        }

        private void Start()
        {
            var gunData = this.GetSystem<GunSystem>().GunDataList.First();
            if (gunData.Key == GunConfig.Pistol.Key)
            {
                UseGun(0);
            }

            Fsm.State(State.Idle)
               .OnUpdate(OnIdleUpdate);
            Fsm.State(State.Rolling)
               .OnEnter(OnRollEnter)
               .OnFixedUpdate(() =>
                {
                    Rigidbody2D.linearVelocity = _rollDirection * (_Property.MoveSpeed * 2f);
                })
               .OnExit(() =>
                {
                    GetComponent<Collider2D>().excludeLayers = 0; // 取消限制
                });

            Fsm.StartState(State.Idle);
        }

        private void OnEnable()
        {
            InputKit.BindPerformed(AssetConfig.Action.CHANGE_GUN, context =>
            {
                // 射击、切枪时不可切换 Gun
                if (CurrentGun.IsShooting || CurrentGun.Clip.IsReloading)
                {
                    return;
                }

                var gunList         = this.GetSystem<GunSystem>().GunDataList;
                var currentGunIndex = gunList.FindIndex(gun => gun == CurrentGun.Data);

                var newIndex = currentGunIndex + (int) context.ReadValue<float>();
                if (newIndex < 0)
                {
                    newIndex = gunList.Count - 1;
                }
                else if (newIndex >= gunList.Count)
                {
                    newIndex = 0;
                }
                UseGun(newIndex);
            }).UnBindAllWhenGameObjectDisabled(this);

            InputKit.BindPerformed(AssetConfig.Action.ROLL, _ =>
            {
                Fsm.StartState(State.Rolling);
            }).UnBindAllPerformedWhenGameObjectDisabled(this);
            
            InputKit.BindPerformed(AssetConfig.Action.ROLL, _ =>
            {
                UIKit.ShowPanel<UIGunList>();
            }).UnBindAllPerformedWhenGameObjectDisabled(this);
        }

        // Update is called once per frame
        void Update()
        {
            Fsm.Update();
        }

        private void FixedUpdate()
        {
            Fsm.FixedUpdate();
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

            // _Property.Hp.Value -= (int) damage;

            FxFactory.PlayHurtFx(this.GetPosition(), Color.green);
            FxFactory.PlayPlayerBlood(this.GetPosition());

            AudioKit.PlaySound(AssetConfig.Sound.PLAYER_HURT);

            GamePlay.PlayHurtFlashScreen();
        }

        private void OnIdleUpdate()
        {
            var moveDirection = _moveAction.ReadValue<Vector2>();
            Rigidbody2D.linearVelocity = moveDirection * _Property.MoveSpeed;

            if (moveDirection != Vector2.zero)
            {
                AnimationHelper.UpDownAnimation(SpriteRenderer, Time.time, 0.2f, _playerSpriteOriginLocalPosY, 0.05f);
                AnimationHelper.UpDownAnimation(WeaponTransform, Time.time, 0.2f, _weaponTransformOriginLocalPosY, 0.05f);

                AnimationHelper.RotateAnimation(SpriteRenderer, Time.time, 0.4f, 3);
            }

            // 相机位置随鼠标的指向偏移
            var cameraOffSetLength = this.Distance2D(CurrentGun.MousePosition);
            CameraController.CameraPosOffset = CurrentGun.ShootDirection * (cameraOffSetLength * 0.25f).Clamp(0, 3);

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

        private void OnRollEnter()
        {
            GetComponent<Collider2D>().excludeLayers = ~LayerMask.GetMask("Wall"); // 只和 Wall 层碰撞

            _rollDirection = _moveAction.ReadValue<Vector2>();
            if (_rollDirection == Vector2.zero)
            {
                _rollDirection = this.Direction2DTo(CurrentGun.MousePosition);
            }

            var facing = _rollDirection.x.Sign(); // 当前的水平朝向
            if (facing == 0)
            {
                facing = SpriteRenderer.flipX ? -1 : 1;
            }

            // 播放动画
            ActionKit.Lerp01(0.4f, f =>
                {
                    f = EasyTween.InSine(0, 1, f);

                    SpriteRenderer.SetLocalEulerAngles(z: -f * 360 * facing);
                    WeaponTransform.SetLocalEulerAngles(z: -f * 360 * facing);
                }, () =>
                {
                    SpriteRenderer.SetLocalEulerAngles(z: 0);
                    WeaponTransform.SetLocalEulerAngles(z: 0);
                    Fsm.ChangeState(State.Idle);
                })
               .Start(this);
        }

        public Gun GetGun(string key)
        {
            for (int i = 0; i < WeaponTransform.childCount; i++)
            {
                var gun = WeaponTransform.GetChild(i).GetComponent<Gun>();
                if (!gun)
                {
                    continue;
                }

                if (gun.name == key)
                {
                    return gun;
                }
            }

            return null;
        }

        public void UseGun(int gunIndex)
        {
            var oldGun = CurrentGun;
            CurrentGun?.DisableGameObject();

            var newGunData = this.GetSystem<GunSystem>().GunDataList[gunIndex];
            CurrentGun = GetGun(newGunData.Key);
            CurrentGun.EnableGameObject();
            CurrentGun.SetData(newGunData);

            var gunChange = CurrentGun != oldGun;

            if (gunChange)
            {
                AudioKit.PlaySound(GunTakeOutSounds.RandomTakeOne());
                TypeEventSystem.GLOBAL.Send(new GunChangeEvent(oldGun, CurrentGun));
            }

            CameraController.AdditionalOrthographicSize = CurrentGun.AdditionalCameraSize;
        }

        protected override IArchitecture _Architecture { get => Game.Architecture; }

        public void OnSingletonInit()
        {
            this.BindHierarchyComponent();
        }
    }
}