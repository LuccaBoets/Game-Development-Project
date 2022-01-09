using GameEngine.Environment;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using System;
using System.Collections.Generic;
using System.Text;

namespace GameEngine.Scenes
{
    public class MenuState : SceneState
    {

        List<Background> background2;
        Background backgroundCharacterSelect;
        public Song songMenu { get; set; }

        Texture2D image;
        Texture2D imageTitel;
        int count = 0;
        public double ElapsedGameTime { get; set; }
        public MenuState(MainGame game, GraphicsDeviceManager graphics, SpriteBatch spriteBatch) : base(game, graphics, spriteBatch)
        {
            LoadContent();
        }

        public override void Draw(GameTime gameTime)
        {
            _spriteBatch.Begin(SpriteSortMode.FrontToBack, null, SamplerState.PointClamp);
            ElapsedGameTime += gameTime.ElapsedGameTime.TotalMilliseconds;
            background2[count].Draw(_spriteBatch);
            if (ElapsedGameTime >= 120)
            {
                count++;
                ElapsedGameTime = 0;
            }

            if (count >= 39) 
            {
                count = 0;
            }
            _spriteBatch.Draw(imageTitel, new Vector2(310, 100), imageTitel.Bounds, Color.White, 0, Vector2.Zero, 1.2f, SpriteEffects.None, 0f);
            _spriteBatch.Draw(image, new Vector2(510,700), image.Bounds, Color.White, 0, Vector2.Zero, 2f, SpriteEffects.None, 0f);

            _spriteBatch.End();
        }

        public override void Initialize()
        {
            
            throw new NotImplementedException();
        }

        public override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(MainGame.GraphicsDevice);
            background2 = new List<Background>();

            songMenu = Content.Load<Song>("Sound/MenuSong");
            MediaPlayer.Volume = 0.1f;
            MediaPlayer.IsRepeating = true;
            MediaPlayer.Play(songMenu);
            for (int i = 0; i < 39; i++)
            {
                background2.Add(new Background(MainGame.Content.Load<Texture2D>("startScreen/startScreen-" + i), new Rectangle(0, 0, 1600, 900)));
            }

            imageTitel = MainGame.Content.Load<Texture2D>("Text/TitelNieuw");
            image = MainGame.Content.Load<Texture2D>("Text/PressEnter2");
        }

        public override void Update(GameTime gameTime)
        {
            if (Keyboard.GetState().IsKeyDown(Keys.Enter))
            { 
                MainGame.ChangeSceneState(new DeathState(MainGame, _graphics, _spriteBatch));

            }

        }
    }
}
