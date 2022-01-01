using GameEngine.Behavior;
using GameEngine.Characters;
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
    public abstract class Enemy : ICollisionable, IAnimationable, IHitable, IMoveable
    {
        public abstract Animatie currentAnimation { get; set; }
        public abstract List<Animatie> Animaties { get; set; }
        public abstract bool lookingLeft { get; set; }
        public abstract Stats stats { get; set; }
        public abstract double invisibleTimer { get; set; }
        public abstract bool invisible { get; set; }
        public abstract Movement Movement { get; set; }
        public abstract Vector2 position { get; set; }
        public abstract Animatie projectileHitAnimation { get; set; }
        public abstract Animatie projectileInAirAnimation { get; set; }
        public abstract List<Projectile> projectiles { get; set; }
        public abstract double attackCooldownTimer { get; set; }
        public abstract bool attackCooldown { get; set; }

        public abstract void Draw(SpriteBatch spriteBacth);
        public abstract void Update(GameTime gameTime, Hero hero, Tilemap tilemap);
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
        public abstract void Hit(int damage);
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
                        break;
                    default:
                        break;
                }
            }
        }

    }
}
