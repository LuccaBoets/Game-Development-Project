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
using GameEngine.Scenes;

namespace GameEngine
{
    public class MainGame : Game
    {
        public SceneState SceneState { get; set; }

        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        public MainGame()
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


            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            this.ChangeSceneState(new MenuState(this, this._graphics, this._spriteBatch));

            //this.SceneState.LoadContent();
        }

        protected override void Update(GameTime gameTime)
        {


            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            this.SceneState.Update(gameTime);


            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            this.SceneState.Draw(gameTime);

            base.Draw(gameTime);
        }

        public void ChangeSceneState(SceneState sceneState)
        {
            this.SceneState = sceneState;
        }
    }
}
