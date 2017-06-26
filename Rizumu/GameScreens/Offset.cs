/*
 * Mapscreen I used for offsets.
 * Though this isn't really neccesary, feel free to improve and add to Options
 */

using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Rizumu.GameScreens
{
    class Offset
    {
        public static int offset = 0;
        public static int timer = 0;
        public static string y = "";
        public static void draw(SpriteBatch spriteBatch)
        {
            Text.draw(GameResources.font, "Press space when the timer hits 3000", 10, 10, spriteBatch);
            if (Keyboard.GetState().IsKeyDown(Keys.Space))
            {
                GameResources.offset = timer - 3000;
                GameResources.GameScreen = 0;
            }
            Text.draw(GameResources.font, y + timer, 50, 50, spriteBatch);
        }

        public static void update()
        {
            timer++;
        }
    }
}
