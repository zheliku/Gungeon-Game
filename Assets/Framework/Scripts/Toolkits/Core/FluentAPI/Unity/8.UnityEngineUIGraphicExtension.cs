// ------------------------------------------------------------
// @file       8.UnityEngineUIGraphicExtension.cs
// @brief
// @author     zheliku
// @Modified   2024-10-21 00:10:56
// @Copyright  Copyright (c) 2024, zheliku
// ------------------------------------------------------------

namespace Framework.Toolkits.FluentAPI
{
    using UnityEngine;
    using UnityEngine.UI;

    /// <summary>
    /// UnityEngine.UI.Graphic 静态扩展
    /// </summary>
    public static class UnityEngineUIGraphicExtension
    {
        public static T SetColor<T>(this T selfGraphic, Color color) where T : Graphic
        {
            selfGraphic.color = color;
            return selfGraphic;
        }

        public static T SetColor<T>(
            this T selfGraphic,
            float? r = null,
            float? g = null,
            float? b = null,
            float? a = null
        ) where T : Graphic
        {
            var color = selfGraphic.color;
            selfGraphic.color = new Color(r ?? color.r, g ?? color.g, b ?? color.b, a ?? color.a);
            return selfGraphic;
        }
    }
}