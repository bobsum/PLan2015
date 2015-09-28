using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Bismuth.Framework.Animations.EasingFunctions;
using Bismuth.Framework.Animations.Timelines;
using Bismuth.Framework.Content;
using Microsoft.Xna.Framework.Content;

namespace Bismuth.Framework.Assets.Animations.Timelines
{
    public class KeyFrameTimelineAsset : IAsset
    {
        [ContentSerializer(Optional = true)]
        public string Name { get; set; }

        public string TargetName { get; set; }
        public string PropertyTimelineType { get; set; }

        public float BeginTime { get; set; }
        public float EndTime { get; set; }
        public float Duration { get; set; }

        public List<KeyFrameAsset> KeyFrames { get; set; }

        public object Load(IContentManager contentManager)
        {
            Type propertyTimelineType = Type.GetType(PropertyTimelineType);

            float inverseDuration = 1.0f / Duration;

            //for (int i = 0; i < _keyFrames.Count; i++)
            //{
            //    _keyFrames[i].Time *= inverseDuration;
            //}

            if (KeyFrames.Count > 1)
            {
                List<SequentialTimeline> timelines = new List<SequentialTimeline>();

                SequentialTimeline sequentialTimeline = new SequentialTimeline();
                sequentialTimeline.BeginTime = 0;
                sequentialTimeline.EndTime = 1;
                sequentialTimeline.Duration = 1;

                timelines.Add(sequentialTimeline);

                //masterTimeline.Children.Add(sequentialTimeline);

                IPropertyTimeline previous = null;
                for (int i = 1; i < KeyFrames.Count; i++)
                {
                    //if (_keyFrames[i].FillBehavior == KeyFrameFillBehavior.BeginNewTimeline)
                    //{
                    //    sequentialTimeline.EndTime = KeyFrames[i].Time;


                    //    sequentialTimeline = new SequentialTimeline();
                    //    sequentialTimeline.BeginTime = KeyFrames[i].Time;

                    //    masterTimeline.Children.Add(sequentialTimeline);
                    //}
                    //else
                    //{
                    IPropertyTimeline propertyTimeline = (IPropertyTimeline)Activator.CreateInstance(propertyTimelineType);

                    propertyTimeline.TargetName = TargetName;

                    propertyTimeline.EasingFunction = KeyFrames[i - 1].EasingFunction;
                    propertyTimeline.BeginTime = KeyFrames[i - 1].Time * inverseDuration;
                    propertyTimeline.EndTime = 1;
                    propertyTimeline.Duration = KeyFrames[i].Time * inverseDuration - propertyTimeline.BeginTime;
                    propertyTimeline.FillBehavior = (FillBehavior)KeyFrames[i].FillBehavior;

                    if (previous != null) previous.EndTime = propertyTimeline.BeginTime;

                    propertyTimelineType.GetProperty("From").SetValue(propertyTimeline, KeyFrames[i - 1].Value, null);
                    propertyTimelineType.GetProperty("To").SetValue(propertyTimeline, KeyFrames[i].Value, null);

                    sequentialTimeline.Children.Add(propertyTimeline);
                    //sequentialTimeline.Duration = KeyFrames[i].Time;

                    previous = propertyTimeline;
                    //}
                }

                return sequentialTimeline;
            }
            else
            {
                IPropertyTimeline propertyTimeline = (IPropertyTimeline)Activator.CreateInstance(propertyTimelineType);
                propertyTimeline.TargetName = TargetName;
                propertyTimeline.BeginTime = 0;
                propertyTimeline.EndTime = 1;
                propertyTimeline.Duration = 1;

                propertyTimeline.EasingFunction = new LinearEase();

                if (KeyFrames.Count > 0)
                {
                    propertyTimelineType.GetProperty("From").SetValue(propertyTimeline, KeyFrames[0].Value, null);
                    propertyTimelineType.GetProperty("To").SetValue(propertyTimeline, KeyFrames[0].Value, null);
                }

                return propertyTimeline;
            }
        }
    }

    public enum KeyFrameFillBehavior
    {
        HoldEnd,
        Repeat,
        ReverseAndRepeat,
        BeginNewTimeline
    }

    public class KeyFrameAsset
    {
        public KeyFrameFillBehavior FillBehavior { get; set; }
        public IEasingFunction EasingFunction { get; set; }
        public float Time { get; set; }
        public object Value { get; set; }
    }
}
