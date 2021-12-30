using GameEngine.Behavior;
using GameEngine.Characters;
using GameEngine.Data;
using GameEngine.Environment;
using GameEngine.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace GameEngine.Charaters
{
    public abstract class Enemy : ICollisionable, IAnimationable, IHitable, IMoveable
    {
        public abstract Animatie currentAnimation { get; set; }
        public abstract List<Animatie> Animaties { get; set; }
        public abstract bool lookingLeft { get; set; }
        public abstract Stats stats { get; set; }
        public abstract double invisibleTimer { get; set; }
        public abstract bool invisible { get; set; }
        public abstract Movement Movement { get; set; }
        public abstract Vector2 position { get; set; }

        public abstract void Draw(SpriteBatch spriteBacth);

        public abstract void Update(GameTime gameTime, Hero hero, Tilemap tilemap);

        public abstract void changeAnimation(AnimationsTypes animationsTypes, bool ignorePriority = false);

        public abstract Tuple<CollisionDirection, Rectangle> CollisionDetection(Rectangle rectangle);

        public abstract Rectangle GetMonsterRangeRectangle();
        public abstract Rectangle GetCollisionRectangle();

        public abstract Rectangle GetNextCollisionRectangle();
        public abstract void Hit(int damage);
        public abstract void endOfAnimation();
    }
}
