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

        int state = 0;

        int distance;
        float oldstance;

        public Movement Movement { get; set; }
        public override Stats stats { get; set; }
        public override double invisibleTimer { get; set; }
        public override bool invisible { get; set; } = false;

        public MushroomMonster(List<Animatie> animaties, Vector2 newPosition, int newDistance)
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


            Follow(hero);
            Random random = new Random();
            int forwardChance = 10;
            int backChance = 10;
            int leftChance = 10;
            int rightChance = 10;

            if (invisible)
            {
                invisibleTimer += gameTime.ElapsedGameTime.TotalMilliseconds;
            }

            if (invisibleTimer >= 1000)
            {
                invisible = false;
                invisibleTimer = 0;
            }


            if (position.Y + 50 > hero.position.Y)
            {

                backChance += 50;
            }
            else if (position.Y - 50 < hero.position.Y)
            {
                forwardChance += 50;
            }
            if (position.X - 50 > hero.position.X)
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
                leftChance += 50;
            }
            else if (position.X + 50 < hero.position.X)
            {
                rightChance += 50;
            }
            backChance = random.Next(backChance);
            rightChance = random.Next(rightChance);
            leftChance = random.Next(leftChance);
            forwardChance = random.Next(forwardChance);

            int vergelijkingUD, vergelijkingLR, vergelijking;
            vergelijkingUD = Math.Max(forwardChance, backChance);
            vergelijkingLR = Math.Max(leftChance, rightChance);
            vergelijking = Math.Max(vergelijkingLR, vergelijkingUD);

            if (vergelijking == vergelijkingLR) //Left Right
            {
                if (vergelijkingLR == leftChance)
                {
                    state = 1;

                }
                else
                {
                    state = 2;

                }
            }
            /*else //Up Down
            {
                if (vergelijkingUD == backChance)
                {
                    state = 3;
                }
                else
                {
                    state = 4;
                }
            }*/

            currentAnimation.update(gameTime);

        }

        public void Follow(Hero hero)
        {
            distance += 2;
            if (state == 0)
            {
                distance = 0;
                //update(hero);
            }

            switch (state)
            {

                case 2:
                    position.X += 2.0f;
                    state = 0;
                    lookingLeft = false;
                    break;
                case 1:
                    position.X -= 2.0f;
                    lookingLeft = true;
                    state = 0;
                    break;
                /*case 3:
                    position.Y -= 2.0f;
                    break;
                case 4:
                    position.Y += 2.0f;
                    break;
                */

                case 0:
                    break;
            }







            if (position.X < hero.position.X + 50 && position.X > hero.position.X - 50)
            {
                Random random = new Random();
                int aanval = random.Next(0, 2);
                if (aanval == 0)
                {
                    currentAnimation = Animaties.First(x => x.AnimatieNaam == AnimationsTypes.attack1);
                }
                else if (aanval == 1)
                {

                    if (lookingLeft)

                    {
                        currentAnimation = Animaties.First(x => x.AnimatieNaam == AnimationsTypes.attack2);
                    }
                    else
                    {

                        currentAnimation = Animaties.First(x => x.AnimatieNaam == AnimationsTypes.attack3);
                    }
                    hero.currentAnimation = hero.Animaties.First(x => x.AnimatieNaam == AnimationsTypes.hit);
                }
            }
            else if (lookingLeft)
            {

                currentAnimation = Animaties.First(x => x.AnimatieNaam == AnimationsTypes.run);

                currentAnimation = Animaties.First(x => x.AnimatieNaam == AnimationsTypes.run);
                distance -= 5;
                velocity.X = 5f;


            }

            else
            {
                currentAnimation = Animaties.First(x => x.AnimatieNaam == AnimationsTypes.run);
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

        public override void changeAnimation(AnimationsTypes animationsTypes, bool ignorePriority = false)
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

        public override void endOfAnimation()
        {
            throw new NotImplementedException();
        }
    }
}



