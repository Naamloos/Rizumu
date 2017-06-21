using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rizumu
{
    public class IngamePopup
    {
        public static string Title;
        public static string PopupText;
        public static Sprite Popup;
        public static float Opacity;

        public static void Preload(SpriteBatch spriteBatch, Texture2D popup)
        {
            Popup = new Sprite(spriteBatch, (Game1.graphics.PreferredBackBufferWidth / 2) - 200, (Game1.graphics.PreferredBackBufferHeight / 2) - 125, popup, GameResources.basecolor)
            {
                color = new Microsoft.Xna.Framework.Color(GameResources.basecolor, Opacity)
            };
        }

        public static void SetPopup(string title, string text)
        {
            Title = title;
            PopupText = text;
            Opacity = 2f;
            Popup.color = new Microsoft.Xna.Framework.Color(GameResources.basecolor, Opacity);
        }

        public static void TryDraw()
        {
            if (Opacity > 0)
            {
                Opacity = Opacity - 0.01f;
                Popup.color = new Microsoft.Xna.Framework.Color(GameResources.basecolor, Opacity);
                Popup.draw();
                Text.draw(GameResources.font, Title, ((Game1.graphics.PreferredBackBufferWidth / 2) - 200) + 25,
                    ((Game1.graphics.PreferredBackBufferHeight / 2) - 125) + 15, Popup.spriteBatch, Opacity);
                Text.draw(GameResources.debug, PopupText, ((Game1.graphics.PreferredBackBufferWidth / 2) - 200) + 30,
                    ((Game1.graphics.PreferredBackBufferHeight / 2) - 125) + 75, Popup.spriteBatch, Opacity);
            }
        }
    }
}
