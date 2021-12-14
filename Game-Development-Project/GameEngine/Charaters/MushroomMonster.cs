using GameEngine.Behavior;
using GameEngine.Characters;
using GameEngine.Environment;
using GameEngine.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using GameEngine.Data;

namespace GameEngine.Charaters
{
    public class MushroomMonster : ICollisionable, IAnimationable
    {
        public Animatie currentAnimation { get; set; }
        public List<Animatie> Animaties { get; set; }
        public bool lookingRight { get; set; }

        public Vector2 position;

        public Vector2 velocity;

        public Vector2 origin;

        float distance;
        float oldstance;

        public Movement Movement { get; set; }



        public MushroomMonster(List<Animatie> animaties, Vector2 newPosition, float newDistance)
        {
            this.Animaties = animaties;

            this.position = newPosition;

            this.distance = newDistance;

            oldstance = distance;


            Movement = new Movement();

            this.lookingRight = true;

            this.currentAnimation = animaties.First(x => x.AnimatieNaam == AnimationsTypes.idle);
        }
        public Tuple<CollisionDirection, Rectangle> CollisionDetection(Rectangle rectangle)
        {
            throw new NotImplementedException();
        }

       

        public void update(GameTime gameTime, Hero hero)
        {

            Follow(hero);

            currentAnimation.update(gameTime);

            position += velocity;
            origin = new Vector2(1000, 300);

            if (distance <= 0)
            {
                lookingRight = false;
                velocity.X = 1f;
            }else if (distance <= oldstance)
            {
                lookingRight = true;
                velocity.X = 1f;
            }

            if (lookingRight) 
            {
                distance += 1;
            }
            else
            {
                distance -= 1;
            }
        }

        public void Follow(Hero hero)
        {
            if (hero.position.X < position.X + 100 && hero.position.X > position.X - 100)
            {
                if (lookingRight)
                {
                    distance -= 5;
                    currentAnimation = Animaties.First(x => x.AnimatieNaam == AnimationsTypes.run);
                    velocity.X = 5f;
                    position -= velocity;
                }
                else
                {
                    currentAnimation = Animaties.First(x => x.AnimatieNaam == AnimationsTypes.run);
                    distance -= 5;
                    velocity.X = 5f;
                   
                }

              


            }
        }

            public Rectangle GetCollisionRectangle()
        {
            throw new NotImplementedException();
        }

        public Rectangle GetNextCollisionRectangle()
        {
            throw new NotImplementedException();
        }


        public void draw(SpriteBatch _spriteBatch)
        {
            var spriteEffects = SpriteEffects.None;

            if (lookingRight)
            {
                spriteEffects = SpriteEffects.FlipHorizontally;
            }

            _spriteBatch.Draw(currentAnimation.texture, position + currentAnimation.offset, currentAnimation.currentFrame.borders, Color.White, 0, Vector2.Zero, 2f, spriteEffects, 0.5f);
        }
    }
}
