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
using GameEngine.Charaters;

namespace GameEngine
{

    public enum AnimationsTypes
    {
        idle,
        run,
        jump,
        fall,
        attack1,
        attack2,
        attack3,
        death,
        hit
    }
    public class Hero : ICollisionable, IMoveable, IAnimationable, IHitable
    {

        public Animatie currentAnimation { get; set; }
        public List<Animatie> Animaties { get; set; }
        public bool lookingLeft { get; set; }

        public Vector2 position { get; set; }

        public Movement Movement { get; set; }
        public Stats stats { get; set; }
        public double invisibleTimer { get; set; }
        public bool invisible { get; set; }

        public Hero(List<Animatie> animaties)
        {
            this.Animaties = animaties;

            this.position = new Vector2(700, 300);

            Movement = new Movement();

            this.lookingLeft = true;

            changeAnimation(AnimationsTypes.idle);

            stats = new Stats(100, 20);
        }


        public void update(GameTime gameTime, Tilemap tilemap)
        {
            this.Movement.update(gameTime, this, this);

            List<Tuple<CollisionDirection, Rectangle>> directions = tilemap.hitAnyTile(GetCollisionRectangle());
            foreach (var direction in directions)
            {
                if (this.Movement.Velocity.X < 0 && direction.Item1 == CollisionDirection.left)
                {
                    this.Movement.Velocity.X = 0;
                    position += new Vector2(direction.Item2.Width, 0);

                    //position += new Vector2(direction.Item2.Left - GetCollisionRectangle().Width, 0);
                }

                if (this.Movement.Velocity.X > 0 & direction.Item1 == CollisionDirection.right)
                {
                    this.Movement.Velocity.X = 0;
                    position += new Vector2(-direction.Item2.Width, 0);

                }

                if (this.Movement.Velocity.Y < 0 && direction.Item1 == CollisionDirection.up)
                {
                    this.Movement.Velocity.Y = 0;
                    position += new Vector2(0, direction.Item2.Height);
                }

                if ((this.Movement.Velocity.Y > 0 & direction.Item1 == CollisionDirection.down))
                {
                    position += new Vector2(0, -direction.Item2.Height);

                    this.Movement.Velocity.Y = 0;
                    this.Movement.InAir = false;
                }
            }

            //Position += Velocity;
            //Debug.WriteLine(direction.Item1);

            //switch (direction.Item1)
            //{
            //    case CollisionDirection.up:
            //        Debug.WriteLine(position);
            //        position = new Vector2(position.X, direction.Item2.Y - GetTextureRectangle().Height + 12);
            //        Movement.inAir = false;
            //        Movement.velocity.Y = 0;
            //        break;

            //    case CollisionDirection.down:
            //        //position += new Vector2(0, direction.Item2.Height);
            //        position = new Vector2(position.X, direction.Item2.Y - direction.Item2.Height - 12);

            //        Movement.velocity.Y = 0;
            //        break;

            //    case CollisionDirection.left:
            //        position = new Vector2(direction.Item2.X + direction.Item2.Width - 63*2, position.Y);


            //        Movement.velocity.X = 0;
            //        break;

            //    case CollisionDirection.right:
            //        position = new Vector2(direction.Item2.X - GetCollisionRectangle().Width - 63*2, position.Y);

            //        //position += new Vector2(-direction.Item2.Width, 0);
            //        Movement.velocity.X = 0;
            //        break;
            //    default:
            //        break;
            //}
            //}

            if (this.Movement.InAir == false)
            {
                if (tilemap.hitAnyTile(GetUnderCollisionRectangle()).Count <= 0)
                {
                    this.Movement.InAir = true;
                }
            }

            currentAnimation.update(gameTime);
        }

        public Rectangle GetNextCollisionRectangle()
        {
            var collisionRectangle = this.GetCollisionRectangle();

            return new Rectangle((int)(collisionRectangle.X + Movement.Velocity.X), (int)(collisionRectangle.Y + Movement.Velocity.Y), 34 * 2, 54 * 2);
            //return new Rectangle((int)(position.X + Movement.velocity.X), (int)(position.Y + Movement.velocity.Y), 34 * 2, 54 * 2);
        }

        public Rectangle GetCollisionRectangle()
        {
            var beginPoint = GetTextureRectangle().Center;
            beginPoint.X -= 16 * 2;
            beginPoint.Y -= 4 * 2 + 1;


            return new Rectangle(beginPoint, new Point(34 * 2, 54 * 2));
        }

        public Rectangle GetTextureRectangle()
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
            var rectangle = GetCollisionRectangle();
            rectangle.Height += 10;
            return rectangle;
        }


        public void draw(SpriteBatch _spriteBatch)
        {
            var spriteEffects = SpriteEffects.None;

            if (lookingLeft)
            {
                spriteEffects = SpriteEffects.FlipHorizontally;
            }

            _spriteBatch.Draw(currentAnimation.texture, position + currentAnimation.offset, currentAnimation.currentFrame.borders, Color.White, 0, Vector2.Zero, 2f, spriteEffects, 0.5f);
        }

        public Tuple<CollisionDirection, Rectangle> CollisionDetection(Rectangle rectangle)
        {
            throw new NotImplementedException();
        }

        public void attack1(List<Enemy> enemies)
        {
            if (currentAnimation.count == 3 && currentAnimation.AnimatieNaam == AnimationsTypes.attack1)
            {

                Rectangle attackCollsionRectangle = new Rectangle();
                if (lookingLeft)
                {
                    attackCollsionRectangle = new Rectangle(GetCollisionRectangle().Left - 36 * 3, GetCollisionRectangle().Top + 10, 54 * 2, 36 * 2);
                }
                else
                {
                    attackCollsionRectangle = new Rectangle(GetCollisionRectangle().Right, GetCollisionRectangle().Top + 10, 54 * 2, 36 * 2);
                }

                foreach (var enemy in enemies)
                {
                    if (CollisionManager.Detection(enemy.GetCollisionRectangle(), attackCollsionRectangle))
                    {
                        enemy.Hit(stats.damage);
                    }
                }
            }

        }

        public void Hit(int damage)
        {
            throw new NotImplementedException();
        }

        public void changeAnimation(AnimationsTypes animationsTypes)
        {
            this.currentAnimation = this.Animaties.FirstOrDefault(x => x.AnimatieNaam == animationsTypes);
        }
    }
}
