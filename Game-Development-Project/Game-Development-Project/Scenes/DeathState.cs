using GameEngine.Environment;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Text;

namespace GameEngine.Scenes
{
    public class DeathState : SceneState
    {
      
        Background backgroundDeath;
        Texture2D imageYouDied;
     
        public DeathState(MainGame game, GraphicsDeviceManager graphics, SpriteBatch spriteBatch) : base(game, graphics, spriteBatch)
        {
            LoadContent();
        }

        public override void Draw(GameTime gameTime)
        {
            _spriteBatch.Begin(SpriteSortMode.FrontToBack, null, SamplerState.PointClamp);
            backgroundDeath.Draw(_spriteBatch);

            _spriteBatch.Draw(imageYouDied, new Vector2(310, 100), imageYouDied.Bounds, Color.White, 0, Vector2.Zero, 1.2f, SpriteEffects.None, 0f);

            _spriteBatch.End();
        }

        public override void Initialize()
        {
            throw new NotImplementedException();
        }

        public override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(MainGame.GraphicsDevice);
            
            backgroundDeath = new Background(MainGame.Content.Load<Texture2D>("Game_Over"), new Rectangle(0, 0, 1600, 900));
            imageYouDied = MainGame.Content.Load<Texture2D>("YouDied");


        }

        public override void Update(GameTime gameTime)
        {
            if (Keyboard.GetState().IsKeyDown(Keys.Enter))
            {
                MainGame.ChangeSceneState(new LvlOneState(MainGame, _graphics, _spriteBatch));
            }

        }
    

    }
}
