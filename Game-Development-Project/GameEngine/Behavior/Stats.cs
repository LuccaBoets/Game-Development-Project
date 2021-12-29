using System;
using System.Collections.Generic;
using System.Text;

namespace GameEngine.Behavior
{
    public class Stats
    {
        public int health { get; set; }
        public int damage { get; set; }

        public Stats(int health, int damage)
        {
            this.health = health;
            this.damage = damage;
        }
    }
}
