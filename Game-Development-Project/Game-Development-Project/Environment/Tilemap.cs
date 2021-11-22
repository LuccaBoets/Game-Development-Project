using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GameDevelopmentProject.Environment
{
    public class Tilemap
    {
        public List<Tile> tiles { get; set; }
        public float scale { get; set; }

        public Tilemap()
        {
            tiles = new List<Tile>();
            this.scale = 2f;
        }

        public void addTile(Texture2D texture, Vector2 position)
        {
            var tileType = TileFactory.GetTileType(texture);
            tiles.Add(new Tile(tileType, position * 16 * scale));
        }

        public void addTile(Texture2D textureTileSet, Vector2 position, Rectangle rectangle, GraphicsDevice graphicsDevice)
        {
            Texture2D croppedTexture = cropTexture(textureTileSet, rectangle, graphicsDevice);

            addTile(croppedTexture, position);
        }

        public void draw(SpriteBatch _spriteBatch)
        {
            foreach (var tile in tiles)
            {
                tile.draw(_spriteBatch);
            }
        }

        private static Texture2D cropTexture(Texture2D textureTileSet, Rectangle rectangle, GraphicsDevice graphicsDevice)
        {
            Texture2D croppedTexture = new Texture2D(graphicsDevice, rectangle.Width, rectangle.Height);

            Color[] data = new Color[rectangle.Width * rectangle.Height];
            textureTileSet.GetData(0, rectangle, data, 0, rectangle.Width * rectangle.Height);
            croppedTexture.SetData(data);
            return croppedTexture;
        }

        public bool touchGround(Rectangle rectangle)
        {
            //var temp = Rectangle.Intersect(tiles[0].getRectangle(), rectangle);
            return tiles.Any(x => !Rectangle.Intersect(x.getRectangle(), rectangle).IsEmpty);
        }
    }
}