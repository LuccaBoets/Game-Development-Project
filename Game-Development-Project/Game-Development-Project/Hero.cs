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

        public Hero(List<Animatie> animaties)
        {
            this.Animaties = animaties;
            this.position = Vector2.One;

            this.currentAnimation = animaties.First(x => x.AnimatieNaam == HeroAnimations.idle);
        }

        public void update(GameTime gameTime)
        {
            currentAnimation.update(gameTime);
        }

        public void draw(SpriteBatch _spriteBatch)
        {
            _spriteBatch.Draw(currentAnimation.texture, position, currentAnimation.currentFrame.borders, Color.White, 0, Vector2.Zero, 4f, SpriteEffects.None, 0f);
        }


    }
}
