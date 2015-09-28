using System.Collections.Generic;

namespace Bismuth.Framework.Animations.Timelines
{
    public class SequentialTimeline : Timeline, ICompositeTimeline
    {
        public List<ITimeline> Children { get { return _children; } }
        private readonly List<ITimeline> _children = new List<ITimeline>();

        private int _lastIndex;
        private float _lastTime;

        public override void Update(float time)
        {
            time = NormalizeTime(time);

            if (time < _lastTime)
            {
                for (int i = _lastIndex; i >= 0; i--)
                {
                    ITimeline child = Children[i];
                    if (child.BeginTime <= time && time <= child.EndTime)
                    {
                        _lastIndex = i;
                        _lastTime = time;

                        child.Update(time);
                        break;
                    }
                }
            }
            else
            {
                for (int i = _lastIndex; i < Children.Count; i++)
                {
                    ITimeline child = Children[i];
                    if (child.BeginTime <= time && time <= child.EndTime)
                    {
                        _lastIndex = i;
                        _lastTime = time;

                        child.Update(time);
                        break;
                    }
                }
            }
        }
    }
}
