using Bismuth.Framework.Animations.EasingFunctions;
using Bismuth.Framework.Composite;

namespace Bismuth.Framework.Animations.Timelines
{
    public interface IPropertyTimeline : ITimeline
    {
        string TargetName { get; set; }
        INode Target { get; set; }

        IEasingFunction EasingFunction { get; set; }
    }
}
