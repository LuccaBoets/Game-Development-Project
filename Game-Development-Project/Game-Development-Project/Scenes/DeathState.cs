using GameEngine.Data;
using GameEngine.Environment;
using GameEngine.Graphics;
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

        public Animatie heroDeath { get; set; }
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

            _spriteBatch.Draw(imageYouDied, new Vector2(430, 100), imageYouDied.Bounds, Color.White, 0, Vector2.Zero, 2f, SpriteEffects.None, 0f);

            _spriteBatch.Draw(heroDeath.texture, new Vector2(Settings.ScreenW / 2 - heroDeath.bounds.X +30, Settings.ScreenH / 2 - heroDeath.bounds.Y + 75) + heroDeath.offset, heroDeath.currentFrame.borders, Color.White, 0, Vector2.Zero, 2f, SpriteEffects.None, 0.5f);

            _spriteBatch.End();
        }

        public override void Initialize()
        {
            throw new NotImplementedException();
        }

        public override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(MainGame.GraphicsDevice);

            heroDeath = HeroAnimations.GetDeathFromHero(Content);
            backgroundDeath = new Background(Content.Load<Texture2D>("Game_Over"), new Rectangle(0, 0, 1600, 900));
            imageYouDied = MainGame.Content.Load<Texture2D>("YouDied");


        }

        public override void Update(GameTime gameTime)
        {
            if (Keyboard.GetState().IsKeyDown(Keys.Enter))
            {
                MainGame.ChangeSceneState(new LvlOneState(MainGame, _graphics, _spriteBatch));
            }

            if (!(heroDeath.count == 5))
            {
                heroDeath.update(gameTime);
            }
        }
    }
}
