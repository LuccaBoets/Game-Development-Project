﻿using System;
using GameEngine.Behavior;
using GameEngine.Characters;
using GameEngine.Data;
using GameEngine.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;



namespace GameEngine
{
    public class Sprite : Component
    {

        protected float _layer { get; set; }

        protected Texture2D _texture;

        public float Layer
        {
            get { return _layer; }
            set
            {
                _layer = value;
            }
        }


        public Vector2 Position;
  
        public Sprite(Texture2D texture)
        {
            _texture = texture;
        }

        public Rectangle Rectangle
        {
            get
            {
                return new Rectangle((int)Position.X, (int)Position.Y, _texture.Width, _texture.Height);
            }
        }

        public override void Update (GameTime gameTime)
        {
            throw new NotImplementedException();
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(_texture, Position, null, Color.White, 0, new Vector2(0, 0), 1f, SpriteEffects.None, Layer);
        }
    }
    
}
