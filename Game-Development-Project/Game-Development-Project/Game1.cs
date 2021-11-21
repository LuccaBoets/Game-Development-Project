using GameDevelopmentProject.Environment;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
using System.Linq;

namespace GameDevelopmentProject
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        private Hero hero { get; set; }
        private Tilemap tilemap { get; set; }



        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            //_graphics.ToggleFullScreen();
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            _graphics.PreferredBackBufferWidth = Data.ScreenW;
            _graphics.PreferredBackBufferHeight = Data.ScreenH;
            _graphics.ApplyChanges();
            // TODO: Add your initialization logic here
            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            var heroAnimaties = new List<Animatie>() { Animaties.GetIdleAnimatieFromHero(Content), Animaties.GetRunAnimatieFromHero(Content) };
            hero = new Hero(heroAnimaties);

            tilemap = new Tilemap();

            Texture2D textureTileSet = Content.Load<Texture2D>("SET1_Mainlev_build");

            for (int i = 0; i < 30; i++)
            {
                tilemap.addTile(textureTileSet, new Vector2(i, 5), new Rectangle(96, 448, 16, 16), GraphicsDevice);
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
                hero.position -= new Vector2(3.0f, 0.0f);
                hero.currentAnimation = hero.Animaties.First(x => x.AnimatieNaam == HeroAnimations.run);
                hero.lookingRight = true;
                idle = false;
                //Left
            }

            if (Keyboard.GetState().IsKeyDown(Keys.D))
            {
                hero.position += new Vector2(3.0f, 0.0f);
                hero.currentAnimation = hero.Animaties.First(x => x.AnimatieNaam == HeroAnimations.run);
                hero.lookingRight = false;
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

            hero.update(gameTime, tilemap);

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here

            _spriteBatch.Begin(SpriteSortMode.Deferred, null, SamplerState.PointClamp);

            hero.draw(_spriteBatch);
            tilemap.draw(_spriteBatch);

            Rectangle rectangle = GraphicsDevice.Viewport.Bounds;
            rectangle.X = (int)((Data.ScreenW / 2) - hero.position.X - 17*2);
            rectangle.Y = (int)((Data.ScreenH / 2) - hero.position.Y - 27*2);

            GraphicsDevice.Viewport = new Viewport(rectangle);


            _spriteBatch.End();


            base.Draw(gameTime);
        }
    }
}
