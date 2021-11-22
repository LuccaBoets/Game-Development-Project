using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace GameDevelopmentProject.Environment
{
    public class Tile
    {
        public TileType tileType { get; set; }

        public Vector2 position { get; set; }

        public Tile(TileType tileType, Vector2 position)
        {
            this.tileType = tileType;
            this.position = position;
        }

        public void draw(SpriteBatch _spriteBatch)
        {
            _spriteBatch.Draw(tileType.texture, position, tileType.texture.Bounds, Color.White, 0, Vector2.Zero, 2f, SpriteEffects.None, 0f);
        }

        public Rectangle getRectangle()
        {
            var rectangle = tileType.texture.Bounds;

            rectangle.X += (int) position.X;
            rectangle.Y += (int) position.Y;

            return rectangle;
        }
    }
}
