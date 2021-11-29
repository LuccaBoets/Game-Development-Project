using GameDevelopmentProject.Behavior;
using GameDevelopmentProject.Environment;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GameDevelopmentProject
{

    public enum HeroAnimations
    {
        idle,
        run,
        jump,
        fall,
        attack1,
        attack2,
        attack3,
        death
    }
    public class Hero : ICollisionable
    {

        public Movement move;

        // dict key: enum (idle,attack,...) , value: Animatie
        public Animatie currentAnimation { get; set; }
        public List<Animatie> Animaties { get; set; }

        public Vector2 position { get; set; }

        public bool lookingRight { get; set; }

        public Hero(List<Animatie> animaties)
        {
            this.Animaties = animaties;

            this.position = new Vector2(0, 700);


            move = new Movement();
            move.jumped = true;

            this.lookingRight = true;

            this.currentAnimation = animaties.First(x => x.AnimatieNaam == HeroAnimations.idle);
        }

        public void update(GameTime gameTime, Tilemap tilemap)
        {
            currentAnimation.update(gameTime);
            position += move.velocity;
            var rectangle = currentAnimation.texture.Bounds;

            rectangle.X += (int)position.X;
            rectangle.Y += (int)position.Y;
            rectangle.Width = (int)(currentAnimation.bounds.X * 2);
            rectangle.Height = (int)(currentAnimation.bounds.Y * 2);

            foreach (var direction in tilemap.hitAnyTile(rectangle))
            {
                switch (direction)
                {
                    case CollisionDirection.north:

                        //position += new Vector2(0, -3);
                        move.velocity.Y = 0;
                        break;
                    case CollisionDirection.south:
                        //position += new Vector2(0, 3f);
                        move.velocity.Y = 0;
                        break;
                    case CollisionDirection.west:
                        //position += new Vector2(3, 0);
                        move.velocity.X = 0;

                        break;
                    case CollisionDirection.east:
                        //position += new Vector2(-3, 0);
                        move.velocity.X = 0;

                        break;
                    case CollisionDirection.noHit:
                        break;
                    default:
                        break;
                }
            }
        }

        public Rectangle getCollsionRectangle()
        {
            throw new NotImplementedException();
        }

        public CollisionDirection CollisionDetection(Rectangle rectangle)
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

            _spriteBatch.Draw(currentAnimation.texture, position + currentAnimation.offset, currentAnimation.currentFrame.borders, Color.White, 0, Vector2.Zero, 2f, spriteEffects, 0f);
        }

    }

}
