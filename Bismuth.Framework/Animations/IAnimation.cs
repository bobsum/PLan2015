using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Bismuth.Framework.Animations.Timelines;

namespace Bismuth.Framework.Animations
{
    public enum AnimationDirection
    {
        Forward,
        Reverse
    }

    public enum AnimationMode
    {
        Once,
        Loop,
        PingPong
    }

    public enum AnimationState
    {
        Stopped,
        Playing,
        Finished
    }

    public interface IAnimation
    {
        AnimationMode Mode { get; set; }
        AnimationDirection Direction { get; set; }
        AnimationState State { get; }

        float Fps { get; set; }
        int FrameCount { get; set; }

        ITimeline Timeline { get; set; }

        void Play();
        void Stop();
        void Reset();

        void Update(GameTime gameTime);
    }
}
