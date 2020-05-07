using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Rizumu.GameLogic.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rizumu.Engine
{
    internal class InputManager
    {
        public bool Left = false;

        public bool Right = false;

        public bool Up = false;

        public bool Down = false;

        public bool FullscreenToggle = false;

        private Settings _settings;

        public InputManager(Settings stg)
        {
            _settings = stg;
        }

        KeyboardState _previousState;
        GamePadState _previousGPState;

        public void Update()
        {
            var ks = Keyboard.GetState();
            var gs = GamePad.GetState(PlayerIndex.One);
            if (_previousState == null)
                _previousState = ks;

            if (_previousGPState == null)
                _previousGPState = gs;

            if (_settings.EnableKeyboard)
            {
                Left = ks.IsKeyDown(RizumuGame.Settings.LeftKey) && _previousState.IsKeyUp(RizumuGame.Settings.LeftKey);
                Right = ks.IsKeyDown(RizumuGame.Settings.RightKey) && _previousState.IsKeyUp(RizumuGame.Settings.RightKey);
                Up = ks.IsKeyDown(RizumuGame.Settings.UpKey) && _previousState.IsKeyUp(RizumuGame.Settings.UpKey);
                Down = ks.IsKeyDown(RizumuGame.Settings.DownKey) && _previousState.IsKeyUp(RizumuGame.Settings.DownKey);
                FullscreenToggle = ks.IsKeyDown(Keys.F12) && _previousState.IsKeyUp(Keys.F12);
            }

            if (_settings.EnableGamepad)
            {
                Left = (_settings.EnableKeyboard && Left)
                || gs.IsButtonDown(Buttons.DPadLeft) && _previousGPState.IsButtonUp(Buttons.DPadLeft)
                || gs.IsButtonDown(Buttons.X) && _previousGPState.IsButtonUp(Buttons.X)
                || gs.ThumbSticks.Left.X < -0.7 && _previousGPState.ThumbSticks.Left.X > -0.7 && _settings.EnableThumbsticks
                || gs.ThumbSticks.Right.X < -0.7 && _previousGPState.ThumbSticks.Right.X > -0.7 && _settings.EnableThumbsticks;

                Right = (_settings.EnableKeyboard && Right)
                || gs.IsButtonDown(Buttons.DPadRight) && _previousGPState.IsButtonUp(Buttons.DPadRight)
                || gs.IsButtonDown(Buttons.B) && _previousGPState.IsButtonUp(Buttons.B)
                || gs.ThumbSticks.Left.X > 0.7 && _previousGPState.ThumbSticks.Left.X < 0.7 && _settings.EnableThumbsticks
                || gs.ThumbSticks.Right.X > 0.7 && _previousGPState.ThumbSticks.Right.X < 0.7 && _settings.EnableThumbsticks;

                Up = (_settings.EnableKeyboard && Up)
                || gs.IsButtonDown(Buttons.DPadUp) && _previousGPState.IsButtonUp(Buttons.DPadUp)
                || gs.IsButtonDown(Buttons.Y) && _previousGPState.IsButtonUp(Buttons.Y)
                || gs.ThumbSticks.Left.Y > 0.7 && _previousGPState.ThumbSticks.Left.Y < 0.7 && _settings.EnableThumbsticks
                || gs.ThumbSticks.Right.Y > 0.7 && _previousGPState.ThumbSticks.Right.Y < 0.7 && _settings.EnableThumbsticks;

                Down = (_settings.EnableKeyboard && Down)
                || gs.IsButtonDown(Buttons.DPadDown) && _previousGPState.IsButtonUp(Buttons.DPadDown)
                || gs.IsButtonDown(Buttons.A) && _previousGPState.IsButtonUp(Buttons.A)
                || gs.ThumbSticks.Left.Y < -0.7 && _previousGPState.ThumbSticks.Left.Y > -0.7 && _settings.EnableThumbsticks
                || gs.ThumbSticks.Right.Y < -0.7 && _previousGPState.ThumbSticks.Right.Y > -0.7 && _settings.EnableThumbsticks;
            }

            _previousState = ks;
            _previousGPState = gs;
        }
    }
}
