using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace GameEngine.Graphics
{
    public enum AnimationsTypes
    {
        idle,
        run,
        attack1,
        attack2,
        attack3,
        jump,
        fall,
        hit,
        death,
    }

    public class Animatie
    {
        public AnimationsTypes AnimatieNaam { get; set; }

        public Texture2D texture { get; set; }

        public List<AnimatieFrame> frames { get; set; }

        public AnimatieFrame currentFrame { get; set; }

        public double ElapsedGameTime { get; set; }

        public int count { get; set; }

        public Vector2 offset { get; set; }

        public Vector2 bounds { get; set; }

        public bool isFinished { get; set; }

        public Animatie(List<AnimatieFrame> frames, Texture2D texture)
        {
            this.frames = frames;
            this.texture = texture;
            this.offset = new Vector2();

            currentFrame = frames[0];
            bounds = new Vector2(frames[0].borders.Width, frames[0].borders.Height);
            count = 0;
            isFinished = false;
        }

        public Animatie(Texture2D texture)
        {
            this.texture = texture;
            this.frames = new List<AnimatieFrame>();
            this.offset = new Vector2();

            count = 0;
            isFinished = false;
        }

        public void addFrame(AnimatieFrame animatieFrame)
        {
            this.frames.Add(animatieFrame);
            currentFrame = frames[0];
            bounds = new Vector2(frames[0].borders.Width, frames[0].borders.Height);
        }

        public void update(GameTime gameTime)
        {
            ElapsedGameTime += gameTime.ElapsedGameTime.TotalMilliseconds;

            if (ElapsedGameTime >= 200)
            {
                count++;
                ElapsedGameTime = 0;
            }


            if (count > frames.Count - 1)
            {
                count = 0;
                isFinished = true;
            }

            currentFrame = frames[count];
        }

        public void reset()
        {
            count = 0;
            isFinished = false;
        }
    }
}
