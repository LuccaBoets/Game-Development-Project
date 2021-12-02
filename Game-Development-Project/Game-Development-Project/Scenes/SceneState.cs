using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;
using GameEngine;

namespace GameEngine.Scenes
{
    public abstract class SceneState
    {
        public MainGame MainGame { get; set; }

        public GraphicsDeviceManager _graphics;
        public SpriteBatch _spriteBatch;

        protected SceneState(MainGame game, GraphicsDeviceManager graphics, SpriteBatch spriteBatch)
        {
            this.MainGame = game;
            _graphics = graphics;
            _spriteBatch = spriteBatch;
        }

        public abstract void Initialize();

        public abstract void LoadContent();

        public abstract void Update(GameTime gameTime);

        public abstract void Draw(GameTime gameTime);
    }
}
