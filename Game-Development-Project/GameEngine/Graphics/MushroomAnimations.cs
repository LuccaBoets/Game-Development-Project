using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;
namespace GameEngine.Graphics
{
    public class MushroomAnimations
    {

        public static int _width { get; set; } = 160;
        public static int _height { get; set; } = 111;

        public static List<Animatie> AllAnimation(ContentManager content)
        {
            List<Animatie> mushroomAnimaties = new List<Animatie>() {
                   MushroomAnimations.GetIdleAnimatieFromMushroomMonster(content),
                   MushroomAnimations.GetRunAnimatieFromMushroomMonster(content),
                   MushroomAnimations.GetTakeHitAnimatieFromMushroomMonster(content),
                   MushroomAnimations.GetDeathAnimatieFromMushroomMonster(content),
                   MushroomAnimations.GetAttack1AnimatieFromMushroomMonster(content),
                   MushroomAnimations.GetAttack2AnimatieFromMushroomMonster(content),
                   MushroomAnimations.GetAttack3AnimatieFromMushroomMonster(content)
            };

            return mushroomAnimaties;
        }


        public static Animatie GetIdleAnimatieFromMushroomMonster(ContentManager content)
        {
            Texture2D texture = content.Load<Texture2D>("Monsters/Monsters_Creatures_Fantasy/Mushroom/Idle");

            var animation = new Animatie(texture);

            animation.AnimatieNaam = AnimationsTypes.idle;

            for (int i = 0; i < 4; i++)
            {
                animation.addFrame(new AnimatieFrame(new Rectangle(_width * i, 0, _width, _height)));
            }

            return animation;
        }


        public static Animatie GetRunAnimatieFromMushroomMonster(ContentManager content)
        {
            Texture2D texture = content.Load<Texture2D>("Monsters/Monsters_Creatures_Fantasy/Mushroom/Run");

            var animation = new Animatie(texture);

            animation.AnimatieNaam = AnimationsTypes.run;

            for (int i = 0; i < 8; i++)
            {
                animation.addFrame(new AnimatieFrame(new Rectangle(_width * i, 0, _width, _height)));
            }

            return animation;
        }
        public static Animatie GetTakeHitAnimatieFromMushroomMonster(ContentManager content)
        {
            Texture2D texture = content.Load<Texture2D>("Monsters/Monsters_Creatures_Fantasy/Mushroom/Take Hit");

            var animation = new Animatie(texture);

            animation.AnimatieNaam = AnimationsTypes.hit;

            for (int i = 0; i < 4; i++)
            {
                animation.addFrame(new AnimatieFrame(new Rectangle(_width * i, 0, _width, _height)));
            }

            return animation;
        }
        public static Animatie GetDeathAnimatieFromMushroomMonster(ContentManager content)
        {
            Texture2D texture = content.Load<Texture2D>("Monsters/Monsters_Creatures_Fantasy/Mushroom/Death");

            var animation = new Animatie(texture);

            animation.AnimatieNaam = AnimationsTypes.death;

            for (int i = 0; i < 4; i++)
            {
                animation.addFrame(new AnimatieFrame(new Rectangle(_width * i, 0, _width, _height)));
            }

            return animation;
        }
        public static Animatie GetAttack1AnimatieFromMushroomMonster(ContentManager content)
        {
            Texture2D texture = content.Load<Texture2D>("Monsters/Monsters_Creatures_Fantasy/Mushroom/Attack");

            var animation = new Animatie(texture);

            animation.AnimatieNaam = AnimationsTypes.attack1;

            for (int i = 0; i < 8; i++)
            {
                animation.addFrame(new AnimatieFrame(new Rectangle(_width * i, 0, _width, _height)));
            }

            return animation;
        }


        public static Animatie GetAttack2AnimatieFromMushroomMonster(ContentManager content)
        {
            Texture2D texture = content.Load<Texture2D>("Monsters/Monster_Creatures_Fantasy(Version 1.2)/Mushroom/Attack2");

            var animation = new Animatie(texture);

            animation.AnimatieNaam = AnimationsTypes.attack2;

            for (int i = 0; i < 8; i++)
            {
                animation.addFrame(new AnimatieFrame(new Rectangle(_width * i, 0, _width, _height)));
            }

            return animation;
        }
        public static Animatie GetAttack3AnimatieFromMushroomMonster(ContentManager content)
        {
            Texture2D texture = content.Load<Texture2D>("Monsters/Monster_Creatures_Fantasy(Version 1.3)/Mushroom/Attack3");

            var animation = new Animatie(texture);

            animation.AnimatieNaam = AnimationsTypes.attack1;

            for (int i = 0; i < 11; i++)
            {
                animation.addFrame(new AnimatieFrame(new Rectangle(_width * i, 0, _width, _height)));
            }

           


            return animation;
        }

        //public static Animatie GetAttack3AnimatieFromMushroomMonster(ContentManager content)
        //{
        //    Texture2D texture = content.Load<Texture2D>("Monsters/Monsters_Creatures_Fantasy(Version 1.3)/Mushroom/Attack");

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
