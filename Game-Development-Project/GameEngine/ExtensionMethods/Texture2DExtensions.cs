using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GameEngine.ExtensionMethods
{
    public static class Texture2DExtensions
    {
        public static Texture2D Cut(this Texture2D texture, Rectangle borders, GraphicsDevice graphicsDevice)
        {
            Texture2D croppedTexture = new Texture2D(graphicsDevice , borders.Width, borders.Height);

            Color[] data = new Color[borders.Width * borders.Height];
            texture.GetData(0, borders, data, 0, borders.Width * borders.Height);
            croppedTexture.SetData(data);
            return croppedTexture;
        }

        public static bool IsTransparent(this Texture2D texture)
        {
            int size = texture.Width * texture.Height;
            Color[] buffer = new Color[size];
            texture.GetData(0, texture.Bounds, buffer, 0, size);
            return buffer.All(c => c == Color.Transparent);
        }
    }
}
