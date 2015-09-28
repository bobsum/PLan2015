using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Bismuth.Framework.Animations.Timelines;
using Bismuth.Framework.Composite;
using Microsoft.Xna.Framework;

namespace Bismuth.Framework.Animations
{
    public class Animation : IAnimation
    {
        public AnimationMode Mode { get; set; }
        public AnimationDirection Direction { get; set; }
        public AnimationState State { get; private set; }

        public float Fps
        {
            get { return FrameTimeSpan * 1000f; }
            set { FrameTimeSpan = value * 0.001f; }
        }

        public int FrameCount { get; set; }
        public float FrameTime { get; set; }
        public float FrameTimeSpan { get; private set; }

        public ITimeline Timeline { get; set; }

        public void Play()
        {
            State = AnimationState.Playing;
        }

        public void Stop()
        {
            State = AnimationState.Stopped;
        }

        public void Reset()
        {
            FrameTime = 0;
            State = AnimationState.Stopped;
        }

        public void Update(GameTime gameTime)
        {
            if (State == AnimationState.Playing)
            {
                float elapsedTime = (float)gameTime.ElapsedGameTime.TotalMilliseconds;

                if (Direction == AnimationDirection.Forward)
                    FrameTime += elapsedTime * FrameTimeSpan;
                else if (Direction == AnimationDirection.Reverse)
                    FrameTime -= elapsedTime * FrameTimeSpan;

                if (Mode == AnimationMode.PingPong)
                {
                    if (FrameTime < 0)
                    {
                        Direction = AnimationDirection.Forward;
                        FrameTime = -FrameTime;
                    }
                    else if (FrameTime > FrameCount)
                    {
                        Direction = AnimationDirection.Reverse;
                        FrameTime = 2 * FrameCount - FrameTime;
                    }

                    // Clamp frame time.
                    FrameTime %= FrameCount;
                    if (FrameTime < 0) FrameTime += FrameCount;
                }
                else if (Mode == AnimationMode.Loop)
                {
                    // Clamp frame time.
                    FrameTime %= FrameCount;
                    if (FrameTime < 0) FrameTime += FrameCount;
                }
                else if (Mode == AnimationMode.Once)
                {
                    if (FrameTime < 0)
                    {
                        FrameTime = 0;
                        State = AnimationState.Finished;
                    }
                    else if (FrameTime > FrameCount)
                    {
                        FrameTime = FrameCount;
                        State = AnimationState.Finished;
                    }
                }

                Timeline.Update(FrameTime / FrameCount);
            }

            //if (IsEnabled)
            //    Update(FrameTime / FrameCount);
        }

        #region Binding

        public void Bind(INode root)
        {
            List<INode> nodes = new List<INode>();

            NodeTree.Flatten(root, nodes);

            // Build dictionary.
            Dictionary<string, INode> nodeDictionary = new Dictionary<string, INode>();
            for (int i = 0; i < nodes.Count; i++)
            {
                INode node = nodes[i];
                if (string.IsNullOrEmpty(node.Name)) continue;
                if (nodeDictionary.ContainsKey(node.Name)) continue;
                //if (nodeDictionary.ContainsKey(node.Name)) throw new InvalidOperationException("The node tree can not contain two nodes with the same name.");

                nodeDictionary.Add(node.Name, node);
            }

            Bind(Timeline, nodeDictionary);
        }

        private void Bind(ITimeline timeline, Dictionary<string, INode> nodeDictionary)
        {
            IPropertyTimeline propertyTimeline = timeline as IPropertyTimeline;
            if (propertyTimeline != null && !string.IsNullOrEmpty(propertyTimeline.TargetName))
            {
                INode node;
                nodeDictionary.TryGetValue(propertyTimeline.TargetName, out node);
                propertyTimeline.Target = node;
            }

            ICompositeTimeline compositeTimeline = timeline as ICompositeTimeline;
            if (compositeTimeline != null)
            {
                for (int i = 0; i < compositeTimeline.Children.Count; i++)
                {
                    Bind(compositeTimeline.Children[i], nodeDictionary);
                }
            }
        }

        #endregion
    }
}
