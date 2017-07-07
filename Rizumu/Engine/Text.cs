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
        public SpriteBatch SpriteBatch;
        public SpriteFont Font;
        public Color Color;
        public string Content;
        public int X;
        public int Y;
        public int Width;
        public int Height;
        public float Opacity;

        public Text(SpriteBatch spriteBatch, SpriteFont font, string content, int x, int y, Color color, float opacity = 1f)
        {
            SpriteBatch = spriteBatch;
            Font = font;
            Content = content;
            X = x;
            Y = y;
            Opacity = opacity;
            Color = color;

            Width = (int)font.MeasureString(content).X;
            Height = (int)font.MeasureString(content).Y;
        }

        public void Draw()
        {
            Color black = new Color(Color.Black, Opacity);
            Color main = new Color(Color, Opacity);
            SpriteBatch.DrawString(Font, Content, new Vector2(X-1, Y-1), black);
            SpriteBatch.DrawString(Font, Content, new Vector2(X+1, Y+1), black);
            SpriteBatch.DrawString(Font, Content, new Vector2(X+1, Y-1), black);
            SpriteBatch.DrawString(Font, Content, new Vector2(X-1, Y+1), black);
            SpriteBatch.DrawString(Font, Content, new Vector2(X, Y), main);
        }
    }
}
