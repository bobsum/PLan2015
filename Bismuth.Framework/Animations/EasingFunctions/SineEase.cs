using System;
using Microsoft.Xna.Framework;

namespace Bismuth.Framework.Animations.EasingFunctions
{
    public class SineEase : EasingFunctionBase
    {
        protected override float EaseIn(float normalizedTime)
        {
            return 1.0f - (float)Math.Sin(MathHelper.PiOver2 * (1.0f - normalizedTime));
        }

        //public override float Ease(float elapsedTime, float startValue, float totalValueChange, float totalDuration)
        //{
        //    if (EasingMode == EasingMode.EaseOut)
        //    {
        //        double d = (elapsedTime / totalDuration) * MathHelper.PiOver2;
        //        return totalValueChange * (float)Math.Sin(d) + startValue;
        //    }
        //    else if (EasingMode == EasingMode.EaseIn)
        //    {
        //        double d = (elapsedTime / totalDuration) * MathHelper.PiOver2;
        //        return totalValueChange * (1.0f - (float)Math.Cos(d)) + startValue;
        //    }
        //    else
        //    {
        //        double d = (elapsedTime / totalDuration) * MathHelper.Pi;
        //        return totalValueChange * (1.0f - (float)Math.Cos(d)) * 0.5f + startValue;
        //    }
        //}
    }
}
