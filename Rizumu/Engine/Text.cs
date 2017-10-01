using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rizumu.Engine
{
    class Text
    {
        private readonly SpriteFont font;
        private readonly Color color;
        private readonly float opacity;
        public string Content { get; set; }
        public int X { get; set; }
        public int Y { get; set; }
        public int Width => (int)font.MeasureString(Content).X;
        public int Height => (int)font.MeasureString(Content).Y;

        public Text(SpriteFont font, string content, int x, int y, Color color, float opacity = 1f)
        {
            this.font = font;
            this.Content = content;
            this.X = x;
            this.Y = y;
            this.opacity = opacity;
            this.color = color;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            Color black = new Color(Color.Black, opacity);
            Color main = new Color(color, opacity);
            spriteBatch.DrawString(font, Content, new Vector2(X-1, Y-1), black);
            spriteBatch.DrawString(font, Content, new Vector2(X+1, Y+1), black);
            spriteBatch.DrawString(font, Content, new Vector2(X+1, Y-1), black);
            spriteBatch.DrawString(font, Content, new Vector2(X-1, Y+1), black);
            spriteBatch.DrawString(font, Content, new Vector2(X, Y), main);
        }
    }
}
