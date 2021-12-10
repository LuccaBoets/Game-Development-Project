using GameEngine.Environment;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Text;

namespace GameEngine.Scenes
{
    public class MenuState : SceneState
    {

        Background background2;
        Background backgroundCharacterSelect;
        Texture2D image;

        public MenuState(MainGame game, GraphicsDeviceManager graphics, SpriteBatch spriteBatch) : base(game, graphics, spriteBatch)
        {
            LoadContent();
        }

        public override void Draw(GameTime gameTime)
        {
            _spriteBatch.Begin(SpriteSortMode.FrontToBack, null, SamplerState.PointClamp);

            background2.Draw(_spriteBatch);

            _spriteBatch.Draw(image, new Vector2(620, 400), image.Bounds, Color.White, 0, Vector2.Zero, 4f, SpriteEffects.None, 0f);

            _spriteBatch.End();
        }

        public override void Initialize()
        {
            throw new NotImplementedException();
        }

        public override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(MainGame.GraphicsDevice);
            background2 = new Background(MainGame.Content.Load<Texture2D>("hills"), new Rectangle(0, 0, 1600, 900));
            backgroundCharacterSelect = new Background(MainGame.Content.Load<Texture2D>("hills"), new Rectangle(0, 0, 1600, 900));

            image = MainGame.Content.Load<Texture2D>("press-enter-text");
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
