using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace GameEngine.Graphics
{
    public class GoblinAnimations
    {

        public static int _width { get; set; } = 160;
        public static int _height { get; set; } = 111;

        public static List<Animatie> AllAnimation(ContentManager content)
        {
            List<Animatie> GoblinAnimaties = new List<Animatie>() {
                   GoblinAnimations.GetIdleAnimatieFromGoblinMonster(content),
                   GoblinAnimations.GetRunAnimatieFromGoblinMonster(content),
                   GoblinAnimations.GetTakeHitAnimatieFromGoblinMonster(content),
                   GoblinAnimations.GetDeathAnimatieFromGoblinMonster(content),
                   GoblinAnimations.GetAttack1AnimatieFromGoblinMonster(content),
                   GoblinAnimations.GetAttack2AnimatieFromGoblinMonster(content),
                   GoblinAnimations.GetAttack3AnimatieFromGoblinMonster(content)
            };

            return GoblinAnimaties;
        }


        public static Animatie GetIdleAnimatieFromGoblinMonster(ContentManager content)
        {
            Texture2D texture = content.Load<Texture2D>("Monsters/Monsters_Creatures_Fantasy/Goblin/Idle");

            var animation = new Animatie(texture);

            animation.AnimatieNaam = AnimationsTypes.idle;

            for (int i = 0; i < 4; i++)
            {
                animation.addFrame(new AnimatieFrame(new Rectangle(_width * i, 0, _width, _height)));
            }

            return animation;
        }


        public static Animatie GetRunAnimatieFromGoblinMonster(ContentManager content)
        {
            Texture2D texture = content.Load<Texture2D>("Monsters/Monsters_Creatures_Fantasy/Goblin/Run");

            var animation = new Animatie(texture);

            animation.AnimatieNaam = AnimationsTypes.run;

            for (int i = 0; i < 8; i++)
            {
                animation.addFrame(new AnimatieFrame(new Rectangle(_width * i, 0, _width, _height)));
            }

            return animation;
        }
        public static Animatie GetTakeHitAnimatieFromGoblinMonster(ContentManager content)
        {
            Texture2D texture = content.Load<Texture2D>("Monsters/Monsters_Creatures_Fantasy/Goblin/Take Hit");

            var animation = new Animatie(texture);

            animation.AnimatieNaam = AnimationsTypes.hit;

            for (int i = 0; i < 4; i++)
            {
                animation.addFrame(new AnimatieFrame(new Rectangle(_width * i, 0, _width, _height)));
            }

            return animation;
        }
        public static Animatie GetDeathAnimatieFromGoblinMonster(ContentManager content)
        {
            Texture2D texture = content.Load<Texture2D>("Monsters/Monsters_Creatures_Fantasy/Goblin/Death");

            var animation = new Animatie(texture);

            animation.AnimatieNaam = AnimationsTypes.death;

            for (int i = 0; i < 4; i++)
            {
                animation.addFrame(new AnimatieFrame(new Rectangle(_width * i, 0, _width, _height)));
            }

            return animation;
        }
        public static Animatie GetAttack1AnimatieFromGoblinMonster(ContentManager content)
        {
            Texture2D texture = content.Load<Texture2D>("Monsters/Monsters_Creatures_Fantasy/Goblin/Attack");

            var animation = new Animatie(texture);

            animation.AnimatieNaam = AnimationsTypes.attack1;

            for (int i = 0; i < 8; i++)
            {
                animation.addFrame(new AnimatieFrame(new Rectangle(_width * i, 0, _width, _height)));
            }

            return animation;
        }


        public static Animatie GetAttack2AnimatieFromGoblinMonster(ContentManager content)
        {
            Texture2D texture = content.Load<Texture2D>("Monsters/Monster_Creatures_Fantasy(Version 1.2)/Goblin/Attack2");

            var animation = new Animatie(texture);

            animation.AnimatieNaam = AnimationsTypes.attack2;

            for (int i = 0; i < 8; i++)
            {
                animation.addFrame(new AnimatieFrame(new Rectangle(_width * i, 0, _width, _height)));
            }

            return animation;
        }
        public static Animatie GetAttack3AnimatieFromGoblinMonster(ContentManager content)
        {
            Texture2D texture = content.Load<Texture2D>("Monsters/Monster_Creatures_Fantasy(Version 1.3)/Goblin/Attack3");

            var animation = new Animatie(texture);

            animation.AnimatieNaam = AnimationsTypes.attack1;

            for (int i = 0; i < 12; i++)
            {
                animation.addFrame(new AnimatieFrame(new Rectangle(_width * i, 0, _width, _height)));
            }




            return animation;
        }

        //public static Animatie GetAttack3AnimatieFromGoblinMonster(ContentManager content)
        //{
        //    Texture2D texture = content.Load<Texture2D>("Monsters/Monsters_Creatures_Fantasy(Version 1.3)/Goblin/Attack");

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
