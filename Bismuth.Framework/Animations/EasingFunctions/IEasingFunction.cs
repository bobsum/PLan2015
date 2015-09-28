namespace Bismuth.Framework.Animations.EasingFunctions
{
    /// <summary>
    /// Defines the mode in which easing functions perform their ease.
    /// </summary>
    public enum EasingMode
    {
        EaseIn,
        EaseOut,
        EaseInOut
    }

    /// <summary>
    /// Defines the interface of all easing functions.
    /// </summary>
    public interface IEasingFunction
    {
        /// <summary>
        /// Gets or sets the easing mode of this function.
        /// </summary>
        EasingMode EasingMode { get; set; }

        /// <summary>
        /// Executes the easing function.
        /// </summary>
        /// <param name="elapsedTime">The elapsed time since the beginning of the animation.</param>
        /// <param name="startValue">The start value of the animation.</param>
        /// <param name="totalValueChange">The total amount the value will change during the animation.</param>
        /// <param name="totalDuration">The total duration of the animation.</param>
        /// <returns>The interpolated value.</returns>
        float Ease(float elapsedTime, float startValue, float totalValueChange, float totalDuration);


        float Ease(float normalizedTime, float from, float to);

        /// <summary>
        /// Executes the easing function.
        /// </summary>
        /// <param name="normalizedTime">The time normalized to a value between 0 and 1.</param>
        /// <returns>Returns the interpolated value between 0 and 1.</returns>
        float Ease(float normalizedTime);
    }
}
