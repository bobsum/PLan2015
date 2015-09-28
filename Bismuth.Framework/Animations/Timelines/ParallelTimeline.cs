using System.Collections.Generic;

namespace Bismuth.Framework.Animations.Timelines
{
    public class ParallelTimeline : Timeline, ICompositeTimeline
    {
        public List<ITimeline> Children { get { return _children; } }
        private readonly List<ITimeline> _children = new List<ITimeline>();

        public override void Update(float time)
        {
            time = NormalizeTime(time);

            for (int i = 0; i < Children.Count; i++)
            {
                ITimeline child = Children[i];
                if (child.BeginTime <= time && time <= child.EndTime)
                {
                    child.Update(time);
                }
            }
        }
    }
}
