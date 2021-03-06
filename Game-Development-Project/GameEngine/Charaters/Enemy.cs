using GameEngine.Behavior;
using GameEngine.Characters;
using GameEngine.Data;
using GameEngine.Environment;
using GameEngine.ExtensionMethods;
using GameEngine.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GameEngine.Charaters
{
    public enum moveActions
    {
        left,
        right,
        idle
    }

    public abstract class Enemy : ICollisionable, IAnimationable, IHitable, IMoveable
    {
        public Animatie currentAnimation { get; set; }
        public List<Animatie> Animaties { get; set; }
        public bool lookingLeft { get; set; }
        public Stats stats { get; set; }
        public double invisibleTimer { get; set; }
        public bool invisible { get; set; }
        public virtual Movement Movement { get; set; }
        public Vector2 position { get; set; }
        public Animatie projectileHitAnimation { get; set; }
        public Animatie projectileInAirAnimation { get; set; }
        public List<Projectile> projectiles { get; set; }
        public double attackCooldownTimer { get; set; }
        public bool attackCooldown { get; set; }
        public bool isDead { get; set; }
        public moveActions moveActions { get; set; }
        public double moveCooldownTimer { get; set; }
        public int moveOffset { get; set; }

        public SoundEffect deathSound { get; set; } 

        public abstract void Draw(SpriteBatch spriteBacth);
        public virtual void Update(GameTime gameTime, Hero hero, Tilemap tilemap)
        {
            Follow(gameTime, hero, tilemap);

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
        public abstract void Follow(GameTime gameTime, Hero hero, Tilemap tilemap);
        public virtual void move(GameTime gameTime, Tilemap tilemap)
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

            if (this.Movement.InAir == false)
            {
                if (tilemap.hitAnyTile(GetUnderCollisionRectangle()).Count <= 0)
                {
                    this.Movement.InAir = true;
                }
            }
        }

        public virtual void randomMovement(GameTime gameTime)
        {

            if (moveCooldownTimer >= 1000)
            {
                moveCooldownTimer = 0;

                Random random = new Random();
                var getal = random.Next(0, 4);
                if (getal == 1)
                {
                    moveActions = moveActions.left;
                }
                else if (getal == 2)
                {
                    moveActions = moveActions.right;
                }
                else
                {
                    moveActions = moveActions.idle;
                }
            }

            moveCooldownTimer += gameTime.ElapsedGameTime.TotalMilliseconds;
            

            switch (moveActions)
            {
                case moveActions.left:
                    if (moveOffset >= -20)
                    {
                        Movement.left(this);
                        moveOffset--;
                    }
                    break;
                case moveActions.right:
                    if (moveOffset <= 20)
                    {
                        Movement.right(this);
                        moveOffset++;
                    }
                    break;
                case moveActions.idle:
                    break;
                default:
                    break;
            }
        }

        public void changeAnimation(AnimationsTypes animationsTypes, bool ignorePriority = false)
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
        public abstract Tuple<CollisionDirection, Rectangle> CollisionDetection(Rectangle rectangle);
        public Rectangle GetUnderCollisionRectangle()
        {
            var rectangle = GetCollisionRectangle();
            rectangle.Height += 10;
            return rectangle;
        }
        public abstract Rectangle GetMonsterRangeRectangle();
        public abstract Rectangle GetCollisionRectangle();
        public abstract Rectangle GetNextCollisionRectangle();
        public abstract void attack1(Hero hero);
        public abstract void attack2(Hero hero);
        public abstract void attack3(Hero hero);
        public void Hit(int damage)
        {
            if (!invisible)
            {
                stats.health -= damage;
                invisible = true;
                changeAnimation(AnimationsTypes.hit);
                if (stats.health <= 0)
                {
                    changeAnimation(AnimationsTypes.death);
                    deadSound();
                }
            }
        }
        public abstract void deadSound();
        public void endOfAnimation()
        {
            if (this.currentAnimation.isFinished)
            {
                switch (this.currentAnimation.AnimatieNaam)
                {
                    case AnimationsTypes.attack1:
                    case AnimationsTypes.attack2:
                    case AnimationsTypes.attack3:
                        changeAnimation(AnimationsTypes.idle, true);
                        attackCooldown = true;
                        break;
                    case AnimationsTypes.hit:
                        changeAnimation(AnimationsTypes.idle, true);
                        break;
                    case AnimationsTypes.death:
                        position = new Vector2(100, 100);
                        isDead = true;
                        break;
                    default:
                        break;
                }
            }
        }

    }
}
