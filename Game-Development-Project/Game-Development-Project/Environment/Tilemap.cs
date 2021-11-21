using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace GameDevelopmentProject.Environment
{
    class Tilemap
    {
        public List<Tile> tiles { get; set; }

        public Tilemap()
        {
            tiles = new List<Tile>();
        }

        public void addTile(Texture2D texture, Vector2 position)
        {
            var tileType = TileFactory.GetTileType(texture);
            tiles.Add(new Tile(tileType, position));
        }

        public void addTile(Texture2D textureTileSet, Vector2 position, Rectangle rectangle, GraphicsDevice graphicsDevice)
        {
            Texture2D croppedTexture = new Texture2D(graphicsDevice, rectangle.Width, rectangle.Height);

            Color[] data = new Color[rectangle.Width * rectangle.Height];
            textureTileSet.GetData(0, rectangle, data, 0, rectangle.Width * rectangle.Height);
            croppedTexture.SetData(data);

            var tileType = TileFactory.GetTileType(croppedTexture);
            tiles.Add(new Tile(tileType, position));
        }

        public void draw(SpriteBatch _spriteBatch)
        {
            foreach (var tile in tiles)
            {
                tile.draw(_spriteBatch);
            }
        }
    }
}
