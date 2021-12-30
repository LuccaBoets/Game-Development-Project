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
    public class MushroomMonster : Enemy
    {
        public override Animatie currentAnimation { get; set; }
        public override List<Animatie> Animaties { get; set; }
        public override bool lookingLeft { get; set; }

        public override Vector2 position { get; set; }

        int state = 0;


        public override Movement Movement { get; set; }
        public override Stats stats { get; set; }
        public override double invisibleTimer { get; set; }
        public override bool invisible { get; set; } = false;

        public MushroomMonster(List<Animatie> animaties, Vector2 newPosition)
        {
            this.Animaties = animaties;

            this.position = newPosition;

            Movement = new Movement();
            Movement.MaxSpeedX = 1f;
            Movement.jumpSpeed = 3f;

            this.lookingLeft = true;

            this.currentAnimation = animaties.First(x => x.AnimatieNaam == AnimationsTypes.idle);

            this.stats = new Stats(50, 10);
        }
        public override Tuple<CollisionDirection, Rectangle> CollisionDetection(Rectangle rectangle)
        {
            throw new NotImplementedException();
        }

        public override void Update(GameTime gameTime, Hero hero, Tilemap tilemap)
        {
            Follow(hero);

            move(gameTime, tilemap);

            currentAnimation.update(gameTime);

            endOfAnimation();

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

            if (this.Movement.InAir == false)
            {
                if (tilemap.hitAnyTile(GetUnderCollisionRectangle()).Count <= 0)
                {
                    this.Movement.InAir = true;
                }
            }
        }

        public void Follow(Hero hero)
        {
            if (CollisionManager.Detection(GetMonsterRangeRectangle(), hero.GetCollisionRectangle()))
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
        }

        public Rectangle GetUnderCollisionRectangle()
        {
            var rectangle = GetCollisionRectangle();
            rectangle.Height += 10;
            return rectangle;
        }

        public override Rectangle GetMonsterRangeRectangle()
        {
            var center = GetCollisionRectangle().Center.ToVector2();
            var rectangle = new Rectangle(0, 0, 500, 500);
            rectangle.X = (int)(-rectangle.Width / 2 + center.X);
            rectangle.Y = (int)(-rectangle.Height / 2 + center.Y);
            return rectangle;
        }

        public override Rectangle GetCollisionRectangle()
        {
            var beginPoint = position.ToPoint();
            beginPoint.X += 126;
            beginPoint.Y += 130;
            return new Rectangle(beginPoint, new Point(23 * 2, 37 * 2));
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
        }

        public override void changeAnimation(AnimationsTypes animationsTypes, bool ignorePriority = false)
        {
            if (!(this.currentAnimation.AnimatieNaam == animationsTypes) && (this.currentAnimation.AnimatieNaam.isHigherPriority(animationsTypes) || ignorePriority))
            {
                this.currentAnimation = this.Animaties.FirstOrDefault(x => x.AnimatieNaam == animationsTypes);
                if (this.currentAnimation == null)
                {
                    this.currentAnimation = this.Animaties.FirstOrDefault(x => x.AnimatieNaam == AnimationsTypes.idle);
                }
                this.currentAnimation.reset();
            }
        }

        public override void Hit(int damage)
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

        public override void endOfAnimation()
        {
            if (this.currentAnimation.isFinished)
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
                        position = new Vector2(100, 100);
                        break;
                    default:
                        break;
                }
            }
        }
    }
}



