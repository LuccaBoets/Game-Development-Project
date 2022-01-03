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
    public class GoblinMonster : Enemy
    {

        public GoblinMonster(List<Animatie> animaties, List<Animatie> projectileAnimation, Vector2 newPosition)
        {
            this.Animaties = animaties;
            this.projectileHitAnimation = projectileAnimation[1];
            this.projectileInAirAnimation = projectileAnimation[0];
            this.position = newPosition;

            this.Movement = new Movement();
            this.Movement.MaxSpeedX = 1f;
            this.Movement.jumpSpeed = 4f;

            this.lookingLeft = true;

            this.currentAnimation = animaties.First(x => x.AnimatieNaam == AnimationsTypes.idle);

            this.stats = new Stats(5, 1);
            this.projectiles = new List<Projectile>();
        }
        public override Tuple<CollisionDirection, Rectangle> CollisionDetection(Rectangle rectangle)
        {
            throw new NotImplementedException();
        }

        public override void Update(GameTime gameTime, Hero hero, Tilemap tilemap)
        {
            Follow(hero, tilemap);

            move(gameTime, tilemap);

            currentAnimation.update(gameTime);

            endOfAnimation();

            if (attackCooldown)
            {
                attackCooldownTimer -= gameTime.ElapsedGameTime.TotalMilliseconds;
            }

            if (attackCooldownTimer <= 0)
            {
                attackCooldown = false;
                attackCooldownTimer = 0;
            }

            if (invisible)
            {
                invisibleTimer += gameTime.ElapsedGameTime.TotalMilliseconds;
            }

            if (invisibleTimer >= 2000)
            {
                invisible = false;
                invisibleTimer = 0;
            }

            if (currentAnimation.AnimatieNaam == AnimationsTypes.attack1)
            {
                attack1(hero);
            }

            if (currentAnimation.AnimatieNaam == AnimationsTypes.attack2)
            {
                attack2(hero);
            }

            if (currentAnimation.AnimatieNaam == AnimationsTypes.attack3)
            {
                attack3(hero);
            }

            foreach (var projectile in projectiles)
            {
                projectile.Update(gameTime, hero, tilemap);
            }

            projectiles.RemoveAll(x => x.isRemove);
        }

        public override void Follow(Hero hero, Tilemap tilemap)
        {
            if (CollisionManager.Detection(GetMonsterRangeRectangle(), hero.GetCollisionRectangle()))
            {
                if (!CollisionManager.Detection(hero.GetCollisionRectangle(), GetCollisionRectangle().Center, 100, 30))
                {
                    if (hero.position.X >= position.X)
                    {
                        Movement.right(this);
                    }
                    else
                    {
                        Movement.left(this);
                    }
                }


                if (!attackCooldown)
                {
                    if (CollisionManager.Detection(hero.GetCollisionRectangle(), GetCollisionRectangle().Center, 200, 30) && currentAnimation.AnimatieNaam.canMove())
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
                    else if (CollisionManager.Detection(hero.GetCollisionRectangle(), GetCollisionRectangle().Center, 1000, 30) && currentAnimation.AnimatieNaam.canMove())
                    {
                        attack3(hero);
                    }
                }
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
            beginPoint.X += 126;
            beginPoint.Y += 130;
            return new Rectangle(beginPoint, new Point(33 * 2, 36 * 2));
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

            const int Width = 25 * 2;
            const int Height = 36 * 2;
            const int yOffset = 0;

            if (currentAnimation.AnimatieNaam == AnimationsTypes.attack1 && currentAnimation.count == 6)
            {
                Random random = new Random();

                attackCooldownTimer = random.Next(0, 2) * 500;
                Rectangle attackCollsionRectangle;
                if (lookingLeft)
                {
                    attackCollsionRectangle = new Rectangle(GetCollisionRectangle().Left - Width, GetCollisionRectangle().Top + yOffset, Width, Height);
                }
                else
                {
                    attackCollsionRectangle = new Rectangle(GetCollisionRectangle().Right, GetCollisionRectangle().Top + yOffset, Width, Height);
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

            const int Width = 50 * 2;
            const int Height = 29 * 2;
            const int yOffset = 0;

            if (currentAnimation.AnimatieNaam == AnimationsTypes.attack2 && currentAnimation.count == 6)
            {
                Random random = new Random();

                attackCooldownTimer = random.Next(0, 3) * 500;
                Rectangle attackCollsionRectangle;
                if (lookingLeft)
                {
                    attackCollsionRectangle = new Rectangle(GetCollisionRectangle().Left - Width, GetCollisionRectangle().Top + yOffset, Width, Height);
                }
                else
                {
                    attackCollsionRectangle = new Rectangle(GetCollisionRectangle().Right, GetCollisionRectangle().Top + yOffset, Width, Height);
                }

                if (CollisionManager.Detection(hero.GetCollisionRectangle(), attackCollsionRectangle))
                {
                    hero.Hit(stats.damage + 1);
                }
            }
        }

        public override void attack3(Hero hero)
        {
            Random random = new Random();
            attackCooldownTimer = 1000 + (random.Next(0, 6) * 500);

            changeAnimation(AnimationsTypes.attack3);
            if (currentAnimation.AnimatieNaam == AnimationsTypes.attack3 && currentAnimation.count == 10)
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
                projectiles.Add(new Projectile(projectileInAirAnimation.clone(), projectileHitAnimation.clone(), lookingLeft, center, new Rectangle(48*2, 48*2, 9*2, 9*2)));
            }
        }
    }
}