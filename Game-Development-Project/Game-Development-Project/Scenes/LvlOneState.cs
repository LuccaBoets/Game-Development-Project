using GameEngine.Data;
using GameEngine.Environment;
using GameEngine.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GameEngine.Scenes
{
    public class LvlOneState : SceneState
    {
        private Hero hero { get; set; }

        Scrolling scrolling1;
        Scrolling scrolling2;

        private Texture2D background;
        private Tilemap tilemap { get; set; }

        const int speed = 3;

        public LvlOneState(MainGame game, GraphicsDeviceManager graphics, SpriteBatch spriteBatch) : base(game, graphics, spriteBatch)
        {
            LoadContent();
        }

        public override void Draw(GameTime gameTime)
        {
            var position = Matrix.CreateTranslation(
                  -hero.position.X - (hero.GetCollsionRectangle().Width / 2),
                  -hero.position.Y - (hero.GetCollsionRectangle().Height / 2),
                  0);

            var offset = Matrix.CreateTranslation(
                Settings.ScreenW / 2,
                Settings.ScreenH / 2,
                0);

            var Transform = position * offset;

            _spriteBatch.Begin(SpriteSortMode.FrontToBack, null, SamplerState.PointClamp, transformMatrix: Transform);

            scrolling1.Draw(_spriteBatch);
            scrolling2.Draw(_spriteBatch);
            hero.draw(_spriteBatch);
            tilemap.draw(_spriteBatch);

            _spriteBatch.End();
        }

        public override void Initialize()
        {
            throw new NotImplementedException();
        }

        public override void LoadContent()
        {
            scrolling1 = new Scrolling(MainGame.Content.Load<Texture2D>("Background"), new Rectangle(0, 0, 928 * 2, 793 * 2));
            scrolling2 = new Scrolling(MainGame.Content.Load<Texture2D>("Background"), new Rectangle(928 * 2, 0, 928 * 2, 793 * 2));

            var heroAnimaties = new List<Animatie>() {
                Animaties.GetIdleAnimatieFromHero(MainGame.Content),
                Animaties.GetRunAnimatieFromHero(MainGame.Content),
                Animaties.GetFallAnimatieFromHero(MainGame.Content),
                Animaties.GetJumpAnimatieFromHero(MainGame.Content)
            };

            hero = new Hero(heroAnimaties);

            tilemap = new Tilemap();
            tilemap.addTiles(MainGame.Content.Load<Texture2D>("test2"), MainGame.GraphicsDevice);
            tilemap.addTiles(MainGame.Content.Load<Texture2D>("ForeGround1"), MainGame.GraphicsDevice, 1);
            tilemap.addTiles(MainGame.Content.Load<Texture2D>("Background2"), MainGame.GraphicsDevice, -1);
            tilemap.addTiles(MainGame.Content.Load<Texture2D>("Background1"), MainGame.GraphicsDevice, -2);

            #region old tilemap
            //Texture2D textureTileSet = Content.Load<Texture2D>("SET1_Mainlev_build");

            //List<Texture2D> ground = new List<Texture2D>() {
            //    textureTileSet.Cut(new Rectangle(96, 448, 16, 16), GraphicsDevice),
            //    textureTileSet.Cut(new Rectangle(112, 448, 16, 16), GraphicsDevice),
            //    textureTileSet.Cut(new Rectangle(128, 448, 16, 16), GraphicsDevice),
            //    textureTileSet.Cut(new Rectangle(144, 448, 16, 16), GraphicsDevice)
            //};

            //List<Texture2D> grass = new List<Texture2D>() {
            //    textureTileSet.Cut(new Rectangle(96, 432, 16, 16), GraphicsDevice),
            //    textureTileSet.Cut(new Rectangle(112, 432, 16, 16), GraphicsDevice),
            //    textureTileSet.Cut(new Rectangle(128, 432, 16, 16), GraphicsDevice),
            //    textureTileSet.Cut(new Rectangle(144, 432, 16, 16), GraphicsDevice)
            //};

            //tilemap.addTile(textureTileSet, new Vector2(5, 25), new Rectangle(96, 448, 16, 16), GraphicsDevice, SpriteEffects.None);

            //Random random = new Random(1);

            //tilemap.addTile(Content.Load<Texture2D>("Island"), new Vector2(5, 15), SpriteEffects.None);

            //for (int i = 10; i < 100; i++)
            //{
            //    if (random.Next(0, 2) == 1)
            //    {
            //        tilemap.addTile(ground[random.Next(0, ground.Count)], new Vector2(i, 25), SpriteEffects.FlipHorizontally);

            //    }
            //    else
            //    {
            //        tilemap.addTile(ground[random.Next(0, ground.Count)], new Vector2(i, 25), SpriteEffects.None);

            //    }

            //    if (random.Next(0, 2) == 1)
            //    {
            //        tilemap.addTile(grass[random.Next(0, grass.Count)], new Vector2(i, 24), SpriteEffects.FlipHorizontally, false);

            //    }
            //    else
            //    {
            //        tilemap.addTile(grass[random.Next(0, grass.Count)], new Vector2(i, 24), SpriteEffects.None, false);

            //    }
            //}

            //for (int i = 10; i < 20; i++)
            //{
            //    if (random.Next(0, 2) == 1)
            //    {
            //        tilemap.addTile(ground[random.Next(0, ground.Count)], new Vector2(i, 20), SpriteEffects.FlipHorizontally);

            //    }
            //    else
            //    {
            //        tilemap.addTile(ground[random.Next(0, ground.Count)], new Vector2(i, 20), SpriteEffects.None);

            //    }

            //    if (random.Next(0, 2) == 1)
            //    {
            //        tilemap.addTile(grass[random.Next(0, grass.Count)], new Vector2(i, 19), SpriteEffects.FlipHorizontally, false);

            //    }
            //    else
            //    {
            //        tilemap.addTile(grass[random.Next(0, grass.Count)], new Vector2(i, 19), SpriteEffects.None, false);

            //    }
            //}

            //for (int i = 0; i < 30; i++)
            //{
            //    tilemap.addTile(textureTileSet, new Vector2(16 * i * 2.5f, 4 * 16 * 2.5f), new Rectangle(96, 448-16, 16, 16), GraphicsDevice);
            //}
            #endregion
        }

        public override void Update(GameTime gameTime)
        {
            var idle = true;

            if (Keyboard.GetState().IsKeyDown(Keys.Q))
            {
                hero.Movement.left(hero);
                scrolling1.Update(-speed);
                scrolling2.Update(-speed);
                idle = false;
                //Left
            }

            if (Keyboard.GetState().IsKeyDown(Keys.D))
            {
                hero.Movement.right(hero);

                //hero.movement.Move(hero, speed, gameTime);
                scrolling1.Update(speed);
                scrolling2.Update(speed);
                idle = false;
                //Right
            }

            if (Keyboard.GetState().IsKeyDown(Keys.S))
            {
                idle = false;
                //Down
                hero.position += new Vector2(0.0f, 3.0f);

            }


            if (Keyboard.GetState().IsKeyDown(Keys.Z))
            {

                idle = false;
                //Up
                hero.position += new Vector2(0.0f, -3.0f);
            }

            if (idle)

            {
                hero.currentAnimation = hero.Animaties.First(x => x.AnimatieNaam == HeroAnimations.idle);
            }



            if (Keyboard.GetState().IsKeyDown(Keys.Space) && hero.Movement.inAir == false)
            {

                hero.Movement.jump();

                //hero.movement.inAir = true;

            }

            hero.update(gameTime, tilemap);
        }
    }
}
