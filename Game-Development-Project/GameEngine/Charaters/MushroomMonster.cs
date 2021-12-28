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

namespace GameEngine.Charaters
{
    public class MushroomMonster : Enemy
    {
        public override Animatie currentAnimation { get; set; }
        public override List<Animatie> Animaties { get; set; }
        public override bool lookingLeft { get; set; }

        public Vector2 position;

        public Vector2 velocity;

        public Vector2 origin;

        float distance;
        float oldstance;

        public Movement Movement { get; set; }
        public override Stats stats { get; set; }
        public override double invisibleTimer { get; set; }
        public override bool invisible { get; set; } = false;

        public MushroomMonster(List<Animatie> animaties, Vector2 newPosition, float newDistance)
        {
            this.Animaties = animaties;

            this.position = newPosition;

            this.distance = newDistance;

            oldstance = distance;

            Movement = new Movement();

            this.lookingLeft = true;

            this.currentAnimation = animaties.First(x => x.AnimatieNaam == AnimationsTypes.idle);

            this.stats = new Stats(50, 10);
        }
        public override Tuple<CollisionDirection, Rectangle> CollisionDetection(Rectangle rectangle)
        {
            throw new NotImplementedException();
        }



        public override void Update(GameTime gameTime, Hero hero)
        {
            //Follow(hero);

            if (invisible)
            {
                invisibleTimer += gameTime.ElapsedGameTime.TotalMilliseconds;
            }

            if (invisibleTimer >= 1000)
            {
                invisible = false;
                invisibleTimer = 0;
            }

            currentAnimation.update(gameTime);

            position += velocity;
            origin = new Vector2(1000, 300);

            if (distance <= 0)
            {
                lookingLeft = false;
                velocity.X = 1f;
            }
            else if (distance <= oldstance)
            {
                lookingLeft = true;
                velocity.X = 1f;
            }

            if (lookingLeft)
            {
                distance += 1;
            }
            else
            {
                distance -= 1;
            }
        }

        public void Follow(Hero hero)
        {
            if (hero.position.X < position.X + 100 && hero.position.X > position.X - 100)
            {
                if (lookingLeft)
                {
                    distance -= 5;
                    currentAnimation = Animaties.First(x => x.AnimatieNaam == AnimationsTypes.run);
                    velocity.X = 5f;
                    position -= velocity;
                }
                else
                {
                    currentAnimation = Animaties.First(x => x.AnimatieNaam == AnimationsTypes.run);
                    distance -= 5;
                    velocity.X = 5f;

                }
            }
        }

        public override Rectangle GetCollisionRectangle()
        {
            return new Rectangle((int)position.X, (int)position.Y, currentAnimation.texture.Bounds.Width, currentAnimation.texture.Bounds.Height);
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

        public override void changeAnimation(AnimationsTypes animationsTypes)
        {
            this.currentAnimation = this.Animaties.FirstOrDefault(x => x.AnimatieNaam == animationsTypes);
        }

        public override void Hit(int damage)
        {
            if (!invisible)
            {
                Debug.WriteLine("hit");
                stats.health -= damage;
                invisible = true;
                if (stats.health <= 0)
                {
                    position.X = 10000;
                }
            }
        }
    }
}
