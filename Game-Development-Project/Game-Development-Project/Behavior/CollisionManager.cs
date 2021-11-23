using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace GameDevelopmentProject.Behavior
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
        public static CollisionDirection detection(Rectangle rectangle1, Rectangle rectangle2)
        {
            if (!Rectangle.Intersect(rectangle1, rectangle2).IsEmpty)
            {
                return rectanglesToCardinalDirection(rectangle1, rectangle2);
            }

            return CollisionDirection.noHit;
        }

        private static CollisionDirection rectanglesToCardinalDirection(Rectangle rectangle1, Rectangle rectangle2)
        {
            double degrees = rectanglesToDegrees(rectangle1, rectangle2);

            Debug.WriteLine(degrees);

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
