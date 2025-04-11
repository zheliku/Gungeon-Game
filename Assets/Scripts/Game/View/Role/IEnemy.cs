// ------------------------------------------------------------
// @file       IEnemy.cs
// @brief
// @author     zheliku
// @Modified   2025-03-09 12:51:58
// @Copyright  Copyright (c) 2025, zheliku
// ------------------------------------------------------------

namespace Game
{
    using UnityEngine;

    public interface IEnemy
    {
        GameObject GameObject { get; }

        Transform Transform { get; }

        Vector3 Position
        {
            get => Transform.position;
        }

        Room Room { get; set; }
        
        void Hurt(float damage, HitInfo hitInfo);
    }
}