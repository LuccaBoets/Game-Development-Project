﻿using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GameDevelopmentProject.Environment
{
    public class TileFactory
    {

        public static List<TileType> TileTypes { get; set; } = new List<TileType>();

        public static TileType GetTileType(Texture2D texture, bool solid = true)
        {
             var tileType = TileTypes.FirstOrDefault(x => x.texture.Equals(texture));

            if (tileType == null)
            {
                tileType = new TileType(texture, solid);
                TileTypes.Add(tileType);
            }

            return tileType;
        }
    }
}
