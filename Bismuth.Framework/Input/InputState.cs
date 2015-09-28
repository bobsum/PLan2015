using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace Bismuth.Framework.Input
{
    /// <summary>
    /// A struct used to store the state of all XNA input devices.
    /// </summary>
    public struct InputState
    {
        static InputState()
        {
            _vibratorStates[0] = new GamePadVibratorState(PlayerIndex.One);
            _vibratorStates[1] = new GamePadVibratorState(PlayerIndex.Two);
            _vibratorStates[2] = new GamePadVibratorState(PlayerIndex.Three);
            _vibratorStates[3] = new GamePadVibratorState(PlayerIndex.Four);
        }

        public KeyboardState KeyboardState;
        public MouseState MouseState;
        public GamePadState GamePadState1;
        public GamePadState GamePadState2;
        public GamePadState GamePadState3;
        public GamePadState GamePadState4;

        private static InputState _state;
        private static InputState _previousState;

        private static GamePadVibratorState[] _vibratorStates = new GamePadVibratorState[4];

        public static GamePadVibratorState GetVibratorState(PlayerIndex playerIndex)
        {
            return _vibratorStates[(int)playerIndex];
        }

        /// <summary>
        /// Gets the current state of all XNA input devices.
        /// </summary>
        public static InputState GetState() { return _state; }

        /// <summary>
        /// Gets the previous state of all XNA input devices.
        /// </summary>
        public static InputState GetPreviousState() { return _previousState; }

        /// <summary>
        /// Updates the input state.
        /// This method should be called once and only once in the game update loop.
        /// </summary>
        public static void Update(GameTime gameTime)
        {
            _previousState = _state;

            _state.KeyboardState = Keyboard.GetState();
            _state.MouseState = Mouse.GetState();
            _state.GamePadState1 = GamePad.GetState(PlayerIndex.One);
            _state.GamePadState2 = GamePad.GetState(PlayerIndex.Two);
            _state.GamePadState3 = GamePad.GetState(PlayerIndex.Three);
            _state.GamePadState4 = GamePad.GetState(PlayerIndex.Four);

            for (int i = 0; i < 4; i++)
            {
                _vibratorStates[i].Update(gameTime);
            }
        }
    }
}
