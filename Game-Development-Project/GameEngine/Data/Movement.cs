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
        public const int speed = 3;
        public const float gravity = 1;
        public bool inAir;
        public Vector2 velocity;
        public int maxSpeedX { get; set; } = 5;
        public bool isButtonXPressed { get; set; } = false;


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
            velocity.X += 1f;
            isButtonXPressed = true;


            animationable.lookingRight = false;

            if (!animationable.currentAnimation.AnimatieNaam.Equals(HeroAnimations.run))
            {
                animationable.currentAnimation = animationable.Animaties.First(x => x.AnimatieNaam == HeroAnimations.run);
            }
        }

        public void left(IAnimationable animationable)
        {
            velocity.X -= 1f;
            isButtonXPressed = true;

            animationable.lookingRight = true;

            if (!animationable.currentAnimation.AnimatieNaam.Equals(HeroAnimations.run))
            {
                animationable.currentAnimation = animationable.Animaties.First(x => x.AnimatieNaam == HeroAnimations.run);
            }
        }

        public void jump(IMoveable moveable)
        {

            //hero.position = new Vector2(hero.position.X, hero.position.Y -10f);
            velocity.Y = -6f;
            moveable.position += new Vector2(0, -1f);
            inAir = true;

        }

        public void down()
        {
            velocity.Y += 0.15f * gravity;

        }

        public void update(GameTime gameTime, IAnimationable animationable, IMoveable moveable)
        {
            if (velocity.X >= maxSpeedX)
            {
                velocity.X = maxSpeedX;
            }
            else if (velocity.X <= -maxSpeedX)
            {
                velocity.X = -maxSpeedX;
            }

            if (!isButtonXPressed)
            {
                if (velocity.X > 0)
                {
                    velocity.X -= 0.5f;

                }
                else if (velocity.X < 0)
                {
                    velocity.X += 0.5f;
                }
            }



            if (inAir)
            {

                down();

                if (velocity.Y > 0)
                {
                    animationable.currentAnimation = animationable.Animaties.First(x => x.AnimatieNaam == HeroAnimations.fall);
                }
                else
                {
                    animationable.currentAnimation = animationable.Animaties.First(x => x.AnimatieNaam == HeroAnimations.jump);
                }
            }

            moveable.position += velocity * gameTime.ElapsedGameTime.Ticks / 100000;

            //Debug.WriteLine(velocity);

            //if (moveable.position.Y >= )
            //{

            //}
            isButtonXPressed = false;
        }
    }
}
