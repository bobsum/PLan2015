using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Bismuth.Framework.Animations.Timelines
{
    public interface ICompositeTimeline : ITimeline
    {
        List<ITimeline> Children { get; }
    }
}
