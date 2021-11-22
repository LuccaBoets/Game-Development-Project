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
        Movement move2;

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
            _graphics.ApplyChanges();
            move2 = new Movement();
            move2.jumped = true;
            // TODO: Add your initialization logic here
            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            scrolling1 = new Scrolling(Content.Load<Texture2D>("Background"), new Rectangle(0, 0, 1600, 900));
            scrolling2 = new Scrolling(Content.Load<Texture2D>("Background"), new Rectangle(800, 0, 1600, 900));
            var heroAnimaties = new List<Animatie>() { Animaties.GetIdleAnimatieFromHero(Content), Animaties.GetRunAnimatieFromHero(Content) };
            hero = new Hero(heroAnimaties);

            // TODO: use this.Content to load your game content here
        }

        protected override void Update(GameTime gameTime)
        {

            hero.position += move2.velocity;
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            if (Keyboard.GetState().IsKeyDown(Keys.Q))
            {

                move2.Move(hero, -speed, 0.0f);
                hero.lookingRight = true;
                scrolling1.Update(-speed);
                scrolling2.Update(-speed);
                //Left
              
            }
            else
            if (Keyboard.GetState().IsKeyDown(Keys.D))
            {

                move2.Move(hero, speed, 0.0f);
                hero.lookingRight = false;
                scrolling1.Update(speed);
                scrolling2.Update(speed);
                //Right
            }
            else
            if (Keyboard.GetState().IsKeyDown(Keys.S))
            {
                //Down
            }
            else

            if (Keyboard.GetState().IsKeyDown(Keys.Z))
            {
                //Up
            }
           
            

           
            else
            {
                hero.currentAnimation = hero.Animaties.First(x => x.AnimatieNaam == HeroAnimations.idle);

            }


            if (Keyboard.GetState().IsKeyDown(Keys.Space) && move2.jumped == false)
            {

                move2.Jump(hero);
                move2.jumped = true;

            }


            if (move2.jumped)
            {
                move2.velocity.Y +=  0.15f * 1f;
               
            }

            if(hero.position.Y > 700)
            {
                move2.jumped = false;
            }
              
           
            if (move2.jumped == false)
            {
                move2.velocity.Y = 0.0f;
            }
          

            if (scrolling1.rectangle.X + scrolling1.texture.Width <= 0) 
            scrolling1.rectangle.X = scrolling2.rectangle.X + scrolling2.texture.Width;
            if (scrolling2.rectangle.X + scrolling2.texture.Width <= 0) 
            scrolling2.rectangle.X = scrolling1.rectangle.X + scrolling2.texture.Width;
            /*scrolling1.Update();
            scrolling2.Update();
            */

            hero.update(gameTime);

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

            _spriteBatch.End();


            base.Draw(gameTime);
        }
    }
}
