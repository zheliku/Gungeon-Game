// ------------------------------------------------------------
// @file       10.UnityEngineOthersExtension.cs
// @brief
// @author     zheliku
// @Modified   2024-12-17 21:12:22
// @Copyright  Copyright (c) 2024, zheliku
// ------------------------------------------------------------

namespace Framework.Toolkits.FluentAPI
{
    using System.Collections.Generic;
    using System.Linq;
    using Core;
    using UnityEngine;

    public static class UnityEngineOtherExtension
    {
        public static SpriteRenderer SetAlpha(this SpriteRenderer self, float alpha)
        {
            var color = self.color;
            color.a    = alpha;
            self.color = color;
            return self;
        }
    }
}