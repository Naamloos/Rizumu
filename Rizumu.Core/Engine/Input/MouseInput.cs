using System;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace MyGameEngine.Engine.Input
{
    public static class MouseInput
    {
        private static MouseState _currentState;
        private static MouseState _previousState;

        private static int X;
        private static int Y;

        static MouseInput()
        {
            _currentState = Mouse.GetState();
            _previousState = new MouseState();
        }

        public static void Update(int screenWidth, int screenHeight)
        {
            _previousState = _currentState;
            _currentState = Mouse.GetState();
            
            float xscale = 1920f / screenWidth;
            float yscale = 1080f / screenHeight;
            X = (int)(Position.X * xscale);
            Y = (int)(Position.Y * yscale);
        }
        
        public static Rectangle HitBox => new Rectangle(X, Y, 1, 1);
        
        public static Point Position => _currentState.Position;
        
        public static int ScrollWheelValue => _currentState.ScrollWheelValue;
        
        public static bool IsButtonDown(params MouseButtons[] buttons)
        {
            return buttons.Any(button => _currentState.IsDown(button));
        }
        
        public static bool IsButtonUp(params MouseButtons[] buttons)
        {
            return  buttons.Any(button => !_currentState.IsDown(button));
        }
        
        public static bool IsButtonPressed(params MouseButtons[] buttons)
        {
            return buttons.Any(button => _currentState.IsDown(button) && !_previousState.IsDown(button));
        }

        private static bool IsDown(this MouseState mouseState, MouseButtons button)
        {
            return mouseState.GetButton(button) == ButtonState.Pressed;
        }

        private static ButtonState GetButton(this MouseState mouseState, MouseButtons button)
        {
            switch (button)
            {
                case MouseButtons.LeftButton:   return mouseState.LeftButton;
                case MouseButtons.MiddleButton: return mouseState.MiddleButton;
                case MouseButtons.RightButton:  return mouseState.RightButton;
                case MouseButtons.XButton1:     return mouseState.XButton1;
                case MouseButtons.XButton2:     return mouseState.XButton2;
                default:
                    throw new ArgumentOutOfRangeException(nameof(button), button, null);
            }
        }
    }
}