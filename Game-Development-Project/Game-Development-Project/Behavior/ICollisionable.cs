using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace GameDevelopmentProject.Behavior
{
    interface ICollisionable
    {
        public Rectangle getCollsionRectangle();
        public CollisionDirection CollisionDetection(Rectangle rectangle);

    }
}
