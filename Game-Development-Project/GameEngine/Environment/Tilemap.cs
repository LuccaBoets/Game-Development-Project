using GameEngine.Behavior;
using GameEngine.ExtensionMethods;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GameEngine.Environment
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

        public Tilemap(Texture2D map, GraphicsDevice graphicsDevice)
        {
            tiles = new List<Tile>();
            this.scale = 2f;

            for (int i = 0; i < map.Width/16; i++)
            {
                for (int j = 0; j < map.Height/16; j++)
                {
                    Texture2D texture = map.Cut(new Rectangle(i * 16, j * 16, 16, 16), graphicsDevice);
                    if (!texture.IsTransparent())
                    {
                        addTile(texture, new Vector2(i, j));
                    }
                }
            }
        }

        public void addTile(Texture2D texture, Vector2 position, SpriteEffects spriteEffects = SpriteEffects.None, bool noHitBox = true)
        {
            var tileType = TileFactory.GetTileType(texture, noHitBox);
            tiles.Add(new Tile(tileType, position * 16 * scale, spriteEffects));
        }

        public void addTile(Texture2D textureTileSet, Vector2 position, Rectangle rectangle, GraphicsDevice graphicsDevice, SpriteEffects spriteEffects = SpriteEffects.None, bool noHitBox = true)
        {
            Texture2D texture = textureTileSet.Cut(rectangle, graphicsDevice);
            if (!texture.IsTransparent())
            {
                addTile(texture, position, spriteEffects, noHitBox);
            }
        }

        public void draw(SpriteBatch _spriteBatch)
        {
            foreach (var tile in tiles)
            {
                tile.draw(_spriteBatch);
            }
        }

        //private static Texture2D cropTexture(Texture2D textureTileSet, Rectangle rectangle, GraphicsDevice graphicsDevice)
        //{
        //    Texture2D croppedTexture = new Texture2D(graphicsDevice, rectangle.Width, rectangle.Height);

        //    Color[] data = new Color[rectangle.Width * rectangle.Height];
        //    textureTileSet.GetData(0, rectangle, data, 0, rectangle.Width * rectangle.Height);
        //    croppedTexture.SetData(data);
        //    return croppedTexture;
        //}

        public List<Tuple<CollisionDirection, Rectangle>> hitAnyTile(Rectangle rectangle)
        {
            return tiles.Select(x => x.CollisionDetection(rectangle)).Where(x => x != null).Where(x => x.Item1 != CollisionDirection.noHit).GroupBy(x => x.Item1).Select(x => x.First()).ToList();
        }
    }
}