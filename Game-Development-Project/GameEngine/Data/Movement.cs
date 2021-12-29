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
    public class Movement
    {
        public const int Speed = 3;
        public const float Gravity = 1;
        public bool InAir;
        public Vector2 Velocity;
        public int MaxSpeedX { get; set; } = 5;
        public bool IsButtonXPressed { get; set; } = false;

        public void right(IAnimationable animationable)
        {

            if (animationable.currentAnimation.AnimatieNaam.canMove())
            {
                Velocity.X += 1f;
                IsButtonXPressed = true;

                animationable.lookingLeft = false;

                if (!animationable.currentAnimation.AnimatieNaam.Equals(AnimationsTypes.run))
                {
                    animationable.changeAnimation(AnimationsTypes.run);
                }
            }
        }

        public void left(IAnimationable animationable)
        {
            if (animationable.currentAnimation.AnimatieNaam.canMove())
            {
                Velocity.X -= 1f;
                IsButtonXPressed = true;

                animationable.lookingLeft = true;

                if (!animationable.currentAnimation.AnimatieNaam.Equals(AnimationsTypes.run))
                {
                    animationable.changeAnimation(AnimationsTypes.run);
                }
            }
        }

        public void jump()
        {
            Velocity.Y = -6f;
            InAir = true;

        }

        public void down()
        {
            Velocity.Y += 0.15f * Gravity;
        }

        public void update(GameTime gameTime, IAnimationable animationable, IMoveable moveable)
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
                    Velocity.X -= 0.5f;

                }
                else if (Velocity.X < 0)
                {
                    Velocity.X += 0.5f;
                }
            }

            if (InAir)
            {

                down();

                if (Velocity.Y > 0)
                {
                    animationable.changeAnimation(AnimationsTypes.fall);

                }
                else
                {
                    animationable.changeAnimation(AnimationsTypes.jump);
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
