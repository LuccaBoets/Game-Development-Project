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

        Scrolling scrolling1;
        Scrolling scrolling2;
        

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
                
            _graphics.PreferredBackBufferWidth = Data.ScreenW;
            _graphics.PreferredBackBufferHeight = Data.ScreenH;
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

            scrolling1 = new Scrolling(Content.Load<Texture2D>("Background"), new Rectangle(0, 0, 1600, 900));
            scrolling2 = new Scrolling(Content.Load<Texture2D>("Background"), new Rectangle(900, 0, 1600, 900));
            var heroAnimaties = new List<Animatie>() { Animaties.GetIdleAnimatieFromHero(Content), Animaties.GetRunAnimatieFromHero(Content), Animaties.GetFallAnimatieFromHero(Content), Animaties.GetJumpAnimatieFromHero(Content) };

            hero = new Hero(heroAnimaties);

            tilemap = new Tilemap();

            Texture2D textureTileSet = Content.Load<Texture2D>("SET1_Mainlev_build");

            tilemap.addTile(textureTileSet, new Vector2(5, 25), new Rectangle(96, 448, 16, 16), GraphicsDevice);


            for (int i = 10; i < 30; i++)
            {
                tilemap.addTile(textureTileSet, new Vector2(i, 25), new Rectangle(96, 448, 16, 16), GraphicsDevice);
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
                hero.move.Move(hero, -speed, gameTime);
                scrolling1.Update(-speed);
                scrolling2.Update(-speed);
                idle = false;
               //Left
            }

            if (Keyboard.GetState().IsKeyDown(Keys.D))
            { 
                hero.move.Move(hero, speed, gameTime);
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



            if (Keyboard.GetState().IsKeyDown(Keys.Space) && hero.move.jumped == false)
            {

                hero.move.Jump(hero);
         
                hero.move.jumped = true;

            }


            if (hero.move.jumped)
            {
                hero.move.Down(hero);//gravity
            }

            if(hero.position.Y > 800)
            {
                hero.move.jumped = false;
            }
              
           
            if (hero.move.jumped == false)
            {
                hero.move.velocity.Y = 0.0f;
            }
          

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

            scrolling1.Draw(_spriteBatch);
            scrolling2.Draw(_spriteBatch);
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
