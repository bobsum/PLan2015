using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Audio;

namespace Bismuth.Framework.Audio
{
    public class XactCue : ICue
    {
        private readonly Cue _cue;

        public XactCue(Cue cue)
        {
            _cue = cue;
        }

        public bool IsPlaying { get { return _cue.IsPlaying; } }
        public bool IsPaused { get { return _cue.IsPaused; } }
        public bool IsStopped { get { return _cue.IsStopped; } }

        public void Play()
        {
            _cue.Play();
        }

        public void Pause()
        {
            _cue.Pause();
        }

        public void Resume()
        {
            _cue.Resume();
        }

        public void Stop()
        {
            _cue.Stop(AudioStopOptions.Immediate);
        }

        public float GetVariable(string name)
        {
            return _cue.GetVariable(name);
        }

        public void SetVariable(string name, float value)
        {
            _cue.SetVariable(name, value);
        }
    }
}
