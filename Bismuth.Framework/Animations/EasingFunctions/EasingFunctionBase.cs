namespace Bismuth.Framework.Animations.EasingFunctions
{
    /// <summary>
    /// Just an implementation if the IEasingFunction interface,
    /// so that it can be XML serialized.
    /// </summary>
    public abstract class EasingFunctionBase : IEasingFunction
    {
        public EasingMode EasingMode { get; set; }

        public float Ease(float elapsedTime, float startValue, float totalValueChange, float totalDuration)
        {
            float normalizedTime = elapsedTime / totalDuration;
            return startValue + totalValueChange * Ease(normalizedTime);
        }

        public float Ease(float normalizedTime, float from, float to)
        {
            return from + (to - from) * Ease(normalizedTime);
        }

        public float Ease(float normalizedTime)
        {
            if (EasingMode == EasingMode.EaseIn)
                return EaseIn(normalizedTime);

            if (EasingMode == EasingMode.EaseOut)
                return 1.0f - EaseIn(1.0f - normalizedTime);

            if (normalizedTime > 0.5f)
                return ((1.0f - EaseIn((1.0f - normalizedTime) * 2.0f)) * 0.5f) + 0.5f;

            return EaseIn(normalizedTime * 2.0f) * 0.5f;
        }

        protected abstract float EaseIn(float normalizedTime);
    }
}
