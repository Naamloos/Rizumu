/* 
 * class i use for drawing text with a black outline
 */

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Rizumu
{
    class Text
    {
        public static void draw(SpriteFont font, string text, int x, int y, SpriteBatch spriteBatch)
        {
            spriteBatch.DrawString(font, text, new Vector2(x + 1, y + 1), Color.Black);
            spriteBatch.DrawString(font, text, new Vector2(x + 1, y - 1), Color.Black);
            spriteBatch.DrawString(font, text, new Vector2(x - 1, y + 1), Color.Black);
            spriteBatch.DrawString(font, text, new Vector2(x - 1, y - 1), Color.Black);
            spriteBatch.DrawString(font, text, new Vector2(x, y), GameResources.basecolor);
        }
    }
}
