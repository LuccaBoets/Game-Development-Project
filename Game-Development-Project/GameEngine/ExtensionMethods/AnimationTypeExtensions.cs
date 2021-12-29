using GameEngine.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GameEngine.ExtensionMethods
{
    public static class AnimationTypeExtensions
    {
        public static bool isHigherPriority(this AnimationsTypes animationsType1, AnimationsTypes animationsType2)
        {
            return animationsType1 < animationsType2;
        }

        public static bool canMove(this AnimationsTypes animationsType1)
        {
            List<AnimationsTypes> animationsTypes = new List<AnimationsTypes>() { AnimationsTypes.attack1, AnimationsTypes.attack2, AnimationsTypes.attack3, AnimationsTypes.hit, AnimationsTypes.death };
            return !animationsTypes.Contains(animationsType1);
        }
    }
}
