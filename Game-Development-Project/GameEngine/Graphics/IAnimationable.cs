using System;
using System.Collections.Generic;
using System.Text;

namespace GameEngine.Graphics
{
    public interface IAnimationable
    {
        public Animatie currentAnimation { get; set; }
        public List<Animatie> Animaties { get; set; }
        public bool lookingRight { get; set; }
    }
}
