using Bismuth.Framework.Animations.EasingFunctions;
using Bismuth.Framework.Composite;
using Microsoft.Xna.Framework;

namespace Bismuth.Framework.Animations.Timelines
{
    public abstract class PropertyTimeline<T> : Timeline, IPropertyTimeline
    {
        public string TargetName { get; set; }
        public INode Target { get; set; }

        public IEasingFunction EasingFunction { get; set; }

        public T From { get; set; }
        public T To { get; set; }

        /// <summary>
        /// Updates the timeline.
        /// NOTE: Do not override this unless you know what you are doing.
        /// </summary>
        /// <param name="time">Normalized time relative to the parent timeline.</param>
        public override void Update(float time)
        {
            if (Target != null)
            {
                UpdateTarget(NormalizeTime(time));
            }
        }

        protected abstract void UpdateTarget(float normalizedTime);
    }

    public class PositionTimeline : PropertyTimeline<Vector2>
    {
        protected override void UpdateTarget(float normalizedTime)
        {
            float x = EasingFunction.Ease(normalizedTime, From.X, To.X);
            float y = EasingFunction.Ease(normalizedTime, From.Y, To.Y);

            Target.Position = new Vector2(x, y);
        }
    }

    public class PositionXTimeline : PropertyTimeline<float>
    {
        protected override void UpdateTarget(float normalizedTime)
        {
            Vector2 position = Target.Position;
            position.X = EasingFunction.Ease(normalizedTime, From, To);
            Target.Position = position;
        }
    }

    public class PositionYTimeline : PropertyTimeline<float>
    {
        protected override void UpdateTarget(float normalizedTime)
        {
            Vector2 position = Target.Position;
            position.Y = EasingFunction.Ease(normalizedTime, From, To);
            Target.Position = position;
        }
    }

    public class RotationTimeline : PropertyTimeline<float>
    {
        protected override void UpdateTarget(float normalizedTime)
        {
            Target.Rotation = EasingFunction.Ease(normalizedTime, From, To);
        }
    }

    public class ScaleTimeline : PropertyTimeline<float>
    {
        protected override void UpdateTarget(float normalizedTime)
        {
            Target.Scale = EasingFunction.Ease(normalizedTime, From, To);
        }
    }

    public class FlipXTimeline : PropertyTimeline<bool>
    {
        protected override void UpdateTarget(float normalizedTime)
        {
            float from = From ? 1f : 0f;
            float to = To ? 1f : 0f;

            float value = EasingFunction.Ease(normalizedTime, from, to);

            Target.FlipX = value > 0.5f;
        }
    }

    public class FlipYTimeline : PropertyTimeline<bool>
    {
        protected override void UpdateTarget(float normalizedTime)
        {
            float from = From ? 1f : 0f;
            float to = To ? 1f : 0f;

            float value = EasingFunction.Ease(normalizedTime, from, to);

            Target.FlipY = value > 0.5f;
        }
    }

    public class OpacityTimeline : PropertyTimeline<float>
    {
        protected override void UpdateTarget(float normalizedTime)
        {
            Target.Opacity = EasingFunction.Ease(normalizedTime, From, To);
        }
    }

    public class IsVisibleTimeline : PropertyTimeline<bool>
    {
        protected override void UpdateTarget(float normalizedTime)
        {
            float from = From ? 1f : 0f;
            float to = To ? 1f : 0f;

            float value = EasingFunction.Ease(normalizedTime, from, to);

            Target.IsVisible = value > 0.5f;
        }
    }

    public class ZIndexTimeline : PropertyTimeline<int>
    {
        protected override void UpdateTarget(float normalizedTime)
        {
            Target.ZIndex = (int)EasingFunction.Ease(normalizedTime, From, To);
        }
    }
}
