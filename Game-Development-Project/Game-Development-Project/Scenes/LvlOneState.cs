﻿using GameEngine.Behavior;
using GameEngine.Charaters;
using GameEngine.Data;
using GameEngine.Environment;
using GameEngine.ExtensionMethods;
using GameEngine.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace GameEngine.Scenes
{
    public class LvlOneState : SceneState
    {
        private Hero hero { get; set; }

        private List<Enemy> Monsters { get; set; }

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
            Monsters = new List<Enemy>();
            hero = new Hero(HeroAnimations.AllAnimation(Content));
            Monsters.Add(new MushroomMonster(MushroomAnimations.AllAnimation(Content), new Vector2(900, 950), 500));


            _scrollingBackgrounds = new List<Scrolling>()
            {
                new Scrolling(Content.Load<Texture2D>("Background/Layer_0000_9"), hero, 60f)
                {
                    Layer = 0.47f,
                },
                new Scrolling(Content.Load<Texture2D>("Background/Layer_0001_8"), hero, 60f)
                {
                    Layer = 0.47f,
                },
                new Scrolling(Content.Load<Texture2D>("Background/Layer_0002_7"), hero, 60f)
                {
                    Layer = 0.47f,
                },
                new Scrolling(Content.Load<Texture2D>("Background/Layer_0003_6"), hero, 60f)
                {
                    Layer = 0.40f,
                },
                new Scrolling(Content.Load<Texture2D>("Background/Layer_0004_Lights"), hero, 60f)
                {
                    Layer = 0.40f,
                },
                new Scrolling(Content.Load<Texture2D>("Background/Layer_0005_5"), hero, 40f)
                {
                Layer = 0.39f,
                },
                new Scrolling(Content.Load<Texture2D>("Background/Layer_0006_4"), hero, 30f)
                {
                Layer = 0.39f,
                },
                new Scrolling(Content.Load<Texture2D>("Background/Layer_0007_Lights"), hero, 30f)
                {
                    Layer = 0.39f,
                },
                new Scrolling(Content.Load<Texture2D>("Background/Layer_0008_3"), hero, 15f)
                {
                    Layer = 0.37f,
                },
                new Scrolling(Content.Load<Texture2D>("Background/Layer_0009_2"), hero, 10f)
                {
                    Layer = 0.35f,
                },
                new Scrolling(Content.Load<Texture2D>("Background/Layer_0010_1"), hero, 0f)
                {
                    Layer = 0.30f,
                },
                new Scrolling(Content.Load<Texture2D>("Background/Layer_0011_0"), hero, 0f)
                {
                    Layer = 0.30f,
                },
                new Scrolling(Content.Load<Texture2D>("Background/CloudsLayer"), hero, 20f)
                {
                    Layer = 0.35f,
                },
                 new Scrolling(Content.Load<Texture2D>("Background/SunLayer5"), hero, 0f)
                {
                    Layer = 0.10f,
                },


            };

            //tilemap = new Tilemap();
            //tilemap.addTiles(MainGame.Content.Load<Texture2D>("lvl1.0"), MainGame.GraphicsDevice);
            //tilemap.addTiles(MainGame.Content.Load<Texture2D>("lvl1.1"), MainGame.GraphicsDevice, 1);
            //tilemap.addTiles(MainGame.Content.Load<Texture2D>("lvl1.2"), MainGame.GraphicsDevice, 2);
            //tilemap.addTiles(MainGame.Content.Load<Texture2D>("lvl1.3"), MainGame.GraphicsDevice, 3);
            //tilemap.addTiles(MainGame.Content.Load<Texture2D>("lvl1.-1"), MainGame.GraphicsDevice, -1);
            //tilemap.addTiles(MainGame.Content.Load<Texture2D>("lvl1.-2"), MainGame.GraphicsDevice, -2);


            TileFactory.load(GraphicsDevice, Content.Load<Texture2D>("ExportedTileSet"));
            using (FileStream fs = File.OpenRead(@"../../../Content/ExportedTilemapData.txt"))
            {
                tilemap = new Tilemap(fs);
            }

            //TileFactory.load(MainGame.GraphicsDevice, MainGame.Content.Load<Texture2D>("ExportedTileSet"));
            //using (FileStream fs = File.OpenRead(@"ExportedTilemapData.txt"))
            //{
            //    tilemap = new Tilemap(fs);
            //}
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
                //hero.position += new Vector2(0.0f, 3.0f);

            }


            if (Keyboard.GetState().IsKeyDown(Keys.Z))
            {

                idle = false;
                //Up
                //hero.position += new Vector2(0.0f, -3.0f);
            }

            if (idle)
            {
                hero.changeAnimation(AnimationsTypes.idle);
            }

            if (Mouse.GetState().LeftButton == ButtonState.Pressed)
            {
                idle = false;
                hero.attack1(this.Monsters);

                hero.changeAnimation(AnimationsTypes.attack1);

            }

            if (Keyboard.GetState().IsKeyDown(Keys.A))
            {
                hero.changeAnimation(AnimationsTypes.hit);

            }


            if (Keyboard.GetState().IsKeyDown(Keys.Space) && hero.Movement.InAir == false)
            {

                hero.Movement.jump();

                //hero.movement.inAir = true;

            }

            if (Keyboard.GetState().IsKeyDown(Keys.L))
            {
                TileFactory.Save(GraphicsDevice);
                using (FileStream fs = File.Create(@"ExportedTilemapData.txt"))
                {
                    tilemap.Save(fs);
                }
            }

            foreach (var sb in _scrollingBackgrounds)
            {
                sb.Update(gameTime);


            }
            //base.Update(gameTime);

            if (Mouse.GetState().RightButton == ButtonState.Pressed)
            {
                hero.changeAnimation(AnimationsTypes.attack2);
            }

            hero.update(gameTime, tilemap);


            foreach (var monster in this.Monsters)
            {
                monster.Update(gameTime, hero);
            }

        }
        public override void Draw(GameTime gameTime)
        {
            var position = Matrix.CreateTranslation(
                  -hero.GetCollisionRectangle().Center.X,
                  -hero.GetCollisionRectangle().Center.Y,
                  0);


            var offset = Matrix.CreateTranslation(
                Settings.ScreenW / 2,
                Settings.ScreenH / 2,
                0);

            var Transform = position * offset;
            
            _spriteBatch.Begin(SpriteSortMode.FrontToBack, null, SamplerState.PointClamp, transformMatrix: Transform);

            //var Texture2D = new Texture2D(MainGame.GraphicsDevice, 1, 1);
            //Texture2D.SetData(new[] { Color.Red });
            //_spriteBatch.Draw(Texture2D, new Vector2(hero.GetCollisionRectangle().Right,hero.GetCollisionRectangle().Top+10), new Rectangle(0, 0, 54 * 2, 36 * 2), Color.Red, 0, Vector2.Zero, 1f, SpriteEffects.None, 0.9f);
            //// new Vector2(hero.position.X+10,hero.position.Y+36*2),

            //_spriteBatch.Draw(Texture2D, hero.GetCollisionRectangle().Location.ToVector2(), hero.GetCollisionRectangle(), Color.Yellow, 0, Vector2.Zero, 1f, SpriteEffects.None, 0.9f);

            hero.draw(_spriteBatch);

            foreach (var monster in this.Monsters)
            {
                monster.Draw(_spriteBatch);
            }

            tilemap.draw(_spriteBatch);

        
            foreach (var sb in _scrollingBackgrounds)
            {
                sb.Draw(_spriteBatch);
            }
            _spriteBatch.End();
        }

    }
}
