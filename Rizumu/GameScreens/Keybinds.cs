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
            new Background(spriteBatch, GameResources.background_menu).draw();

            if (oldstate == null)
                oldstate = Keyboard.GetState();
            if (keyshad == 0 && !Keyboard.GetState().IsKeyDown(Keys.F5))
            {
                Text.draw(GameResources.font, "Press key for left", 
                    (Game1.graphics.PreferredBackBufferWidth / 2) - (int)(GameResources.font.MeasureString("Press key for left").X / 2), 50, spriteBatch);

                new Sprite(spriteBatch, (Game1.graphics.PreferredBackBufferWidth / 2) - (GameResources.LeftVisual.Width / 2),
                    (Game1.graphics.PreferredBackBufferHeight / 2) - (GameResources.LeftVisual.Height / 2), GameResources.LeftVisual, GameResources.basecolor).draw();

                var ks = Keyboard.GetState();
                if (ks != oldstate && ks.GetPressedKeys().Length == 1)
                {
                    GameResources.Optionss.Left = (int)ks.GetPressedKeys().GetValue(0);
                    GameResources.left = (Keys)ks.GetPressedKeys().GetValue(0);
                    lastbound = (Keys)ks.GetPressedKeys().GetValue(0);
                    keyshad++;
                }
            }
            else if (keyshad == 1 && !Keyboard.GetState().IsKeyDown(lastbound) && !Keyboard.GetState().IsKeyDown(Keys.F5))
            {
                Text.draw(GameResources.font, "Press key for up",
                    (Game1.graphics.PreferredBackBufferWidth / 2) - (int)(GameResources.font.MeasureString("Press key for up").X / 2), 50, spriteBatch);

                new Sprite(spriteBatch, (Game1.graphics.PreferredBackBufferWidth / 2) - (GameResources.UpVisual.Width / 2),
                    (Game1.graphics.PreferredBackBufferHeight / 2) - (GameResources.UpVisual.Height / 2), GameResources.UpVisual, GameResources.basecolor).draw();

                var ks = Keyboard.GetState();
                if (ks != oldstate && ks.GetPressedKeys().Length == 1)
                {
                    GameResources.Optionss.Up = (int)ks.GetPressedKeys().GetValue(0);
                    GameResources.up = (Keys)ks.GetPressedKeys().GetValue(0);
                    lastbound = (Keys)ks.GetPressedKeys().GetValue(0);
                    keyshad++;
                }
            }
            else if (keyshad == 2 && !Keyboard.GetState().IsKeyDown(lastbound) && !Keyboard.GetState().IsKeyDown(Keys.F5))
            {
                Text.draw(GameResources.font, "Press key for right",
                    (Game1.graphics.PreferredBackBufferWidth / 2) - (int)(GameResources.font.MeasureString("Press key for right").X / 2), 50, spriteBatch);

                new Sprite(spriteBatch, (Game1.graphics.PreferredBackBufferWidth / 2) - (GameResources.RightVisual.Width / 2),
                    (Game1.graphics.PreferredBackBufferHeight / 2) - (GameResources.RightVisual.Height / 2), GameResources.RightVisual, GameResources.basecolor).draw();

                var ks = Keyboard.GetState();
                if (ks != oldstate && ks.GetPressedKeys().Length == 1)
                {
                    GameResources.Optionss.Right = (int)ks.GetPressedKeys().GetValue(0);
                    GameResources.right = (Keys)ks.GetPressedKeys().GetValue(0);
                    lastbound = (Keys)ks.GetPressedKeys().GetValue(0);
                    keyshad++;
                }
            }
            else if (keyshad == 3 && !Keyboard.GetState().IsKeyDown(lastbound) && !Keyboard.GetState().IsKeyDown(Keys.F5))
            {
                Text.draw(GameResources.font, "Press key for down",
                    (Game1.graphics.PreferredBackBufferWidth / 2) - (int)(GameResources.font.MeasureString("Press key for down").X / 2), 50, spriteBatch);

                new Sprite(spriteBatch, (Game1.graphics.PreferredBackBufferWidth / 2) - (GameResources.DownVisual.Width / 2),
                    (Game1.graphics.PreferredBackBufferHeight / 2) - (GameResources.DownVisual.Height / 2), GameResources.DownVisual, GameResources.basecolor).draw();

                var ks = Keyboard.GetState();
                if (ks != oldstate && ks.GetPressedKeys().Length == 1)
                {
                    GameResources.Optionss.Down = (int)ks.GetPressedKeys().GetValue(0);
                    GameResources.down = (Keys)ks.GetPressedKeys().GetValue(0);
                    lastbound = (Keys)ks.GetPressedKeys().GetValue(0);
                    keyshad++;
                }
            }
            else if (keyshad == 4)
            {
                File.WriteAllText("settings.json", JObject.FromObject(GameResources.Optionss).ToString());
                keyshad = 0;
                GameResources.GameScreen = 0;
            }

            oldstate = Keyboard.GetState();
        }
    }
}
