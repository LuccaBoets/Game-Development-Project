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
        public bool noHitBox { get; set; }

        public TileType(Texture2D texture)
        {
            this.texture = texture;
            this.noHitBox = false;
        }

        public TileType(Texture2D texture, bool noHitBox) : this(texture)
        {
            this.noHitBox = noHitBox;
        }
    }
}
