namespace Bismuth.Framework.Animations.EasingFunctions
{
    public class LinearEase : EasingFunctionBase
    {
        protected override float EaseIn(float normalizedTime)
        {
            return normalizedTime;
        }
    }
}
