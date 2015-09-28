using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace Bismuth.Framework.Input
{
    public static class InputExtensions
    {
        public static Vector2 GetPosition(this MouseState mouseState)
        {
            return new Vector2(mouseState.X, mouseState.Y);
        }

        public static Vector2 GetPosition(this MouseState mouseState, Matrix transform)
        {
            return Vector2.Transform(new Vector2(mouseState.X, mouseState.Y), transform);
        }

        public static bool IsButtonPressed(this MouseState mouseState, MouseButtons mouseButton)
        {
            switch (mouseButton)
            {
                case MouseButtons.Left: return mouseState.LeftButton == ButtonState.Pressed;
                case MouseButtons.Middle: return mouseState.MiddleButton == ButtonState.Pressed;
                case MouseButtons.Right: return mouseState.RightButton == ButtonState.Pressed;
                case MouseButtons.XButton1: return mouseState.XButton1 == ButtonState.Pressed;
                case MouseButtons.XButton2: return mouseState.XButton2 == ButtonState.Pressed;
                default: return false;
            }
        }

        public static bool IsButtonPressedOnce(this MouseState mouseState, MouseButtons mouseButton)
        {
            InputState previousState = InputState.GetPreviousState();
            return IsButtonPressed(mouseState, mouseButton) && !IsButtonPressed(previousState.MouseState, mouseButton);
        }

        public static bool IsKeyPressedOnce(this KeyboardState keyboardState, Keys key)
        {
            InputState previousState = InputState.GetPreviousState();
            return keyboardState.IsKeyDown(key) && !previousState.KeyboardState.IsKeyDown(key);
        }

        public static bool HasMoved(this MouseState mouseState)
        {
            InputState previousState = InputState.GetPreviousState();
            return mouseState.X != previousState.MouseState.X || mouseState.Y != previousState.MouseState.Y;
        }

        public static int ScrollWheelChange(this MouseState mouseState)
        {
            InputState previousState = InputState.GetPreviousState();
            return mouseState.ScrollWheelValue - previousState.MouseState.ScrollWheelValue;
        }
    }
}
