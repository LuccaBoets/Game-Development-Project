using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace GameEngine.Behavior
{

    public enum CollisionDirection
    {
        up,
        down,
        left,
        right,
        noHit
    }

    public static class CollisionManager
    {
        public static Tuple<CollisionDirection, Rectangle> Detection(Rectangle rectangle1, Rectangle rectangle2)
        {
            var intersectRectangle = Rectangle.Intersect(rectangle1, rectangle2);
            if (!intersectRectangle.IsEmpty)
            {
                return new Tuple<CollisionDirection, Rectangle>(RectanglesToDirection(rectangle1, rectangle2), intersectRectangle);
            }

            return new Tuple<CollisionDirection, Rectangle>(CollisionDirection.noHit, intersectRectangle);
        }

        //private static CollisionDirection RectanglesToDirection(Rectangle rectangle1, Rectangle rectangle2)
        //{
        //    double degrees = RectanglesToDegrees(rectangle1, rectangle2);

        //    if (degrees >= 45 && degrees < 135)
        //    {
        //        return CollisionDirection.up;

        //    }
        //    else if (degrees >= 135 && degrees < 225)
        //    {
        //        return CollisionDirection.left;
        //    }
        //    else if (degrees >= 225 && degrees < 315)
        //    {
        //        return CollisionDirection.down;
        //    }
        //    else
        //    {
        //        return CollisionDirection.right;
        //    }
        //}

        private static CollisionDirection RectanglesToDirection(Rectangle rectangle1, Rectangle rectangle2)
        {
            if (IsTouchingLeft(rectangle1, rectangle2))
            {
                return CollisionDirection.left;

            }
            else if (IsTouchingRight(rectangle1, rectangle2))
            {
                return CollisionDirection.right;

            }
            else if (IsTouchingTop(rectangle1, rectangle2))
            {
                return CollisionDirection.up;

            }
            else if (IsTouchingBottom(rectangle1, rectangle2))
            {
                return CollisionDirection.down;
            }

            return CollisionDirection.noHit;
        }

        private static double RectanglesToDegrees(Rectangle rectangle1, Rectangle rectangle2)
        {
            var deltaX = rectangle2.Center.X - rectangle1.Center.X;
            var deltaY = rectangle2.Center.Y - rectangle1.Center.Y;

            var radians = Math.Atan2(deltaY, deltaX);

            double degrees = radians * (180 / Math.PI);
            return degrees + 180;
        }

        public static bool IsTouchingLeft(Rectangle rectangle1, Rectangle rectangle2)
        {
            return rectangle1.Right > rectangle2.Left &&
              rectangle1.Left < rectangle2.Left &&
              rectangle1.Bottom > rectangle2.Top &&
              rectangle1.Top < rectangle2.Bottom;
        }

        public static bool IsTouchingRight(Rectangle rectangle1, Rectangle rectangle2)
        {
            return rectangle1.Left < rectangle2.Right &&
              rectangle1.Right > rectangle2.Right &&
              rectangle1.Bottom > rectangle2.Top &&
              rectangle1.Top < rectangle2.Bottom;
        }

        public static bool IsTouchingTop(Rectangle rectangle1, Rectangle rectangle2)
        {
            return rectangle1.Bottom > rectangle2.Top &&
              rectangle1.Top < rectangle2.Top &&
              rectangle1.Right > rectangle2.Left &&
              rectangle1.Left < rectangle2.Right;
        }

        public static bool IsTouchingBottom(Rectangle rectangle1, Rectangle rectangle2)
        {
            return rectangle1.Top < rectangle2.Bottom &&
              rectangle1.Bottom > rectangle2.Bottom &&
              rectangle1.Right > rectangle2.Left &&
              rectangle1.Left < rectangle2.Right;
        }
    }
}
