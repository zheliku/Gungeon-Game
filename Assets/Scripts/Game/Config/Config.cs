// ------------------------------------------------------------
// @file       Config.cs
// @brief
// @author     zheliku
// @Modified   2025-02-23 04:02:31
// @Copyright  Copyright (c) 2025, zheliku
// ------------------------------------------------------------

namespace Game
{
    public class AssetConfig
    {
        public class Sound
        {
            public const string EMPTY_BULLET         = "EmptyBullet";
            public const string HP1                  = "Hp1";
            public const string CHEST                = "Chest";
            public const string DOOR_OPEN            = "DoorOpen";
            public const string PLAYER_HURT          = "PlayerHurt";
            public const string ENEMY_DIE            = "EnemyDie";
            public const string COIN                 = "Coin";
            public const string ARMOR1               = "Armor1";
            public const string USE_ARMOR            = "UseArmor";
            public const string POWER_UP_HALF_BULLET = "PowerUpHalfBullet";
        }

        public class Action
        {
            public const string ATTACK      = "Attack";
            public const string MOVE        = "Move";
            public const string BUY         = "Buy";
            public const string LOAD_BULLET = "LoadBullet";
            public const string OPEN_MAP    = "OpenMap";
            public const string CHANGE_GUN  = "ChangeGun";
        }
    }
}