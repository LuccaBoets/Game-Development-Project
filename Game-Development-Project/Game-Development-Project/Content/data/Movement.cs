using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GameDevelopmentProject
{
    class Movement
{
        public const int speed = 3;
        public const float gravity = 1;
        public bool jumped;
        public Vector2 velocity;
      

       

        public void Move(Hero hero, int Xnumber, float Ynumber)
        {

                hero.position += new Vector2(Xnumber, Ynumber);
                hero.currentAnimation = hero.Animaties.First(x => x.AnimatieNaam == HeroAnimations.run);
            
              
        }

        public void Jump(Hero hero)
        {

           hero.position = new Vector2(hero.position.X, hero.position.Y -10f);
           velocity.Y = -6f;
   
        }

        public void Down()
        {
            velocity.Y = 0.15f * gravity;
         
        }

    }
}
