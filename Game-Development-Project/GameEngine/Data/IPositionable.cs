using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace GameEngine.Data
{
    public interface IPositionable
    {
        public Vector2 position { get; set; }
    }
}
