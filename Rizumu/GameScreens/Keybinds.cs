/*
 * A test screen for key binding
 */

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using System;
using System.IO;

namespace Rizumu.GameScreens
{
    class Keybinds
    {
        public static bool set = false;
        public static void draw(SpriteBatch spriteBatch)
        {
            Text.draw(GameResources.debug, "Press key to test keybind (numpad4)", 0, 0, spriteBatch);
            if (!Keyboard.GetState().IsKeyDown(Keys.F6) && Keyboard.GetState().GetPressedKeys().Length > 0)
            {
                GameResources.left = Keyboard.GetState().GetPressedKeys()[0];
                set = true;
            }
            if (set)
            {
                Text.draw(GameResources.debug, "test key set", 0, 20, spriteBatch);
            }
        }
    }
}
