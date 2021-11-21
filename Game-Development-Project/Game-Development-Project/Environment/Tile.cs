using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace GameDevelopmentProject.Environment
{
    class Tile
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
            _spriteBatch.Draw(tileType.texture, position, tileType.texture.Bounds, Color.White, 0, Vector2.Zero, 2.5f, SpriteEffects.None, 0f);
        }
    }
}
