﻿using GameEngine.Graphics;
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


        //public void Move(Hero hero, int Xnumber, GameTime gameTime)
        //{
        //    var delta = (float)gameTime.ElapsedGameTime.TotalMilliseconds;
        //    if (Xnumber > 0) //Right
        //    {
        //        hero.position += new Vector2(0.3f * delta, 0.0f);
        //        //velocity.X += 1f;
        //        hero.lookingRight = false;
        //    }
        //    else //Left
        //    {
        //        hero.position += new Vector2(-0.3f * delta, 0.0f);
        //        //velocity.X += -1f;

        //        hero.lookingRight = true;
        //    }

        //    hero.currentAnimation = hero.Animaties.First(x => x.AnimatieNaam == HeroAnimations.run);


        //}

        public void right(IAnimationable animationable)
        {
            Velocity.X += 1f;
            IsButtonXPressed = true;


            animationable.lookingLeft = false;

            if (!animationable.currentAnimation.AnimatieNaam.Equals(AnimationsTypes.run))
            {
                animationable.changeAnimation(AnimationsTypes.run);
            }
        }

        public void left(IAnimationable animationable)
        {
            Velocity.X -= 1f;
            IsButtonXPressed = true;

            animationable.lookingLeft = true;

            if (!animationable.currentAnimation.AnimatieNaam.Equals(AnimationsTypes.run))
            {
                animationable.changeAnimation(AnimationsTypes.run);
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

            moveable.position += Velocity * gameTime.ElapsedGameTime.Ticks / 100000;

            //Debug.WriteLine(velocity);

            //if (moveable.position.Y >= )
            //{

            //}
            IsButtonXPressed = false;
        }
    }
}
