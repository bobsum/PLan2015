using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Bismuth.Framework.Animations.EasingFunctions
{
    public class StepEase : EasingFunctionBase
    {
        protected override float EaseIn(float normalizedTime)
        {
            return normalizedTime < 1 ? 0 : 1;
            // Or ???
            //return 0;
        }
    }
}
