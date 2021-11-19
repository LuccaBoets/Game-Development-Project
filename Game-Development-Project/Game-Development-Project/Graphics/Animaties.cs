using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;


namespace GameDevelopmentProject
{
    public static class Animaties
    {

        public static Animatie GetIdleAnimatieFromHero(ContentManager content)
        {
            Texture2D texture = content.Load<Texture2D>("Idle");

            var animation = new Animatie(texture);

            animation.AnimatieNaam = HeroAnimations.idle;

            for (int i = 0; i < 8; i++)
            {
                animation.addFrame(new AnimatieFrame(new Rectangle(34 * i, 0, 34, 54)));
            }

            return animation;
        }

    }
}
