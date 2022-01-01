using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;
namespace GameEngine.Graphics
{
    public class SkeletonAnimations
    {
        public static int _width { get; set; } = 150;
        public static int _height { get; set; } = 150;

        public static List<Animatie> AllAnimation(ContentManager content)
        {
            List<Animatie> skeletonAnimaties = new List<Animatie>() {
                   SkeletonAnimations.GetIdleAnimatieFromSkeletonMonster(content),
                   SkeletonAnimations.GetRunAnimatieFromSkeletonMonster(content),
                   SkeletonAnimations.GetTakeHitAnimatieFromSkeletonMonster(content),
                   SkeletonAnimations.GetDeathAnimatieFromSkeletonMonster(content),
                   SkeletonAnimations.GetAttack1AnimatieFromSkeletonMonster(content),
                   SkeletonAnimations.GetAttack2AnimatieFromSkeletonMonster(content),
                   SkeletonAnimations.GetAttack3AnimatieFromSkeletonMonster(content)
            };

            return skeletonAnimaties;
        }


        public static Animatie GetIdleAnimatieFromSkeletonMonster(ContentManager content)
        {
            Texture2D texture = content.Load<Texture2D>("Monsters/Skeleton/Idle");

            var animation = new Animatie(texture);

            animation.AnimatieNaam = AnimationsTypes.idle;

            for (int i = 0; i < 4; i++)
            {
                animation.addFrame(new AnimatieFrame(new Rectangle(_width * i, 0, _width, _height)));
            }

            return animation;
        }


        public static Animatie GetRunAnimatieFromSkeletonMonster(ContentManager content)
        {
            Texture2D texture = content.Load<Texture2D>("Monsters/Skeleton/Walk");

            var animation = new Animatie(texture);

            animation.AnimatieNaam = AnimationsTypes.run;

            for (int i = 0; i < 4; i++)
            {
                animation.addFrame(new AnimatieFrame(new Rectangle(_width * i, 0, _width, _height)));
            }

            return animation;
        }
        public static Animatie GetTakeHitAnimatieFromSkeletonMonster(ContentManager content)
        {
            Texture2D texture = content.Load<Texture2D>("Monsters/Skeleton/Take Hit");

            var animation = new Animatie(texture);

            animation.AnimatieNaam = AnimationsTypes.hit;

            for (int i = 0; i < 4; i++)
            {
                animation.addFrame(new AnimatieFrame(new Rectangle(_width * i, 0, _width, _height)));
            }

            return animation;
        }
        public static Animatie GetDeathAnimatieFromSkeletonMonster(ContentManager content)
        {
            Texture2D texture = content.Load<Texture2D>("Monsters/Skeleton/Death");

            var animation = new Animatie(texture);

            animation.AnimatieNaam = AnimationsTypes.death;

            for (int i = 0; i < 4; i++)
            {
                animation.addFrame(new AnimatieFrame(new Rectangle(_width * i, 0, _width, _height)));
            }

            return animation;
        }
        public static Animatie GetAttack1AnimatieFromSkeletonMonster(ContentManager content)
        {
            Texture2D texture = content.Load<Texture2D>("Monsters/Skeleton/Attack");

            var animation = new Animatie(texture);

            animation.AnimatieNaam = AnimationsTypes.attack1;

            for (int i = 0; i < 8; i++)
            {
                animation.addFrame(new AnimatieFrame(new Rectangle(_width * i, 0, _width, _height)));
            }

            return animation;
        }


        public static Animatie GetAttack2AnimatieFromSkeletonMonster(ContentManager content)
        {
            Texture2D texture = content.Load<Texture2D>("Monsters/Skeleton/Attack2");

            var animation = new Animatie(texture);

            animation.AnimatieNaam = AnimationsTypes.attack2;

            for (int i = 0; i < 8; i++)
            {
                animation.addFrame(new AnimatieFrame(new Rectangle(_width * i, 0, _width, _height)));
            }

            return animation;
        }
        public static Animatie GetAttack3AnimatieFromSkeletonMonster(ContentManager content)
        {
            Texture2D texture = content.Load<Texture2D>("Monsters/Skeleton/Attack3");

            var animation = new Animatie(texture);

            animation.AnimatieNaam = AnimationsTypes.attack3;

            for (int i = 0; i < 6; i++)
            {
                animation.addFrame(new AnimatieFrame(new Rectangle(_width * i, 0, _width, _height)));
            }

            return animation;
        }

        //public static Animatie GetAttack3AnimatieFromSkeletonMonster(ContentManager content)
        //{
        //    Texture2D texture = content.Load<Texture2D>("Monsters/Monsters_Creatures_Fantasy(Version 1.3)/Skeleton/Attack");

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
