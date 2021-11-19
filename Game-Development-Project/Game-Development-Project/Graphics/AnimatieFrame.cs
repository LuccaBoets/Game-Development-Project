using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace GameDevelopmentProject
{
    public class AnimatieFrame
    {
        public Rectangle borders { get; set; }

        public AnimatieFrame(Rectangle borders)
        {
            this.borders = borders;
        }
    }
}
