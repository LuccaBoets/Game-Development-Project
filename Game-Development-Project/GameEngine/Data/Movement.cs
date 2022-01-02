﻿using GameEngine.ExtensionMethods;
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
        public float acceleration { get; set; } = 1f;
        public float deceleration { get; set; } = 0.5f;
        public float jumpSpeed { get; set; } = 5f;
        public const float Gravity = 1f;
        public bool InAir;
        public Vector2 Velocity;
        public float MaxSpeedX { get; set; } = 4f;
        public bool IsButtonXPressed { get; set; } = false;

        public void right(IAnimationable animationable)
        {

            if (animationable.currentAnimation.AnimatieNaam.canMove())
            {
                Velocity.X += acceleration;
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
                Velocity.X -= acceleration;
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
            if (!InAir)
            {
                Velocity.Y = -jumpSpeed;
                InAir = true;
            }
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
                    Velocity.X -= deceleration;

                }
                else if (Velocity.X < 0)
                {
                    Velocity.X += deceleration;
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
