﻿using GameEngine.Behavior;
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

        public Texture2D hartjeVol { get; set; }
        public Texture2D hartjeLeeg { get; set; }

        public bool isDead { get; set; }

        public Hero(List<Animatie> animaties)
        {
            this.Animaties = animaties;

            this.position = new Vector2(700, 300);

            Movement = new Movement();

            this.lookingLeft = true;

            this.currentAnimation = this.Animaties.FirstOrDefault(x => x.AnimatieNaam == AnimationsTypes.idle);

            stats = new Stats(10, 2); 
        }

        public void update(GameTime gameTime, Tilemap tilemap, List<Enemy> enemies)
        {
            move(gameTime, tilemap);

            currentAnimation.update(gameTime);

            endOfAnimation();

            if (stats.health <= 0)
            {
                changeAnimation(AnimationsTypes.death);
            }

            if (currentAnimation.AnimatieNaam == AnimationsTypes.attack1)
            {
                attack1(enemies);
            }


            if (invisible)
            {
                invisibleTimer += gameTime.ElapsedGameTime.TotalMilliseconds;
            }

            if (invisibleTimer >= 1000)
            {
                invisible = false;
                invisibleTimer = 0;
            }
        }

        public void move(GameTime gameTime, Tilemap tilemap)
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

            for (int i = 0; i < stats.maxHealth; i++)
            {
                Texture2D heart;
                if (stats.health > i)
                {
                    heart = hartjeVol;
                }
                else
                {
                    heart = hartjeLeeg;
                }

                _spriteBatch.Draw(heart, new Vector2(29 * (i + 1) + GetCollisionRectangle().Center.X - Settings.ScreenW / 2 - 10, GetCollisionRectangle().Center.Y - Settings.ScreenH / 2 + 20), hartjeVol.Bounds, Color.White, 0, Vector2.Zero, 2f, SpriteEffects.None, 1f);
            }
        }

        public Tuple<CollisionDirection, Rectangle> CollisionDetection(Rectangle rectangle)
        {
            throw new NotImplementedException();
        }

        public void attack1(List<Enemy> enemies)
        {
            if (currentAnimation.AnimatieNaam == AnimationsTypes.attack1 && currentAnimation.count == 2)
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
            if (!invisible)
            {
                Debug.WriteLine("hit");
                stats.health -= damage;
                invisible = true;
                changeAnimation(AnimationsTypes.hit);
                if (stats.health <= 0)
                {
                    changeAnimation(AnimationsTypes.death);
                }
            }
        }

        public void changeAnimation(AnimationsTypes animationsTypes, bool ignorePriority = false)
        {
            if (!(this.currentAnimation.AnimatieNaam == animationsTypes) && (this.currentAnimation.AnimatieNaam.isHigherPriority(animationsTypes) || ignorePriority))
            {
                this.currentAnimation = this.Animaties.FirstOrDefault(x => x.AnimatieNaam == animationsTypes);
                this.currentAnimation.reset();
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
                        isDead = true;
                        break;
                    default:
                        break;
                }
            }
        }
    }
}
