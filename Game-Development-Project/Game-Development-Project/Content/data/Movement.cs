using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GameDevelopmentProject
{
    public class Movement
{
        public const int speed = 3;
        public const float gravity = 1;
        public bool jumped;
        public Vector2 velocity;
      

       

        public void Move(Hero hero, int Xnumber, GameTime gameTime)
        {
            var delta = (float)gameTime.ElapsedGameTime.TotalMilliseconds;
            if (Xnumber > 0) //Right
            {
                hero.position += new Vector2(0.3f * delta, 0.0f);
                hero.lookingRight = false;
            }
            else //Left
            {
                hero.position += new Vector2(-0.3f * delta, 0.0f);
                hero.lookingRight = true;
            }
            
                hero.currentAnimation = hero.Animaties.First(x => x.AnimatieNaam == HeroAnimations.run);
            
              
        }

        public void Jump(Hero hero)
        {

           hero.position = new Vector2(hero.position.X, hero.position.Y -10f);
           velocity.Y = -6f;
   
        }

        public void Down(Hero hero)
        {
            velocity.Y += 0.15f * gravity;

            if (velocity.Y > 0)
            {
                hero.currentAnimation = hero.Animaties.First(x => x.AnimatieNaam == HeroAnimations.fall);
            }
            else
            {
                hero.currentAnimation = hero.Animaties.First(x => x.AnimatieNaam == HeroAnimations.jump);
            }
        }

        public void update(GameTime gameTime)
        {

        }

    }
}
