using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace GameDevelopmentProject.Content.data
{
    interface IMoveable
    {
        public Movement movement { get; set; }

        public Vector2 position { get; set; }
    }
}
