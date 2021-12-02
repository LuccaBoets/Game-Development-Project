using GameEngine.Data;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace GameEngine.Environment
{
    public class Scrolling : IPositionable
    {

        public Texture2D texture;
        public Rectangle rectangle3;

        public Vector2 position { get; set; }

        public void Draw(SpriteBatch spriteBatch)
        {
            //Texture2D texture, Vector2 position, Rectangle? sourceRectangle, Color color, float rotation, Vector2 origin, float scale, SpriteEffects effects, float layerDepth
            spriteBatch.Draw(texture, rectangle3, texture.Bounds, Color.White, 0f, Vector2.Zero, SpriteEffects.None, 0f);
        }
        public Scrolling(Texture2D newTexture, Rectangle newRectangle)
        {
            texture = newTexture;
            rectangle3 = newRectangle;
        }

        public void Update(int Speed)
        {
            rectangle3.X -= Speed;
        }
    }
}
