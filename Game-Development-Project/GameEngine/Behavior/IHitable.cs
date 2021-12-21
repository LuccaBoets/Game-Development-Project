using GameEngine.Characters;
using System;
using System.Collections.Generic;
using System.Text;

namespace GameEngine.Behavior
{
    public interface IHitable : ICollisionable
    {
        public void hit();
    }
}
