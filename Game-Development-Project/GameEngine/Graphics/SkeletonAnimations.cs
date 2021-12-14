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
        public static int _width { get; set; } = 160;
        public static int _height { get; set; } = 111;

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
            Texture2D texture = content.Load<Texture2D>("Monsters/Monsters_Creatures_Fantasy/Skeleton/Idle");

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
            Texture2D texture = content.Load<Texture2D>("Monsters/Monsters_Creatures_Fantasy/Skeleton/Walk");

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
            Texture2D texture = content.Load<Texture2D>("Monsters/Monsters_Creatures_Fantasy/Skeleton/Take Hit");

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
            Texture2D texture = content.Load<Texture2D>("Monsters/Monsters_Creatures_Fantasy/Skeleton/Death");

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
            Texture2D texture = content.Load<Texture2D>("Monsters/Monsters_Creatures_Fantasy/Skeleton/Attack");

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
            Texture2D texture = content.Load<Texture2D>("Monsters/Monster_Creatures_Fantasy(Version 1.2)/Skeleton/Attack2");

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
            Texture2D texture = content.Load<Texture2D>("Monsters/Monster_Creatures_Fantasy(Version 1.3)/Skeleton/Attack3");

            var animation = new Animatie(texture);

            animation.AnimatieNaam = AnimationsTypes.attack1;

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
