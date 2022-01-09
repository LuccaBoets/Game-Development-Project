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
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;

namespace GameEngine.Scenes
{
    public class LvlTwoState : SceneState
    {
        private Hero hero { get; set; }

        public Song song2 { get; set; }

        public Song song2Battle { get; set; }
        private bool muziekBattle2 = false;

        public Enemy boss1 { get; set; }
        public Enemy boss2 { get; set; }
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

            SoundEffect deathSkeleton = Content.Load<SoundEffect>("Sound/SkeletonShatter");
            SoundEffect deathEye = Content.Load<SoundEffect>("Sound/monster-15");
            hero = new Hero(HeroAnimations.AllAnimation(Content), new Vector2(1353, 1450));

            monsters.Add(new Boss2(SkeletonAnimations.AllAnimation(Content), ProjectileAnimations.AllSkeletonAnimation(Content), new Vector2(11344, 1518),deathSkeleton));
            monsters.Add(new Boss2(SkeletonAnimations.AllAnimation(Content), ProjectileAnimations.AllSkeletonAnimation(Content), new Vector2(12147, 1518),deathSkeleton));
            boss1 = monsters[0];
            boss2 = monsters[1];

            monsters.Add(EnemyFactory.CreateEnemy("SkeletonMonster", SkeletonAnimations.AllAnimation(Content), ProjectileAnimations.AllSkeletonAnimation(Content), new Vector2(1211, 1828), deathSkeleton));
            monsters.Add(EnemyFactory.CreateEnemy("SkeletonMonster", SkeletonAnimations.AllAnimation(Content), ProjectileAnimations.AllSkeletonAnimation(Content), new Vector2(2270, 1828), deathSkeleton));
            monsters.Add(EnemyFactory.CreateEnemy("SkeletonMonster", SkeletonAnimations.AllAnimation(Content), ProjectileAnimations.AllSkeletonAnimation(Content), new Vector2(3433, 2532), deathSkeleton));
            monsters.Add(EnemyFactory.CreateEnemy("SkeletonMonster", SkeletonAnimations.AllAnimation(Content), ProjectileAnimations.AllSkeletonAnimation(Content), new Vector2(3646, 2532), deathSkeleton));
            monsters.Add(EnemyFactory.CreateEnemy("SkeletonMonster", SkeletonAnimations.AllAnimation(Content), ProjectileAnimations.AllSkeletonAnimation(Content), new Vector2(4124, 2298), deathSkeleton));
            monsters.Add(EnemyFactory.CreateEnemy("SkeletonMonster", SkeletonAnimations.AllAnimation(Content), ProjectileAnimations.AllSkeletonAnimation(Content), new Vector2(4165, 2042), deathSkeleton));
            monsters.Add(EnemyFactory.CreateEnemy("SkeletonMonster", SkeletonAnimations.AllAnimation(Content), ProjectileAnimations.AllSkeletonAnimation(Content), new Vector2(4095, 1828), deathSkeleton));
            monsters.Add(EnemyFactory.CreateEnemy("SkeletonMonster", SkeletonAnimations.AllAnimation(Content), ProjectileAnimations.AllSkeletonAnimation(Content), new Vector2(4568, 1828), deathSkeleton));
            monsters.Add(EnemyFactory.CreateEnemy("SkeletonMonster", SkeletonAnimations.AllAnimation(Content), ProjectileAnimations.AllSkeletonAnimation(Content), new Vector2(5342, 1828), deathSkeleton));
            monsters.Add(EnemyFactory.CreateEnemy("SkeletonMonster", SkeletonAnimations.AllAnimation(Content), ProjectileAnimations.AllSkeletonAnimation(Content), new Vector2(5982, 1828), deathSkeleton));
            monsters.Add(EnemyFactory.CreateEnemy("SkeletonMonster", SkeletonAnimations.AllAnimation(Content), ProjectileAnimations.AllSkeletonAnimation(Content), new Vector2(6808, 1828), deathSkeleton));
            monsters.Add(EnemyFactory.CreateEnemy("SkeletonMonster", SkeletonAnimations.AllAnimation(Content), ProjectileAnimations.AllSkeletonAnimation(Content), new Vector2(7967, 1828), deathSkeleton));
            monsters.Add(EnemyFactory.CreateEnemy("SkeletonMonster", SkeletonAnimations.AllAnimation(Content), ProjectileAnimations.AllSkeletonAnimation(Content), new Vector2(9240, 1828), deathSkeleton));
            monsters.Add(EnemyFactory.CreateEnemy("SkeletonMonster", SkeletonAnimations.AllAnimation(Content), ProjectileAnimations.AllSkeletonAnimation(Content), new Vector2(8567, 900), deathSkeleton));
            monsters.Add(EnemyFactory.CreateEnemy("FlyingEyeMonster", FlyingEyeAnimations.AllAnimation(Content), ProjectileAnimations.AllFlyingEyeAnimation(Content), new Vector2(8305, 1246), deathEye));
            monsters.Add(EnemyFactory.CreateEnemy("FlyingEyeMonster", FlyingEyeAnimations.AllAnimation(Content), ProjectileAnimations.AllFlyingEyeAnimation(Content), new Vector2(8075, 1182), deathEye));
            monsters.Add(EnemyFactory.CreateEnemy("FlyingEyeMonster", FlyingEyeAnimations.AllAnimation(Content), ProjectileAnimations.AllFlyingEyeAnimation(Content), new Vector2(8000, 1282), deathEye));
            monsters.Add(EnemyFactory.CreateEnemy("FlyingEyeMonster", FlyingEyeAnimations.AllAnimation(Content), ProjectileAnimations.AllFlyingEyeAnimation(Content), new Vector2(7317, 1094), deathEye));
            monsters.Add(EnemyFactory.CreateEnemy("FlyingEyeMonster", FlyingEyeAnimations.AllAnimation(Content), ProjectileAnimations.AllFlyingEyeAnimation(Content), new Vector2(8367, 820), deathEye));
            monsters.Add(EnemyFactory.CreateEnemy("FlyingEyeMonster", FlyingEyeAnimations.AllAnimation(Content), ProjectileAnimations.AllFlyingEyeAnimation(Content), new Vector2(8667, 820), deathEye));
            monsters.Add(EnemyFactory.CreateEnemy("FlyingEyeMonster", FlyingEyeAnimations.AllAnimation(Content), ProjectileAnimations.AllFlyingEyeAnimation(Content), new Vector2(8567, 850), deathEye));
            monsters.Add(EnemyFactory.CreateEnemy("FlyingEyeMonster", FlyingEyeAnimations.AllAnimation(Content), ProjectileAnimations.AllFlyingEyeAnimation(Content), new Vector2(9540, 1166), deathEye));
            monsters.Add(EnemyFactory.CreateEnemy("FlyingEyeMonster", FlyingEyeAnimations.AllAnimation(Content), ProjectileAnimations.AllFlyingEyeAnimation(Content), new Vector2(9240, 1638), deathEye));

            song2 = Content.Load<Song>("Sound/lvl2");
            song2Battle = Content.Load<Song>("Sound/FinalBossBattle");
            MediaPlayer.Volume = 0.1f;
            MediaPlayer.Stop();
            MediaPlayer.Play(song2);
            hero.hartjeVol = Content.Load<Texture2D>("icons/volvol");
            hero.hartjeLeeg = Content.Load<Texture2D>("icons/hartleeg");

            _scrollingBackgrounds = new List<Scrolling>()
            {
                new Scrolling(Content.Load<Texture2D>("Background/background_night3"), hero, 10f)
                {
                    Layer = 0.45f,
                },

                new Scrolling(Content.Load<Texture2D>("Background/background_night2"), hero, 2f)
                {
                    Layer = 0.35f,
                },
                new Scrolling(Content.Load<Texture2D>("Background/background_night1"), hero, 0f)
                {
                    Layer = 0.31f,
                },
                 new Scrolling(Content.Load<Texture2D>("Background/SunLayer5"), hero, 0f)
                {
                    Layer = 0.10f,
                },


            };

            //tilemap = new Tilemap();
            //tilemap.addTiles(MainGame.Content.Load<Texture2D>("Temp/lvl2.0"), MainGame.GraphicsDevice);
            //tilemap.addTiles(MainGame.Content.Load<Texture2D>("Temp/lvl2.1"), MainGame.GraphicsDevice, 1);
            //tilemap.addTiles(MainGame.Content.Load<Texture2D>("Temp/lvl2.2"), MainGame.GraphicsDevice, 2);
            //tilemap.addTiles(MainGame.Content.Load<Texture2D>("Temp/lvl2.-1"), MainGame.GraphicsDevice, -1);
            //tilemap.addTiles(MainGame.Content.Load<Texture2D>("Temp/lvl2.-2"), MainGame.GraphicsDevice, -2);

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
                Debug.WriteLine($"X:{hero.position.X}, Y:{hero.position.Y}");
                hero.changeAnimation(AnimationsTypes.attack1);
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

            foreach (var monster in this.monsters.Where(x => CollisionManager.Detection(x.GetMonsterRangeRectangle(), getScreen())))
            {
                monster.Update(gameTime, hero, tilemap);
            }


            if (hero.position.X > 9570 && muziekBattle2 == false)
            {
                muziekBattle2 = true;
                MediaPlayer.Stop();
                MediaPlayer.Volume = 0.5f;
                MediaPlayer.Play(song2Battle);
            }


            if (hero.isDead)
            {
                MainGame.ChangeSceneState(new DeathState(MainGame, _graphics, _spriteBatch));
            }




            if (boss1.isDead && boss2.isDead)
            {
                MainGame.ChangeSceneState(new VictoryState(MainGame, _graphics, _spriteBatch));
            }
        }

        public override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(new Color(23, 22, 22));

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

            foreach (var monster in this.monsters.Where(x => CollisionManager.Detection(x.GetMonsterRangeRectangle(), getScreen())))
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
