using System.Linq;
using Microsoft.Xna.Framework.Input;

namespace MyGameEngine.Engine.Input
{
    public static class KeyboardInput
    {
        private static KeyboardState _currentState;
        private static KeyboardState _previousState;

        static KeyboardInput()
        {
            _currentState = Keyboard.GetState();
            _previousState = new KeyboardState();
        }

        public static void Update()
        {
            _previousState = _currentState;
            _currentState = Keyboard.GetState();
        }
        
        public static bool IsCapsLockOn => _currentState.CapsLock;
        
        public static bool IsNumLockOn => _currentState.NumLock;

        public static bool IsKeyDown(params Keys[] keys)
        {
            return keys.Any(key => _currentState.IsKeyDown(key));
        }

        public static bool IsKeyUp(params Keys[] keys)
        {
            return keys.Any(key => _currentState.IsKeyUp(key));
        }

        public static bool IsKeyPressed(params Keys[] keys)
        {
            return keys.Any(key => _currentState.IsKeyDown(key) && !_previousState.IsKeyDown(key));
        }
        
        public static Keys[] GetPressedKeys()
        {
            return _currentState.GetPressedKeys();
        }
    }
}