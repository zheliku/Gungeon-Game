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

        public static Texture2D ToTexture2D(Texture texture)
        {
            // 创建一个Texture2D对象，宽度和高度与传入的Texture对象相同
            var texture2D = new Texture2D(texture.width, texture.height, TextureFormat.RGBA32, false);

            // 获取当前活动的RenderTexture
            var currentRT = RenderTexture.active;

            // 创建一个临时的RenderTexture，宽度和高度与传入的Texture对象相同
            var renderTexture = RenderTexture.GetTemporary(texture.width, texture.height, 32);

            // 将传入的Texture对象渲染到临时的RenderTexture上
            Graphics.Blit(texture, renderTexture);

            // 将临时的RenderTexture设置为活动的RenderTexture
            RenderTexture.active = renderTexture;

            // 从临时的RenderTexture中读取像素，并将其存储到Texture2D对象中
            texture2D.ReadPixels(new Rect(0, 0, renderTexture.width, renderTexture.height), 0, 0);

            // 应用Texture2D对象中的像素
            texture2D.Apply();

            // 将活动的RenderTexture设置为原来的RenderTexture
            RenderTexture.active = currentRT;

            // 释放临时的RenderTexture
            RenderTexture.ReleaseTemporary(renderTexture);

            // 返回Texture2D对象
            return texture2D;
        }
    }
}