﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace GameEngine.Environment
{
    public class TileType
    {
        public Texture2D texture { get; set; }

        public TileType(Texture2D texture)
        {
            this.texture = texture;
        }
    }
}
