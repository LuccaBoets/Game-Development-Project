using GameEngine.Behavior;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace GameEngine.Characters
{
    interface ICollisionable
    {
        public Rectangle GetCollsionRectangle();
        public Tuple<CollisionDirection, Rectangle> CollisionDetection(Rectangle rectangle);

    }
}
