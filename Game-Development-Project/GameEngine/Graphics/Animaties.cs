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
        private const int _heroWidth = 160;
        private const int _heroHeight = 111;

        public static Animatie GetIdleAnimatieFromHero(ContentManager content)
        {
            Texture2D texture = content.Load<Texture2D>("Idle");

            var animation = new Animatie(texture);

            animation.AnimatieNaam = HeroAnimations.idle;

            for (int i = 0; i < 8; i++)
            {
                animation.addFrame(new AnimatieFrame(new Rectangle(_heroWidth * i, 0, _heroWidth, _heroHeight)));
            }

            return animation;
        }

        public static Animatie GetRunAnimatieFromHero(ContentManager content)
        {
            Texture2D texture = content.Load<Texture2D>("Run");

            var animation = new Animatie(texture);

            animation.AnimatieNaam = HeroAnimations.run;
            //animation.offset = new Vector2(0, 100);

            for (int i = 0; i < 8; i++)
            {
                animation.addFrame(new AnimatieFrame(new Rectangle(_heroWidth * i, 0, _heroWidth, _heroHeight)));
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
                animation.addFrame(new AnimatieFrame(new Rectangle(_heroWidth * i, 0, _heroWidth, _heroHeight)));
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
                animation.addFrame(new AnimatieFrame(new Rectangle(_heroWidth * i, 0, _heroWidth, _heroHeight)));
            }

            return animation;
        }

        public static Animatie GetAttack2AnimatieFromHero(ContentManager content)
        {
            Texture2D texture = content.Load<Texture2D>("Attack2");

            var animation = new Animatie(texture);

            animation.AnimatieNaam = HeroAnimations.attack2;
            //animation.offset = new Vector2(35*2, 0); // 35

            for (int i = 0; i < 4; i++)
            {
                animation.addFrame(new AnimatieFrame(new Rectangle(_heroWidth * i, 0, _heroWidth, _heroHeight)));
            }

            return animation;
        }
    }
}
