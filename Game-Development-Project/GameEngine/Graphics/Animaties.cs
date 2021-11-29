using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;


namespace GameEngine.Graphics
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

        public static Animatie GetRunAnimatieFromHero(ContentManager content)
        {
            Texture2D texture = content.Load<Texture2D>("Run");

            var animation = new Animatie(texture);

            animation.AnimatieNaam = HeroAnimations.run;
            animation.offset = new Vector2(1, 0);

            for (int i = 0; i < 8; i++)
            {
                animation.addFrame(new AnimatieFrame(new Rectangle(47 * i, 0, 47, 53)));
            }

            return animation;
        }
        public static Animatie GetFallAnimatieFromHero(ContentManager content)
        {
            Texture2D texture = content.Load<Texture2D>("Fall");

            var animation = new Animatie(texture);

            animation.AnimatieNaam = HeroAnimations.fall;

            for (int i = 0; i < 2; i++)
            {
                animation.addFrame(new AnimatieFrame(new Rectangle(44 * i, 0, 44, 51)));
            }

            return animation;
        }

        public static Animatie GetJumpAnimatieFromHero(ContentManager content)
        {
            Texture2D texture = content.Load<Texture2D>("Jump");

            var animation = new Animatie(texture);

            animation.AnimatieNaam = HeroAnimations.jump;

            for (int i = 0; i < 2; i++)
            {
                animation.addFrame(new AnimatieFrame(new Rectangle(41 * i, 0, 41, 54)));
            }

            return animation;
        }
    }
}
