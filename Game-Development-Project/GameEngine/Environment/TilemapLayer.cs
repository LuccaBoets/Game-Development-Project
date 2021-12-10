using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace GameEngine.Environment
{
    public class TilemapLayer
    {
        public List<Tile> Tiles { get; set; }
        public int LayerDepth { get; set; }

        public TilemapLayer(int layerDepth)
        {
            Tiles = new List<Tile>();
            LayerDepth = layerDepth;
        }

        internal void Draw(SpriteBatch _spriteBatch)
        {
            foreach (var tile in Tiles)
            {
                tile.Draw(_spriteBatch, 0.5f + ((float)LayerDepth / 100f));
            }
        }

        internal void Save(BinaryWriter writer)
        {
            writer.Write(LayerDepth);
            writer.Write(Tiles.Count);

            foreach (var tile in Tiles)
            {
                tile.Save(writer);
            }            
        }
    }
}
