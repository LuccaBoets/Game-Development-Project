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

        private List<Scrolling> _scrollingBackgrounds;
        private Tilemap tilemap { get; set; }


        const int speed = 3;

        public LvlOneState(MainGame game, GraphicsDeviceManager graphics, SpriteBatch spriteBatch) : base(game, graphics, spriteBatch)
        {
            LoadContent();
        }

        public override void Initialize()
        {
            throw new NotImplementedException();
        }

        public override void LoadContent()
        {
      

            var heroAnimaties = new List<Animatie>() {
                Animaties.GetIdleAnimatieFromHero(MainGame.Content),
                Animaties.GetRunAnimatieFromHero(MainGame.Content),
                Animaties.GetFallAnimatieFromHero(MainGame.Content),
                Animaties.GetJumpAnimatieFromHero(MainGame.Content),
                Animaties.GetAttack1FromHero(MainGame.Content), 
                Animaties.GetHitFromHero(MainGame.Content),
                Animaties.GetAttack2AnimatieFromHero(MainGame.Content)

            };

            hero = new Hero(heroAnimaties);

            _scrollingBackgrounds = new List<Scrolling>()
            {
                new Scrolling(MainGame.Content.Load<Texture2D>("Background/Layer_0000_9"), hero, 60f)
                {
                    Layer = 0.47f,
                },
                new Scrolling(MainGame.Content.Load<Texture2D>("Background/Layer_0001_8"), hero, 60f)
                {
                    Layer = 0.47f,
                },
                new Scrolling(MainGame.Content.Load<Texture2D>("Background/Layer_0002_7"), hero, 60f)
                {
                    Layer = 0.47f,
                },
                new Scrolling(MainGame.Content.Load<Texture2D>("Background/Layer_0003_6"), hero, 60f)
                {
                    Layer = 0.40f,
                },
                new Scrolling(MainGame.Content.Load<Texture2D>("Background/Layer_0004_Lights"), hero, 60f)
                {
                    Layer = 0.40f,
                },
                new Scrolling(MainGame.Content.Load<Texture2D>("Background/Layer_0005_5"), hero, 40f)
                {
                Layer = 0.39f,
                },
                new Scrolling(MainGame.Content.Load<Texture2D>("Background/Layer_0006_4"), hero, 30f)
                {
                Layer = 0.39f,
                },
                new Scrolling(MainGame.Content.Load<Texture2D>("Background/Layer_0007_Lights"), hero, 30f)
                {
                    Layer = 0.39f,
                },
                new Scrolling(MainGame.Content.Load<Texture2D>("Background/Layer_0008_3"), hero, 15f)
                {
                    Layer = 0.37f,
                },
                new Scrolling(MainGame.Content.Load<Texture2D>("Background/Layer_0009_2"), hero, 10f)
                {
                    Layer = 0.35f,
                },
                new Scrolling(MainGame.Content.Load<Texture2D>("Background/Layer_0010_1"), hero, 0f)
                {
                    Layer = 0.30f,
                },
                new Scrolling(MainGame.Content.Load<Texture2D>("Background/Layer_0011_0"), hero, 0f)
                {
                    Layer = 0.30f,
                },
                new Scrolling(MainGame.Content.Load<Texture2D>("Background/CloudsLayer"), hero, 20f,true)
                {
                    Layer = 0.35f,
                },
                 new Scrolling(MainGame.Content.Load<Texture2D>("Background/SunLayer5"), hero, 0f)
                {
                    Layer = 0.10f,
                },


            };

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
          
                idle = false;
                //Left
            }

            if (Keyboard.GetState().IsKeyDown(Keys.D))
            {
                hero.Movement.right(hero);

                //hero.movement.Move(hero, speed, gameTime);
            
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

            if (Mouse.GetState().LeftButton == ButtonState.Pressed)
            {

                hero.currentAnimation = hero.Animaties.First(x => x.AnimatieNaam == HeroAnimations.attack1);


            }

            if (Mouse.GetState().RightButton == ButtonState.Pressed)
            {

                hero.currentAnimation = hero.Animaties.First(x => x.AnimatieNaam == HeroAnimations.hit);


            }


            if (Keyboard.GetState().IsKeyDown(Keys.Space) && hero.Movement.InAir == false)
            {

                hero.Movement.jump();

                //hero.movement.inAir = true;

            }

            foreach (var sb in _scrollingBackgrounds)
            {
                sb.Update(gameTime);


            }
            //base.Update(gameTime);

            if (Mouse.GetState().RightButton == ButtonState.Pressed)
            {
                hero.currentAnimation = hero.Animaties.First(x => x.AnimatieNaam == HeroAnimations.attack2);

            }

            hero.update(gameTime, tilemap);
        }
        public override void Draw(GameTime gameTime)
        {
            var position = Matrix.CreateTranslation(
                  -hero.position.X - (hero.GetCollisionRectangle().Width / 2),
                  -hero.position.Y - (hero.GetCollisionRectangle().Height / 2),
                  0);


            var offset = Matrix.CreateTranslation(
                Settings.ScreenW / 2,
                Settings.ScreenH / 2,
                0);



            var Transform = position * offset;

            _spriteBatch.Begin(SpriteSortMode.FrontToBack, null, SamplerState.PointClamp, transformMatrix: Transform);



            hero.draw(_spriteBatch);
            tilemap.draw(_spriteBatch);

        
            foreach (var sb in _scrollingBackgrounds)
            {
                sb.Draw(gameTime, _spriteBatch);


            }
            //base.Draw(gameTime);
            _spriteBatch.End();
        }

    }
}
