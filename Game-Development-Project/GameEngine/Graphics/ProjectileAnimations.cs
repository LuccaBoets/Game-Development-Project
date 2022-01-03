using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;
namespace GameEngine.Graphics
{
    public class ProjectileAnimations
    {
        public static List<Animatie> AllMushroomAnimation(ContentManager content)
        {
            List<Animatie> projectileAnimaties = new List<Animatie>() {
                   ProjectileAnimations.GetProjectileInAirAnimatieFromMushroomMonster(content),
                   ProjectileAnimations.GetProjectileHitAnimatieFromMushroomMonster(content)
            };

            return projectileAnimaties;
        }

        public static List<Animatie> AllSkeletonAnimation(ContentManager content)
        {
            List<Animatie> projectileAnimaties = new List<Animatie>() {
                   ProjectileAnimations.GetProjectileInAirAnimatieFromSkeletonMonster(content),
                   ProjectileAnimations.GetProjectileHitAnimatieFromSkeletonMonster(content)
            };

            return projectileAnimaties;
        }

        public static List<Animatie> AllGoblinAnimation(ContentManager content)
        {
            List<Animatie> projectileAnimaties = new List<Animatie>() {
                   ProjectileAnimations.GetProjectileInAirAnimatieFromGoblinMonster(content),
                   ProjectileAnimations.GetProjectileHitAnimatieFromGoblinMonster(content)
            };

            return projectileAnimaties;
        }

        public static List<Animatie> AllFlyingEyeAnimation(ContentManager content)
        {
            List<Animatie> projectileAnimaties = new List<Animatie>() {
                   ProjectileAnimations.GetProjectileInAirAnimatieFromFlyingEyeMonster(content),
                   ProjectileAnimations.GetProjectileHitAnimatieFromFlyingEyeMonster(content)
            };

            return projectileAnimaties;
        }

        public static Animatie GetProjectileInAirAnimatieFromMushroomMonster(ContentManager content)
        {
            int _width = 50;
            int _height = 40;

            Texture2D texture = content.Load<Texture2D>("Monsters/Mushroom/ProjectileInAir");

            var animation = new Animatie(texture);

            animation.AnimatieNaam = AnimationsTypes.idle;

            for (int i = 0; i < 4; i++)
            {
                animation.addFrame(new AnimatieFrame(new Rectangle(_width * i, 0, _width, _height)));
            }

            return animation;
        }

        public static Animatie GetProjectileHitAnimatieFromMushroomMonster(ContentManager content)
        {
            int _width = 50;
            int _height = 40;

            Texture2D texture = content.Load<Texture2D>("Monsters/Mushroom/ProjectileHit");

            var animation = new Animatie(texture);

            animation.AnimatieNaam = AnimationsTypes.attack1;

            for (int i = 0; i < 4; i++)
            {
                animation.addFrame(new AnimatieFrame(new Rectangle(_width * i, 0, _width, _height)));
            }

            return animation;
        }

        public static Animatie GetProjectileInAirAnimatieFromSkeletonMonster(ContentManager content)
        {
            int _width = 92;
            int _height = 102;

            Texture2D texture = content.Load<Texture2D>("Monsters/Skeleton/ProjectileInAir");

            var animation = new Animatie(texture);

            animation.AnimatieNaam = AnimationsTypes.idle;

            for (int i = 0; i < 3; i++)
            {
                animation.addFrame(new AnimatieFrame(new Rectangle(_width * i, 0, _width, _height)));
            }

            return animation;
        }

        public static Animatie GetProjectileHitAnimatieFromSkeletonMonster(ContentManager content)
        {
            int _width = 92;
            int _height = 102;

            Texture2D texture = content.Load<Texture2D>("Monsters/Skeleton/ProjectileHit");

            var animation = new Animatie(texture);

            animation.AnimatieNaam = AnimationsTypes.attack1;

            for (int i = 0; i < 5; i++)
            {
                animation.addFrame(new AnimatieFrame(new Rectangle(_width * i, 0, _width, _height)));
            }

            return animation;
        }

        public static Animatie GetProjectileInAirAnimatieFromGoblinMonster(ContentManager content)
        {
            int _width = 100;
            int _height = 100;

            Texture2D texture = content.Load<Texture2D>("Monsters/Goblin/ProjectileInAir");

            var animation = new Animatie(texture);

            animation.AnimatieNaam = AnimationsTypes.idle;

            for (int i = 0; i < 9; i++)
            {
                animation.addFrame(new AnimatieFrame(new Rectangle(_width * i, 0, _width, _height)));
            }

            return animation;
        }

        public static Animatie GetProjectileHitAnimatieFromGoblinMonster(ContentManager content)
        {
            int _width = 100;
            int _height = 100;

            Texture2D texture = content.Load<Texture2D>("Monsters/Goblin/ProjectileHit");

            var animation = new Animatie(texture);

            animation.AnimatieNaam = AnimationsTypes.attack1;

            for (int i = 0; i < 10; i++)
            {
                animation.addFrame(new AnimatieFrame(new Rectangle(_width * i, 0, _width, _height)));
            }

            return animation;
        }

        public static Animatie GetProjectileInAirAnimatieFromFlyingEyeMonster(ContentManager content)
        {
            int _width = 48;
            int _height = 48;

            Texture2D texture = content.Load<Texture2D>("Monsters/Flying eye/ProjectileInAir");

            var animation = new Animatie(texture);

            animation.AnimatieNaam = AnimationsTypes.idle;

            for (int i = 0; i < 3; i++)
            {
                animation.addFrame(new AnimatieFrame(new Rectangle(_width * i, 0, _width, _height)));
            }

            return animation;
        }

        public static Animatie GetProjectileHitAnimatieFromFlyingEyeMonster(ContentManager content)
        {
            int _width = 48;
            int _height = 48;

            Texture2D texture = content.Load<Texture2D>("Monsters/Flying eye/ProjectileHit");

            var animation = new Animatie(texture);

            animation.AnimatieNaam = AnimationsTypes.attack1;

            for (int i = 0; i < 5; i++)
            {
                animation.addFrame(new AnimatieFrame(new Rectangle(_width * i, 0, _width, _height)));
            }

            return animation;
        }
    }
}
