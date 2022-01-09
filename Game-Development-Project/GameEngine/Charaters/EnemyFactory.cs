using GameEngine.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using System;
using System.Collections.Generic;
using System.Text;

namespace GameEngine.Charaters
{
    public static class EnemyFactory
    {
        public static Enemy CreateEnemy(string monsterType, List<Animatie> animaties, List<Animatie> projectileAnimation, Vector2 newPosition, SoundEffect effect)
        {

            try
            {
                return (Enemy)Activator.CreateInstance(Type.GetType($"GameEngine.Charaters.{monsterType}"), new Object[] { animaties, projectileAnimation, newPosition, effect });
            }
            catch (Exception e)
            {

                throw;
            }
        }
    }
}
