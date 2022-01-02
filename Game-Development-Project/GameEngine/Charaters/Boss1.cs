using GameEngine.Behavior;
using GameEngine.Data;
using GameEngine.Environment;
using GameEngine.ExtensionMethods;
using GameEngine.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GameEngine.Charaters
{
    public class Boss1 : MushroomMonster
    {
        public Boss1(List<Animatie> animaties, List<Animatie> projectileAnimation, Vector2 newPosition) : base(animaties, projectileAnimation, newPosition)
        {
            stats.maxHealth = 20;
            stats.health = 20;
        }

        public override void Update(GameTime gameTime, Hero hero, Tilemap tilemap)
        {
            Follow(hero, tilemap);

            move(gameTime, tilemap);

            currentAnimation.update(gameTime, 150);

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

            //changeAnimation(AnimationsTypes.attack1);
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

        public void Follow(Hero hero, Tilemap tilemap)
        {
            if (CollisionManager.Detection(GetMonsterRangeRectangle(), hero.GetCollisionRectangle()))
            {
                if (!CollisionManager.Detection(hero.GetCollisionRectangle(), GetCollisionRectangle().Center, 200, 50))
                {
                    if (hero.GetCollisionRectangle().Center.X >= GetCollisionRectangle().Center.X)
                    {
                        Movement.right(this);
                    }
                    else
                    {
                        Movement.left(this);
                    }
                }
                else
                {
                    if (hero.GetCollisionRectangle().Center.X >= GetCollisionRectangle().Center.X)
                    {
                        lookingLeft = false;
                    }
                    else
                    {
                        lookingLeft = true;
                    }
                }

                if (!attackCooldown)
                {
                    if (CollisionManager.Detection(hero.GetCollisionRectangle(), GetCollisionRectangle().Center, 300, 50) && currentAnimation.AnimatieNaam.canMove())
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
                    else if (CollisionManager.Detection(hero.GetCollisionRectangle(), GetCollisionRectangle().Center, 1000, 50) && currentAnimation.AnimatieNaam.canMove())
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
            if (lookingLeft)
            {
                beginPoint.X += 256;
                beginPoint.Y += 256;
            }
            else
            {
                beginPoint.X += 252;
                beginPoint.Y += 256;
            }

            return new Rectangle(beginPoint, new Point(23 * 4, 37 * 4));
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

            _spriteBatch.Draw(currentAnimation.texture, position + currentAnimation.offset, currentAnimation.currentFrame.borders, Color.White, 0, Vector2.Zero, 4f, spriteEffects, 0.5f);

            foreach (var projectile in projectiles)
            {
                projectile.Draw(_spriteBatch);
            }
        }

        public void attack1(Hero hero)
        {
            changeAnimation(AnimationsTypes.attack1);

            const int Width = 47 * 2;
            const int Height = 43 * 2;
            const int yOffset = 0;

            if (currentAnimation.AnimatieNaam == AnimationsTypes.attack1 && currentAnimation.count == 6)
            {
                Random random = new Random();

                attackCooldownTimer = random.Next(0, 1) * 500;
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

        public void attack2(Hero hero)
        {
            changeAnimation(AnimationsTypes.attack2);

            const int Width = 53 * 2;
            const int Height = 79 * 2;
            const int yOffset = 0;

            if (currentAnimation.AnimatieNaam == AnimationsTypes.attack2 && currentAnimation.count == 6)
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
                    hero.Hit(stats.damage + 1);
                }
            }
        }

        public void attack3(Hero hero)
        {
            Random random = new Random();
            attackCooldownTimer = 2000 + (random.Next(0, 5) * 500);

            changeAnimation(AnimationsTypes.attack3);
            if (currentAnimation.AnimatieNaam == AnimationsTypes.attack3 && currentAnimation.count == 10)
            {
                shoot();
            }
        }
    }
}
