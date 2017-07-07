using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rizumu.Engine
{
    class Background
    {
        public SpriteBatch SpriteBatch;
        public Texture2D Texture;
        public Color Color;
        public int Width;
        public int Height;

        public Background(SpriteBatch spriteBatch, Texture2D texture, Color color, int width, int height)
        {
            SpriteBatch = spriteBatch;
            Texture = texture;
            Color = color;
            Width = width;
            Height = height;
        }

        public void Draw()
        {
            SpriteBatch.Draw(Texture, new Rectangle(0, 0, Width, Height), Color);
        }
    }
}
