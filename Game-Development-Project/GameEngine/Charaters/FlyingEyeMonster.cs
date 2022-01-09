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
using GameEngine.ExtensionMethods;

namespace GameEngine.Charaters
{
    public class FlyingEyeMonster : Enemy
    {
        public FlyingEyeMonster(List<Animatie> animaties, List<Animatie> projectileAnimation, Vector2 newPosition)
        {
            this.Animaties = animaties;
            this.projectileHitAnimation = projectileAnimation[1];
            this.projectileInAirAnimation = projectileAnimation[0];
            this.position = newPosition;

            this.Movement = new FlyingMovement();
            this.Movement.MaxSpeedX = 1.5f;
            this.Movement.jumpSpeed = 4f;

            this.lookingLeft = true;

            this.currentAnimation = animaties.First(x => x.AnimatieNaam == AnimationsTypes.idle);

            this.stats = new Stats(7, 1);
            this.projectiles = new List<Projectile>();
        }

        public override Tuple<CollisionDirection, Rectangle> CollisionDetection(Rectangle rectangle)
        {
            throw new NotImplementedException();
        }

        public override void move(GameTime gameTime, Tilemap tilemap)
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
                this.Movement.Velocity.X = 0;
            }
        }

        public override void Follow(GameTime gameTime, Hero hero, Tilemap tilemap)
        {
            if (CollisionManager.Detection(GetMonsterRangeRectangle(), hero.GetCollisionRectangle()))
            {
                if (!CollisionManager.Detection(hero.GetCollisionRectangle(), GetCollisionRectangle().Center, 30, 50))
                {
                    Movement.moveTo(position, hero.position);

                    if (Movement.Velocity.X > 0)
                    {
                        lookingLeft = false;
                    }
                    else if (Movement.Velocity.X < 0)
                    {
                        lookingLeft = true;
                    }
                }

                if (!attackCooldown)
                {
                    if (CollisionManager.Detection(hero.GetCollisionRectangle(), GetCollisionRectangle().Center, 100, 50) && currentAnimation.AnimatieNaam.canMove())
                    {
                        Random rand = new Random();
                        if (rand.Next(0, 2) == 1)
                        {
                            attack1(hero);
                        }
                        else
                        {
                            attack2(hero);
                        }
                    }
                    else if (!CollisionManager.Detection(hero.GetCollisionRectangle(), GetCollisionRectangle().Center, 700, 50) && CollisionManager.Detection(hero.GetCollisionRectangle(), GetCollisionRectangle().Center, 1000, 50) && currentAnimation.AnimatieNaam.canMove())
                    {
                        attack3(hero);
                    }
                }
            }
            else
            {
                randomMovement(gameTime);
            }

            foreach (var tile in tilemap.MiddleGround.Tiles)
            {

                if (CollisionManager.Detection(new Rectangle((int)GetCollisionRectangle().Left - 45, (int)GetCollisionRectangle().Bottom - 30, 45, 16), tile.GetCollisionRectangle()) && Movement.Velocity.X < 0)
                {
                    Movement.jump();
                }
                else if (CollisionManager.Detection(new Rectangle((int)GetCollisionRectangle().Right, (int)GetCollisionRectangle().Bottom - 30, 45, 16), tile.GetCollisionRectangle()) && Movement.Velocity.X > 0)
                {
                    Movement.jump();
                }
            }

        }

        public override Rectangle GetMonsterRangeRectangle()
        {
            var center = GetCollisionRectangle().Center.ToVector2();
            var rectangle = new Rectangle(0, 0, 900, 900);
            rectangle.X = (int)(-rectangle.Width / 2 + center.X);
            rectangle.Y = (int)(-rectangle.Height / 2 + center.Y);
            return rectangle;
        }

        public override Rectangle GetCollisionRectangle()
        {
            var beginPoint = position.ToPoint();
            if (lookingLeft)
            {
                beginPoint.X += 52 * 2;
                beginPoint.Y += 60 * 2 - 1;
            }
            else
            {
                beginPoint.X += 57 * 2;
                beginPoint.Y += 60 * 2 - 1;
            }

            return new Rectangle(beginPoint, new Point(41 * 2, 32 * 2));
            //return new Rectangle((int)position.X, (int)position.Y, (int)currentAnimation.bounds.X, (int)currentAnimation.bounds.Y);
        }

        public override Rectangle GetNextCollisionRectangle()
        {
            throw new NotImplementedException();
        }

        public override void Draw(SpriteBatch _spriteBatch)
        {
            var spriteEffects = SpriteEffects.None;

            if (lookingLeft)
            {
                spriteEffects = SpriteEffects.FlipHorizontally;

            }

            _spriteBatch.Draw(currentAnimation.texture, position + currentAnimation.offset, currentAnimation.currentFrame.borders, Color.White, 0, Vector2.Zero, 2f, spriteEffects, 0.5f);

            foreach (var projectile in projectiles)
            {
                projectile.Draw(_spriteBatch);
            }
        }

        public override void attack1(Hero hero)
        {

            changeAnimation(AnimationsTypes.attack1);

            const int Width = 30 * 2;
            const int Height = 32 * 2;
            const int yOffset = 0;

            if (currentAnimation.AnimatieNaam == AnimationsTypes.attack1 && currentAnimation.count == 6)
            {
                Random random = new Random();

                attackCooldownTimer = random.Next(0, 2) * 500;
                Rectangle attackCollsionRectangle;
                if (lookingLeft)
                {
                    attackCollsionRectangle = new Rectangle(GetCollisionRectangle().Left - 20, GetCollisionRectangle().Top + yOffset, Width, Height);
                }
                else
                {
                    attackCollsionRectangle = new Rectangle(GetCollisionRectangle().Right - 20, GetCollisionRectangle().Top + yOffset, Width, Height);
                }

                if (CollisionManager.Detection(hero.GetCollisionRectangle(), attackCollsionRectangle))
                {
                    hero.Hit(stats.damage);
                }
            }
        }

        public override void attack2(Hero hero)
        {
            changeAnimation(AnimationsTypes.attack2);


            if (currentAnimation.AnimatieNaam == AnimationsTypes.attack2 && currentAnimation.count == 6)
            {
                Random random = new Random();

                attackCooldownTimer = random.Next(0, 3) * 500;

                var attackCollsionRectangle = GetCollisionRectangle();
                attackCollsionRectangle.X -= 10;
                attackCollsionRectangle.Y -= 10;
                attackCollsionRectangle.Width += 20;
                attackCollsionRectangle.Height += 20;

                if (CollisionManager.Detection(hero.GetCollisionRectangle(), attackCollsionRectangle))
                {
                    hero.Hit(stats.damage + 1);
                }
            }
        }

        public override void attack3(Hero hero)
        {
            Random random = new Random();
            attackCooldownTimer = 2000 + (random.Next(0, 10) * 500);

            changeAnimation(AnimationsTypes.attack3);
            if (currentAnimation.AnimatieNaam == AnimationsTypes.attack3 && currentAnimation.count == 3)
            {
                shoot();
            }
        }

        public void shoot()
        {
            if (!attackCooldown)
            {
                attackCooldown = true;

                var center = GetCollisionRectangle().Center.ToVector2();
                center -= new Vector2(projectileInAirAnimation.bounds.X, projectileInAirAnimation.bounds.Y);
                projectiles.Add(new Projectile(projectileInAirAnimation.clone(), projectileHitAnimation.clone(), lookingLeft, center, new Rectangle(16 * 2, 16 * 2, 16 * 2, 16 * 2)));
            }
        }

        public override void deadSound()
        {
            throw new NotImplementedException();
        }
    }
}