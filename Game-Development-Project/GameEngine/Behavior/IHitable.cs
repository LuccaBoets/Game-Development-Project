using GameEngine.Characters;
using System;
using System.Collections.Generic;
using System.Text;

namespace GameEngine.Behavior
{
    public interface IHitable : ICollisionable
    {
        public Stats stats { get; set; }

        public double invisibleTimer { get; set; }
        public bool invisible { get; set; }
        public void Hit(int damage);
    }
}
