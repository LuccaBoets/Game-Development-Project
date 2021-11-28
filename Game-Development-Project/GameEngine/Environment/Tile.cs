using GameEngine.Behavior;
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
        public SpriteEffects spriteEffects { get; set; }

        public Tile(TileType tileType, Vector2 position, SpriteEffects spriteEffects)
        {
            this.tileType = tileType;
            this.position = position;
            this.spriteEffects = spriteEffects;
        }

        public void draw(SpriteBatch _spriteBatch)
        {
            _spriteBatch.Draw(tileType.texture, position, tileType.texture.Bounds, Color.White, 0, Vector2.Zero, 2f, spriteEffects, 0f);
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
            if (tileType.solid)
            {
                Rectangle rectangle1 = this.GetCollsionRectangle();
                var temp = CollisionManager.detection(rectangle1, rectangle);
                return temp;
            }

            return null;
            //return CollisionManager.detection(this.getCollsionRectangle(), rectangle);
        }
    }
}
