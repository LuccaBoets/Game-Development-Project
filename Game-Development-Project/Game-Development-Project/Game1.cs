using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using GameEngine.Environment;
using GameEngine.Graphics;
using GameEngine.ExtensionMethods;
using GameEngine.Data;

namespace GameEngine
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        private Texture2D background;
        private Hero hero { get; set; }

        Scrolling scrolling1;
        Scrolling scrolling2;
        public bool buttonIspressed = false;
        Background background2;
        Background backgroundCharacterSelect;
        Texture2D image;

        private Tilemap tilemap { get; set; }


        const int speed = 3;


        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            //_graphics.ToggleFullScreen();
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {

            _graphics.PreferredBackBufferWidth = Settings.ScreenW;
            _graphics.PreferredBackBufferHeight = Settings.ScreenH;
            this.Window.ClientSizeChanged +=
            (sender, e) =>
            {

            };
            _graphics.ApplyChanges();

            // TODO: Add your initialization logic here
            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            background2 = new Background(Content.Load<Texture2D>("hills"), new Rectangle(0, 0, 1600, 900));
            backgroundCharacterSelect = new Background(Content.Load<Texture2D>("hills"), new Rectangle(0, 0, 1600, 900));
            scrolling1 = new Scrolling(Content.Load<Texture2D>("Background"), new Rectangle(0, 0, 928*2, 793*2));
            scrolling2 = new Scrolling(Content.Load<Texture2D>("Background"), new Rectangle(928 * 2, 0, 928 * 2, 793 * 2));
            image = Content.Load<Texture2D>("press-enter-text");


            var heroAnimaties = new List<Animatie>() {
                Animaties.GetIdleAnimatieFromHero(Content),
                Animaties.GetRunAnimatieFromHero(Content),
                Animaties.GetFallAnimatieFromHero(Content),
                Animaties.GetJumpAnimatieFromHero(Content)
            };

            hero = new Hero(heroAnimaties);

            tilemap = new Tilemap();

            Texture2D textureTileSet = Content.Load<Texture2D>("SET1_Mainlev_build");

            List<Texture2D> ground = new List<Texture2D>() {
                textureTileSet.Cut(new Rectangle(96, 448, 16, 16), GraphicsDevice),
                textureTileSet.Cut(new Rectangle(112, 448, 16, 16), GraphicsDevice),
                textureTileSet.Cut(new Rectangle(128, 448, 16, 16), GraphicsDevice),
                textureTileSet.Cut(new Rectangle(144, 448, 16, 16), GraphicsDevice)
            };

            List<Texture2D> grass = new List<Texture2D>() {
                textureTileSet.Cut(new Rectangle(96, 432, 16, 16), GraphicsDevice),
                textureTileSet.Cut(new Rectangle(112, 432, 16, 16), GraphicsDevice),
                textureTileSet.Cut(new Rectangle(128, 432, 16, 16), GraphicsDevice),
                textureTileSet.Cut(new Rectangle(144, 432, 16, 16), GraphicsDevice)
            };

            tilemap.addTile(textureTileSet, new Vector2(5, 25), new Rectangle(96, 448, 16, 16), GraphicsDevice, SpriteEffects.None);

            Random random = new Random(1);

            tilemap.addTile(Content.Load<Texture2D>("Island"), new Vector2(5, 15), SpriteEffects.None);

            for (int i = 10; i < 100; i++)
            {
                if (random.Next(0,2) == 1)
                {
                    tilemap.addTile(ground[random.Next(0, ground.Count)], new Vector2(i, 25), SpriteEffects.FlipHorizontally);

                }
                else
                {
                    tilemap.addTile(ground[random.Next(0, ground.Count)], new Vector2(i, 25), SpriteEffects.None);

                }

                if (random.Next(0, 2) == 1)
                {
                    tilemap.addTile(grass[random.Next(0, grass.Count)], new Vector2(i, 24), SpriteEffects.FlipHorizontally, false);

                }
                else
                {
                    tilemap.addTile(grass[random.Next(0, grass.Count)], new Vector2(i, 24), SpriteEffects.None, false);

                }
            }

            for (int i = 10; i < 20; i++)
            {
                if (random.Next(0, 2) == 1)
                {
                    tilemap.addTile(ground[random.Next(0, ground.Count)], new Vector2(i, 20), SpriteEffects.FlipHorizontally);

                }
                else
                {
                    tilemap.addTile(ground[random.Next(0, ground.Count)], new Vector2(i, 20), SpriteEffects.None);

                }

                if (random.Next(0, 2) == 1)
                {
                    tilemap.addTile(grass[random.Next(0, grass.Count)], new Vector2(i, 19), SpriteEffects.FlipHorizontally, false);

                }
                else
                {
                    tilemap.addTile(grass[random.Next(0, grass.Count)], new Vector2(i, 19), SpriteEffects.None, false);

                }
            }

            //for (int i = 0; i < 30; i++)
            //{
            //    tilemap.addTile(textureTileSet, new Vector2(16 * i * 2.5f, 4 * 16 * 2.5f), new Rectangle(96, 448-16, 16, 16), GraphicsDevice);
            //}

        }

        protected override void Update(GameTime gameTime)
        {


            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

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


            if (Keyboard.GetState().IsKeyDown(Keys.Enter))
            {

                buttonIspressed = true;

            }

            //if (hero.movement.jumped)
            //{
            //    hero.movement.down(hero);//gravity
            //}

            //if(hero.position.Y > 800)
            //{
            //    hero.movement.jumped = false;
            //}


            //if (hero.movement.jumped == false)
            //{
            //    hero.movement.velocity.Y = 0.0f;
            //}


            /*if (scrolling1.rectangle.X + scrolling1.texture.Width <= 0) 
            scrolling1.rectangle.X = scrolling2.rectangle.X + scrolling2.texture.Width;
            if (scrolling2.rectangle.X + scrolling2.texture.Width <= 0) 
            scrolling2.rectangle.X = scrolling1.rectangle.X + scrolling2.texture.Width;
            /*scrolling1.Update();
            scrolling2.Update();
            */

            hero.update(gameTime, tilemap);

            base.Update(gameTime);
        }
    
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here
          

            _spriteBatch.Begin(SpriteSortMode.Deferred, null, SamplerState.PointClamp);


          
            if (buttonIspressed)
            {

       
                Rectangle rectangle = GraphicsDevice.Viewport.Bounds;
                rectangle.X = (int)((Settings.ScreenW / 2) - hero.position.X - 17 * 2);
                //rectangle.Y = (int)((Settings.ScreenH / 2) - hero.position.Y - 27 * 2);
                GraphicsDevice.Viewport = new Viewport(rectangle);

                scrolling1.Draw(_spriteBatch);
                scrolling2.Draw(_spriteBatch);
                hero.draw(_spriteBatch);
                tilemap.draw(_spriteBatch);

            }
            else
            {
                background2.Draw(_spriteBatch);
             
                _spriteBatch.Draw(image, new Vector2(620, 400), image.Bounds, Color.White, 0, Vector2.Zero, 4f, SpriteEffects.None, 0f);
            }

            _spriteBatch.End();


            base.Draw(gameTime);
        }
    }
}
