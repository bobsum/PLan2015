using Bismuth.Framework.Audio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Plan2015.Score.ScoreBoard.Audio
{
    public class NoCue : ICue
    {
        public bool IsPlaying
        {
            get { return false; }
        }

        public bool IsPaused
        {
            get { return false; }
        }

        public bool IsStopped
        {
            get { return true; }
        }

        public void Play()
        {
        }

        public void Pause()
        {
        }

        public void Resume()
        {
        }

        public void Stop()
        {
        }

        public float GetVariable(string name)
        {
            return 1;
        }

        public void SetVariable(string name, float value)
        {
        }
    }
}
