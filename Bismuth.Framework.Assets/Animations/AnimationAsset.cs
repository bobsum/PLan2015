using System.Collections.Generic;
using Bismuth.Framework.Animations;
using Bismuth.Framework.Animations.Timelines;
using Bismuth.Framework.Assets.Animations.Timelines;
using Bismuth.Framework.Content;
using Microsoft.Xna.Framework.Content;

namespace Bismuth.Framework.Assets.Animations
{
    public class AnimationAsset : IAsset
    {
        [ContentSerializer(Optional = true)]
        public string Name { get; set; }

        [ContentSerializer(Optional = true)]
        public string NodeAssetName { get; set; }

        public AnimationMode Mode { get; set; }
        public AnimationDirection Direction { get; set; }

        public float Fps { get; set; }
        public int FrameCount { get; set; }

        public List<KeyFrameTimelineAsset> Timelines { get; set; }

        public object Load(IContentManager contentManager)
        {
            Animation animation = new Animation();
            animation.Mode = Mode;
            animation.Direction = Direction;
            animation.Fps = Fps;
            animation.FrameCount = FrameCount;

            if (Timelines.Count > 1)
            {
                ParallelTimeline parallelTimeline = new ParallelTimeline();
                parallelTimeline.BeginTime = 0;
                parallelTimeline.EndTime = 1;
                parallelTimeline.Duration = 1;

                for (int i = 0; i < Timelines.Count; i++)
                {
                    ITimeline timeline = (ITimeline)Timelines[i].Load(contentManager);

                    parallelTimeline.Children.Add(timeline);
                }

                animation.Timeline = parallelTimeline;
            }
            else if (Timelines.Count > 0)
            {
                animation.Timeline = (ITimeline)Timelines[0].Load(contentManager);
            }

            return animation;
        }
    }
}
