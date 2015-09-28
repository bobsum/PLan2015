using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Bismuth.Framework.Audio
{
    public interface ICue
    {
        bool IsPlaying { get; }
        bool IsPaused { get; }
        bool IsStopped { get; }

        void Play();
        void Pause();
        void Resume();
        void Stop();

        float GetVariable(string name);
        void SetVariable(string name, float value);
    }
}
