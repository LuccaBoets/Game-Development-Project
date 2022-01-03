using GameEngine.Behavior;
using GameEngine.Environment;
using GameEngine.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace GameEngine.Charaters
{
    public class Projectile
    {
        public Animatie inAirAnimation { get; set; }
        public Animatie hitAnimation { get; set; }
        public Animatie currentAnimation { get; set; }
        public bool lookingLeft { get; set; }

        public Vector2 position { get; set; }

        public int damage { get; set; } = 2;

        public bool isHit { get; set; } = false;
        public float speed = 5f;
        public bool isRemove { get; set; } = false;
        public Rectangle collisionRectangle { get; set; }

        public Projectile(Animatie inAirAnimation, Animatie hitAnimation, bool lookingLeft, Vector2 position, Rectangle collisionRectangle)
        {
            this.inAirAnimation = inAirAnimation;
            this.hitAnimation = hitAnimation;
            this.lookingLeft = lookingLeft;
            this.position = position;
            this.collisionRectangle = collisionRectangle;

            this.currentAnimation = inAirAnimation;
        }

        public void Update(GameTime gameTime, Hero hero, Tilemap tilemap)
        {
            if (lookingLeft)
            {
                position -= new Vector2(speed, 0) * gameTime.ElapsedGameTime.Ticks / 100000;
            }
            else
            {
                position += new Vector2(speed, 0) * gameTime.ElapsedGameTime.Ticks / 100000;
            }

            if (!isHit)
            {
                if (CollisionManager.Detection(hero.GetCollisionRectangle(), GetCollisionRectangle()))
                {
                    hero.Hit(damage);
                    hit();
                }

                foreach (var tile in tilemap.MiddleGround.Tiles)
                {
                    if (CollisionManager.Detection(tile.GetCollisionRectangle(), GetCollisionRectangle()))
                    {
                        hit();
                    }
                }
            }

            currentAnimation.update(gameTime);

            if (currentAnimation.isFinished && isHit)
            {
                remove();
            }
        }

        private void hit()
        {
            isHit = true;
            speed = 0;
            currentAnimation = hitAnimation;
        }

        public void remove()
        {
            isRemove = true;
        }

        public void Draw(SpriteBatch _spriteBatch, float scale = 2f)
        {
            var spriteEffects = SpriteEffects.None;

            if (lookingLeft)
            {
                spriteEffects = SpriteEffects.FlipHorizontally;
            }

            _spriteBatch.Draw(currentAnimation.texture, position + currentAnimation.offset, currentAnimation.currentFrame.borders, Color.White, 0, Vector2.Zero, scale, spriteEffects, 0.5f);
        }

        public Rectangle GetCollisionRectangle()
        {
            Rectangle rectangle = new Rectangle((int)position.X + collisionRectangle.X, (int)position.Y + collisionRectangle.Y, collisionRectangle.Width, collisionRectangle.Height);

            return rectangle;
        }
    }
}
