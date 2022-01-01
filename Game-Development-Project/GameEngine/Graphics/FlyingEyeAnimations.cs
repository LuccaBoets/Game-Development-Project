using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;
namespace GameEngine.Graphics
{
    public class FlyingEyeAnimations
    {
        public static int _width { get; set; } = 160;
        public static int _height { get; set; } = 111;

        public static List<Animatie> AllAnimation(ContentManager content)
        {
            List<Animatie> FlyingEyeAnimaties = new List<Animatie>() {
                   FlyingEyeAnimations.GetIdleAnimatieFromFlyingEyeMonster(content),
                   FlyingEyeAnimations.GetRunAnimatieFromFlyingEyeMonster(content),
                   FlyingEyeAnimations.GetTakeHitAnimatieFromFlyingEyeMonster(content),
                   FlyingEyeAnimations.GetDeathAnimatieFromFlyingEyeMonster(content),
                   FlyingEyeAnimations.GetAttack1AnimatieFromFlyingEyeMonster(content),
                   FlyingEyeAnimations.GetAttack2AnimatieFromFlyingEyeMonster(content),
                   FlyingEyeAnimations.GetAttack3AnimatieFromFlyingEyeMonster(content)
            };

            return FlyingEyeAnimaties;
        }


        public static Animatie GetIdleAnimatieFromFlyingEyeMonster(ContentManager content)
        {
            Texture2D texture = content.Load<Texture2D>("Monsters/FlyingEye/Flight");

            var animation = new Animatie(texture);

            animation.AnimatieNaam = AnimationsTypes.idle;

            for (int i = 0; i < 8; i++)
            {
                animation.addFrame(new AnimatieFrame(new Rectangle(_width * i, 0, _width, _height)));
            }

            return animation;
        }


        public static Animatie GetRunAnimatieFromFlyingEyeMonster(ContentManager content)
        {
            Texture2D texture = content.Load<Texture2D>("Monsters/FlyingEye/Flight");

            var animation = new Animatie(texture);

            animation.AnimatieNaam = AnimationsTypes.run;

            for (int i = 0; i < 8; i++)
            {
                animation.addFrame(new AnimatieFrame(new Rectangle(_width * i, 0, _width, _height)));
            }

            return animation;
        }
        public static Animatie GetTakeHitAnimatieFromFlyingEyeMonster(ContentManager content)
        {
            Texture2D texture = content.Load<Texture2D>("Monsters/FlyingEye/Take Hit");

            var animation = new Animatie(texture);

            animation.AnimatieNaam = AnimationsTypes.hit;

            for (int i = 0; i < 4; i++)
            {
                animation.addFrame(new AnimatieFrame(new Rectangle(_width * i, 0, _width, _height)));
            }

            return animation;
        }
        public static Animatie GetDeathAnimatieFromFlyingEyeMonster(ContentManager content)
        {
            Texture2D texture = content.Load<Texture2D>("Monsters/FlyingEye/Death");

            var animation = new Animatie(texture);

            animation.AnimatieNaam = AnimationsTypes.death;

            for (int i = 0; i < 4; i++)
            {
                animation.addFrame(new AnimatieFrame(new Rectangle(_width * i, 0, _width, _height)));
            }

            return animation;
        }
        public static Animatie GetAttack1AnimatieFromFlyingEyeMonster(ContentManager content)
        {
            Texture2D texture = content.Load<Texture2D>("Monsters/FlyingEye/Attack");

            var animation = new Animatie(texture);

            animation.AnimatieNaam = AnimationsTypes.attack1;

            for (int i = 0; i < 8; i++)
            {
                animation.addFrame(new AnimatieFrame(new Rectangle(_width * i, 0, _width, _height)));
            }

            return animation;
        }


        public static Animatie GetAttack2AnimatieFromFlyingEyeMonster(ContentManager content)
        {
            Texture2D texture = content.Load<Texture2D>("Monsters/FlyingEye/Attack2");

            var animation = new Animatie(texture);

            animation.AnimatieNaam = AnimationsTypes.attack2;

            for (int i = 0; i < 8; i++)
            {
                animation.addFrame(new AnimatieFrame(new Rectangle(_width * i, 0, _width, _height)));
            }

            return animation;
        }
        public static Animatie GetAttack3AnimatieFromFlyingEyeMonster(ContentManager content)
        {
            Texture2D texture = content.Load<Texture2D>("Monsters/FlyingEye/Attack3");

            var animation = new Animatie(texture);

            animation.AnimatieNaam = AnimationsTypes.attack1;

            for (int i = 0; i < 6; i++)
            {
                animation.addFrame(new AnimatieFrame(new Rectangle(_width * i, 0, _width, _height)));
            }

            return animation;
        }

        //public static Animatie GetAttack3AnimatieFromFlyingEyeMonster(ContentManager content)
        //{
        //    Texture2D texture = content.Load<Texture2D>("Monsters/Monsters_Creatures_Fantasy(Version 1.3)/FlyingEye/Attack");

        //    var animation = new Animatie(texture);

        //    animation.AnimatieNaam = AnimationsTypes.attack1;

        //    for (int i = 0; i < 11; i++)
        //    {
        //        animation.addFrame(new AnimatieFrame(new Rectangle(_width * i, 0, _width, _height)));
        //    }




        //    return animation;
        //}

    }
}
