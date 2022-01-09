using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;


namespace GameEngine.Graphics
{
    public class HeroAnimations
    {
        public static int _width { get; set; } = 160;
        public static int _height { get; set; } = 111;

        public static List<Animatie> AllAnimation(ContentManager content)
        {
            List<Animatie> heroAnimaties = new List<Animatie>() {
                HeroAnimations.GetIdleAnimatieFromHero(content),
                HeroAnimations.GetRunAnimatieFromHero(content),
                HeroAnimations.GetFallAnimatieFromHero(content),
                HeroAnimations.GetJumpAnimatieFromHero(content),
                HeroAnimations.GetAttack1FromHero(content),
                HeroAnimations.GetHitFromHero(content),
                HeroAnimations.GetDeathFromHero(content),
                HeroAnimations.GetAttack2AnimatieFromHero(content)
            };

            return heroAnimaties;
        }

        public static Animatie GetIdleAnimatieFromHero(ContentManager content)
        {
            Texture2D texture = content.Load<Texture2D>("Hero/Idle");

            var animation = new Animatie(texture);

            animation.AnimatieNaam = AnimationsTypes.idle;

            for (int i = 0; i < 8; i++)
            {
                animation.addFrame(new AnimatieFrame(new Rectangle(_width * i, 0, _width, _height)));
            }

            return animation;
        }

        public static Animatie GetRunAnimatieFromHero(ContentManager content)
        {
            Texture2D texture = content.Load<Texture2D>("Hero/Run");

            var animation = new Animatie(texture);

            animation.AnimatieNaam = AnimationsTypes.run;
            //animation.offset = new Vector2(0, 100);

            for (int i = 0; i < 8; i++)
            {
                animation.addFrame(new AnimatieFrame(new Rectangle(_width * i, 0, _width, _height)));
            }

            return animation;
        }
        public static Animatie GetFallAnimatieFromHero(ContentManager content)
        {
            Texture2D texture = content.Load<Texture2D>("Hero/Fall");

            var animation = new Animatie(texture);

            animation.AnimatieNaam = AnimationsTypes.fall;

            for (int i = 0; i < 2; i++)
            {
                animation.addFrame(new AnimatieFrame(new Rectangle(_width * i, 0, _width, _height)));
            }

            return animation;
        }

        public static Animatie GetJumpAnimatieFromHero(ContentManager content)
        {
            Texture2D texture = content.Load<Texture2D>("Hero/Jump");

            var animation = new Animatie(texture);

            animation.AnimatieNaam = AnimationsTypes.jump;

            for (int i = 0; i < 2; i++)
            {
                animation.addFrame(new AnimatieFrame(new Rectangle(_width * i, 0, _width, _height)));
            }

            return animation;
        }

        public static Animatie GetAttack2AnimatieFromHero(ContentManager content)
        {
            Texture2D texture = content.Load<Texture2D>("Hero/Attack2");

            var animation = new Animatie(texture);

            animation.AnimatieNaam = AnimationsTypes.attack2;
            //animation.offset = new Vector2(35*2, 0); // 35

            for (int i = 0; i < 4; i++)
            {
                animation.addFrame(new AnimatieFrame(new Rectangle(_width * i, 0, _width, _height)));
            }

            return animation;
        }

        public static Animatie GetAttack1FromHero(ContentManager content)
        {
            Texture2D texture = content.Load<Texture2D>("Hero/Attack1");

            var animation = new Animatie(texture);

            animation.AnimatieNaam = AnimationsTypes.attack1;

            for (int i = 0; i < 4; i++)
            {
                animation.addFrame(new AnimatieFrame(new Rectangle(_width * i, 0, _width, _height)));
            }

            return animation;
        }

        public static Animatie GetHitFromHero(ContentManager content)
        {
            Texture2D texture = content.Load<Texture2D>("Hero/Take Hit - white silhouette");

            var animation = new Animatie(texture);

            animation.AnimatieNaam = AnimationsTypes.hit;

            for (int i = 0; i < 4; i++)
            {
                animation.addFrame(new AnimatieFrame(new Rectangle(_width * i, 0, _width, _height)));
            }

            return animation;
        }

        public static Animatie GetDeathFromHero(ContentManager content)
        {
            Texture2D texture = content.Load<Texture2D>("Hero/Death");

            var animation = new Animatie(texture);

            animation.AnimatieNaam = AnimationsTypes.death;

            for (int i = 0; i < 6; i++)
            {
                animation.addFrame(new AnimatieFrame(new Rectangle(_width * i, 0, _width, _height)));
            }

            return animation;
        }
    }
}
