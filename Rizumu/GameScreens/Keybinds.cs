/*
 * A test screen for key binding
 */

using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Newtonsoft.Json.Linq;
using System.IO;

namespace Rizumu.GameScreens
{
    class Keybinds
    {
        public static bool set = false;
        public static int keyshad = 0;
        public static KeyboardState oldstate;
        public static Keys lastbound;
        public static void draw(SpriteBatch spriteBatch)
        {
            if (oldstate == null)
                oldstate = Keyboard.GetState();
            if (keyshad == 0 && !Keyboard.GetState().IsKeyDown(Keys.F5))
            {
                Text.draw(GameResources.font, "Press key for left", (Game1.graphics.PreferredBackBufferWidth / 2) - (18 * 4), 50, spriteBatch);
                var ks = Keyboard.GetState();
                if (ks != oldstate && ks.GetPressedKeys().Length == 1)
                {
                    GameResources.Optionss.Left = (int)ks.GetPressedKeys().GetValue(0);
                    GameResources.left = (Keys)ks.GetPressedKeys().GetValue(0);
                    lastbound = (Keys)ks.GetPressedKeys().GetValue(0);
                    keyshad++;
                }
            }
            if (keyshad == 1 && !Keyboard.GetState().IsKeyDown(lastbound) && !Keyboard.GetState().IsKeyDown(Keys.F5))
            {
                Text.draw(GameResources.font, "Press key for up", (Game1.graphics.PreferredBackBufferWidth / 2) - (16 * 4), 50, spriteBatch);
                var ks = Keyboard.GetState();
                if (ks != oldstate && ks.GetPressedKeys().Length == 1)
                {
                    GameResources.Optionss.Up = (int)ks.GetPressedKeys().GetValue(0);
                    GameResources.up = (Keys)ks.GetPressedKeys().GetValue(0);
                    lastbound = (Keys)ks.GetPressedKeys().GetValue(0);
                    keyshad++;
                }
            }
            if (keyshad == 2 && !Keyboard.GetState().IsKeyDown(lastbound) && !Keyboard.GetState().IsKeyDown(Keys.F5))
            {
                Text.draw(GameResources.font, "Press key for right", (Game1.graphics.PreferredBackBufferWidth / 2) - (19 * 4), 50, spriteBatch);
                var ks = Keyboard.GetState();
                if (ks != oldstate && ks.GetPressedKeys().Length == 1)
                {
                    GameResources.Optionss.Right = (int)ks.GetPressedKeys().GetValue(0);
                    GameResources.right = (Keys)ks.GetPressedKeys().GetValue(0);
                    lastbound = (Keys)ks.GetPressedKeys().GetValue(0);
                    keyshad++;
                }
            }
            if (keyshad == 3 && !Keyboard.GetState().IsKeyDown(lastbound) && !Keyboard.GetState().IsKeyDown(Keys.F5))
            {
                Text.draw(GameResources.font, "Press key for down", (Game1.graphics.PreferredBackBufferWidth / 2) - (18 * 4), 50, spriteBatch);
                var ks = Keyboard.GetState();
                if (ks != oldstate && ks.GetPressedKeys().Length == 1)
                {
                    GameResources.Optionss.Down = (int)ks.GetPressedKeys().GetValue(0);
                    GameResources.down = (Keys)ks.GetPressedKeys().GetValue(0);
                    lastbound = (Keys)ks.GetPressedKeys().GetValue(0);
                    keyshad++;
                }
            }
            if (keyshad == 4)
            {
                File.WriteAllText("settings.json", JObject.FromObject(GameResources.Optionss).ToString());
                keyshad = 0;
                GameResources.GameScreen = 0;
            }

            oldstate = Keyboard.GetState();
        }
    }
}
