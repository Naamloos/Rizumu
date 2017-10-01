﻿using Microsoft.Xna.Framework;
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
        public SpriteFont Font;
        public Color Color;
        public string Content;
        public int X;
        public int Y;
        public int Width => (int)Font.MeasureString(Content).X;
        public int Height => (int)Font.MeasureString(Content).Y;
        public float Opacity;

        public Text(SpriteFont font, string content, int x, int y, Color color, float opacity = 1f)
        {
            Font = font;
            Content = content;
            X = x;
            Y = y;
            Opacity = opacity;
            Color = color;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            Color black = new Color(Color.Black, Opacity);
            Color main = new Color(Color, Opacity);
            spriteBatch.DrawString(Font, Content, new Vector2(X-1, Y-1), black);
            spriteBatch.DrawString(Font, Content, new Vector2(X+1, Y+1), black);
            spriteBatch.DrawString(Font, Content, new Vector2(X+1, Y-1), black);
            spriteBatch.DrawString(Font, Content, new Vector2(X-1, Y+1), black);
            spriteBatch.DrawString(Font, Content, new Vector2(X, Y), main);
        }
    }
}
