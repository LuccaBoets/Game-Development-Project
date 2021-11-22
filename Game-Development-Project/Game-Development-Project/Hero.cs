using GameDevelopmentProject.Environment;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GameDevelopmentProject
{
    public enum HeroAnimations
    {
        idle,
        run,
        jump,
        fall,
        attack1,
        attack2,
        attack3,
        death
    }
    public class Hero
    {


        // dict key: enum (idle,attack,...) , value: Animatie
        public Animatie currentAnimation { get; set; }
        public List<Animatie> Animaties { get; set; }

        public Vector2 position { get; set; }

        public bool lookingRight { get; set; }

        public Hero(List<Animatie> animaties)
        {
            this.Animaties = animaties;
            this.position = new Vector2(0, 500);
            this.lookingRight = true;

            this.currentAnimation = animaties.First(x => x.AnimatieNaam == HeroAnimations.idle);
        }

        public void update(GameTime gameTime, Tilemap tilemap)
        {
            currentAnimation.update(gameTime);

            var rectangle = currentAnimation.texture.Bounds;

            rectangle.X += (int)position.X;
            rectangle.Y += (int)position.Y;
            rectangle.Width = (int)(currentAnimation.bounds.X * 2);
            rectangle.Height = (int)(currentAnimation.bounds.Y * 2);

            if (tilemap.touchGround(rectangle))
            {
                position -= new Vector2(0, -10f);
            }
        }

        public void draw(SpriteBatch _spriteBatch)
        {
            var spriteEffects = SpriteEffects.None;

            if (lookingRight)
            {
                spriteEffects = SpriteEffects.FlipHorizontally;
            }

            _spriteBatch.Draw(currentAnimation.texture, position + currentAnimation.offset, currentAnimation.currentFrame.borders, Color.White, 0, Vector2.Zero, 2f, spriteEffects, 0f);
        }


    }
}
