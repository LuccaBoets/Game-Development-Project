using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace GameEngine.Behavior
{

    public enum CollisionDirection
    {
        north,
        south,
        west,
        east,
        noHit
    }

    public static class CollisionManager
    {
        public static Tuple<CollisionDirection, Rectangle> detection(Rectangle rectangle1, Rectangle rectangle2)
        {
            var intersectRectangle = Rectangle.Intersect(rectangle1, rectangle2);
            if (!intersectRectangle.IsEmpty)
            {
                return new Tuple<CollisionDirection, Rectangle>( rectanglesToCardinalDirection(rectangle1, rectangle2), intersectRectangle);
            }

            return new Tuple<CollisionDirection, Rectangle>(CollisionDirection.noHit, intersectRectangle);
        }

        private static CollisionDirection rectanglesToCardinalDirection(Rectangle rectangle1, Rectangle rectangle2)
        {
            double degrees = rectanglesToDegrees(rectangle1, rectangle2);

            if (degrees >= 45 && degrees < 135)
            {
                return CollisionDirection.north;

            }
            else if (degrees >= 135 && degrees < 225)
            {
                return CollisionDirection.west;
            }
            else if (degrees >= 225 && degrees < 315)
            {
                return CollisionDirection.south;
            }
            else
            {
                return CollisionDirection.east;
            }
        }

        private static double rectanglesToDegrees(Rectangle rectangle1, Rectangle rectangle2)
        {
            var deltaX = rectangle2.Center.X - rectangle1.Center.X;
            var deltaY = rectangle2.Center.Y - rectangle1.Center.Y;

            var radians = Math.Atan2(deltaY, deltaX);

            double degrees = radians * (180 / Math.PI);
            return degrees + 180;
        }
    }
}
