using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace Bismuth.Framework.Input
{
    public class GamePadVibratorState
    {
        private PlayerIndex _playerIndex;

        private float _leftMotorSpeed;
        private float _rightMotorSpeed;

        private float _leftDuration;
        private float _rightDuration;

        public GamePadVibratorState(PlayerIndex playerIndex)
        {
            _playerIndex = playerIndex;
        }

        public void RumbleLeft(float motorSpeed, float duration)
        {
            _leftMotorSpeed = motorSpeed;
            _leftDuration = duration;

            SetVibration();
        }

        public void RumbleRight(float motorSpeed, float duration)
        {
            _rightMotorSpeed = motorSpeed;
            _rightDuration = duration;

            SetVibration();
        }

        public void Update(GameTime gameTime)
        {
            float elapsedTime = (float)gameTime.ElapsedGameTime.TotalMilliseconds;

            if (_leftDuration > 0)
                _leftDuration -= elapsedTime;

            if (_rightDuration > 0)
                _rightDuration -= elapsedTime;

            bool setVibration = false;

            if (_leftMotorSpeed > 0 && _leftDuration <= 0)
            {
                _leftMotorSpeed = 0;
                setVibration = true;
            }

            if (_rightMotorSpeed > 0 && _rightDuration <= 0)
            {
                _rightMotorSpeed = 0;
                setVibration = true;
            }

            if (setVibration)
            {
                SetVibration();
            }
        }

        private void SetVibration()
        {
            GamePad.SetVibration(_playerIndex, _leftMotorSpeed, _rightMotorSpeed);
        }
    }
}
