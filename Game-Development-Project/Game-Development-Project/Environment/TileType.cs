using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace GameDevelopmentProject.Environment
{
    public class TileType
    {
        public Texture2D texture { get; set; }
        public bool solid { get; set; }

        public TileType(Texture2D texture)
        {
            this.texture = texture;
            this.solid = true;
        }

        public TileType(Texture2D texture, bool noHitBox) : this(texture)
        {
            this.solid = noHitBox;
        }
    }
}
