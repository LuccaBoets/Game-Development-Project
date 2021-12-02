﻿using GameEngine.Behavior;
using GameEngine.Characters;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace GameEngine.Environment
{
    public class Tile : ICollisionable
    {
        public TileType tileType { get; set; }

        public Vector2 position { get; set; }

        public Tile(TileType tileType, Vector2 position)
        {
            this.tileType = tileType;
            this.position = position;
        }

        public void Draw(SpriteBatch _spriteBatch, float layerDepth = 0.5f)
        {
            _spriteBatch.Draw(tileType.texture, position, tileType.texture.Bounds, Color.White, 0, Vector2.Zero, 2f, SpriteEffects.None, layerDepth);
        }

        public Rectangle GetCollsionRectangle()
        {
            var rectangle = tileType.texture.Bounds;

            rectangle.X = (int)position.X;
            rectangle.Y = (int)position.Y;
            rectangle.Width *= 2;
            rectangle.Height *= 2;

            return rectangle;
        }

        public Tuple<CollisionDirection, Rectangle> CollisionDetection(Rectangle rectangle)
        {

            Rectangle rectangle1 = this.GetCollsionRectangle();
            var temp = CollisionManager.detection(rectangle1, rectangle);
            return temp;
            //return CollisionManager.detection(this.getCollsionRectangle(), rectangle);
        }
    }
}
