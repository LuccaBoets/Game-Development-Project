using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace GameEngine.Environment
{
    public class Scrolling
    {

        public Texture2D texture3;
        public Rectangle rectangle3;

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture3, rectangle3, Color.White);
        }
        public Scrolling(Texture2D newTexture, Rectangle newRectangle)
        {
            texture3 = newTexture;
            rectangle3 = newRectangle;
        }

        public void Update(int Speed)
        {
            rectangle3.X -= Speed;
        }
    }
}
