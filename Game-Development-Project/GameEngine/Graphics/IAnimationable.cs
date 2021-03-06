using System;
using System.Collections.Generic;
using System.Text;

namespace GameEngine.Graphics
{
    public interface IAnimationable
    {
        public Animatie currentAnimation { get; set; }
        public List<Animatie> Animaties { get; set; }
        public bool lookingLeft { get; set; }

        public void changeAnimation(AnimationsTypes animationsTypes, bool ignorePriority = false);

        public void endOfAnimation();
    }
}
