using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace GameEngine.Data
{
    public interface IMoveable : IPositionable
    {
        public Movement Movement { get; set; }
    }
}
