/* 
 * class i use for drawing text with a black outline
 */

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Rizumu
{
    class Text
    {
        public static void draw(SpriteFont font, string text, int x, int y, SpriteBatch spriteBatch, float Opacity = 1.0f)
        {
            Color Black = new Color(Color.Black, Opacity);
            Color White = new Color(GameResources.basecolor, Opacity);
            spriteBatch.DrawString(font, text, new Vector2(x + 1, y + 1), Black);
            spriteBatch.DrawString(font, text, new Vector2(x + 1, y - 1), Black);
            spriteBatch.DrawString(font, text, new Vector2(x - 1, y + 1), Black);
            spriteBatch.DrawString(font, text, new Vector2(x - 1, y - 1), Black);
            spriteBatch.DrawString(font, text, new Vector2(x, y), White);
        }
    }
}
