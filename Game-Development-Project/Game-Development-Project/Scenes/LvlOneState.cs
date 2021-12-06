using GameEngine.Data;
using GameEngine.Environment;
using GameEngine.ExtensionMethods;
using GameEngine.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GameEngine.Scenes
{
    public class LvlOneState : SceneState
    {
        private Hero hero { get; set; }

        Scrolling scrolling1;
        Scrolling scrolling2;

        private Texture2D background;
        private Tilemap tilemap { get; set; }

        const int speed = 3;

        public LvlOneState(MainGame game, GraphicsDeviceManager graphics, SpriteBatch spriteBatch) : base(game, graphics, spriteBatch)
        {
            LoadContent();
        }

        public override void Draw(GameTime gameTime)
        {
            var position = Matrix.CreateTranslation(
                  -hero.position.X - (hero.GetNextCollisionRectangle().Width / 2),
                  -hero.position.Y - (hero.GetNextCollisionRectangle().Height / 2),
                  0);

            var offset = Matrix.CreateTranslation(
                Settings.ScreenW / 2,
                Settings.ScreenH / 2,
                0);

            var Transform = position * offset;

            _spriteBatch.Begin(SpriteSortMode.FrontToBack, null, SamplerState.PointClamp, transformMatrix: Transform);

            scrolling1.Draw(_spriteBatch);
            scrolling2.Draw(_spriteBatch);
            hero.draw(_spriteBatch);
            tilemap.draw(_spriteBatch);

            Texture2D _texture;

            _texture = new Texture2D(MainGame.GraphicsDevice, 1, 1);
            _texture.SetData(new Color[] { Color.DarkSlateGray });
            _spriteBatch.Draw(_texture, hero.GetTextureRectangle(), Color.Blue);

            _spriteBatch.Draw(_texture, hero.GetCollisionRectangle(), Color.Red);
            _spriteBatch.Draw(_texture, hero.GetNextCollisionRectangle(), Color.Yellow);


            _spriteBatch.End();
        }

        public override void Initialize()
        {
            throw new NotImplementedException();
        }

        public override void LoadContent()
        {
            scrolling1 = new Scrolling(MainGame.Content.Load<Texture2D>("Background"), new Rectangle(0, 0, 928 * 2, 793 * 2));
            scrolling2 = new Scrolling(MainGame.Content.Load<Texture2D>("Background"), new Rectangle(928 * 2, 0, 928 * 2, 793 * 2));

            var heroAnimaties = new List<Animatie>() {
                Animaties.GetIdleAnimatieFromHero(MainGame.Content),
                Animaties.GetRunAnimatieFromHero(MainGame.Content),
                Animaties.GetFallAnimatieFromHero(MainGame.Content),
                Animaties.GetJumpAnimatieFromHero(MainGame.Content),
                Animaties.GetAttack2AnimatieFromHero(MainGame.Content)
            };

            hero = new Hero(heroAnimaties);

            tilemap = new Tilemap();
            tilemap.addTiles(MainGame.Content.Load<Texture2D>("test2"), MainGame.GraphicsDevice);
            tilemap.addTiles(MainGame.Content.Load<Texture2D>("ForeGround1"), MainGame.GraphicsDevice, 1);
            tilemap.addTiles(MainGame.Content.Load<Texture2D>("Background2"), MainGame.GraphicsDevice, -1);
            tilemap.addTiles(MainGame.Content.Load<Texture2D>("Background1"), MainGame.GraphicsDevice, -2);
        }

        public override void Update(GameTime gameTime)
        {
            var idle = true;

            if (Keyboard.GetState().IsKeyDown(Keys.Q))
            {
                hero.Movement.left(hero);
                scrolling1.Update(-speed);
                scrolling2.Update(-speed);
                idle = false;
                //Left
            }

            if (Keyboard.GetState().IsKeyDown(Keys.D))
            {
                hero.Movement.right(hero);

                //hero.movement.Move(hero, speed, gameTime);
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



            if (Keyboard.GetState().IsKeyDown(Keys.Space) && hero.Movement.InAir == false)
            {

                hero.Movement.jump();

                //hero.movement.inAir = true;

            }

            if (Mouse.GetState().RightButton == ButtonState.Pressed)
            {
                hero.currentAnimation = hero.Animaties.First(x => x.AnimatieNaam == HeroAnimations.attack2);

            }

            hero.update(gameTime, tilemap);
        }
    }
}
