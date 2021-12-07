using GameEngine.Data;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace GameEngine.Environment
{
    public class Scrolling : Component
    {


        private bool _constantSpeed;

        private float _layer;

        private float _scrollingSpeed;

        private List<Sprite> _sprites;

        private readonly Hero hero;

        private float _speed;

        public float Layer
        {
            get { return _layer; }
                set{
                _layer = value;
                foreach (var sprite in _sprites)
                {
                    sprite.Layer = _layer;
                }
                }
        }

        public Scrolling(Texture2D texture, Hero player, float scrollingSpeed, bool constantSpeed = false)
            :this(new List<Texture2D>() { texture, texture }, player, scrollingSpeed, constantSpeed)
        {
            
        }

        public Scrolling(List<Texture2D> textures, Hero player, float scrollingSpeed, bool constantSpeed = false)
        {
            hero = player;
            _scrollingSpeed = scrollingSpeed;
            _constantSpeed = constantSpeed;
            _sprites = new List<Sprite>();
    

            for (int i = 0; i < textures.Count; i++)
            {
                var texture = textures[i];

                _sprites.Add(new Sprite(texture)
                {
                    Position = new Vector2((i * texture.Width) - 1, Settings.ScreenH - texture.Height)
                });

            }
        }
        //public Texture2D texture;
        //public Rectangle rectangle3;
        //public Vector2 position { get; set; }

        public override void Draw(GameTime gametime, SpriteBatch spriteBatch)
        {

            foreach (var sprite in _sprites)
            {
                sprite.Draw(gametime, spriteBatch);
            }

           
        }
    

        public override void Update(GameTime gameTime)
        {
            ApplySpeed(gameTime);

            CheckPosition();
          
        }

        private void ApplySpeed(GameTime gameTime)
        {
            _speed = (float)(_scrollingSpeed * gameTime.ElapsedGameTime.TotalSeconds);

            if(!_constantSpeed || hero.Movement.Velocity.X != 0)
            {
              
                _speed *= hero.Movement.Velocity.X;
            }

            foreach (var sprite in _sprites)
            {
                sprite.Position.X -= _speed;
            }
            
        }

         
        private void CheckPosition()
        {
            for (int i = 0; i < _sprites.Count; i++)
            {
                var sprite = _sprites[i];

                if(sprite.Rectangle.Right <= 0)
                {
                    var index = i - 1;

                    if (index < 0)
                    {
                        index = _sprites.Count - 1;
                    }

                    sprite.Position.X = _sprites[index].Rectangle.Right - (_speed * 2);
                }
            }
        }
    }
}
