﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace GameDevelopmentProject
{
    public class Animatie
    {
        public HeroAnimations AnimatieNaam { get; set; }

        public Texture2D texture { get; set; }

        public List<AnimatieFrame> frames { get; set; }

        public AnimatieFrame currentFrame { get; set; }

        public double ElapsedGameTime { get; set; }

        public int count { get; set; }

        public Animatie(List<AnimatieFrame> frames, Texture2D texture)
        {
            this.frames = frames;
            this.texture = texture;

            currentFrame = frames[0];
            count = 0;
        }

        public Animatie(Texture2D texture)
        {
            this.texture = texture;
            this.frames = new List<AnimatieFrame>();
            count = 0;
        }

        public void addFrame(AnimatieFrame animatieFrame)
        {
            this.frames.Add(animatieFrame);
            currentFrame = frames[0];
        }

        public void update(GameTime gameTime)
        {
            ElapsedGameTime += gameTime.ElapsedGameTime.TotalMilliseconds;

            if (ElapsedGameTime >= 200)
            {
                count++;
                ElapsedGameTime = 0;
            }

            if (count > 7)
            {
                count = 0;
            }

            currentFrame = frames[count];
        }
    }
}
