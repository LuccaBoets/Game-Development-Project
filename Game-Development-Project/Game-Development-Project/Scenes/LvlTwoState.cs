using GameEngine.Behavior;
using GameEngine.Charaters;
using GameEngine.Data;
using GameEngine.Environment;
using GameEngine.ExtensionMethods;
using GameEngine.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace GameEngine.Scenes
{
    public class LvlTwoState : SceneState
    {
        private Hero hero { get; set; }

        public Song song1 { get; set; }

        private List<Enemy> monsters { get; set; }
        
        private List<Scrolling> _scrollingBackgrounds;
        private Tilemap tilemap { get; set; }

        const int speed = 3;


        private MouseState lastMouseState = new MouseState();
        public LvlTwoState(MainGame game, GraphicsDeviceManager graphics, SpriteBatch spriteBatch) : base(game, graphics, spriteBatch)
        {
            LoadContent();
        }

        public override void Initialize()
        {
            throw new NotImplementedException();
        }

        public override void LoadContent()
        {
            monsters = new List<Enemy>();
            hero = new Hero(HeroAnimations.AllAnimation(Content));
            //monsters.Add(new MushroomMonster(MushroomAnimations.AllAnimation(Content), ProjectileAnimations.AllMushroomAnimation(Content), new Vector2(900, 700)));

            //monsters.Add(new SkeletonMonster(SkeletonAnimations.AllAnimation(Content), ProjectileAnimations.AllSkeletonAnimation(Content), new Vector2(900, 700)));
            monsters.Add(new GoblinMonster(GoblinAnimations.AllAnimation(Content), ProjectileAnimations.AllGoblinAnimation(Content), new Vector2(900, 700)));

            song1 = Content.Load<Song>("Adventure1");
            MediaPlayer.Volume = 0.1f;
            MediaPlayer.Play(song1);
            hero.hartjeVol = Content.Load<Texture2D>("icons/volvol");
            hero.hartjeLeeg = Content.Load<Texture2D>("icons/hartleeg");

            _scrollingBackgrounds = new List<Scrolling>()
            {
                new Scrolling(Content.Load<Texture2D>("Background/Layer_0000_9"), hero, 15f)
                {
                    Layer = 0.45f,
                },
                new Scrolling(Content.Load<Texture2D>("Background/Layer_0001_8"), hero, 15f)
                {
                    Layer = 0.46f,
                },
                new Scrolling(Content.Load<Texture2D>("Background/Layer_0002_7"), hero, 15f)
                {
                    Layer = 0.44f,
                },
                new Scrolling(Content.Load<Texture2D>("Background/Layer_0003_6"), hero, 15f)
                {
                    Layer = 0.41f,
                },
                new Scrolling(Content.Load<Texture2D>("Background/Layer_0004_Lights"), hero, 15f)
                {
                    Layer = 0.40f,
                },
                new Scrolling(Content.Load<Texture2D>("Background/Layer_0005_5"), hero, 10f)
                {
                Layer = 0.39f,
                },
                new Scrolling(Content.Load<Texture2D>("Background/Layer_0006_4"), hero, 7.5f)
                {
                Layer = 0.38f,
                },
                new Scrolling(Content.Load<Texture2D>("Background/Layer_0007_Lights"), hero, 7.5f)
                {
                    Layer = 0.37f,
                },
                new Scrolling(Content.Load<Texture2D>("Background/Layer_0008_3"), hero, 4f)
                {
                    Layer = 0.36f,
                },
                new Scrolling(Content.Load<Texture2D>("Background/Layer_0009_2"), hero, 2f)
                {
                    Layer = 0.35f,
                },
                new Scrolling(Content.Load<Texture2D>("Background/Layer_0010_1"), hero, 0f)
                {
                    Layer = 0.31f,
                },
                new Scrolling(Content.Load<Texture2D>("Background/Layer_0011_0"), hero, 0f)
                {
                    Layer = 0.30f,
                },
                new Scrolling(Content.Load<Texture2D>("Background/CloudsLayer"), hero, 5f)
                {
                    Layer = 0.34f,
                },
                 new Scrolling(Content.Load<Texture2D>("Background/SunLayer5"), hero, 0f)
                {
                    Layer = 0.10f,
                },


            };

            //tilemap = new Tilemap();
            //tilemap.addTiles(MainGame.Content.Load<Texture2D>("lvl2.0"), MainGame.GraphicsDevice);
            //tilemap.addTiles(MainGame.Content.Load<Texture2D>("lvl2.1"), MainGame.GraphicsDevice, 1);
            //tilemap.addTiles(MainGame.Content.Load<Texture2D>("lvl2.-1"), MainGame.GraphicsDevice, -1);
            //tilemap.addTiles(MainGame.Content.Load<Texture2D>("lvl2.-2"), MainGame.GraphicsDevice, -2);

            TileFactory.load(GraphicsDevice, Content.Load<Texture2D>("Tilemap/Castle/ExportedTileSet"));
            using (FileStream fs = File.OpenRead(@"../../../Content/Tilemap/Castle/ExportedTilemapData.txt"))
            {
                tilemap = new Tilemap(fs);
            }
        }

        public override void Update(GameTime gameTime)
        {

            MouseState currentState = Mouse.GetState();

            if (Keyboard.GetState().IsKeyDown(Keys.Q))
            {
                hero.Movement.left(hero);
            }

            if (Keyboard.GetState().IsKeyDown(Keys.D))
            {
                hero.Movement.right(hero);
            }

            if (Mouse.GetState().LeftButton == ButtonState.Pressed && currentState.LeftButton == ButtonState.Pressed &&
        lastMouseState.LeftButton == ButtonState.Released)
            {
                //hero.attack1(this.Monsters);

                hero.changeAnimation(AnimationsTypes.attack1);
                
            }
            else if (Keyboard.GetState().IsKeyDown(Keys.A))
            {
                hero.changeAnimation(AnimationsTypes.hit);
            }
            else if (Mouse.GetState().RightButton == ButtonState.Pressed)
            {
                hero.changeAnimation(AnimationsTypes.attack2);
            }
            else
            {
                hero.changeAnimation(AnimationsTypes.idle);
            }

            if (Keyboard.GetState().IsKeyDown(Keys.Space) && hero.Movement.InAir == false)
            {
                hero.Movement.jump();
            }

            if (Keyboard.GetState().IsKeyDown(Keys.L))
            {
                TileFactory.Save(GraphicsDevice);
                using (FileStream fs = File.Create(@"ExportedTilemapData.txt"))
                {
                    tilemap.Save(fs);
                }
            }

            lastMouseState = currentState;

            foreach (var sb in _scrollingBackgrounds)
            {
                sb.Update(gameTime);


            }

            hero.update(gameTime, tilemap, monsters);

            foreach (var monster in this.monsters)
            {
                monster.Update(gameTime, hero, tilemap);
            }

            if (hero.isDead)
            {
                MainGame.ChangeSceneState(new DeathState(MainGame, _graphics, _spriteBatch));
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

            const int Width = 50 * 2;
            const int Height = 29 * 2;
            const int yOffset = 0;

            //var attackCollsionRectangle = monsters[0].GetCollisionRectangle();
            //var attackCollsionRectangle = new Rectangle(monsters[0].GetCollisionRectangle().Left - Width, monsters[0].GetCollisionRectangle().Top + yOffset, Width, Height);

            var Texture2D = new Texture2D(MainGame.GraphicsDevice, 1, 1);
            Texture2D.SetData(new[] { Color.Red });
            //_spriteBatch.Draw(Texture2D, attackCollsionRectangle.Location.ToVector2(), attackCollsionRectangle, Color.Yellow, 0, Vector2.Zero, 1f, SpriteEffects.None, 0.49f);
            //_spriteBatch.Draw(Texture2D, hero.GetCollisionRectangle().Location.ToVector2(), hero.GetCollisionRectangle(), Color.Yellow, 0, Vector2.Zero, 1f, SpriteEffects.None, 0.9f);

            hero.draw(_spriteBatch);

            foreach (var monster in this.monsters)
            {
                monster.Draw(_spriteBatch);
            }

            tilemap.draw(_spriteBatch, getScreen());


            foreach (var sb in _scrollingBackgrounds)
            {
                sb.Draw(_spriteBatch);
            }
            _spriteBatch.End();
        }

        public Rectangle getScreen()
        {
            var position = new Point(hero.GetCollisionRectangle().Center.X - Settings.ScreenW / 2, hero.GetCollisionRectangle().Center.Y - Settings.ScreenH / 2);

            return new Rectangle(position, new Point(Settings.ScreenW, Settings.ScreenH));
        }
    }
}
