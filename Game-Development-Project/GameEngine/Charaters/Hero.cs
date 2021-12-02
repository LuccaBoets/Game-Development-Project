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

namespace GameEngine
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
    public class Hero : ICollisionable, IMoveable, IAnimationable
    {

        public Animatie currentAnimation { get; set; }
        public List<Animatie> Animaties { get; set; }
        public bool lookingRight { get; set; }

        public Vector2 position { get; set; }

        public Movement Movement { get; set; }

        public Hero(List<Animatie> animaties)
        {
            this.Animaties = animaties;

            this.position = new Vector2(700, 700);

            Movement = new Movement();

            this.lookingRight = true;

            this.currentAnimation = animaties.First(x => x.AnimatieNaam == HeroAnimations.idle);
        }

        public void update(GameTime gameTime, Tilemap tilemap)
        {
            List<Tuple<CollisionDirection, Rectangle>> directions = tilemap.hitAnyTile(GetCollsionRectangle());

            foreach (var direction in directions)
            {
                switch (direction.Item1)
                {
                    case CollisionDirection.north:
                        Debug.WriteLine(position);
                        position = new Vector2(position.X, direction.Item2.Y - GetCollsionRectangle().Height);
                        Movement.inAir = false;
                        Movement.velocity.Y = 0;
                        break;

                    case CollisionDirection.south:
                        //position += new Vector2(0, direction.Item2.Height);
                        position = new Vector2(position.X, direction.Item2.Y + direction.Item2.Height);

                        Movement.velocity.Y = 0;
                        break;

                    case CollisionDirection.west:
                        position = new Vector2(direction.Item2.X + direction.Item2.Width, position.Y);


                        Movement.velocity.X = 0;
                        break;

                    case CollisionDirection.east:
                        position = new Vector2(direction.Item2.X - GetCollsionRectangle().Width, position.Y);

                        //position += new Vector2(-direction.Item2.Width, 0);
                        Movement.velocity.X = 0;
                        break;

                    default:
                        break;
                }
            }

            if (tilemap.hitAnyTile(GetUnderCollisionRectangle()).Count <= 0)
            {
                Movement.inAir = true;
            }

            Movement.update(gameTime, this, this);

            currentAnimation.update(gameTime);
        }

        public Rectangle GetCollsionRectangle()
        {

            //var rectangle = currentAnimation.texture.Bounds;

            //rectangle.X += (int)position.X;
            //rectangle.Y += (int)position.Y;
            //rectangle.Width = (int)(currentAnimation.bounds.X * 2);
            //rectangle.Height = (int)(currentAnimation.bounds.Y * 2);

            //return rectangle;
            return new Rectangle((int)(position.X + Movement.velocity.X), (int)(position.Y + Movement.velocity.Y), 34 * 2, 54 * 2);
        }

        public Rectangle getTextureRectangle()
        {
            var rectangle = currentAnimation.texture.Bounds;

            rectangle.X += (int)position.X;
            rectangle.Y += (int)position.Y;
            rectangle.Width = (int)(currentAnimation.bounds.X * 2);
            rectangle.Height = (int)(currentAnimation.bounds.Y * 2);

            return rectangle;
        }

        public Rectangle GetUnderCollisionRectangle()
        {
            return new Rectangle((int)(position.X), (int)(position.Y + 54 * 2), 34 * 2, 1);
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

        public Tuple<CollisionDirection, Rectangle> CollisionDetection(Rectangle rectangle)
        {
            throw new NotImplementedException();
        }
    }

}
