using GameEngine.ExtensionMethods;
using GameEngine.Graphics;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace GameEngine.Data
{
    public class FlyingMovement : Movement
    {

        public void moveTo(Vector2 position,Vector2 target)
        {
            var temp = position - target;
            temp.Normalize();
            Velocity = temp;
        }

        public override void update(GameTime gameTime, IAnimationable animationable, IMoveable moveable)
        {
            if (Velocity.X >= MaxSpeedX)
            {
                Velocity.X = MaxSpeedX;
            }
            else if (Velocity.X <= -MaxSpeedX)
            {
                Velocity.X = -MaxSpeedX;
            }

            if (!IsButtonXPressed)
            {
                if (Velocity.X > 0)
                {
                    Velocity.X -= deceleration;

                }
                else if (Velocity.X < 0)
                {
                    Velocity.X += deceleration;
                }
            }

            if (Velocity.X == 0 && Velocity.Y == 0)
            {
                if (animationable.currentAnimation.AnimatieNaam.canMove())
                {
                    animationable.changeAnimation(AnimationsTypes.idle, true);
                }
            }

            moveable.position += Velocity * gameTime.ElapsedGameTime.Ticks / 100000;

            IsButtonXPressed = false;
        }
    }
}
