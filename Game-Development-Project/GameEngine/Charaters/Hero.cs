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
using GameEngine.ExtensionMethods;

namespace GameEngine
{
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

        public int health { get; set; }

        public int visualHealth { get; set; }

        public Texture2D hartjeVol { get; set; }
        public Texture2D hartjeLeeg { get; set; }

        public Hero(List<Animatie> animaties)
        {
            this.Animaties = animaties;

            this.position = new Vector2(700, 300);

            Movement = new Movement();

            this.lookingLeft = true;

            this.currentAnimation = this.Animaties.FirstOrDefault(x => x.AnimatieNaam == AnimationsTypes.idle);

            stats = new Stats(100, 20);
        }

        public void update(GameTime gameTime, Tilemap tilemap)
        {
            if (currentAnimation.AnimatieNaam.canMove())
            {

                this.Movement.update(gameTime, this, this);

                List<Tuple<CollisionDirection, Rectangle>> directions = tilemap.hitAnyTile(GetCollisionRectangle());
                foreach (var direction in directions)
                {
                    if (this.Movement.Velocity.X < 0 && direction.Item1 == CollisionDirection.left)
                    {
                        this.Movement.Velocity.X = 0;
                        position += new Vector2(direction.Item2.Width, 0);
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
            }

            else
            {
                this.Movement.Velocity = new Vector2(0, 0);
            }


            if (this.Movement.InAir == false)
            {
                if (tilemap.hitAnyTile(GetUnderCollisionRectangle()).Count <= 0)
                {
                    this.Movement.InAir = true;
                }
            }

            currentAnimation.update(gameTime);

            endOfAnimation();
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
            health = 7;
           for (int i = 0; i < health; i++)
            {
                _spriteBatch.Draw(hartjeVol, new Vector2(29 * (i + 1) + GetCollisionRectangle().Center.X - Settings.ScreenW / 2 - 10, GetCollisionRectangle().Center.Y - Settings.ScreenH / 2 + 20), hartjeVol.Bounds, Color.White, 0, Vector2.Zero, 2f, SpriteEffects.None, 1f);
            }

            if (health < 7)
            {
                for (int i = 7; i > health; i--)
                {
                    _spriteBatch.Draw(hartjeLeeg, new Vector2(10 * i, 500), hartjeLeeg.Bounds, Color.White, 0, Vector2.Zero, 2f, SpriteEffects.None, 1f);
                }
            }
            
        }

        public Tuple<CollisionDirection, Rectangle> CollisionDetection(Rectangle rectangle)
        {
            throw new NotImplementedException();
        }

        public void attack1(List<Enemy> enemies)
        {
            Debug.WriteLine(currentAnimation.count);

            if (currentAnimation.AnimatieNaam == AnimationsTypes.attack1)
            {
                Rectangle attackCollsionRectangle;
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


        public void ResetHealth(int health)
        {
            health = 7;
            visualHealth = health;
        }


        public void changeAnimation(AnimationsTypes animationsTypes, bool ignorePriority = false)
        {

            if (!(this.currentAnimation.AnimatieNaam == animationsTypes) && (this.currentAnimation.AnimatieNaam.isHigherPriority(animationsTypes) || ignorePriority))
            {
                this.currentAnimation = this.Animaties.FirstOrDefault(x => x.AnimatieNaam == animationsTypes);
                this.currentAnimation.count = 0;
            }


        }

        public void endOfAnimation()
        {
            if (this.currentAnimation.count >= this.currentAnimation.frames.Count - 1)
            {
                Debug.WriteLine("test");
                switch (this.currentAnimation.AnimatieNaam)
                {
                    case AnimationsTypes.attack1:
                    case AnimationsTypes.attack2:
                    case AnimationsTypes.attack3:
                        changeAnimation(AnimationsTypes.idle, true);
                        break;
                    case AnimationsTypes.hit:
                        changeAnimation(AnimationsTypes.idle, true);
                        break;
                    case AnimationsTypes.death:
                        break;
                    default:
                        break;
                }
            }
        }
    }
}
