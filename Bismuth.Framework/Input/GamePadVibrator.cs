using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using System.Xml.Serialization;

namespace Bismuth.Framework.Input
{
    public class GamePadVibrator
    {
        public PlayerIndex PlayerIndex { get; set; }

        public void RumbleLeft(float motorSpeed, float duration)
        {
            InputState.GetVibratorState(PlayerIndex).RumbleLeft(motorSpeed, duration);
        }

        public void RumbleRight(float motorSpeed, float duration)
        {
            InputState.GetVibratorState(PlayerIndex).RumbleRight(motorSpeed, duration);
        }
    }
}
