// ------------------------------------------------------------
// @file       Config.cs
// @brief
// @author     zheliku
// @Modified   2025-02-23 04:02:31
// @Copyright  Copyright (c) 2025, zheliku
// ------------------------------------------------------------

namespace Game
{
    using System.Collections.Generic;
    using Framework.Toolkits.FluentAPI;

    public class AssetConfig
    {
        public class Sound
        {
            public const string EMPTY_BULLET         = "EmptyBullet";
            public const string HP1                  = "Hp1";
            public const string CHEST                = "Chest";
            public const string KEY                  = "Key";
            public const string DOOR_OPEN            = "DoorOpen";
            public const string PLAYER_HURT          = "PlayerHurt";
            public const string ENEMY_DIE            = "EnemyDie";
            public const string COIN                 = "Coin";
            public const string ARMOR1               = "Armor1";
            public const string USE_ARMOR            = "UseArmor";
            public const string POWER_UP_HALF_BULLET = "PowerUpHalfBullet";

            public static string BulletSound
            {
                get => $"bullet_shell ({(1, 72 + 1).RandomSelect()})";
            }
        }

        public class Music
        {
            public const string SMOOTH_SAILING        = "Smooth Sailing";
            public const string UNDERGROUND_CONCOURSE = "UndergroundConcourse";
            public const string CHECKING_INSTRUMENTS  = "Checking Instruments";
            public const string D0_S_88_MARATHON_MAN  = "D0S-88 - Marathon Man";
            public const string DARKASCENT            = "darkascent";
            public const string DOS_88_AUTOMATAV2     = "DOS-88 - Automatav2";
            public const string DOS_88_PRESS_START    = "DOS-88 - Press Start";
            public const string FLOW_STATE            = "FlowState";
            public const string NIGHT_LIFE            = "Night Life";
            public const string ONLY_IN_DREAM         = "OnlyInDreams";
            public const string REST_EASY             = "Rest Easy";

            public static readonly List<string> ALL = new List<string>()
            {
                SMOOTH_SAILING,
                UNDERGROUND_CONCOURSE,
                CHECKING_INSTRUMENTS,
                D0_S_88_MARATHON_MAN,
                DARKASCENT,
                DOS_88_AUTOMATAV2,
                DOS_88_PRESS_START,
                FLOW_STATE,
                NIGHT_LIFE,
                ONLY_IN_DREAM,
                REST_EASY,
            };
        }

        public class Action
        {
            public const string ATTACK      = "Attack";
            public const string ROLL        = "Roll";
            public const string MOVE        = "Move";
            public const string BUY         = "Buy";
            public const string LOAD_BULLET = "LoadBullet";
            public const string OPEN_MAP    = "OpenMap";
            public const string CHANGE_GUN  = "ChangeGun";
        }

        public class Scene
        {
            public const string GAME = "Game";
        }

        public class EnemyName
        {
            public const string ENEMY_A = "EnemyA";
            public const string ENEMY_B = "EnemyB";
            public const string ENEMY_C = "EnemyC";
            public const string ENEMY_D = "EnemyD";
            public const string ENEMY_E = "EnemyE";
            public const string ENEMY_F = "EnemyF";
            public const string ENEMY_G = "EnemyG";
            public const string ENEMY_H = "EnemyH";

            public const string ENEMY_A_BIG = "EnemyA_Big";
            public const string ENEMY_B_BIG = "EnemyB_Big";
            public const string ENEMY_C_BIG = "EnemyC_Big";
            public const string ENEMY_D_BIG = "EnemyD_Big";
        }
    }
}